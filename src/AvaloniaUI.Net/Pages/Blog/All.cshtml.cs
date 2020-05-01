using System.Collections.Generic;
using System.IO;
using AvaloniaUI.Net.Models;
using AvaloniaUI.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AvaloniaUI.Net.Pages.Blog
{
    [ResponseCache(Duration = 60)]
    public class AllModel : PageModel
    {
        private const string BlogRelativePath = "blog";
        private readonly IWebHostEnvironment _env;

        public AllModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<BlogIndexItem>? Index { get; private set; }

        public void OnGet()
        {
            var blogPath = Path.Combine(_env.WebRootPath, BlogRelativePath);
            var loader = new BlogPostLoader(_env, Url);
            Index = loader.LoadIndex(blogPath);
        }
    }
}
