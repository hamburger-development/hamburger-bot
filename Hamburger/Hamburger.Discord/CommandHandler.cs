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

namespace Hamburger.Discord
{
    public class CommandHandler
    {
        private readonly IDiscord _client;
        private readonly CommandService _commandService;
        private ILogger _logger;
        //private readonly IServiceProvider _services;

        public CommandHandler(IDiscord client, ILogger logger /*, IServiceProvider services*/)
        {
            _client = client;
            _commandService = new CommandService();
            _logger = logger;
            //_services = services;
        }

        public async Task InstallCommandsAsync()
        {
            _client.Client.MessageReceived += HandleCommandAsync;

            await _commandService.AddModulesAsync(assembly: Assembly.GetExecutingAssembly(),
                services: null);
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
            var result = await _commandService.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null
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
