using System.Diagnostics;
using MessagePack;
using Microsoft.AspNetCore.Mvc.Formatters;
using BlazorMsgPack.Shared;
using Microsoft.Extensions.Primitives;

namespace BlazorMsgPack.Server
{
    #region Lz4
    public class MessagePackLz4InputFormatter(MessagePackSerializerOptions options) : IInputFormatter
    {
        private const string ContentType = MsgPack.MessagePackMediaTypeLz4;

        public MessagePackLz4InputFormatter() : this(MsgPack.CustomFormatterLz4) { }

        public bool CanRead(InputFormatterContext context) => context.HttpContext.Request.ContentType == ContentType;

        public async Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var result = await MessagePackSerializer.DeserializeAsync(context.ModelType, request.Body, options, context.HttpContext.RequestAborted).ConfigureAwait(false);
            return await InputFormatterResult.SuccessAsync(result).ConfigureAwait(false);
        }
    }

    public class MessagePackLz4OutputFormatter(MessagePackSerializerOptions options) : IOutputFormatter
    {
        private const string ContentType = MsgPack.MessagePackMediaTypeLz4;

        public MessagePackLz4OutputFormatter() : this(MsgPack.CustomFormatterLz4) { }

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            Debug.WriteLine("Input for Lz4");
            if (context.ContentType.HasValue) return context.ContentType.Value == ContentType;
            context.ContentType = new StringSegment(ContentType);
            return true;
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            Debug.WriteLine("Output for Lz4");

            context.HttpContext.Response.ContentType = ContentType;

            if (context.ObjectType == typeof(object))
            {
                if (context.Object == null)
                {
#if NETSTANDARD2_0
                    context.HttpContext.Response.Body.WriteByte(MessagePackCode.Nil);
                    return Task.CompletedTask;
#else
                    var writer = context.HttpContext.Response.BodyWriter;
                    var span = writer.GetSpan(1);
                    span[0] = MessagePackCode.Nil;
                    writer.Advance(1);
                    return writer.FlushAsync().AsTask();
#endif
                }
                else
                {
#if NETSTANDARD2_0
                    return MessagePackSerializer.SerializeAsync(context.Object.GetType(), context.HttpContext.Response.Body, context.Object, options, context.HttpContext.RequestAborted);
#else
                    var writer = context.HttpContext.Response.BodyWriter;
                    MessagePackSerializer.Serialize(context.Object.GetType(), writer, context.Object, options, context.HttpContext.RequestAborted);
                    return writer.FlushAsync().AsTask();
#endif
                }
            }
            else
            {
#if NETSTANDARD2_0
                return MessagePackSerializer.SerializeAsync(context.ObjectType, context.HttpContext.Response.Body, context.Object, options, context.HttpContext.RequestAborted);
#else
                var writer = context.HttpContext.Response.BodyWriter;
                MessagePackSerializer.Serialize(context.ObjectType, writer, context.Object, options, context.HttpContext.RequestAborted);
                return writer.FlushAsync().AsTask();
#endif
            }
        }
    }
    #endregion Lz4
    
    #region Lz4A
    public class MessagePackLz4AInputFormatter(MessagePackSerializerOptions options) : IInputFormatter
    {
        private const string ContentType = MsgPack.MessagePackMediaTypeLz4A;

        public MessagePackLz4AInputFormatter() : this(MsgPack.CustomFormatterLz4A) { }

        public bool CanRead(InputFormatterContext context) => context.HttpContext.Request.ContentType == ContentType;

        public async Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            Debug.WriteLine("Input for Lz4A");
            var request = context.HttpContext.Request;
            var result = await MessagePackSerializer.DeserializeAsync(context.ModelType, request.Body, options, context.HttpContext.RequestAborted).ConfigureAwait(false);
            return await InputFormatterResult.SuccessAsync(result).ConfigureAwait(false);
        }
    }

    public class MessagePackLz4AOutputFormatter(MessagePackSerializerOptions options) : IOutputFormatter
    {
        private const string ContentType = MsgPack.MessagePackMediaTypeLz4A;

        public MessagePackLz4AOutputFormatter() : this(MsgPack.CustomFormatterLz4A) { }

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            if (context.ContentType.HasValue) return context.ContentType.Value == ContentType;
            context.ContentType = new StringSegment(ContentType);
            return true;
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            Debug.WriteLine("Output for Lz4A");

            context.HttpContext.Response.ContentType = ContentType;

            if (context.ObjectType == typeof(object))
            {
                if (context.Object == null)
                {
#if NETSTANDARD2_0
                    context.HttpContext.Response.Body.WriteByte(MessagePackCode.Nil);
                    return Task.CompletedTask;
#else
                    var writer = context.HttpContext.Response.BodyWriter;
                    var span = writer.GetSpan(1);
                    span[0] = MessagePackCode.Nil;
                    writer.Advance(1);
                    return writer.FlushAsync().AsTask();
#endif
                }
                else
                {
#if NETSTANDARD2_0
                    return MessagePackSerializer.SerializeAsync(context.Object.GetType(), context.HttpContext.Response.Body, context.Object, options, context.HttpContext.RequestAborted);
#else
                    var writer = context.HttpContext.Response.BodyWriter;
                    MessagePackSerializer.Serialize(context.Object.GetType(), writer, context.Object, options, context.HttpContext.RequestAborted);
                    return writer.FlushAsync().AsTask();
#endif
                }
            }
            else
            {
#if NETSTANDARD2_0
                return MessagePackSerializer.SerializeAsync(context.ObjectType, context.HttpContext.Response.Body, context.Object, options, context.HttpContext.RequestAborted);
#else
                var writer = context.HttpContext.Response.BodyWriter;
                MessagePackSerializer.Serialize(context.ObjectType, writer, context.Object, options, context.HttpContext.RequestAborted);
                return writer.FlushAsync().AsTask();
#endif
            }
        }
    }
    #endregion Lz4A
}
