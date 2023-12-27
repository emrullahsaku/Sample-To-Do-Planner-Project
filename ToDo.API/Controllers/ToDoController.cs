using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Models;
using ToDo.Application.Services;
using ToDo.Application.Services.Iml;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _service;
        private readonly ImportDataService _importDataService;
        public ToDoController(IToDoService service, ImportDataService importDataService)
        {
            _service = service;
            _importDataService = importDataService;
        }

        [HttpGet(Name = "GetToDos")]
        public async Task<ActionResult> Get()
        {
            await _importDataService.ImportDataAsync();
            var resultModel = await _service.GetAllAsync();

            return Ok(ApiResult<IEnumerable<WeeklyScheduleViewModel>>.Success(resultModel));
        }
    }
}
