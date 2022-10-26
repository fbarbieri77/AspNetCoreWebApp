using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreWebApp.Pages
{
    public class FlyImgModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? PhotoChoice { get; set; }
        public string ImagePath { get; set; } = string.Empty;

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(PhotoChoice))
            {
                ImagePath = @"images\" + PhotoChoice;
            }
        }
    }
}
