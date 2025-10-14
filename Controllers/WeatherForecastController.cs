using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.Db;
using SyskeySoftlabs.Scribbler.Service.Db;

namespace portfolio.service.Controllers;

[ApiController]
[Route("[controller]")]
public class DummyController : ControllerBase
{
    private readonly ILogger<DummyController> _logger;
    private readonly AppDbContext _dbContext;
    public DummyController(ILogger<DummyController> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _dbContext = appDbContext;
    }

    [HttpPost]
    public ActionResult<string> Get(AppConfig appConfig)
    {
        _dbContext.AppConfig.Add(appConfig);
        _dbContext.SaveChanges();
        return Ok(appConfig);
    }
}
