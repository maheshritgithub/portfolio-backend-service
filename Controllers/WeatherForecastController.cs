using Microsoft.AspNetCore.Mvc;

namespace portfolio.service.Controllers;

[ApiController]
[Route("[controller]")]
public class DummyController : ControllerBase
{
    private readonly ILogger<DummyController> _logger;

    public DummyController(ILogger<DummyController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        return "This is a dummy endpoint for testing purposes.";
    }
}
