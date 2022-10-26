using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreWebApp.Pages
{
    public class UploadFileModel : PageModel
    {
        private IWebHostEnvironment _environment;
        public UploadFileModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task OnPostAsync()
        {
            if (Upload != null)
            {
                var file = Path.Combine(_environment.WebRootPath,
                "uploads", Upload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
            }
            
        }
        public void OnGet()
        {
        }
    }
}
