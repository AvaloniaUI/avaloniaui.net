using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaUI.Pages
{
    public class SupportModel : PageModel
    {
        private readonly ILogger<SupportModel> _logger;

        public SupportModel(ILogger<SupportModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
