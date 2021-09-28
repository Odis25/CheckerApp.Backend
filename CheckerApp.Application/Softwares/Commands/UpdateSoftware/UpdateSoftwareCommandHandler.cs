using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.SoftwareEntities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Softwares.Commands.UpdateSoftware
{
    public class UpdateSoftwareCommandHandler : IRequestHandler<UpdateSoftwareCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateSoftwareCommandHandler(IAppDbContext context) => 
            _context = context;

        public async Task<Unit> Handle(UpdateSoftwareCommand request, CancellationToken cancellationToken)
        {
            var software = await _context.Softwares.FindAsync(new object[] { request.Id }, cancellationToken);

            if (software == null)
            {
                throw new NotFoundException(nameof(Software), request.Id);
            }

            software.Name = request.Name;
            software.Version = request.Version;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
