using System;
using System.Threading.Tasks;
using NetExtender.Apps;
using NetExtender.Apps.Data;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Data.Interfaces;
using NetExtender.Utils.IO;

namespace NetExtender
{
    public static class Library
    {
        public static IAppData Data { get; } = new AppData(AppVersion.Default, AppStatus.OpenBeta);

        [STAThread]
        public static async Task Main()
        {
            $"Library version: {Data}".ToConsole(ConsoleColor.Green);
        }
    }
}