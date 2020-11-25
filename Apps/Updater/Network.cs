// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using NetExtender.Utils.Network;

namespace NetExtender.Apps.Updater
{
    public sealed partial class Updater
    {
        private Task<Byte[]> DownloadAsync()
        {
            return _client.DownloadDataTaskAsync(_link, _source.Token);
        }
    }
}