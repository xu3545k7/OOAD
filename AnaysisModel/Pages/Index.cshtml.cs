using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnaysisModel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AnalysisService _analysisService;

        public IndexModel(AnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        [BindProperty]
        public float Cost { get; set; }

        [BindProperty]
        public int Headcount { get; set; }

        [BindProperty]
        public float ROI { get; set; }

        public Report Report { get; private set; }

        public void OnPost()
        {
            Report = _analysisService.AnalyzeStrategies(Cost, Headcount, ROI, "");
        }
    }
}
