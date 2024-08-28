using System.Net.Http.Headers;
using MessagePack;
using MessagePack.Resolvers;

namespace BlazorMsgPack.Shared
{
    public static class MsgPack
    {
        public static long BytesRead { get; set; } = 0;

        public const string MessagePackMediaType = "application/x-msgpack";
        public const string MessagePackMediaTypeLz4 = "application/x-msgpack-lz4";
        public const string MessagePackMediaTypeLz4A = "application/x-msgpack-lz4a";

        public static MessagePackSerializerOptions CustomFormatter =
                MessagePackSerializerOptions.Standard.WithResolver(
                    CompositeResolver.Create(
                        NativeDecimalResolver.Instance,
                        NativeGuidResolver.Instance,
                        NativeDateTimeResolver.Instance,
                        StandardResolver.Instance,
                        ContractlessStandardResolver.Instance))
            //.WithCompression(MessagePackCompression.Lz4BlockArray)
            ;

        public static MessagePackSerializerOptions CustomFormatterLz4 =
            MessagePackSerializerOptions.Standard.WithResolver(
                    CompositeResolver.Create(
                        NativeDecimalResolver.Instance,
                        NativeGuidResolver.Instance,
                        NativeDateTimeResolver.Instance,
                        StandardResolver.Instance,
                        ContractlessStandardResolver.Instance))
                .WithCompression(MessagePackCompression.Lz4BlockArray);

        public static MessagePackSerializerOptions CustomFormatterLz4A =
            MessagePackSerializerOptions.Standard.WithResolver(
                    CompositeResolver.Create(
                        NativeDecimalResolver.Instance,
                        NativeGuidResolver.Instance,
                        NativeDateTimeResolver.Instance,
                        StandardResolver.Instance,
                        ContractlessStandardResolver.Instance))
                .WithCompression(MessagePackCompression.Lz4BlockArray);

        #region Get
        public static Task<T?> GetFromMessagePackAsync<T>(this HttpClient client, string? requestUri, MessagePackSerializerOptions options, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var resultTask = client.GetMessagePackAsync(requestUri, cancellationToken);
            return GetFromMessagePackAsyncCore<T>(resultTask, options, cancellationToken);
        }

        public static Task<T?> GetFromMessagePackAsync<T>(this HttpClient client, Uri? requestUri, MessagePackSerializerOptions options, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var resultTask = client.GetMessagePackAsync(requestUri, cancellationToken);
            return GetFromMessagePackAsyncCore<T>(resultTask, options, cancellationToken);
        }

        public static Task<T?> GetFromMessagePackAsync<T>(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
        {
            // Run with standard options
            //return await client.GetFromMessagePackAsync<T>(requestUri, ContractlessStandardResolver.Options, cancellationToken);

            // Can be run with compression - will make a smaller sizer, but is slower to run
            //return await client.GetFromMessagePackAsync<T>(requestUri, ContractlessStandardResolver.Options.WithCompression(MessagePackCompression.Lz4BlockArray), cancellationToken);

            // Can be run with custom resolver
            return client.GetFromMessagePackAsync<T>(requestUri, CustomFormatter, cancellationToken);

        }

        public static Task<T?> GetFromMessagePackLz4Async<T>(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var resultTask = client.GetMessagePackLz4Async(requestUri, cancellationToken);
            return GetFromMessagePackAsyncCore<T>(resultTask, CustomFormatterLz4, cancellationToken);
        }

        public static Task<T?> GetFromMessagePackLz4Async<T>(this HttpClient client, Uri? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var resultTask = client.GetMessagePackLz4Async(requestUri, cancellationToken);
            return GetFromMessagePackAsyncCore<T>(resultTask, CustomFormatterLz4, cancellationToken);
        }

        public static Task<T?> GetFromMessagePackLz4AAsync<T>(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var resultTask = client.GetMessagePackLz4AAsync(requestUri, cancellationToken);
            return GetFromMessagePackAsyncCore<T>(resultTask, CustomFormatterLz4A, cancellationToken);
        }

        public static Task<T?> GetFromMessagePackLz4AAsync<T>(this HttpClient client, Uri? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var resultTask = client.GetMessagePackLz4AAsync(requestUri, cancellationToken);
            return GetFromMessagePackAsyncCore<T>(resultTask, CustomFormatterLz4A, cancellationToken);
        }

        public static Task<T?> GetFromMessagePackAsync<T>(this HttpClient client, Uri? requestUri, CancellationToken cancellationToken = default)
            => client.GetFromMessagePackAsync<T>(requestUri, CustomFormatter, cancellationToken);

        public static async Task<HttpResponseMessage?> GetMessagePackAsync(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));
            return await client.GetMessagePackAsync(requestUri, MessagePackMediaType, cancellationToken);
        }

