using System;

namespace AvaloniaUI.Net.Models
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
    }
}
