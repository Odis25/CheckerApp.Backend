using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.ContractEntities;
using CheckerApp.Domain.Entities.SoftwareEntities;
using CheckerApp.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Softwares.Commands.CreateSoftware
{
    public class CreateSoftwareCommandHandler : IRequestHandler<CreateSoftwareCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateSoftwareCommandHandler(IAppDbContext context) =>
            _context = context;
        
        public async Task<int> Handle(CreateSoftwareCommand request, CancellationToken cancellationToken)
        {
            var contract = await _context.Contracts.FindAsync(new object[] { request.ContractId }, cancellationToken);

            if (contract == null)
            {
                throw new NotFoundException(nameof(Contract), request.ContractId);
            }

            var entity = request.SoftwareType switch
            {
                SoftwareType.SCADA => new SCADA(),
                SoftwareType.Other => new Software(),
                _ => throw new NotImplementedException()
            };

            entity.Name = request.Name;
            entity.Version = request.Version;

            contract.SoftwareList.Add(entity);
            contract.HasProtocol = false;
            _context.Update(contract);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
