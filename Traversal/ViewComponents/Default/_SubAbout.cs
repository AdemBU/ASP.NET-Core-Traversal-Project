using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.EntityFramework;

namespace Traversal.ViewComponents.Default
{
    public class _SubAbout : ViewComponent
    {
        SubAboutManager subAboutManager = new SubAboutManager(new EfSubAboutDal());
        public IViewComponentResult Invoke()
        {
            var values = subAboutManager.TGetList();
            return View(values); 
        }
    }
}
