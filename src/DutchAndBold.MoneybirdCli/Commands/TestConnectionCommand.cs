using CommandLine;
using MediatR;

namespace DutchAndBold.MoneybirdCli.Commands
{
    [Verb("test-connection", HelpText = "Tests the connection to the API.")]
    public class TestConnectionCommand : IRequest
    {
    }
}