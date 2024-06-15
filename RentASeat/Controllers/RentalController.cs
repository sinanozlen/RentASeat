using Microsoft.AspNetCore.Mvc;
using RentASeat.Models;

namespace RentASeat.Controllers
{
    public class RentalController : Controller
    {
        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RentalInfo rentalInfo)
        {
            if (ModelState.IsValid)
            {
                // İşlem tamamlandıktan sonra kullanıcıyı başka bir sayfaya yönlendir
                // Validasyon Sinan Yapacak
                
                return RedirectToAction("Index", "Home");
            }
            return View(rentalInfo);
        }
    }
}
