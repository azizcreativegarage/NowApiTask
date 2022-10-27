using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NowApiTask.IServices;
using NowApiTask.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NowApiTask.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceRepository _BalanceRepository;
        
        public BalanceController(IBalanceRepository balanceRepository)
        {
            _BalanceRepository = balanceRepository;
        }
        // GET: api/<BalanceController>
        [HttpGet]
        public IEnumerable<Balance> Get()
        {

            return _BalanceRepository.GetBalance();
        }       
    }
}
