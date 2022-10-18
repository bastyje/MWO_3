using Microsoft.AspNetCore.Mvc;
using Lab2.Data;
using Lab2.Data.Enitites;

namespace Lab2.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
   private readonly IPlayerRepository _playerRepository;

   public PlayerController(IPlayerRepository playerRepository)
   {
      _playerRepository = playerRepository;
   }

   [HttpGet("AllByCountry")]
   public ActionResult<List<Player>> GetAll([FromQuery] string countryName)
   {
      var result = _playerRepository.GetAllFilteredByCountry(countryName);
      return result is null ? new NotFoundResult() : new OkObjectResult(result);
   }

   [HttpGet("{id}")]
   public ActionResult<Player> Get(string id)
   {
      var result = _playerRepository.Get(id);
      return result is not null ? new OkObjectResult(result) : new NotFoundResult();
   }

   [HttpPost]
   public ActionResult Post([FromBody] Player player)
   {
      _playerRepository.Add(player);
      return new OkResult();
   }

   [HttpPost]
   public ActionResult Put([FromBody] Player player)
   {
      _playerRepository.Update(player);
      return new OkResult();
   }

   [HttpDelete("{id}")]
   public ActionResult Delete(string id)
   {
      var player = _playerRepository.Get(id);
      if (player is null) return new NotFoundResult();
      _playerRepository.Remove(player);
      return new OkResult();
   }
}
