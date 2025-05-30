using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatalogService.Api.Controllers
{
    //todo this should be moved to indentity servis
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IHttpClientFactory httpClientFactory, ILogger<AuthController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                {
                    _logger.LogWarning("Invalid login request: Username or Password is empty.");
                    return BadRequest("Username and Password are required.");
                }

                var client = _httpClientFactory.CreateClient();
                var discoveryDoc = await client.GetDiscoveryDocumentAsync("https://localhost:7191");
                if (discoveryDoc.IsError)
                {
                    _logger.LogError("Discovery document error: {Error}", discoveryDoc.Error);
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
                    _logger.LogError("Token request error: {Error}", tokenResponse.Error);
                    return BadRequest("Invalid credentials.");
                }

                _logger.LogInformation("Generated token for user: {0}", request.Username); 
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
                _logger.LogError(ex, "Error generating token for user: {0}", request.Username ?? "null");
                return StatusCode(500, "Authentication error.");
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    _logger.LogWarning("Invalid refresh request: RefreshToken is empty.");
                    return BadRequest("RefreshToken is required.");
                }

                var client = _httpClientFactory.CreateClient();
                var discoveryDoc = await client.GetDiscoveryDocumentAsync("https://localhost:5002");
                if (discoveryDoc.IsError)
                {
                    _logger.LogError("Discovery document error: {Error}", discoveryDoc.Error);
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
                    _logger.LogError("Refresh token error: {Error}", tokenResponse.Error);
                    return BadRequest("Invalid refresh token.");
                }

                _logger.LogInformation("Refreshed token for client");
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
                _logger.LogError(ex, "Error refreshing token");
                return StatusCode(500, "Authentication error.");
            }
        }
    }

    public record LoginRequest(string Username, string Password);
    public record RefreshRequest(string RefreshToken);
}