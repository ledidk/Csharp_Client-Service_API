using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _serviceUrl;

        public HomeController(IConfiguration config)
        {
            _configuration = config;
            _serviceUrl = "http://localhost:5000/RestaurantReview";
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_serviceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("RestaurantReview");
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        List<RestaurantInfo> restaurants = JsonSerializer.Deserialize<List<RestaurantInfo>>(responseContent);

                        return View(restaurants);
                    }
                    else
                    {
                        return View(new List<RestaurantInfo>());
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return View(new List<RestaurantInfo>());
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_serviceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"RestaurantReview/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        RestaurantInfo restaurant = JsonSerializer.Deserialize<RestaurantInfo>(responseContent);

                        return View(restaurant);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RestaurantInfo restInfo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_serviceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string jsonContent = JsonSerializer.Serialize(restInfo);
                    HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"RestaurantReview/{restInfo.id}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to the Index view after successful update
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(restInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return View(restInfo);
            }
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(RestaurantInfo restInfo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_serviceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string jsonContent = JsonSerializer.Serialize(restInfo);
                    HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("RestaurantReview", content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to the Index view after successful creation
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(restInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return View(restInfo);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_serviceUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.DeleteAsync($"RestaurantReview/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to the Index view after successful deletion
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return NotFound();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
