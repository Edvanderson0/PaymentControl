using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PaymentControl.Services.Token
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TokenGenerator(string email)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSetting:Secret"]);
           
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSetting:ExpirationMinutes"])),
                Issuer = _configuration["JwtSetting:Issuer"],
                
                Audience = _configuration["JwtSetting:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
