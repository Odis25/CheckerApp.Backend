using AutoMapper;
using AutoMapper.QueryableExtensions;
using CheckerApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Contracts.Queries.GetContractsList
{

    public class GetContractsListQueryHandler 
        : IRequestHandler<GetContractsListQuery, ContractsListDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetContractsListQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ContractsListDto> Handle(GetContractsListQuery request, CancellationToken cancellationToken)
        {
            var contracts = await _context.Contracts
                .ProjectTo<ContractDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new ContractsListDto { Contracts = contracts };
        }
    }

}
