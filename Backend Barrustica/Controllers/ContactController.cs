using Backend_Barrustica.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace Backend_Barrustica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("sendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] FormData formData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var subject = "Nuevo mensaje del contacto de " + formData.Name;
                    var message = formData.Message;

                    // Envía el correo electrónico
                    await _emailService.SendEmailAsync(formData.Email, subject, message);

                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }
    }
    public class FormData
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
    }
}
