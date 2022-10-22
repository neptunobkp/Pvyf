using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Services.Prismic;
using Web.ViewModels;

namespace Web.Pages.Publico
{
    public class IndexModel : PageModel
    {
        private IPrismicStub _prismic;
        public IndexModel(IPrismicStub prismic)
        {
            _prismic = prismic;
        }

        public LandingPageViewModel Vm { get; set; }

        public async Task OnGet()
        {
            Vm = await _prismic.GetLanding();
        }
    }
}
