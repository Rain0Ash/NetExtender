using System;
using System.Threading.Tasks;
using NetExtender.Utils.IO;
using NetExtender.Apps;
using NetExtender.Apps.Data;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Data.Interfaces;

namespace NetExtender
{
    public static class Library
    {
        public static IAppData Data { get; } = new AppData(AppVersion.Default, AppStatus.OpenBeta);

        public static async Task Main()
        {
            $"Library version: {Data}".ToConsole(ConsoleColor.Green);
        }
    }
}