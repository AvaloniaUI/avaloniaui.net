using System;
using System.IO;
using System.Threading.Tasks;
using AvaloniaUI.Net.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.Utilities;

namespace AvaloniaUI.Net.Services
{
    public class MarkdownDocumentLoader
    {
        public async Task<TDocument?> LoadAsync<TDocument, TFrontMatter>(string path)
            where TDocument : class, IMarkdownDocument, new()
            where TFrontMatter : class, IMarkdownFrontMatter, new()
        {
            using var r = File.OpenText(path);
            var line = await r.ReadLineAsync();
            var result = new TDocument();

            if (line is null)
            {
                return null;
            }

            if (line.StartsWith("---", StringComparison.Ordinal))
            {
                result.FrontMatter = ParseFrontMatter<TFrontMatter>(r);
            }

            result.Markdown = await r.ReadToEndAsync();
            result.Title = result.FrontMatter?.Title ?? "Untitled";

            return result;

        }

        public TFrontMatter? LoadFrontMatter<TFrontMatter>(string path)
            where TFrontMatter : class, IMarkdownFrontMatter, new()
        {
            using var r = File.OpenText(path);
            return ParseFrontMatter<TFrontMatter>(r);
        }

        private TFrontMatter? ParseFrontMatter<TFrontMatter>(StreamReader r)
            where TFrontMatter : class, IMarkdownFrontMatter, new()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .BuildValueDeserializer();
            var parser = new Parser(r);

            parser.Consume<StreamStart>();
            parser.Consume<DocumentStart>();

            var result = (TFrontMatter?)deserializer.DeserializeValue(
                parser,
                typeof(TFrontMatter),
                new SerializerState(),
                deserializer);

            // Currently, the parser has read all the document and stays at the last token of it (*before* closing `---`
            // line). At the same time, it has already peeked the first character of the next line *after* the end of
            // the document from the StreamReader instance.
            //
            // So, we have to rewind the stream. To do that, calculate position of the next document start token, and
            // rewind the basic stream and the stream reader itself.
            _ = parser.Consume<DocumentEnd>();
            var nextDocumentStart = (DocumentStart)parser.Current!;
            var position = nextDocumentStart.End.Index // points *before* the closing `---`
                           + 1; // points to the first character of closing `---`
            RewindReaderTo(r, position);
            _ = r.ReadLine(); // should be `---`
            return result;

            static void RewindReaderTo(StreamReader r, int position)
            {
                r.BaseStream.Position = position;
                r.DiscardBufferedData();
            }
        }
    }
}
