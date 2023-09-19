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
        [Route("taller")]
        public IActionResult GetTaller([FromQuery] int tallerId)
        {
            Taller result = _artService.GetTaller(tallerId);

            if(result == null)
            {
                return BadRequest("The artist with that id was not found");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("AddTaller")]
        public IActionResult AddTaller([FromBody] Taller taller)
        {
            _artService.AddTaller(taller.Name, taller.Description, taller.Image);

            return Ok();
        }

        [HttpGet]
        [Route("ListTaller")]
        public async Task<ActionResult<List<Taller>>> GetListTaller()
        {
            List<Taller> result = await _artService.GetListTaller();

            if (result == null)
            {
                return BadRequest("No taller");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("AddSeminario")]
        public IActionResult AddSeminario([FromBody] Seminario seminario)
        {
            _artService.AddSeminario(seminario.Name, seminario.Description, seminario.Image);

            return Ok();
        }

        [HttpGet]
        [Route("ListSeminario")]
        public async Task<ActionResult<List<Seminario>>> GetListSeminario()
        {
            List<Seminario> result = await _artService.GetListSeminario();

            if (result == null)
            {
                return BadRequest("No Seminario");
            }

            return Ok(result);
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
        public IActionResult AddPiece([FromBody] Piece piece)
        {
            _artService.AddPiece(piece.Name, piece.Description, piece.Style, piece.Image, piece.IdArtist);

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
        [HttpDelete]
        [Route("DeleteItem")]
        public ActionResult DeleteItem([FromQuery] string image, [FromQuery] string type)
        {
            _artService.DeleteItem(image, type);

            return Ok();
        }
    }
}
