using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Code_CloudSchool.Services;

public class EmailVerificationService : IEmailVerificationService
{

    private readonly AppDBContext _context;
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpClientFactory;

    public EmailVerificationService(AppDBContext context, IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _config = config;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GenerateAndStoreToken(User user)
        {
            var token = new Random().Next(100000, 999999).ToString(); // 6-digit code
            var expiry = DateTime.UtcNow.AddMinutes(10);

            var tokenEntry = new EmailVerificationToken
            {
                Token = token,
                ExpiryTime = expiry,
                UserId = user.UserId
            };

            _context.EmailVerificationTokens.Add(tokenEntry);
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<bool> SendVerificationEmail(User user, string token, string? roleEmail = null)
        {
            var resendKey = _config["Resend:ApiKey"];
            var fromEmail = _config["Resend:FromEmail"];

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://api.resend.com/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resendKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new
            {
                from = fromEmail,
                to = user.privateEmail,
                subject = "Your verification code",
                html = $"<p>Your CodeCloudSchool verification code is: <strong>{token}</strong></p>" + (roleEmail != null ? $"<p>Your Code Cloud School generated email is: <strong>{roleEmail}</strong></p>" : "")
            };

            var response = await client.PostAsync("emails",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
    }
}
