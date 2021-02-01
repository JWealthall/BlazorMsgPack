using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlazorMsgPack.Shared;
using MessagePack;

namespace BlazorMsgPack.Client
{
    public static class Extensions
    {
        public static async Task<T> GetFromMessagePackAsync<T>(this HttpClient client, string? requestUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-msgpack"));
            var result = await client.SendAsync(request);
            var bytes = await result.Content.ReadAsByteArrayAsync();
            return MessagePackSerializer.Deserialize<T>(bytes, MsgPack.CustomFormatter); ;
        }
    }

}

