// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetExtender.Utils.Types;
using NetExtender.Times;

namespace NetExtender.Apps.Data.Common
{
    [Serializable]
    public readonly struct AppInformation
    {
        public static AppInformation Default { get; } = new AppInformation();

        private const String ProtocolCapture = "protocol";
        private const String DeveloperCapture = "developer";
        private const String RepositoryCapture = "repository";
        private static readonly String Pattern = $@"^(?:(?<{ProtocolCapture}>http(?:s)?):\/\/)?github.com\/(?<{DeveloperCapture}>[^\/]+)/(?<{RepositoryCapture}>[^\/]+)(?:\/)?$";
        private static readonly Regex URLCheck = new Regex(Pattern, RegexOptions.Compiled, Time.Second.Three);
            
        public String Developer { get; }
        public String Repository { get; }
        public String? URL { get; }

        public AppInformation(String url)
        {
            if (String.IsNullOrWhiteSpace(url) || !URLCheck.IsMatch(url))
            {
                throw new ArgumentException(@"Invalid URL", nameof(url));
            }
            
            IDictionary<String, IList<String>> captures = URLCheck.MatchNamedCaptures(url);

            Developer = captures[DeveloperCapture].First();
            Repository = captures[RepositoryCapture].First();
            URL = url;
        }

        public AppInformation(String developer, String repository, String? url = null)
        {
            Developer = developer;
            Repository = repository;
            URL = url;
        }
    }
}