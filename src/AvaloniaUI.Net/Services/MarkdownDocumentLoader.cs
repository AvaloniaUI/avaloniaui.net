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
            using var s = new FileStream(path, FileMode.Open);
            using var r = new StreamReader(s);
            var line = await r.ReadLineAsync();
            var result = new TDocument();

            if (line is null)
            {
                return null;
            }

            if (line.StartsWith("---"))
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
            using var s = new FileStream(path, FileMode.Open);
            using var r = new StreamReader(s);
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

            return (TFrontMatter?)deserializer.DeserializeValue(
                parser,
                typeof(TFrontMatter),
                new SerializerState(),
                deserializer);
        }
    }
}
