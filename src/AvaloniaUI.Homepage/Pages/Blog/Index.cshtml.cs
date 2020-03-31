﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AvaloniaUI.Homepage.Models;
using AvaloniaUI.Homepage.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static AvaloniaUI.Homepage.Services.PathUtilities;

namespace AvaloniaUI.Homepage.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private const string BlogRelativePath = "blog";
        private readonly IWebHostEnvironment _env;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public BlogPost? Article { get; private set; }
        public IEnumerable<BlogIndexItem>? RecentPosts { get; private set; }

        public async Task OnGet(string url)
        {
            var blogPath = Path.Combine(_env.WebRootPath, BlogRelativePath);
            var loader = new BlogPostLoader(_env, Url);
            var index = loader.LoadIndex(blogPath);

            RecentPosts = index.Take(10);

            var articlePath = string.IsNullOrWhiteSpace(url) ?
                index.FirstOrDefault()?.Path :
                NormalizeMarkdownPath(Path.Combine(blogPath, url));

            if (!string.IsNullOrWhiteSpace(articlePath) && System.IO.File.Exists(articlePath))
            {
                var selected = index.FirstOrDefault(x => x.Path == articlePath);

                if (selected is object)
                {
                    selected.IsSelected = true;
                }

                Article = await loader.LoadArticle(articlePath);
            }
        }
    }
}
