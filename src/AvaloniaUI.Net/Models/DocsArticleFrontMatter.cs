namespace AvaloniaUI.Net.Models
{
    public class DocsArticleFrontMatter : IMarkdownFrontMatter
    {
        public string? Title { get; set; }
        public int Order { get; set; } = int.MaxValue;
    }
}
