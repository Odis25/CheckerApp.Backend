﻿using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.SoftwareEntities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Softwares.Commands.DeleteSoftware
{
    public class DeleteSoftwareCommandHandler : IRequestHandler<DeleteSoftwareCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteSoftwareCommandHandler(IAppDbContext context) => 
            _context = context;

        public async Task<Unit> Handle(DeleteSoftwareCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Softwares.FindAsync(new object[] { request.Id }, cancellationToken);
            var contract = await _context.Contracts.FindAsync(new object[] { entity.ContractId }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Software), request.Id);
            }

            var parameters = entity.CheckResult?.CheckParameters;

            if (parameters != null)
            {
                _context.CheckParameters.RemoveRange(parameters);
            }

            _context.Softwares.Remove(entity);

            contract.HasProtocol = false;

            _context.Contracts.Update(contract);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
