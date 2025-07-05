using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ToDoList.MvcFrontend.Models.AuthModel;
using ToDoList.MvcFrontend.Models.Response;
using ToDoList.MvcFrontend.ViewModel.Login;

namespace ToDoList.MvcFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        public HomeController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7241/api/");
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("ApplicationUser/login", content);
            var jso = await response.Content.ReadAsStringAsync();


            var apiResult = JsonConvert.DeserializeObject<ApiAuthResponse>(jso);

            if (apiResult.Succeeded)
            {
                Response.Cookies.Append("AccessToken", apiResult.Data.AccessToken,
                     new CookieOptions
                     {
                         HttpOnly = true,
                         Secure = true,
                         SameSite = SameSiteMode.Strict
                     });
                Response.Cookies.Append(
                "RefreshToken",
    apiResult.Data.RefreshToken.TokenString,
    new CookieOptions
    {
        Expires = apiResult.Data.RefreshToken.ExpireAt,
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.Strict
    }
);
                return RedirectToAction("Index", "Todo");
            }


            ModelState.AddModelError("", "Invalid login.");
            return View("Login", model);
        }
        public async Task<IActionResult> callback(string code)
        {
            var body = new { code = code };
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("OAuth/google/callback", content);
            var jso = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResponse<OAuthCallBack>>(jso);

            if (apiResult.Succeeded)
            {
                Response.Cookies.Append("AccessToken", apiResult.Data.AccessToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });
                return RedirectToAction("Index", "Todo");
            }
            var model = new LoginViewModel();
            return View("Login", model);

        }


    }

}
