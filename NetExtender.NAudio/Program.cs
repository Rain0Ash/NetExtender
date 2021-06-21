// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using NetExtender.Utils.NAudio;

namespace NetExtender.NAudio
{
    public static class Program
    {
        public static async Task Main()
        {
            await Play(@"D:\Папки\Музыка\Прочее\Аквариум\2021 - Дань\06 - Генерал (Кино Cover).mp3");
        }

        private static async Task Play(String path)
        {
            await using AudioFileReader file = new AudioFileReader(path);
            using IWavePlayer device = new WaveOutEvent();

            await device.PlayAsync(file);
        }
    }
}