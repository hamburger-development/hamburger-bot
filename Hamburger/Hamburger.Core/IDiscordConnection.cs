using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hamburger.Core
{
    public interface IDiscordConnection
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
