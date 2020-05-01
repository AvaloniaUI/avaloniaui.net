namespace AvaloniaUI.Net.Models
{
    public class DocsArticle : IMarkdownDocument
    {
        public string? Url { get; set; }
        public DocsArticleFrontMatter? FrontMatter { get; set; }
        public string? Title { get; set; }
        public string? Markdown { get; set; }
        IMarkdownFrontMatter? IMarkdownDocument.FrontMatter
        {
            get => FrontMatter;
            set => FrontMatter = (DocsArticleFrontMatter?)value;
        }
    }
}
