using AutoMapper;
using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.HardwareEntities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Hardwares.Queries
{
    public class GetHardwateDetailQueryHandler : IRequestHandler<GetHardwareDetailQuery, HardwareDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetHardwateDetailQueryHandler(IAppDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<HardwareDto> Handle(GetHardwareDetailQuery request, CancellationToken cancellationToken)
        {
            var hardware = await _context.Hardwares.FindAsync(new object[] { request.Id }, cancellationToken);

            if (hardware == null)
            {
                throw new NotFoundException(nameof(Hardware), request.Id);
            }

            return _mapper.Map<HardwareDto>(hardware);
        }
    }
}
