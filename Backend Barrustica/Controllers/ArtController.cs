using Backend_Barrustica.Models;
using Backend_Barrustica.Service;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Barrustica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtController : ControllerBase
    {
        private readonly IArtService _artService;
        public ArtController(IArtService artService)
        {
            _artService = artService;
        }

        [HttpGet]
        [Route("artist")]
        public IActionResult GetArtist([FromQuery] int artistId)
        {
            Artist result = _artService.GetArtist(artistId);

            if(result == null)
            {
                return BadRequest("The artist with that id was not found");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("AddArtist")]
        public IActionResult AddArtist([FromQuery] string name, [FromQuery] string description, [FromQuery] string image)
        {
            _artService.AddArtist(name, description, image);

            return Ok();
        }

        [HttpGet]
        [Route("piece")]
        public IActionResult GetPiece([FromQuery] int pieceId)
        {
            Piece result = _artService.GetPiece(pieceId);

            if (result == null)
            {
                return BadRequest("The piece with that id was not found");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("AddPiece")]
        public IActionResult AddPiece([FromQuery] string name, [FromQuery] string description, [FromQuery] string style, [FromQuery] string image)
        {
            _artService.AddPiece(name, description, style, image);

            return Ok();
        }

        [HttpGet]
        [Route("ListPiece")]
        public async Task<ActionResult<List<Piece>>> GetListPiece()
        {
            List<Piece> result = await _artService.GetListPiece();

            if (result == null)
            {
                return BadRequest("No pieces");
            }

            return Ok(result);
        }
    }
}
