using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.ContractEntities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Contracts.Commands.UpdateContract
{
    public class UpdateContractCommandHandler 
        : IRequestHandler<UpdateContractCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateContractCommandHandler(IAppDbContext context) =>
            _context = context;

        public async Task<Unit> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
        {
            var contract = await _context.Contracts.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (contract == null)
            {
                throw new NotFoundException(nameof(Contract), request.Id);
            }

            contract.Name = request.Name;
            contract.ContractNumber = request.ContractNumber;
            contract.DomesticNumber = request.DomesticNumber;
            contract.ProjectNumber = request.ProjectNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
