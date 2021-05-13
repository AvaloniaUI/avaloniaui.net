using System.Threading.Tasks;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace AvaloniaUI.Pages
{
    public class ContactModel : PageModel
    {
        public ContactModel([FromServices] IFluentEmail singleEmail, [FromServices] IConfiguration configuration)
        {
            _singleEmail = singleEmail;
            _configuration = configuration;
        }

        [BindProperty] public string ContactName { get; set; }
        [BindProperty] public string ContactEmail { get; set; }
        [BindProperty] public string Message { get; set; }


        public async Task<IActionResult> OnPost()
        {
            //Get this every email so that it can be updated without having to restart the server.
            var contactFormRecipient = _configuration.GetValue<string>("ContactFormRecipient");

            _singleEmail.To(contactFormRecipient)
            .Subject($"{ContactName} - Contacted Us")
            .SetFrom(ContactEmail)
            .Body(Message);

            await _singleEmail.SendAsync();
            return RedirectToPage("Index");
        }

        private readonly IFluentEmail _singleEmail;
        private readonly IConfiguration _configuration;

    }
}
