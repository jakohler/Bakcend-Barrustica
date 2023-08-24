using Backend_Barrustica.Models;
using Backend_Barrustica.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;

namespace Backend_Barrustica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly Random _random = new Random();

        public UserController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult> SignUp([FromBody] User user)
        {

            // Generar el código de autenticación
            string authCode = GenerateAuthenticationCode();

            // Lógica para registrar al usuario en la base de datos usando Entity Framework
            using (var context = new BarrusticaDbContext())
            {
                var newUser = new User
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    AuthCode = authCode,
                    IsAdmin = false
                };

                context.UserEntity.Add(newUser);
                context.SaveChanges();
            }

            // Enviar el código por correo electrónico
            await _emailService.SendEmailAsync(user.Email, "Código de autenticación", $"Tu código de autenticación es: {authCode}");

            return Ok();
        }

        [HttpGet]
        [Route("ConfirmSignUp")]
        public async Task<ActionResult> ConfirmSignUp([FromQuery] string userName)
        {
            // Lógica para confirmar el registro del usuario en la base de datos usando Entity Framework
            // Devuelve un resultado adecuado (por ejemplo, Ok() o BadRequest())
            User user;
            using (var context = new BarrusticaDbContext())
            {
                user = await context.UserEntity.FirstAsync(a => a.Username == userName);
                user.IsAdmin = true;

                context.SaveChanges();
            }
            

            return Ok(user.AuthCode);
        }

        [HttpGet]
        [Route("SignIn")]
        public async Task<ActionResult> SignIn([FromQuery] string userName, [FromQuery] string password)
        {
            // Lógica para confirmar el registro del usuario en la base de datos usando Entity Framework
            // Devuelve un resultado adecuado (por ejemplo, Ok() o BadRequest())
            User user;
            using (var context = new BarrusticaDbContext())
            {
                user = await context.UserEntity.FirstAsync(a => a.Username == userName && a.Password == password);
            }

            if(user != null)
            {
                return Ok();
            }

            return BadRequest("The username or password is not correct");
        }

        private string GenerateAuthenticationCode()
        {
            const int codeLength = 6; // Longitud del código de autenticación
            const string allowedChars = "0123456789"; // Caracteres permitidos para el código

            // Generar el código utilizando los caracteres permitidos
            StringBuilder code = new StringBuilder();
            for (int i = 0; i < codeLength; i++)
            {
                int randomIndex = _random.Next(0, allowedChars.Length);
                code.Append(allowedChars[randomIndex]);
            }

            return code.ToString();
        }
    }
}
