using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marin.Controllers
{
    [Authorize]
    public class BudgetController: Controller
    {
        public IActionResult CreateCategories()
        {
            return View();
        }

        public IActionResult CreateMonthlyBudget()
        {
            return View();
        }

        public IActionResult CreateCosts()
        {
            return View();
        }
    }
}