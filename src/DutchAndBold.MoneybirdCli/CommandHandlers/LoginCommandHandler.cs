using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DutchAndBold.MoneybirdCli.Commands;
using DutchAndBold.MoneybirdCli.Configurations;
using DutchAndBold.MoneybirdSdk.Contracts;
using DutchAndBold.MoneybirdSdk.Models;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DutchAndBold.MoneybirdCli.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand>
    {
        private static string TerminalReadLinePrefix => "> ";

        private readonly IStringLocalizer<LoginCommandHandler> _stringLocalizer;

        private readonly MoneybirdApiConfiguration _apiConfiguration;

        private readonly IAccessTokenAcquirer _accessTokenAcquirer;

        private readonly IAccessTokenStore _accessTokenStore;

        public LoginCommandHandler(
            IStringLocalizer<LoginCommandHandler> stringLocalizer,
            MoneybirdApiConfiguration apiConfiguration,
            IAccessTokenAcquirer accessTokenAcquirer,
            IAccessTokenStore accessTokenStore)
        {
            _stringLocalizer = stringLocalizer;
            _apiConfiguration = apiConfiguration;
            _accessTokenAcquirer = accessTokenAcquirer;
            _accessTokenStore = accessTokenStore;
        }

        public async Task<Unit> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(_stringLocalizer["Welcome!"]);
            Console.WriteLine(_stringLocalizer["To login and create an access token:"]);
            Console.WriteLine(
                _stringLocalizer["1. Open a browser and go to "] +
                "https://moneybird.com/oauth/authorize?" +
                $"client_id={_apiConfiguration.ClientId}" +
                "&redirect_uri=urn:ietf:wg:oauth:2.0:oob" +
                "&response_type=code" +
                "&scope=sales_invoices%20documents%20estimates%20bank%20settings");
            Console.WriteLine(_stringLocalizer["2. Copy and paste code."]);

            Console.Write(TerminalReadLinePrefix);

            var authenticationCode = Console.ReadLine();

            Console.WriteLine("\n" + _stringLocalizer["Retrieving access token..."]);

            AccessToken accessToken;

            try
            {
                accessToken = await _accessTokenAcquirer.AcquireAccessTokenAsync(
                    authenticationCode,
                    cancellationToken);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine(_stringLocalizer["Retrieving access token failed."]);
                return Unit.Value;
            }

            await _accessTokenStore.StoreTokenAsync(accessToken, cancellationToken);

            Console.WriteLine(_stringLocalizer["Login successful!"]);

            return Unit.Value;
        }
    }
}