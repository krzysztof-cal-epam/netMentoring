using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatalogService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var discoveryDoc = await client.GetDiscoveryDocumentAsync("https://localhost:7191");
                if (discoveryDoc.IsError)
                {
                    Console.WriteLine("Discovery document error: {Error}", discoveryDoc.Error);
                    return StatusCode(500, "Authentication service unavailable.");
                }

                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = discoveryDoc.TokenEndpoint,
                    ClientId = "catalog-client",
                    ClientSecret = "secret",
                    UserName = request.Username,
                    Password = request.Password,
                    Scope = "CatalogApi openid profile roles"
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine("Token request error: {Error}", tokenResponse.Error);
                    return BadRequest("Invalid credentials.");
                }

                Console.WriteLine("Generated token for user {Username}", request.Username);
                return Ok(new
                {
                    tokenResponse.AccessToken,
                    tokenResponse.RefreshToken,
                    tokenResponse.ExpiresIn,
                    TokenType = "Bearer"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error <{ex}> generating token for user {request.Username}");
                return StatusCode(500, "Authentication error.");
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var discoveryDoc = await client.GetDiscoveryDocumentAsync("https://localhost:5002");
                if (discoveryDoc.IsError)
                {
                    Console.WriteLine("Discovery document error: {Error}", discoveryDoc.Error);
                    return StatusCode(500, "Authentication service unavailable.");
                }

                var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
                {
                    Address = discoveryDoc.TokenEndpoint,
                    ClientId = "catalog-client",
                    ClientSecret = "secret",
                    RefreshToken = request.RefreshToken
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine("Refresh token error: {Error}", tokenResponse.Error);
                    return BadRequest("Invalid refresh token.");
                }

                Console.WriteLine("Refreshed token for client");
                return Ok(new
                {
                    tokenResponse.AccessToken,
                    tokenResponse.RefreshToken,
                    tokenResponse.ExpiresIn,
                    TokenType = "Bearer"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing token <{ex}>");
                return StatusCode(500, "Authentication error.");
            }
        }
    }

    public record LoginRequest(string Username, string Password);
    public record RefreshRequest(string RefreshToken);
}