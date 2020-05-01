using System;

namespace AvaloniaUI.Net.Models
{
    public class BlogPost : IMarkdownDocument
    {
        public string? Url { get; set; }
        public BlogPostFrontMatter? FrontMatter { get; set; }
        public string? Title { get; set; }
        public DateTimeOffset Date { get; set; }
        public string? Markdown { get; set; }

        IMarkdownFrontMatter? IMarkdownDocument.FrontMatter 
        {
            get => FrontMatter;
            set => FrontMatter = (BlogPostFrontMatter?)value;
        }
    }
}
