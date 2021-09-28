using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.ContractEntities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Contracts.Commands.DeleteContract
{
    public class DeleteContractCommandHandler
        : IRequestHandler<DeleteContractCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteContractCommandHandler(IAppDbContext context) =>
            _context = context;

        public async Task<Unit> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Contracts.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Contract), request.Id);
            }

            // Удаляем оборудование
            foreach (var hardware in entity.HardwareList)
            {
                var parameters = hardware.CheckResult?.CheckParameters;

                if (parameters != null)
                {
                    _context.CheckParameters.RemoveRange(parameters);
                }

                _context.Hardwares.Remove(hardware);
            }

            // Удаляем ПО
            foreach (var software in entity.SoftwareList)
            {
                var parameters = software.CheckResult?.CheckParameters;

                if (parameters != null)
                {
                    _context.CheckParameters.RemoveRange(parameters);
                }

                _context.Softwares.Remove(software);
            }

            _context.Contracts.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
