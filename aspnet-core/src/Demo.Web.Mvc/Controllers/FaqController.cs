using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Demo.Controllers;
using Demo.Faqs;
using Demo.Faqs.Dto;
using Demo.Web.Models.Faqs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Web.Controllers
{
    public class FaqController : DemoControllerBase
    {
        private readonly IFaqAppService _faqAppService;

        public FaqController(IFaqAppService faqAppService)
        {
            _faqAppService = faqAppService;
        }

        public ActionResult Index()
        {
            var statuses = Enum.GetValues(typeof(FaqStatus))
                .Cast<FaqStatus>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = GetEnumDescription(e)
                }).ToList();

            var model = new FaqListViewModel
            {
                Statuses = statuses
            };

            return View(model);
        }

        private string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public async Task<ActionResult> EditModal(int faqId)
        {
            var output = await _faqAppService.GetAsync(new EntityDto<int>(faqId));
            var model = new EditFaqModalViewModel
            {
                Faq = output
            };
            
            return PartialView("_EditModal", model);
        }
    }
}
