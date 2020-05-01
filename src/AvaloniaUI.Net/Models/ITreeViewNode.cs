using System.Collections.Generic;

namespace AvaloniaUI.Net.Models
{
    public interface ITreeViewNode
    {
        string? Url { get; }
        string? Header { get; }
        bool IsExpanded { get; }
        bool IsSelected { get; }
        IReadOnlyList<ITreeViewNode>? Children { get; }
    }
}
