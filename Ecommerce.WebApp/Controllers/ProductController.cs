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
        public IActionResult DetailsView(int id)
        {
            // Retrieve the customer by id
            var product = _ProductRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            // Map the customer to the CustomerEdit model
            var model = new ProductInfoDetails()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category=product.Category,
            };

            return View(model);
        }
        public IActionResult Delete(int id)
        {
            // Retrieve the product by id
            var product = _ProductRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            // Database operations 
            bool isSuccess = _ProductRepository.Delete(product);

            if (isSuccess)
            {
                return RedirectToAction("Show");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            // Retrieve the product by id
            var product = _ProductRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            // Map the product to the ProductEdit model
            var model = new ProductInfoDetails()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductInfoDetails model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing product by id
                var existingProduct = _ProductRepository.GetById(model.Id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                // Update the properties of the existing product
                existingProduct.Name = model.Name;
                existingProduct.Price = model.Price;
                existingProduct.Description = model.Description;

                // Database operations 
                bool isSuccess = _ProductRepository.Update(existingProduct);

                if (isSuccess)
                {
                    return RedirectToAction("Show");
                }
            }

            return View();
        }

    }
}
