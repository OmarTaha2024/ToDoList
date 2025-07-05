using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoList.MVC.Models;

namespace ToDoList.MVC.Controllers
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
            var response = await _httpClient.GetAsync("TodoList");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<TodoItem>>(jsonString);
                return View(items);
            }
            return View(new List<TodoItem>());
        }

    }
}
