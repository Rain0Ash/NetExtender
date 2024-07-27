// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Region;
using Newtonsoft.Json;

namespace NetExtender.Utilities.Network
{
    public static class GeoNetworkUtilities
    {
        private record GeoResponse
        {
            public String Ip { get; init; } = null!;
            public String Name { get; init; } = null!;
            
            [JsonProperty("country")]
            public String Country2 { get; init; } = null!;
            
            [JsonProperty("country_3")]
            public String Country3 { get; init; } = null!;
        }
        
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class ClientStorage
        {
            private static HttpClient? client;
            public static HttpClient Client
            {
                get
                {
                    return client ??= new HttpClient();
                }
                set
                {
                    client?.Dispose();
                    client = value;
                }
            }
        }

        public static HttpClient Client
        {
            get
            {
                return ClientStorage.Client;
            }
            private set
            {
                ClientStorage.Client = value;
            }
        }

        public static Task<CountryInfo?> GetCountryInfo()
        {
            return GetCountryInfo(CancellationToken.None);
        }

        public static async Task<CountryInfo?> GetCountryInfo(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            using HttpClient client = new HttpClient();
            return await GetCountryInfo(client, token).ConfigureAwait(false);
        }

        public static Task<CountryInfo?> GetCountryInfo(this HttpClient client)
        {
            return GetCountryInfo(client, CancellationToken.None);
        }

        public static async Task<CountryInfo?> GetCountryInfo(this HttpClient client, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            HttpResponseMessage response = await client.GetAsync("https://get.geojs.io/v1/ip/country.json", token).ConfigureAwait(false);
            return await ToCountry(response, token).ConfigureAwait(false);
        }

        public static Task<CountryInfo?> GetCountryInfo(this IPAddress address)
        {
            return GetCountryInfo(address, CancellationToken.None);
        }

        public static async Task<CountryInfo?> GetCountryInfo(this IPAddress address, CancellationToken token)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            token.ThrowIfCancellationRequested();
            using HttpClient client = new HttpClient();
            return await GetCountryInfo(client, address, token).ConfigureAwait(false);
        }

        public static Task<CountryInfo?> GetCountryInfo(this HttpClient client, IPAddress address)
        {
            return GetCountryInfo(client, address, CancellationToken.None);
        }

        public static async Task<CountryInfo?> GetCountryInfo(this HttpClient client, IPAddress address, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            HttpResponseMessage response = await client.GetAsync($"https://get.geojs.io/v1/ip/country/{address}.json", token).ConfigureAwait(false);
            return await ToCountry(response, token).ConfigureAwait(false);
        }

        private static async Task<CountryInfo?> ToCountry(HttpResponseMessage response, CancellationToken token)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            
            response.EnsureSuccessStatusCode();
            GeoResponse? geo = await response.Content.ReadAsAsync<GeoResponse>(token);
            return geo is not null && CountryInfo.TryParse(geo.Country2, out CountryInfo? result) ? result : null;
        }
        
        // ReSharper disable once ParameterHidesMember
        public static void Reset(HttpClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }
    }
}