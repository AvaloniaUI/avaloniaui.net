using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AvaloniaUI.Net.Models;
using AvaloniaUI.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static AvaloniaUI.Net.Services.PathUtilities;

namespace AvaloniaUI.Net.Pages.Docs
{
    [ResponseCache(Duration = 60)]
    public class IndexModel : PageModel
    {
        private const string DocsRelativePath = "docs";
        private readonly IWebHostEnvironment _env;
        private readonly MarkdownDocumentLoader _loader;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
            _loader = new MarkdownDocumentLoader();
        }

        public DocsArticle? Article { get; private set; }
        public List<DocsIndexItem>? Index { get; private set; }
        public List<DocsIndexItem>? SectionIndex { get; private set; }

        public async Task<IActionResult> OnGet(string url)
        {
            var docsPath = Path.Combine(_env.WebRootPath, DocsRelativePath);
            var articlePath = NormalizeMarkdownPath(Path.Combine(docsPath, url ?? string.Empty));

            Article = await LoadArticle(articlePath);

            if (Article == null)
            {
                return NotFound();
            }

            Index = LoadIndex(docsPath, articlePath);
            Index.Insert(0, new DocsIndexItem
            {
                Url = Url.Content("~/" + DocsRelativePath),
                Title = "Introduction",
                IsSelected = string.IsNullOrWhiteSpace(url),
            });

            if (Path.GetFileName(articlePath).Equals("index.md", StringComparison.InvariantCultureIgnoreCase))
            {
                SectionIndex = LoadSectionIndex(Index);
            }

            return Page();
        }

        private async Task<DocsArticle?> LoadArticle(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return null;
            }

            var article = await _loader.LoadAsync<DocsArticle, DocsArticleFrontMatter>(path);

            if (article == null)
            {
                return null;
            }

            return article;
        }

        private List<DocsIndexItem> LoadIndex(string path, string selectedPath)
        {
            var result = new List<DocsIndexItem>();

            foreach (var filePath in Directory.EnumerateFiles(path, "*.md"))
            {
                var fileName = Path.GetFileName(filePath);

                if (fileName.Equals("index.md", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                var frontMatter = LoadFrontMatter(filePath);

                result.Add(new DocsIndexItem
                {
                    Url = Url.PhysicalPathToContent(_env.WebRootPath, filePath, false),
                    Title = frontMatter?.Title ?? fileName,
                    Order = frontMatter?.Order ?? int.MaxValue,
                    IsSelected = filePath == selectedPath,
                });
            }

            foreach (var dirPath in Directory.EnumerateDirectories(path))
            {
                var indexPath = Path.Combine(dirPath, "index.md");
                var frontMatter = LoadFrontMatter(indexPath);

                if (frontMatter is object)
                {
                    var directoryName = Path.GetFileName(path);
                    var item = new DocsIndexItem
                    {
                        Url = Url.PhysicalPathToContent(_env.WebRootPath, dirPath, true),
                        Title = frontMatter.Title ?? directoryName,
                        Order = frontMatter.Order,
                        Children = LoadIndex(dirPath, selectedPath),
                        IsSelected = indexPath == selectedPath,
                    };

                    item.IsExpanded = item.IsSelected || item.Children.Any(x => x.IsExpanded || x.IsSelected);

                    result.Add(item);
                }
            }

            result.Sort((x, y) => x.Order - y.Order);
            return result;
        }

        private List<DocsIndexItem>? LoadSectionIndex(List<DocsIndexItem> index)
        {
            var i = index.FirstOrDefault(x => x.IsExpanded || x.IsSelected);

            if (i?.IsSelected == true)
            {
                return i.Children;
            }
            else if (i?.Children is object)
            {
                return LoadSectionIndex(i.Children);
            }

            return null;
        }

        private DocsArticleFrontMatter? LoadFrontMatter(string path)
        {
            if (System.IO.File.Exists(path))
            {
                return _loader.LoadFrontMatter<DocsArticleFrontMatter>(path);
            }

            return null;
        }
    }
}
