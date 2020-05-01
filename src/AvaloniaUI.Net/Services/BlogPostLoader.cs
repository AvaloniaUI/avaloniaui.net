using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AvaloniaUI.Net.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AvaloniaUI.Net.Services
{
    public class BlogPostLoader
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUrlHelper _url;
        private readonly MarkdownDocumentLoader _mdLoader = new MarkdownDocumentLoader();

        public BlogPostLoader(IWebHostEnvironment env, IUrlHelper url)
        {
            _env = env;
            _url = url;
        }

        public List<BlogIndexItem> LoadIndex(string path)
        {
            var result = new List<BlogIndexItem>();

            foreach (var filePath in Directory.EnumerateFiles(path, "*.md", SearchOption.AllDirectories))
            {
                var frontMatter = ParseFrontMatter(filePath);

                if (!string.IsNullOrWhiteSpace(frontMatter?.Title) && frontMatter.Published.HasValue)
                {
                    result.Add(new BlogIndexItem
                    {
                        Url = _url.PhysicalPathToContent(_env.WebRootPath, filePath, false),
                        Path = filePath,
                        Title = frontMatter.Title,
                        Published = frontMatter.Published.Value,
                        Author = frontMatter.Author,
                        Categories = frontMatter.Categories,
                    });
                }
            }

            result.Sort((x, y) => (int)(y.Published - x.Published).TotalSeconds);
            return result;
        }

        public async Task<BlogPost?> LoadArticle(string path)
        {
            return await _mdLoader.LoadAsync<BlogPost, BlogPostFrontMatter>(path);
        }

        private BlogPostFrontMatter? ParseFrontMatter(string path)
        {
            if (File.Exists(path))
            {
                return _mdLoader.LoadFrontMatter<BlogPostFrontMatter>(path);
            }

            return null;
        }
    }
}
