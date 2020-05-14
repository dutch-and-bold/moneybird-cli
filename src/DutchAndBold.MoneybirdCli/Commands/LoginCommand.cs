using CommandLine;
using MediatR;

namespace DutchAndBold.MoneybirdCli.Commands
{
    [Verb("login", HelpText = "Login and persist access token.")]
    public class LoginCommand : IRequest
    {
    }
}