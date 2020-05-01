using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Hamburger.Core;
using Hamburger.Logger;
using Ninject;
using LogSeverity = Hamburger.Logger.LogSeverity;

namespace Hamburger.Discord
{
    public class CommandHandler
    {
        private readonly IDiscord _client;
        private readonly CommandService _commandService;
        private readonly ILogger _logger;
        private readonly IKernel _services;

        public CommandHandler(IDiscord client, ILogger logger, IKernel services)
        {
            _client = client;
            _commandService = new CommandService();
            _logger = logger;
            _services = services;
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

            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) || 
                  message.HasMentionPrefix(_client.Client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client.Client, message);

            _logger.Log($"Executed command: {context.Message.ToString().Split(' ')[0].Substring(1)}, in server [{context.Guild.Name}]({context.Channel.Id}) and channel [{context.Channel.Name}]({context.Channel.Id.ToString()}) by user [{context.User.Username}#{context.User.Discriminator}]({context.User.Id.ToString()})",
                LogSeverity.SEVERITY_MESSAGE);

            var result = await _commandService.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services
                );

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
