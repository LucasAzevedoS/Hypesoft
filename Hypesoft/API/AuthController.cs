using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Hypesoft.Domain.Entities;
using System.Security.Claims;

namespace Hypesoft.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AuthController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var keycloakSettings = _configuration.GetSection("Keycloak");
                var realm = keycloakSettings["realm"];
                var authServerUrl = keycloakSettings["auth-server-url"];
                var clientId = keycloakSettings["resource"]; 
                var clientSecret = keycloakSettings["credentials:secret"];

                var tokenEndpoint = $"{authServerUrl}realms/{realm}/protocol/openid-connect/token";

                var formData = new List<KeyValuePair<string, string>>
                {
                    new("grant_type", "password"),
                    new("client_id", clientId),
                    new("client_secret", clientSecret),
                    new("username", request.Username),
                    new("password", request.Password)
                };

                var formContent = new FormUrlEncodedContent(formData);
                var response = await _httpClient.PostAsync(tokenEndpoint, formContent);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<JsonElement>(content);

                    return Ok(new
                    {
                        access_token = tokenResponse.GetProperty("access_token").GetString(),
                        expires_in = tokenResponse.GetProperty("expires_in").GetInt32(),
                        token_type = tokenResponse.GetProperty("token_type").GetString(),
                        refresh_token = tokenResponse.TryGetProperty("refresh_token", out var refresh) ? refresh.GetString() : null
                    });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return BadRequest(new { error = "Invalid credentials", details = errorContent });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }
    }
}