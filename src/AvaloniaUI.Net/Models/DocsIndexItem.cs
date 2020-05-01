using System.Collections.Generic;

namespace AvaloniaUI.Net.Models
{
    public class DocsIndexItem : ITreeViewNode
    {
        public string? Url { get; set; }
        public string? Title { get; set; }
        public int Order { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsSelected { get; set; }
        public List<DocsIndexItem>? Children { get; set; }
        string? ITreeViewNode.Header => Title;
        IReadOnlyList<ITreeViewNode>? ITreeViewNode.Children => Children;
    }
}
