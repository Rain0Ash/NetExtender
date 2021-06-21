// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Domains
{
    [Serializable]
    public readonly struct ApplicationInfo
    {
        public static ApplicationInfo Default { get; } = new ApplicationInfo();

        public String Developer { get; }
        public String Repository { get; }
        public String? URL { get; }

        public ApplicationInfo(String developer, String repository, String? url = null)
        {
            Developer = developer;
            Repository = repository;
            URL = url;
        }
    }
}