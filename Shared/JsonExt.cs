using System.Net.Http.Headers;
using System.Text.Json;

namespace BlazorMsgPack.Shared;

public static class JsonExt
{
    public static long BytesRead { get; set; } = 0;

    public const string JsonExtMediaType = "*/*";

    public static Task<T?> GetFromJsonExtAsync<T>(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonExtAsync<T>(requestUri, null, cancellationToken);

    }

    public static Task<T?> GetFromJsonExtAsync<T>(this HttpClient client, string? requestUri, JsonSerializerOptions options, CancellationToken cancellationToken = default)
    {
        if (client == null) throw new ArgumentNullException(nameof(client));
        if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

        var resultTask = client.GetJsonExtAsync(requestUri, cancellationToken);
        return GetFromJsonExtAsyncCore<T>(resultTask, options, cancellationToken);
    }

    public static async Task<HttpResponseMessage?> GetJsonExtAsync(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
    {
        if (client == null) throw new ArgumentNullException(nameof(client));
        if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));
        return await client.GetJsonExtAsync(requestUri, JsonExtMediaType, cancellationToken);
    }

    private static async Task<HttpResponseMessage?> GetJsonExtAsync(this HttpClient client, string? requestUri, string mediaType, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        return await client.SendAsync(request, cancellationToken);
    }

    private static async Task<T?> GetFromJsonExtAsyncCore<T>(Task<HttpResponseMessage?> taskResponse, JsonSerializerOptions? options, CancellationToken cancellationToken)
    {
        using HttpResponseMessage? response = await taskResponse.ConfigureAwait(false);
        if (response == null) throw new NullReferenceException("Failed to get HTTP Response Message");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonExtAsync<T>(options, cancellationToken).ConfigureAwait(false);
    }
    public static Task<T?> ReadFromJsonExtAsync<T>(this HttpContent content, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
    {
        if (content == null) throw new ArgumentNullException(nameof(content));
        return content.ReadFromJsonExtAsyncCore<T>(options, cancellationToken);
    }

    private static async Task<T?> ReadFromJsonExtAsyncCore<T>(this HttpContent content, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
    {
        var bytes = await content.ReadAsByteArrayAsync(cancellationToken);
        BytesRead = bytes.LongLength;   // This is here for comparison processing
        options ??= new JsonSerializerOptions(JsonSerializerDefaults.Web);
        return JsonSerializer.Deserialize<T>(bytes, options);
    }

}