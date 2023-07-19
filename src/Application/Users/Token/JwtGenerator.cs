using Domain.Entities.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users.Token;

public sealed class JwtGenerator
{
  private readonly JwtOption _option;

  public JwtGenerator(IOptions<JwtOption> option)
  {
    _option = option.Value;
  }

  public GenerateTokenResult GenerateToken(User user)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes(_option.SecretKey);

    var customClaims = user.Claims.Select(e => new Claim(e.ClaimType, e.Value));

    var claims = new List<Claim>()
    {
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new Claim(ClaimTypes.GivenName, user.FullName),
      new Claim(ClaimTypes.Email, user.Email.ToString())
    }.Union(customClaims);

    var tokenDescription = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddSeconds(_option.ExpiredIn),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
    };

    var token = tokenHandler.CreateToken(tokenDescription);

    return new() { Token = tokenHandler.WriteToken(token) };
  }
}
