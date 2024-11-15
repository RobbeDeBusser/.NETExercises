using ContosoUniversity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    public class OperationController : Controller
    {
        public IActionResult Add()
        {
            var addOperationViewModel = new AddOperationViewModel();
            return View(addOperationViewModel);
        }
        [HttpPost]
        public IActionResult Add([Bind("Number1, Number2")] AddOperationViewModel addOperationViewModel)
        {
            addOperationViewModel.Result = addOperationViewModel.Number1 + addOperationViewModel.Number2;
            return View(addOperationViewModel);
        }
    }
}
