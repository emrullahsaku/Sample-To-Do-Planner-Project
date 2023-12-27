using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDo.UI.Models;

namespace ToDo.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5149/api/ToDo");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<IEnumerable<WeeklyScheduleViewModel>>>(result);

            return View(apiResult.Result);
        }
    }
}
