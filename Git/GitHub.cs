// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NetExtender.Utils.Network;
using JetBrains.Annotations;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Domains;
using NetExtender.Utils.Formats;

namespace NetExtender.Git
{
    public static class GitHub
    {
        private static String GetReleasesUrl(AppInformation github)
        {
            return $"https://api.github.com/repos/{github.Developer}/{github.Repository}/releases";
        }
        
        [ItemCanBeNull]
        public static Task<GitHubRelease> GetLatestReleaseAsync(HttpClient client, Boolean stable = true)
        {
            return GetLatestReleaseAsync(client, Domain.Current.Information, stable);
        }
        
        [ItemCanBeNull]
        public static Task<GitHubRelease> GetLatestReleaseAsync(WebClient client, Boolean stable = true)
        {
            return GetLatestReleaseAsync(client, Domain.Current.Information, stable);
        }
        
        public static async Task<GitHubRelease> GetLatestReleaseAsync(HttpClient client, AppInformation information, Boolean stable = true)
        {
            IList<GitHubRelease> releases = await GetReleasesAsync(client, information, stable).ConfigureAwait(false);
            return releases.First();
        }
        
        public static async Task<GitHubRelease> GetLatestReleaseAsync(WebClient client, AppInformation information, Boolean stable = true)
        {
            IList<GitHubRelease> releases = await GetReleasesAsync(client, information, stable).ConfigureAwait(false);
            return releases.First();
        }

        public static async Task<IList<GitHubRelease>> GetReleasesAsync(HttpClient client, AppInformation information, Boolean stable = true)
        {
            GitHubRelease[] releases = await GetReleasesFromURLAsync(client, GetReleasesUrl(information)).ConfigureAwait(false);
            return releases.Where(release => !stable || !release.PreRelease).ToImmutableList();
        }
        
        public static async Task<IList<GitHubRelease>> GetReleasesAsync(WebClient client, AppInformation information, Boolean stable = true)
        {
            GitHubRelease[] releases = await GetReleasesFromURLAsync(client, GetReleasesUrl(information)).ConfigureAwait(false);
            return releases.Where(release => !stable || !release.PreRelease).ToImmutableList();
        }
        
        private static async Task<GitHubRelease[]> GetReleasesFromURLAsync(HttpClient client, String url)
        {
            if (String.IsNullOrEmpty(url))
            {
                throw new ArgumentException(nameof(url));
            }

            String json = await client.DownloadStringAsync(url).ConfigureAwait(false);
            return JSONUtils.DeserializeObject<GitHubRelease[]>(json);
        }
        
        private static async Task<GitHubRelease[]> GetReleasesFromURLAsync(WebClient client, String url)
        {
            if (String.IsNullOrEmpty(url))
            {
                throw new ArgumentException(nameof(url));
            }

            String json = await client.DownloadStringTaskAsync(url).ConfigureAwait(false);
            return JSONUtils.DeserializeObject<GitHubRelease[]>(json);
        }
    }
}