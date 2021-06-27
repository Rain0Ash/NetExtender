// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NAudio.Wave;
using NetExtender.NAudio.Types.Playlist;
using NetExtender.NAudio.Types.Playlist.Interfaces;
using NetExtender.Utils.IO;
using NetExtender.Utils.NAudio;
using NetExtender.Utils.Windows.IO;

namespace NetExtender.NAudio
{
    public static class Program
    {
        public static async Task Main()
        {
            await Play();
        }

        private static async Task Play()
        {
            using IWavePlayer device = new WaveOutEvent();
            
            foreach (String file in WindowsDirectoryUtils.GetFiles(@"D:\Папки\Музыка\Из игр и фильмов", "^.+\\.mp3$", true))
            {
                file.ToConsole();
                await using (WaveStream stream = new AudioFileReader(file))
                {
                    await device.PlayAsync(stream.ProgressWithLength((position, length) => $"{position} / {length}".ToConsole()));
                }
            }
        }
    }
}