        public static async Task<HttpResponseMessage?> GetMessagePackAsync(this HttpClient client, Uri? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));
            return await client.GetMessagePackAsync(requestUri, MessagePackMediaType, cancellationToken);
        }

        public static async Task<HttpResponseMessage?> GetMessagePackLz4Async(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));
            return await client.GetMessagePackAsync(requestUri, MessagePackMediaTypeLz4, cancellationToken);
        }

        public static async Task<HttpResponseMessage?> GetMessagePackLz4Async(this HttpClient client, Uri? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));
            return await client.GetMessagePackAsync(requestUri, MessagePackMediaTypeLz4, cancellationToken);
        }

        public static async Task<HttpResponseMessage?> GetMessagePackLz4AAsync(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));
            return await client.GetMessagePackAsync(requestUri, MessagePackMediaTypeLz4A, cancellationToken);
        }

        public static async Task<HttpResponseMessage?> GetMessagePackLz4AAsync(this HttpClient client, Uri? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));
            return await client.GetMessagePackAsync(requestUri, MessagePackMediaTypeLz4A, cancellationToken);
        }

        private static async Task<HttpResponseMessage?> GetMessagePackAsync(this HttpClient client, string? requestUri, string mediaType, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            return await client.SendAsync(request, cancellationToken);
        }

        private static async Task<HttpResponseMessage?> GetMessagePackAsync(this HttpClient client, Uri? requestUri, string mediaType, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            return await client.SendAsync(request, cancellationToken);
        }

        private static async Task<T?> GetFromMessagePackAsyncCore<T>(Task<HttpResponseMessage?> taskResponse, MessagePackSerializerOptions? options, CancellationToken cancellationToken)
        {
            using HttpResponseMessage? response = await taskResponse.ConfigureAwait(false);
            if (response == null) throw new NullReferenceException("Failed to get HTTP Response Message");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromMessagePackAsync<T>(options, cancellationToken).ConfigureAwait(false);
        }
        #endregion Get

        #region Post
        public static Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, string? requestUri, T value, MessagePackSerializerOptions options, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, options, MessagePackMediaType, cancellationToken);
        }

        public static Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, Uri? requestUri, T value, MessagePackSerializerOptions options, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, options, MessagePackMediaType, cancellationToken);
        }

        public static Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
            => client.PostAsMessagePackAsync(requestUri, value, CustomFormatter, cancellationToken);

        public static Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
            => client.PostAsMessagePackAsync(requestUri, value, CustomFormatter, cancellationToken);

        public static Task<HttpResponseMessage> PostAsMessagePackLz4Async<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, CustomFormatterLz4, MessagePackMediaType, cancellationToken);
        }

        public static Task<HttpResponseMessage> PostAsMessagePackLz4Async<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, CustomFormatterLz4, MessagePackMediaType, cancellationToken);
        }

        public static Task<HttpResponseMessage> PostAsMessagePackLz4AAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, CustomFormatterLz4A, MessagePackMediaType, cancellationToken);
        }

        public static Task<HttpResponseMessage> PostAsMessagePackLz4AAsync<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, CustomFormatterLz4A, MessagePackMediaType, cancellationToken);
        }

        #endregion Post

        #region PostRead
        public static Task<T?> PostReadAsMessagePackAsync<T>(this HttpClient client, string? requestUri, T value, MessagePackSerializerOptions options, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, options, MessagePackMediaType, cancellationToken);
        }

        public static Task<T?> PostReadAsMessagePackAsync<T>(this HttpClient client, Uri? requestUri, T value, MessagePackSerializerOptions options, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, options, MessagePackMediaType, cancellationToken);
        }

        public static Task<T?> PostReadAsMessagePackAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
            => client.PostReadAsMessagePackAsync(requestUri, value, CustomFormatter, cancellationToken);

        public static Task<T?> PostReadAsMessagePackAsync<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
            => client.PostReadAsMessagePackAsync(requestUri, value, CustomFormatter, cancellationToken);

        public static Task<T?> PostReadAsMessagePackLz4Async<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, CustomFormatterLz4, MessagePackMediaType, cancellationToken);
        }

        public static Task<T?> PostReadAsMessagePackLz4Async<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, CustomFormatterLz4, MessagePackMediaType, cancellationToken);
        }

        public static Task<T?> PostReadAsMessagePackLz4AAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, CustomFormatterLz4A, MessagePackMediaType, cancellationToken);
        }

        public static Task<T?> PostReadAsMessagePackLz4AAsync<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, CustomFormatterLz4A, MessagePackMediaType, cancellationToken);
        }

        #endregion PostRead

        #region Put
        public static Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, string? requestUri, T value, MessagePackSerializerOptions options, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, options, MessagePackMediaType, cancellationToken);
        }

        public static Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, Uri? requestUri, T value, MessagePackSerializerOptions options, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, options, MessagePackMediaType, cancellationToken);
        }

        public static Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
            => client.PutAsMessagePackAsync(requestUri, value, CustomFormatter, cancellationToken);

        public static Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
            => client.PutAsMessagePackAsync(requestUri, value, CustomFormatter, cancellationToken);

        public static Task<HttpResponseMessage> PutAsMessagePackLz4Async<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, CustomFormatterLz4, MessagePackMediaType, cancellationToken);
        }

        public static Task<HttpResponseMessage> PutAsMessagePackLz4Async<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, CustomFormatterLz4, MessagePackMediaType, cancellationToken);
        }

        public static Task<HttpResponseMessage> PutAsMessagePackLz4AAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, CustomFormatterLz4A, MessagePackMediaType, cancellationToken);
        }

        public static Task<HttpResponseMessage> PutAsMessagePackLz4AAsync<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendAsMessagePackAsyncCore(request, value, CustomFormatterLz4A, MessagePackMediaType, cancellationToken);
        }

        #endregion Put

        #region PutRead
        public static Task<T?> PutReadAsMessagePackAsync<T>(this HttpClient client, string? requestUri, T value, MessagePackSerializerOptions options, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, options, MessagePackMediaType, cancellationToken);
        }

        public static Task<T?> PutReadAsMessagePackAsync<T>(this HttpClient client, Uri? requestUri, T value, MessagePackSerializerOptions options, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, options, MessagePackMediaType, cancellationToken);
        }

        public static Task<T?> PutReadAsMessagePackAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
            => client.PutReadAsMessagePackAsync(requestUri, value, CustomFormatter, cancellationToken);

        public static Task<T?> PutReadAsMessagePackAsync<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
            => client.PutReadAsMessagePackAsync(requestUri, value, CustomFormatter, cancellationToken);

        public static Task<T?> PutReadAsMessagePackLz4Async<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, CustomFormatterLz4, MessagePackMediaType, cancellationToken);
        }

        public static Task<T?> PutReadAsMessagePackLz4Async<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, CustomFormatterLz4, MessagePackMediaType, cancellationToken);
        }

        public static Task<T?> PutReadAsMessagePackLz4AAsync<T>(this HttpClient client, string? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, CustomFormatterLz4A, MessagePackMediaType, cancellationToken);
        }

        public static Task<T?> PutReadAsMessagePackLz4AAsync<T>(this HttpClient client, Uri? requestUri, T value, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            return client.SendReadAsMessagePackAsyncCore(request, value, CustomFormatterLz4A, MessagePackMediaType, cancellationToken);
        }

        #endregion PutRead

        #region Read
        public static Task<T?> ReadFromMessagePackAsync<T>(this HttpContent content, MessagePackSerializerOptions? options, CancellationToken cancellationToken = default)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            return content.ReadFromMessagePackAsyncCore<T>(options, cancellationToken);
        }
        public static Task<T?> ReadFromMessagePackAsync<T>(this HttpContent content, CancellationToken cancellationToken = default)
        => content.ReadFromMessagePackAsync<T>(CustomFormatter, cancellationToken);

        public static Task<T?> ReadFromMessagePackLz4Async<T>(this HttpContent content, CancellationToken cancellationToken = default)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            return content.ReadFromMessagePackAsyncCore<T>(CustomFormatterLz4, cancellationToken);
        }

        public static Task<T?> ReadFromMessagePackLz4AAsync<T>(this HttpContent content, CancellationToken cancellationToken = default)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            return content.ReadFromMessagePackAsyncCore<T>(CustomFormatterLz4A, cancellationToken);
        }

        private static async Task<T?> ReadFromMessagePackAsyncCore<T>(this HttpContent content, MessagePackSerializerOptions? options, CancellationToken cancellationToken = default)
        {
            var bytes = await content.ReadAsByteArrayAsync(cancellationToken);
            BytesRead = bytes.LongLength;   // This is here for comparison processing
            return MessagePackSerializer.Deserialize<T>(bytes, options);
        }
        #endregion Read

        #region Send
        private static Task<HttpResponseMessage> SendAsMessagePackAsyncCore<T>(this HttpClient client, HttpRequestMessage request, T value, MessagePackSerializerOptions options, string mediaType, CancellationToken cancellationToken)
        {
            var buffer = MessagePackSerializer.Serialize(value, options, cancellationToken);
            var content = new ByteArrayContent(buffer);
            request.Content = content;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue(mediaType);
            return client.SendAsync(request, cancellationToken);
        }

        private static async Task<T?> SendReadAsMessagePackAsyncCore<T>(this HttpClient client, HttpRequestMessage request, T value, MessagePackSerializerOptions options, string mediaType, CancellationToken cancellationToken)
        {
            var resultTask = await client.SendAsMessagePackAsyncCore(request, value, options, mediaType, cancellationToken);
            return await resultTask.Content.ReadFromMessagePackAsync<T>(options, cancellationToken);
        }
        #endregion Send
    }
    
}
