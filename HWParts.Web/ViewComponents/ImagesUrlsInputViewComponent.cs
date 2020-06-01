using HWParts.Core.Application.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HWParts.Web.ViewComponents
{
    public class ImagesUrlsInputViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ComponentBaseViewModel componentBaseViewModel)
        {
            return View(componentBaseViewModel);
        }
    }
}
