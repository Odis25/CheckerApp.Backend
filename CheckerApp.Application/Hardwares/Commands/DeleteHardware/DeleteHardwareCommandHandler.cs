using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.HardwareEntities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Hardwares.Commands.DeleteHardware
{
    public class DeleteHardwareCommandHandler 
        : IRequestHandler<DeleteHardwareCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteHardwareCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteHardwareCommand request, CancellationToken cancellationToken)
        {
            var hardware = await _context.Hardwares.FindAsync(new object[] { request.Id }, cancellationToken);
            var contract = await _context.Contracts.FindAsync(new object[] { hardware.ContractId }, cancellationToken);

            if (hardware == null)
            {
                throw new NotFoundException(nameof(Hardware), request.Id);
            }

            var parameters = hardware.CheckResult?.CheckParameters;

            if (parameters != null)
            {
                _context.CheckParameters.RemoveRange(parameters);
            }

            contract.HasProtocol = false;

            _context.Hardwares.Remove(hardware);
            _context.Contracts.Update(contract);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
