using AutoMapper;
using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.ContractEntities;
using MediatR;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Contracts.Queries.GetContractDetail
{
    public class GetContractQueryHandler
        : IRequestHandler<GetContractDetailQuery, ContractDetailDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public GetContractQueryHandler(IAppDbContext context, IMapper mapper, HttpClient httpClient) =>
            (_context, _mapper, _httpClient) = (context, mapper, httpClient);

        public async Task<ContractDetailDto> Handle(GetContractDetailQuery request, CancellationToken cancellationToken)
        {
            var contract = await _context.Contracts.FindAsync(new object[] { request.Id }, cancellationToken);

            if (contract == null)
            {
                throw new NotFoundException(nameof(Contract), request.Id);
            }

            var result = _mapper.Map<ContractDetailDto>(contract);

            var created = await _httpClient.GetStringAsync($"/account/{contract.CreatedBy}", cancellationToken);
            result.CreatedBy = created;
            if (!string.IsNullOrEmpty(contract.LastModifiedBy))
            {
                var edited = await _httpClient.GetStringAsync($"/account/{contract.LastModifiedBy}", cancellationToken);
                result.LastModifiedBy = edited;
            }

            result.HardwareList = result.HardwareList.OrderBy(h => h.HardwareType);

            return result;
        }
    }

}
