using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Default
{
    public class _Feature : ViewComponent
    {
        FeatureManger featureManger = new FeatureManger(new EfFeatureDal());
        public IViewComponentResult Invoke()
        {
            var values = featureManger.TGetList();
            return View();
        }
    }
}
