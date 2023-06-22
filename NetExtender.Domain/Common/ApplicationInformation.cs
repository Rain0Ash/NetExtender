// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Domains
{
    [Serializable]
    public readonly struct ApplicationInformation
    {
        public static ApplicationInformation Default { get; } = new ApplicationInformation();

        public String Developer { get; }
        public String Repository { get; }
        public String? URL { get; }

        public ApplicationInformation(String developer, String repository)
            : this(developer, repository, null)
        {
        }

        public ApplicationInformation(String developer, String repository, String? url)
        {
            Developer = developer;
            Repository = repository;
            URL = url;
        }
    }
}