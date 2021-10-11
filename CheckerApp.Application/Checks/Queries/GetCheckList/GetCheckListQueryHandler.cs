using AutoMapper;
using CheckerApp.Application.Interfaces;
using CheckerApp.Application.Hardwares.Helpers;
using CheckerApp.Application.Hardwares.Queries;
using CheckerApp.Application.Softwares.Queries.GetSoftwaresList;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Checks.Queries.GetCheckList
{
    public class GetCheckListQueryHandler : IRequestHandler<GetCheckListQuery, CheckListDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetCheckListQueryHandler(IAppDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<CheckListDto> Handle(GetCheckListQuery request, CancellationToken cancellationToken)
        {
            var contract = await _context.Contracts.FindAsync(new object[] { request.ContractId }, cancellationToken);

            var vm = new CheckListDto
            {
                Contract = _mapper.Map<ContractDto>(contract)
            };

            foreach (var hardware in contract.HardwareList)
            {
                HardwareCheckDto hardwareCheckDto;

                if (hardware.CheckResult == null)
                {
                    var dto = _mapper.Map<HardwareDto>(hardware);
                    hardwareCheckDto = CheckHelper.GetHardwareCheckDto(dto.HardwareType);
                    hardwareCheckDto.Hardware = _mapper.Map<HardwareDto>(dto);
                }
                else
                {
                    hardwareCheckDto = _mapper.Map<HardwareCheckDto>(hardware.CheckResult);
                }

                vm.HardwareChecks.Add(hardwareCheckDto);
            }

            foreach (var software in contract.SoftwareList)
            {
                SoftwareCheckDto softwareCheckDto;

                if (software.CheckResult == null)
                {
                    var dto = _mapper.Map<SoftwareDto>(software);
                    softwareCheckDto = CheckHelper.GetSoftwareCheckDto(dto.SoftwareType);
                    softwareCheckDto.Software = dto;
                }
                else
                {
                    softwareCheckDto = _mapper.Map<SoftwareCheckDto>(software.CheckResult);
                }

                vm.SoftwareChecks.Add(softwareCheckDto);
            }

            vm.HardwareChecks = vm.HardwareChecks.OrderBy(h => h.Hardware.HardwareType).ToList();

            return vm;
        }
    }
}
