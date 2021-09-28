using AutoMapper;
using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.ContractEntities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Contracts.Queries.GetContractDetail
{
    public class GetContractQueryHandler 
        : IRequestHandler<GetContractDetailQuery, ContractDetailDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetContractQueryHandler(IAppDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<ContractDetailDto> Handle(GetContractDetailQuery request, CancellationToken cancellationToken)
        {

            var contract = await _context.Contracts.FindAsync(new object[] { request.Id }, cancellationToken);

            if (contract == null)
            {
                throw new NotFoundException(nameof(Contract), request.Id);
            }

            var result = _mapper.Map<ContractDetailDto>(contract);

            result.HardwareList = result.HardwareList.OrderBy(h => h.HardwareType);

            return result;
        }
    }

}
