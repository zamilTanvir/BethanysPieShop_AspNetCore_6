using BethanysPieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Api
{
    [Route("/api/[Controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository; 
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var pies = _pieRepository.AllPies;
            return Ok(pies);
        }

        [HttpGet("{id}")]
        public IActionResult PieById(int id) 
        {
            var pie = _pieRepository.GetPieById(id);
            return Ok(pie);
        }

        [HttpPost]
        public IActionResult SearchQuery([FromBody] string query)
        {
            var pies = _pieRepository.SearchPies(query);
            return new JsonResult(pies);
        }
    }
}
