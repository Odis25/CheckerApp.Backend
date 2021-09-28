using AutoMapper;
using CheckerApp.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Softwares.Queries.GetSoftware
{
    public class GetSoftwareQueryHandler : IRequestHandler<GetSoftwareQuery, SoftwareDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSoftwareQueryHandler(IAppDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<SoftwareDto> Handle(GetSoftwareQuery request, CancellationToken cancellationToken)
        {
            var software = await _context.Softwares.FindAsync(new object[] { request.Id }, cancellationToken);

            return _mapper.Map<SoftwareDto>(software);
        }
    }
}
