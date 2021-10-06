using CheckerApp.Application.Common.Exceptions;
using CheckerApp.Application.Interfaces;
using CheckerApp.Domain.Entities.ContractEntities;
using CheckerApp.Domain.Entities.HardwareEntities;
using CheckerApp.Domain.Enums;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckerApp.Application.Hardwares.Commands.CreateHardware
{
    public class CreateHardwareCommandHandler 
        : IRequestHandler<CreateHardwareCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateHardwareCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateHardwareCommand request, CancellationToken cancellationToken)
        {
            var contract = await _context.Contracts.FindAsync(new object[] { request.ContractId }, cancellationToken);

            if (contract == null)
            {
                throw new NotFoundException(nameof(Contract), request.ContractId);
            }

            Hardware entity = request.HardwareType switch
            {
                HardwareType.Cabinet => new Cabinet
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    Constructed = (DateTime)request.Constructed,
                    ConstructedBy = request.ConstructedBy
                },
                HardwareType.DiffPressure => new DiffPressure
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceModel = request.DeviceModel,
                    DeviceType = request.DeviceType,
                    MinValue = request.MinValue,
                    MaxValue = request.MaxValue,
                    EU = request.EU,
                    SignalType = request.SignalType
                },
                HardwareType.Pressure => new Pressure
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceModel = request.DeviceModel,
                    DeviceType = request.DeviceType,
                    MinValue = request.MinValue,
                    MaxValue = request.MaxValue,
                    EU = request.EU,
                    SignalType = request.SignalType
                },
                HardwareType.Temperature => new Temperature
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceModel = request.DeviceModel,
                    DeviceType = request.DeviceType,
                    MinValue = request.MinValue,
                    MaxValue = request.MaxValue,
                    EU = request.EU,
                    SignalType = request.SignalType
                },
                HardwareType.GasAnalyzer => new GasAnalyzer
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceModel = request.DeviceModel,
                    DeviceType = request.DeviceType,
                    MinValue = request.MinValue,
                    MaxValue = request.MaxValue,
                    EU = request.EU,
                    SignalType = request.SignalType
                },
                HardwareType.FireSensor => new FireSensor
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceModel = request.DeviceModel,
                    DeviceType = request.DeviceType
                },
                HardwareType.Flowmeter => new Flowmeter
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceModel = request.DeviceModel,
                    DeviceType = request.DeviceType,
                    MinValue = request.MinValue,
                    MaxValue = request.MaxValue,
                    EU = request.EU,
                    Kfactor = request.KFactor,
                    SignalType = request.SignalType,
                    Settings = new ModbusSettings
                    {
                        Address = request.ModbusSettings.Address,
                        BoudRate = request.ModbusSettings.BoudRate,
                        DataBits = request.ModbusSettings.DataBits,
                        Parity = Enum.GetName(typeof(Parity), request.ModbusSettings.Parity),
                        StopBit = request.ModbusSettings.StopBit
                    }
                },
                HardwareType.FlowComputer => new FlowComputer
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceModel = request.DeviceModel,
                    IP = request.IPAddress,
                    AssemblyVersion = request.AssemblyVersion,
                    CRC32 = request.CRC32,
                    LastConfigDate = request.LastConfigDate
                },
                HardwareType.PLC => new PLC
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceModel = request.DeviceModel,
                    IP = request.IPAddress,
                    AssemblyVersion = request.AssemblyVersion
                },
                HardwareType.Valve => new Valve
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceType = request.DeviceType,
                    DeviceModel = request.DeviceModel,
                    SignalType = request.SignalType,
                    Settings = new ModbusSettings
                    {
                        Address = request.ModbusSettings.Address,
                        BoudRate = request.ModbusSettings.BoudRate,
                        DataBits = request.ModbusSettings.DataBits,
                        Parity = Enum.GetName(typeof(Parity), request.ModbusSettings.Parity),
                        StopBit = request.ModbusSettings.StopBit
                    }
                },
                HardwareType.Network => new NetworkHardware
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceType = request.DeviceType,
                    DeviceModel = request.DeviceModel,
                    Mask = request.Mask,
                    NetworkDevices = request.NetworkDevices.Select(dto => new NetworkDevice
                    {
                        IP = dto.IP,
                        MacAddress = dto.MacAddress,
                        Name = dto.Name
                    }).ToHashSet()
                },
                HardwareType.InformPanel => new InformPanel
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceModel = request.DeviceModel,
                    DeviceType = request.DeviceType,
                    PanelType = request.PanelType
                },
                HardwareType.ARM => new ARM
                {
                    Position = request.Position,
                    Name = request.ArmName,
                    SerialNumber = request.SerialNumber,
                    Monitor = request.Monitor,
                    MonitorSN = request.MonitorSN,
                    Keyboard = request.Keyboard,
                    KeyboardSN = request.KeyboardSN,
                    Mouse = request.Mouse,
                    MouseSN = request.MouseSN,
                    OS = request.OS,
                    ProductKeyOS = request.ProductKeyOS,
                    HasRAID = request.HasRAID,
                    NetworkAdapters = request.NetworkAdapters.Select(dto => new NetworkAdapter
                    {
                        IP = dto.IP,
                        MacAddress = dto.MacAddress
                    }).ToHashSet()
                },
                HardwareType.APC => new APC 
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceModel = request.DeviceModel,
                    DeviceType = request.DeviceType
                },
                HardwareType.FireModule => new FireModule
                {
                    Position = request.Position,
                    SerialNumber = request.SerialNumber,
                    DeviceType = request.DeviceType
                },
                _ => throw new NotImplementedException()
            };

            contract.HardwareList.Add(entity);
            contract.HasProtocol = false;

            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
