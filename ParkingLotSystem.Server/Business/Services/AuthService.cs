using Microsoft.IdentityModel.Tokens;
using ParkingLotSystem.Server.Business.Interfaces;
using ParkingLotSystem.Server.Core.DTOs;
using ParkingLotSystem.Server.Core.Entities;
using ParkingLotSystem.Server.DataAccess.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace ParkingLotSystem.Server.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public AuthService(IUserRepository userRepository, IConfiguration configuration, HttpClient httpClient)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<(string token, string role, int siteID, string siteSecret)> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || user.Password != password)
                return (null, null, 0, null);

            string token = GenerateJwtToken(user);

            return (token, user.Role, user.SiteID, user.Site.SiteSecret); // ✅ Kullanıcının tüm bilgilerini döndürüyoruz
        }

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("SiteID", user.SiteID.ToString()), //  Kullanıcının SiteID'sini ekledik
                new Claim("SiteSecret", user.Site.SiteSecret) //  Kullanıcının SiteSecret'ini ekledik
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(2), // Token 2 saat geçerli
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<int?> GetSiteIdFromAuthAsync(string clientId, string siteSecret)
        {
            var authRequest = new
            {
                ClientId = clientId,
               SiteSecret= siteSecret,
            };

            var response = await _httpClient.PostAsJsonAsync("https://authserver.com/api/auth/getSiteId", authRequest);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseData = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            return responseData?.SiteId;
        }

    }
}
