using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DutchAndBold.MoneybirdCli.Commands;
using DutchAndBold.MoneybirdSdk.Domain.Models.AdministrationAggregate;
using DutchAndBold.MoneybirdSdk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DutchAndBold.MoneybirdCli.CommandHandlers
{
    public class TestConnectionCommandHandler : IRequestHandler<TestConnectionCommand>
    {
        private readonly IStringLocalizer<TestConnectionCommandHandler> _stringLocalizer;

        private readonly IMoneybirdRepositoryRead<Administration> _administrationRepository;

        public TestConnectionCommandHandler(
            IStringLocalizer<TestConnectionCommandHandler> stringLocalizer,
            IMoneybirdRepositoryRead<Administration> administrationRepository)
        {
            _stringLocalizer = stringLocalizer;
            _administrationRepository = administrationRepository;
        }

        public async Task<Unit> Handle(TestConnectionCommand request, CancellationToken cancellationToken)
        {
            var administrations = await _administrationRepository.GetAsync(cancellationToken);

            Console.WriteLine(_stringLocalizer["Connection OK!"]);
            Console.WriteLine(_stringLocalizer["You have access to:"] + "\n");
            administrations.ToList().ForEach(a => Console.WriteLine($"* {a.Name}"));
            return Unit.Value;
        }
    }
}