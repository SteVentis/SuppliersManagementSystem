using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Observability.Contracts;
using Observability.LoggerService;
using Repository.Core;
using System;
using System.Threading.Tasks;

namespace SuppliersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILoggerManager _logger;

        public CountryController(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                return Ok(await _unitOfWork.Countries.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in DeleteSupplier method: {ex}");
                return BadRequest();
            }
        }
    }
}
