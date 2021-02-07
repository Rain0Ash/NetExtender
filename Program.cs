using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetExtender.Apps;
using NetExtender.Apps.Data;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Data.Interfaces;
using NetExtender.Configuration;
using NetExtender.Types.Culture;
using NetExtender.Types.Drawing;
using NetExtender.Utils.GUI;
using NetExtender.Utils.IO;
using NetExtender.Utils.Numerics;
using NetExtender.Utils.Types;
using NetExtender.Workstation;

namespace NetExtender
{
    public static class Library
    {
        public static IAppData Data { get; } = new AppData(AppVersion.Default, AppStatus.OpenBeta);

        [STAThread]
        public static async Task Main()
        {
            $"Library version: {Data}".ToConsole(ConsoleColor.Green);

            if (false)
            {
                CancellationTokenSource source = new CancellationTokenSource();
                Console.CancelKeyPress += (_, args) =>
                {
                    args.Cancel = true;
                    source.Cancel();
                };

                await using FileStream save = new FileStream("H:\\2516627.ansi.txt", FileMode.Open, FileAccess.ReadWrite);

                Stream memory = save;//await save.DecompressAsync(CompressionType.Deflate, source.Token);

                ASCIIArt art = new ASCIIArt(memory) { VTCode = true };

                ConsoleUtils.Title = "ASCII VT ART";
                ConsoleUtils.VTCode = art.VTCode;
                ConsoleUtils.IsConsoleVisible = true;
                ConsoleUtils.FontSize = 1;

                ConsoleUtils.Buffer = art.Size;
                ConsoleUtils.Size = ConsoleUtils.Buffer;
                ConsoleUtils.IsQuickEditEnabled = false;
                ConsoleUtils.ResetCursorPosition();
                ConsoleUtils.CenterToScreen();

                art.ToConsole(false);

                await TaskUtils.Delay(-1, source.Token);
            }
            
        }
    }
}