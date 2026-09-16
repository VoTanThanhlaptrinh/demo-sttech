using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Web.Models.Faqs
{
    public class FaqListViewModel
    {
        public List<SelectListItem> Statuses { get; set; }
    }
}
