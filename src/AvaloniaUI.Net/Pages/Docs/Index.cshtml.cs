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
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public List<SelectListItem>? Versions { get; private set; }

        public async Task<IActionResult> OnGet(string url)
        {
            var slash = url.IndexOf('/');
            var version = slash >= 0 ? url.Substring(0, slash) : url;
            var article = slash >= 0 ? url.Substring(slash + 1) : string.Empty;
            var docsPath = Path.Combine(_env.WebRootPath, DocsRelativePath, version);
            var articlePath = NormalizeMarkdownPath(Path.Combine(docsPath, article));

            Article = await LoadArticle(articlePath);

            if (Article == null)
            {
                return NotFound();
            }

            Versions = LoadVersions(version);
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

        private List<SelectListItem> LoadVersions(string currentVersion)
        {
            var docsPath = Path.Combine(_env.WebRootPath, DocsRelativePath);
            var result = new List<SelectListItem>();

            foreach (var dirPath in Directory.EnumerateDirectories(docsPath))
            {
                var dirName = Path.GetFileName(dirPath);
                result.Add(new SelectListItem(dirName, dirName, dirName == currentVersion));
            }

            return result;
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

            result.Sort((x, y) =>
            {
                var order = x.Order - y.Order;
                return order != 0 ? order : string.Compare(x.Title, y.Title);
            });

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
