using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Services;
using SalesSystem.Services.Exceptions;
using SalesSystem.Models;
using SalesSystem.Models.ViewModels;
using System.Diagnostics;
using System;

namespace SalesSystem.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            List<Seller> sellers = _sellerService.FindAll();
            return View(sellers);
        }

        public IActionResult Create()
        {
            SellerFormViewModel sellerFormViewModel = new SellerFormViewModel
            {
                Departments = _departmentService.FindAll()
            };
            return View(sellerFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                SellerFormViewModel sellerFormViewModel = new SellerFormViewModel
                {
                    Departments = _departmentService.FindAll(),
                    Seller = seller
                };
                return View(sellerFormViewModel);
            }

            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            SellerFormViewModel sellerFormViewModel = new SellerFormViewModel
            {
                Seller = seller,
                Departments = _departmentService.FindAll()
            };

            return View(sellerFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                SellerFormViewModel sellerFormViewModel = new SellerFormViewModel
                {
                    Departments = _departmentService.FindAll(),
                    Seller = seller
                };
                return View(sellerFormViewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                _sellerService.Update(seller);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(errorViewModel);
        }
    }
}