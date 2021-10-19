using AutoMapper;
using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.HardwareEntities;
using MediatR;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Hardwares.Queries
{
    public class GetHardwateDetailQueryHandler : IRequestHandler<GetHardwareDetailQuery, HardwareDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public GetHardwateDetailQueryHandler(IAppDbContext context, IMapper mapper, HttpClient httpClient) =>
            (_context, _mapper, _httpClient) = (context, mapper, httpClient);
        

        public async Task<HardwareDto> Handle(GetHardwareDetailQuery request, CancellationToken cancellationToken)
        {
            var hardware = await _context.Hardwares.FindAsync(new object[] { request.Id }, cancellationToken);

            if (hardware == null)
            {
                throw new NotFoundException(nameof(Hardware), request.Id);
            }
           
            var dto = _mapper.Map<HardwareDto>(hardware);

            var created = await _httpClient.GetStringAsync($"/account/{hardware.CreatedBy}", cancellationToken);
            dto.CreatedBy = created;

            if (!string.IsNullOrEmpty(hardware.LastModifiedBy))
            {
                var edited = await _httpClient.GetStringAsync($"/account/{hardware.LastModifiedBy}", cancellationToken);
                dto.LastModifiedBy = edited;
            }

            return dto;
        }
    }
}
