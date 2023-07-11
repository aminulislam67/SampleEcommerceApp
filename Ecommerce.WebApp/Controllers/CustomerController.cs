using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModels;
using Ecommerce.Repositories;
using Ecommerce.WebApp.Models;
using Ecommerce.WebApp.Models.CustomerList;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        CustomerRepository _customerRepository;

        public CustomerController()
        {
            _customerRepository = new CustomerRepository();
        }
       

        public IActionResult Index(CustomerSearchCriteria customerSearchCriteria)
        {
            var customers = _customerRepository.Search(customerSearchCriteria);

            ICollection<CustomerListItem> customerModels = customers.Select(c => new CustomerListItem()
            {
                Id= c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
            }).ToList();

            var customerListModel = new CustomerListViewModel(); 
            customerListModel.CustomerList = customerModels;

            return View(customerListModel);
        }

       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerCreate model)
        {

            if (ModelState.IsValid)
            {
                var customer = new Customer()
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                };
                //Database operations 
                bool isSuccess = _customerRepository.Add(customer);

                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
               
            }
            
            return View();
          
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                ViewBag.Error = "Please provide valid id.";
                return View(); 
            }

            var customer = _customerRepository.GetById((int)id);

            if(customer == null)
            {
                ViewBag.Error = "Sorry, no customer found for this id.";
                return View();
            }

            var model = new CustomerEditVM()
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CustomerEditVM model)
        {
            if(ModelState.IsValid)
            {
                var customer = _customerRepository.GetById(model.Id);

                if(customer == null)
                {
                    ViewBag.Error = "Customer not found to update!";
                    return View(model);
                }

                customer.Name = model.Name;
                customer.Email = model.Email;
                customer.Phone = model.Phone;

                bool isSuccess = _customerRepository.Update(customer);
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

    }
}
