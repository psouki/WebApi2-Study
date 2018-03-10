using System;
using System.Net.Http;
using System.Net.Http.Headers;
using BeerDev.Manager.Const;

namespace BeerDev.Manager.Helper
{
    public class HttpClientHelper
    {
        public static HttpClient GetClient()
        {
            // BaseAddress is just the host url 
            // Json was set to the default format
            // every fancy thing can be done here without polluting the other methods 
            HttpClient client = new HttpClient { BaseAddress = new Uri(Address.Base) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Format.Json));

            return client;
        }
    }
}