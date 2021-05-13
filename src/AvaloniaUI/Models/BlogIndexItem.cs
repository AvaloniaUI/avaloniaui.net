using System;

namespace AvaloniaUI.Models
{
    public class BlogIndexItem
    {
        public string? Url { get; set; }
        public string? Path { get; set; }
        public string? Title { get; set; }
        public DateTimeOffset Published { get; set; }
        public string? Author { get; set; }
        public string[]? Categories { get; set; }
        public bool IsSelected { get; set; }
        public string? Excerpt { get; set; }
        public string? FeatureImage { get; set; }
    }
}
