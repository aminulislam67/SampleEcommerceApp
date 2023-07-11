using Ecommerce.Models.EntityModels;
using Ecommerce.Repositories;
using Ecommerce.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository _ProductRepository;

        public ProductController()
        {
            _ProductRepository = new ProductRepository();
        }


        [HttpGet]
        public IActionResult createproduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult createproduct(ProductCreate model)
        {

            if (ModelState.IsValid)
            {
                var Product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = model.Category,

                };
                //Database operations 
                bool isSuccess = _ProductRepository.Add(Product);

                if (isSuccess)
                {
                    return View();
                }

            }

            return View();







        }
    }
}
