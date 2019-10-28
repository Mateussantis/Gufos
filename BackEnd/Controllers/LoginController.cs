using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BackEnd.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        
        GufosContext _context = new GufosContext();

        // Definimos uma variavel para percorrer nossos metodos com as configuraçoes obtidas no  appsettings.json
        private IConfiguration _config;

        // Definimos m metodo construtor para poder acessar estas configs

        public LoginController(IConfiguration config) {
            _config = config;
        }

        // Chamamos nosso metodo para validar o usuario na aplicação
        private Usuario ValidaUsuario(LoginViewModel login) {

            var usuario = _context.Usuario.FirstOrDefault(
                u => u.Email == login.Email && u.Senha == login.Senha
            );

            // if(usuario != null) {
            //     usuario = login;
            // }

            return usuario;
        }

        // Geramos o Token
        private string GerarToken(Usuario userInfo) {

            var securytiKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credintials = new SigningCredentials(securytiKey, SecurityAlgorithms.HmacSha256);

            // Definimos nossas Claims (Dados da sessão)

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.Nome),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credintials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Usamos essa anotação para ignorar a autenticação nesse metodo
        [AllowAnonymous]
        [HttpPost]
        public IActionResult  Login([FromBody]LoginViewModel login) {

            IActionResult response = Unauthorized();

            var user = ValidaUsuario(login);

            if(user != null) {
                var tokenString = GerarToken(user);
                response = Ok(new {token = tokenString});
            }
            return response;
        }

    }
}