using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using AnaysisModel.Interfaces;

namespace AnaysisModel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAnalysisService _analysisService;
        public IReport Report { get; private set; }


        [BindProperty]
        public float Cost { get; set; }

        [BindProperty]
        public int Headcount { get; set; }

        [BindProperty]
        public float ROI { get; set; }

        [BindProperty]
        public string AdditionalRequirement { get; set; }


        public IndexModel(IAnalysisService analysisService, IReport report)
        {
            _analysisService = analysisService;
            Report = report;
        }

        public void OnPost()
        {
            Report = _analysisService.AnalyzeStrategies(Cost, Headcount, ROI, "");
        }


    }
}
