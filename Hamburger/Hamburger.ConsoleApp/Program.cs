﻿using System;
using System.Threading.Tasks;
using Hamburger.Core;
using Hamburger.Discord;
using Hamburger.Logger;
using Ninject;

namespace Hamburger.ConsoleApp
{
    class Program
    {
        private static HamburgerBot _hamburger;

        private static async Task Main(string[] args)
        {
            _hamburger = new HamburgerBot(InversionOfControl.Kernel.Get<IDiscordConnection>());
            await _hamburger.StartAsync();
        }
    }
}
