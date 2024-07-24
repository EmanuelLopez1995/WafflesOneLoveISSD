using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data.SqlClient;
using WafflesBackCommon.Models;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        var user = AuthenticateUser(login);

        if (user == null)
            return Unauthorized();

        var tokenString = GenerateJWT(user);
        return Ok(new { token = tokenString });
    }

    private UsuarioModel AuthenticateUser(LoginModel login)
    {
        UsuarioModel user = null;
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string sql = "SELECT * FROM Usuario WHERE nombreUsuario = @Username AND claveUsuario = @Password";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Username", login.Username);
            command.Parameters.AddWithValue("@Password", login.Password);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = new UsuarioModel
                    {
                        idUsuario = reader.GetInt32(0),
                        nombreUsuario = reader.GetString(1),
                        claveUsuario = reader.GetString(2)
                    };
                }
            }
        }
        return user;
    }

    private string GenerateJWT(UsuarioModel user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.nombreUsuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string sql = "SELECT idSeccion FROM UsuarioSecciones WHERE idUsuario = @UserId";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@UserId", user.idUsuario);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    claims.Add(new Claim("sections", reader.GetInt32(0).ToString()));
                }
            }
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
