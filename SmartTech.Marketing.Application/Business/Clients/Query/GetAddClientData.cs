using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using System.Text;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class GetAddClientDataHandler : IRequestHandler<GetAddClientDataHandlerInput, GetAddClientDataHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<GetAddClientDataHandler> _logger;
        public GetAddClientDataHandler(ILogger<GetAddClientDataHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<GetAddClientDataHandlerOutput> Handle(GetAddClientDataHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAddClientData business logic");
            GetAddClientDataHandlerOutput output = new GetAddClientDataHandlerOutput(request.CorrelationId());
            output.Countries = await _databaseService.Country.Select(o => new CountryData { CountryId = o.Id, CountryName = o.CountryName, CountryPrefix = o.CountryPrefix }).ToListAsync();
            output.ClientTypes = await _databaseService.ClientType.Select(o => new ClientTypeData { Id = o.Id, Type = o.Type }).ToListAsync();
            output.TempPass = GeneratePassword();
            return output;
        }
        public static string GeneratePassword()
        {
            const int passwordLength = 6;
            const int requiredUniqueChars = 1;
            const string upperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string nonAlphanumericChars = "!@#$%^&*()-_=+[]{}|;:',.<>?/`~";

            Random random = new Random();
            StringBuilder password = new StringBuilder();

            // Ensure at least one of each required type
            password.Append(upperCaseChars[random.Next(upperCaseChars.Length)]);
            password.Append(lowerCaseChars[random.Next(lowerCaseChars.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(nonAlphanumericChars[random.Next(nonAlphanumericChars.Length)]);

            // Fill the rest of the password length with random characters from all categories
            string allChars = upperCaseChars + lowerCaseChars + digits + nonAlphanumericChars;
            while (password.Length < passwordLength)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Ensure the password has the required number of unique characters
            while (password.ToString().Distinct().Count() < requiredUniqueChars)
            {
                password.Clear();
                password.Append(upperCaseChars[random.Next(upperCaseChars.Length)]);
                password.Append(lowerCaseChars[random.Next(lowerCaseChars.Length)]);
                password.Append(digits[random.Next(digits.Length)]);
                password.Append(nonAlphanumericChars[random.Next(nonAlphanumericChars.Length)]);
                while (password.Length < passwordLength)
                {
                    password.Append(allChars[random.Next(allChars.Length)]);
                }
            }

            // Shuffle the password to ensure randomness
            return new string(password.ToString().OrderBy(c => random.Next()).ToArray());
        }
    }
}
