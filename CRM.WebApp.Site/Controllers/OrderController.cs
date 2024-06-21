﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CRM.WebApp.Site.Models;

namespace CRM.WebApp.Site.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CRM.API");
            var response = await client.GetAsync("api/order");
            response.EnsureSuccessStatusCode();

            var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderViewModel>>();
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var client = _httpClientFactory.CreateClient("CRM.API");
            var response = await client.GetAsync($"api/order/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var order = await response.Content.ReadFromJsonAsync<OrderViewModel>();
            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("CRM.API");
                var response = await client.PostAsJsonAsync("api/order", orderViewModel);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(orderViewModel);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient("CRM.API");
            var response = await client.GetAsync($"api/order/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var order = await response.Content.ReadFromJsonAsync<OrderViewModel>();
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrderViewModel orderViewModel)
        {
            if (id != orderViewModel.OrderID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("CRM.API");
                var response = await client.PutAsJsonAsync($"api/order/{id}", orderViewModel);
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(orderViewModel);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = _httpClientFactory.CreateClient("CRM.API");
            var response = await client.GetAsync($"api/order/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var order = await response.Content.ReadFromJsonAsync<OrderViewModel>();
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var client = _httpClientFactory.CreateClient("CRM.API");
            var response = await client.DeleteAsync($"api/order/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}