using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ToDoList.MvcFrontend.Models;
using ToDoList.MvcFrontend.Models.Response;

namespace ToDoList.MvcFrontend.Controllers
{


    public class TodoController : Controller
    {
        private readonly HttpClient _httpClient;

        public TodoController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7241/api/");
        }
        public async Task<ActionResult> Index()
        {
            var accessToken = Request.Cookies["AccessToken"]?.ToString();
            if (accessToken == null)
            {

                return RedirectToAction("Login", "Home");
            }
            _httpClient.DefaultRequestHeaders.Authorization =
       new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            //var cookieValue = Request.Cookies[".AspNetCore.Identity.Application"]?.ToString();
            //if (cookieValue == null)
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            //_httpClient.DefaultRequestHeaders.Add("Cookie", $".AspNetCore.Identity.Application={cookieValue}");


            var response = await _httpClient.GetAsync("ToDoItem");
            ViewBag.Title = "قائمة المهام";
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<TodoItem>>>(jsonString);
                var items = apiResponse.Data ?? new List<TodoItem>();
                return View(items);
            }
            return View(new List<TodoItem>());
        }
        // GET: TodoList/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"ToDoItem/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TodoItem>>(json);
            var item = apiResponse.Data;

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TodoItem item)
        {
            var accessToken = Request.Cookies["AccessToken"]?.ToString();
            if (string.IsNullOrEmpty(accessToken))
            {

                return RedirectToAction("Login", "Home");
            }
            if (!ModelState.IsValid)
                return View(item);

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"ToDoItem", content);
            var jso = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResponse<string>>(jso);
            if (apiResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Errors");

            return View(item);
        }
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"ToDoItem/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TodoItem>>(json);
            var item = apiResponse.Data;

            if (item == null)
                return NotFound();

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(TodoItem item)
        {
            if (!ModelState.IsValid)
                return View(item);

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.DeleteAsync($"ToDoItem?ID={item.Id}");
            var jso = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResponse<string>>(jso);
            if (apiResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "حصل خطأ أثناء تعديل المهمة.");

            return View(item);
        }

    }
}
