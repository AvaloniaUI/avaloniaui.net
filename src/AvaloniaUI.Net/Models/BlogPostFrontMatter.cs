using System;
using System.Linq;

namespace AvaloniaUI.Net.Models
{
    public class BlogPostFrontMatter : IMarkdownFrontMatter
    {
        public string? Title { get; set; }
        public DateTimeOffset? Published { get; set; }
        public string[]? Categories { get; set; }
        public string? Author { get; set; }
        public string? Excerpt { get; set; }

        public string? Category
        {
            get => Categories?.FirstOrDefault();
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Categories = new[] { value };
                }
            }
        }
    }
}
