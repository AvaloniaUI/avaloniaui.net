using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaUI.Models
{
    public interface IMarkdownDocument
    {
        string? Url { get; set; }
        IMarkdownFrontMatter? FrontMatter { get; set; }
        string? Title { get; set; }
        string? Markdown { get; set; }
    }
}
