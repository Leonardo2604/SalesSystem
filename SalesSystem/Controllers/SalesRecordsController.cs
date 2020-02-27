﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Services;
using SalesSystem.Models;

namespace SalesSystem.Controllers
{
    public class SalesRecordsController : Controller
    {

        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyy-MM-dd");

            List<SalesRecord> sales = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(sales);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}