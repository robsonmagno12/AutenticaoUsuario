using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        // qual usuario que está recebendo
        public Token CreateToken(IdentityUser<int> usuario)
        {
            Claim[] direitosUsuarios = new Claim[]
            {
                  new Claim("username", usuario.UserName),
                  new Claim("id", usuario.Id.ToString())
             };
            //gerando chave de segurança 
            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
                );
            //gerando as credenciais
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: direitosUsuarios,
                signingCredentials: credenciais,
                //quanto tempo o token vai expirar
                expires: DateTime.UtcNow.AddHours(1)
                );
            //converter em String para armazenar no token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(tokenString);
            

        }
    }
}
