using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaUI.Models
{
    public interface IMarkdownFrontMatter
    {
        string? Title { get; set; }
    }
}
