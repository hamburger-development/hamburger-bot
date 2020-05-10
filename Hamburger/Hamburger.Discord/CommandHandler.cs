using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Hamburger.Core.Models;
using Hamburger.Core.PersistentStorage;
using Hamburger.Logger;
using Ninject;
using System.Reflection;
using System.Threading.Tasks;
using LogSeverity = Hamburger.Logger.LogSeverity;

namespace Hamburger.Discord
{
    public class CommandHandler
    {
        private readonly IDiscord _client;
        private readonly CommandService _commandService;
        private readonly ILogger _logger;
        private readonly IKernel _services;
        private readonly IDbStorage _dbStorage;

        public CommandHandler(IDiscord client, ILogger logger, IKernel services, IDbStorage dbStorage)
        {
            _client = client;
            _commandService = new CommandService();
            _logger = logger;
            _services = services;
            _dbStorage = dbStorage;
        }

        public async Task InstallCommandsAsync()
        {
            _client.Client.MessageReceived += HandleCommandAsync;

            await _commandService.AddModulesAsync(assembly: Assembly.GetExecutingAssembly(),
                services: _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message is null) return;

            var channel = arg.Channel as SocketGuildChannel;
            var guildid = channel.Guild.Id;

            var currentGuild =
                await _dbStorage.FindOne<HamburgerGuildConfiguration>(x => x.DiscordGuildId == guildid, guildid.ToString());

            int argPos = 0;

            if (!(message.HasStringPrefix(currentGuild.CommandPrefix, ref argPos) ||
                  message.HasMentionPrefix(_client.Client.CurrentUser, ref argPos) ||
                  message.Content == _client.Client.CurrentUser.Mention) ||
                message.Author.IsBot)
                return;

            var context = new ShardedCommandContext(_client.Client, message);

            if (message.Content == _client.Client.CurrentUser.Mention)
            {
                var embed = new EmbedBuilder()
                    .WithTitle("Hello!")
                    .WithDescription($"My prefix is `{currentGuild.CommandPrefix}`, you can also mention me with any command \n(Ex: `@Hamburger prefix ?`)")
                    .Build();

                await context.Channel.SendMessageAsync("", false, embed);
                return;
            }

            _logger.Log($"Executed command in server [{context.Guild.Name}]({context.Channel.Id}) and channel [{context.Channel.Name}]({context.Channel.Id.ToString()}) by user [{context.User.Username}#{context.User.Discriminator}]({context.User.Id.ToString()})",
                LogSeverity.SEVERITY_MESSAGE);

            var result = await _commandService.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services
            );

            if (result is null) return;

            if (!result.IsSuccess)
            {
                var builder = new EmbedBuilder()
                    .WithTitle("Error")
                    .WithDescription(result.ErrorReason)
                    .Build();

                await context.Channel.SendMessageAsync("", false, builder);
            }
        }
    }
}
