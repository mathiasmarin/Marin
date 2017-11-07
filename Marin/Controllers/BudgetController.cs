using Microsoft.AspNetCore.Mvc;

namespace Marin.Controllers
{
    public class BudgetController: Controller
    {
        public IActionResult CreateBudget()
        {
            return View();
        }
    }
}