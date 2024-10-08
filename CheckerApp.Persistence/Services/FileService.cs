﻿using CheckerApp.Application.Checks.Queries.GetCheckResultFile;
using CheckerApp.Application.Interfaces;
using CheckerApp.Application.Hardwares.Queries;
using CheckerApp.Application.Softwares.Queries.GetSoftwaresList;
using CheckerApp.Domain.Enums;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerApp.Persistence.Services
{
    public class FileService : IFileService
    {
        public async Task<byte[]> BuildExcelFileAsync(CheckResultDto checkResult)
        {
            using (var excelPackage = new ExcelPackage())
            {
                //Общие настройки книги
                excelPackage.Workbook.Properties.Author = "Incomsystem";
                excelPackage.Workbook.Properties.Title = "Акт заводских испытаний";
                excelPackage.Workbook.Properties.Subject = "Какая-то тема";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                // Создаем лист документа
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Акт");

                worksheet.Cells.Style.Font.Size = 14;
                worksheet.Cells.Style.Font.Name = "Times New Roman";

                // Заголовок документа
                var contract = checkResult.Contract;

                worksheet.Cells["A1:F1"].Merge = true;
                worksheet.Cells["A2:F4"].Merge = true;

                var domectic = string.IsNullOrWhiteSpace(contract.DomesticNumber) ? $"({contract.DomesticNumber})" : string.Empty;

                worksheet.Cells["A1"].Value = "Протокол";
                worksheet.Cells["A2"].Value = $"испытаний оборудования СОИ \"{contract.Name}\", дог.{contract.ContractNumber}{domectic}, вн.проект {contract.ProjectNumber}";

                worksheet.Cells["A2"].Style.WrapText = true;
                worksheet.Cells["A1:F4"].Style.Font.Bold = true;
                worksheet.Cells["A1:F4"].Style.Font.Size = 16;
                worksheet.Cells["A1:F4"].Style.Font.Name = "Times New Roman";
                worksheet.Cells["A1:F4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1:F4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                // Температура, влажность, средство измерения
                worksheet.Cells["A6:B6"].Merge = true;
                worksheet.Cells["A7:B7"].Merge = true;
                worksheet.Cells["A8:B8"].Merge = true;

                var rnd = new Random();

                worksheet.Cells["A6"].Value = $"Температура воздуха: +{rnd.Next(23, 26)}.{rnd.Next(0, 9)} Град С.";
                worksheet.Cells["A7"].Value = $"Влажность воздуха: {rnd.Next(35, 45)}%";
                worksheet.Cells["A8"].Value = "Средство измерений: ИВТМ-7 №56613";


                // Дата и место
                worksheet.Cells["A9:B9"].Merge = true;

                worksheet.Cells["A9"].Value = "г.Казань, АО НИЦ \"ИНКОМСИСТЕМ\"";
                worksheet.Cells["F9"].Value = $"Дата: {DateTime.Now.ToShortDateString()}";

                worksheet.Cells["A9:F9"].Style.Font.UnderLine = true;
                worksheet.Cells["A9:F9"].Style.Font.Bold = true;
                worksheet.Cells["A9:F9"].Style.Font.Name = "Times New Roman";
                worksheet.Cells["A9:F9"].Style.Font.Size = 14;

                // Заголовок таблицы. 1 строка
                worksheet.Cells["A11"].Value = "п/п";
                worksheet.Cells["B11"].Value = "Вид контроля (испытаний)";
                worksheet.Cells["C11"].Value = "Метод проверки";
                worksheet.Cells["D11"].Value = "Результат проверки";
                worksheet.Cells["E11"].Value = "Дата проведения";
                worksheet.Cells["F11"].Value = "Примечание";

                // Заголовок таблицы. 2 строка
                worksheet.Cells["A12"].Value = "1";
                worksheet.Cells["B12"].Value = "2";
                worksheet.Cells["C12"].Value = "3";
                worksheet.Cells["D12"].Value = "4";
                worksheet.Cells["E12"].Value = "5";
                worksheet.Cells["F12"].Value = "6";

                worksheet.Row(11).Height *= 2;
                worksheet.Cells["A11:F12"].Style.Font.Bold = true;
                worksheet.Cells["A11:F12"].Style.Font.Name = "Times New Roman";
                worksheet.Cells["A11:F12"].Style.Font.Size = 14;

                worksheet.Cells["A11:F12"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A11:F12"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                worksheet.Cells["A11:F12"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A11:F12"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A11:F12"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A11:F12"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                // Размеры столбцов
                worksheet.Cells["A11:F11"].AutoFitColumns();
                worksheet.Cells["F9"].AutoFitColumns();

                worksheet.Column(2).Width = 85;
                worksheet.Column(2).Style.WrapText = true;
                worksheet.Column(6).Style.WrapText = true;

                double mergedCellsWidth = 0;

                for (int i = 2; i < 7; i++)
                {
                    mergedCellsWidth += worksheet.Column(i).Width;
                }

                // Список проверяемого оборудования
                var hardwares = checkResult.HardwareChecks.Where(e => e != null).OrderBy(h => h.Hardware.HardwareType).ToArray();
                var row = 13;

                // Список ПО
                var softwares = checkResult.SoftwareChecks.Where(e => e != null && !(e.Software is ScadaDto))?.Select(e => e.Software).ToArray();

                for (int i = 0; i < hardwares.Length; i++)
                {
                    var parameters = hardwares[i].CheckParameters.Except(hardwares[i].CheckParameters.Where(p => p.Result == false && p.Method == "-")).ToArray();

                    // Заголовок проверяемого оборудования
                    worksheet.Cells[$"B{row}:F{row}"].Merge = true;

                    worksheet.Cells[$"B{row}:F{row}"].Style.WrapText = true;
                    worksheet.Cells[$"A{row}:B{row}"].Style.Font.Bold = true;

                    worksheet.Cells[$"A{row}:B{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[$"A{row}:B{row}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[$"A{row}:B{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A{row}:B{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A{row}:B{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A{row}:B{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[$"B{row}"].Value = GetHardwareHeader(hardwares[i]);
                    worksheet.Cells[$"A{row}"].Value = $"{i + 1}";

                    var text = worksheet.Cells[$"B{row}"].Value.ToString();
                    var font = worksheet.Cells[$"B{row}"].Style.Font;

                    worksheet.Row(row).Height = MeasureTextHeight(text, font, mergedCellsWidth);

                    // Подзаголовок проверяемого оборудования
                    if (hardwares[i].Hardware.HardwareType == HardwareType.Network
                        || hardwares[i].Hardware.HardwareType == HardwareType.ARM)
                    {
                        row++;

                        worksheet.Cells[$"A{row - 1}:A{row}"].Merge = true;
                        worksheet.Cells[$"B{row}:F{row}"].Merge = true;

                        worksheet.Cells[$"B{row}:F{row}"].Style.WrapText = true;
                        worksheet.Cells[$"A{row}:B{row}"].Style.Font.Bold = true;

                        worksheet.Cells[$"A{row}:B{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[$"A{row}:B{row}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        worksheet.Cells[$"A{row}:B{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A{row}:B{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A{row}:B{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A{row}:B{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        worksheet.Cells[$"B{row}"].Value = GetHardwareSubHeader(hardwares[i], softwares);

                        text = worksheet.Cells[$"B{row}"].Value.ToString();
                        font = worksheet.Cells[$"B{row}"].Style.Font;

                        worksheet.Row(row).Height = MeasureTextHeight(text, font, mergedCellsWidth);
                    }

                    // Список параметров проверки
                    for (int j = 0; j < parameters.Length; j++)
                    {
                        row++;

                        worksheet.Cells[$"A{row}:E{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[$"B{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[$"A{row}:F{row}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        worksheet.Cells[$"A{row}:F{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A{row}:F{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A{row}:F{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A{row}:F{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        worksheet.Cells[$"A{row}"].Value = $"{i + 1}.{j + 1}";
                        worksheet.Cells[$"B{row}"].Value = parameters[j].Description;
                        worksheet.Cells[$"C{row}"].Value = parameters[j].Method;
                        worksheet.Cells[$"D{row}"].Value = parameters[j].Result ? "Соответствует" : "Не соответствует";
                        worksheet.Cells[$"E{row}"].Value = parameters[j].Date.ToShortDateString();
                        worksheet.Cells[$"F{row}"].Value = parameters[j].Comment;
                    }
                    row++;
                }

                // Список SCADA-систем
                var scadaList = checkResult.SoftwareChecks.Where(e => e != null && e.Software is ScadaDto).ToArray();

                for (int i = 0; i < scadaList.Length; i++)
                {
                    var parameters = scadaList[i].CheckParameters.ToArray();

                    worksheet.Cells[$"B{row}:F{row}"].Merge = true;

                    worksheet.Cells[$"B{row}:F{row}"].Style.WrapText = true;
                    worksheet.Cells[$"A{row}:B{row}"].Style.Font.Bold = true;

                    worksheet.Cells[$"A{row}:B{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[$"A{row}:B{row}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[$"A{row}:B{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A{row}:B{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A{row}:B{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A{row}:B{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[$"A{row}"].Value = $"{i + 1 + hardwares.Length}";
                    worksheet.Cells[$"B{row}"].Value = GetSoftwareHeader(scadaList[i]);

                    var text = worksheet.Cells[$"B{row}"].Value.ToString();
                    var font = worksheet.Cells[$"B{row}"].Style.Font;

                    worksheet.Row(row).Height = MeasureTextHeight(text, font, mergedCellsWidth);

                    for (int j = 0; j < parameters.Length; j++)
                    {
                        row++;

                        worksheet.Cells[$"A{row}:E{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[$"B{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[$"A{row}:F{row}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        worksheet.Cells[$"A{row}:F{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A{row}:F{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A{row}:F{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A{row}:F{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        worksheet.Cells[$"A{row}"].Value = $"{i + 1 + hardwares.Length}.{j + 1}";
                        worksheet.Cells[$"B{row}"].Value = parameters[j].Description;
                        worksheet.Cells[$"C{row}"].Value = parameters[j].Method;
                        worksheet.Cells[$"D{row}"].Value = parameters[j].Result ? "Соответствует" : "Не соответствует";
                        worksheet.Cells[$"E{row}"].Value = parameters[j].Date.ToShortDateString();
                        worksheet.Cells[$"F{row}"].Value = parameters[j].Comment;
                    }
                    row++;
                }

                row--;

                worksheet.Cells[$"F10:F{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                row += 2;

                // Заключение:
                worksheet.Cells[$"A{row}:F{row}"].Merge = true;

                worksheet.Cells[$"A{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[$"A{row}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[$"A{row}"].Style.Font.Bold = true;

                worksheet.Cells[$"A{row}"].Value = "ЗАКЛЮЧЕНИЕ:";

                row += 2;

                worksheet.Cells[$"A{row}:F{row + 2}"].Merge = true;

                worksheet.Cells[$"A{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[$"A{row}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[$"A{row}"].Style.Font.Bold = true;
                worksheet.Cells[$"A{row}"].Style.WrapText = true;

                worksheet.Cells[$"A{row}"].Value = "Вышеперечисленное оборудование прошло заводские испытания согласно ИнКС.366713.050-ПМ1 и готово для проведения ПНР на объекте эксплуатации.";

                // Подписи
                row += 4;
                for (int i = 0; i < 3; i++)
                {
                    worksheet.Cells[$"E{row}:F{row}"].Merge = true;

                    worksheet.Cells[$"E{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[$"C{row + 1}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[$"C{row + 1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    worksheet.Cells[$"C{row + 1}"].Style.Font.Size = 10;
                    worksheet.Cells[$"E{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"C{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[$"B{row}"].Value = "Должность";
                    worksheet.Cells[$"E{row}"].Value = "ФИО";
                    worksheet.Cells[$"C{row + 1}"].Value = "подпись";
                    row += 3;
                }

                // Настройки печати
                worksheet.PrinterSettings.PaperSize = ePaperSize.A4;
                worksheet.PrinterSettings.Orientation = eOrientation.Portrait;
                worksheet.PrinterSettings.TopMargin = 1M;
                worksheet.PrinterSettings.LeftMargin = 0.5M;
                worksheet.PrinterSettings.RightMargin = 0.5M;
                worksheet.PrinterSettings.FitToPage = true;

                return await excelPackage.GetAsByteArrayAsync();
            }
        }

        private string GetHardwareSubHeader(HardwareCheckDto check, params SoftwareDto[] softwares)
        {
            if (check.Hardware.HardwareType == HardwareType.Network)
            {
                var device = (NetworkHardwareDto)check.Hardware;
                var sb = new StringBuilder("Карта IP-адресов ЛВС:");

                if (!string.IsNullOrEmpty(device.Mask))
                {
                    sb.Append($" {device.Mask} - маска подсети;");
                }
                foreach (var item in device.NetworkDevices)
                {
                    sb.Append($" {item.IP} - {item.Name} (MAC-адрес {item.MacAddress});");
                }
                return sb.ToString();
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var item in softwares)
                {
                    sb.Append($"{item.Name} ver.{item.Version}; ");
                }

                return sb.ToString();
            }
        }
        private string GetSoftwareHeader(SoftwareCheckDto check)
        {
            return $"SCADA-система: {check.Software.Name} ver.{check.Software.Version}";
        }
        private string GetHardwareHeader(HardwareCheckDto check)
        {
            var device = check.Hardware;
            string header = "";
            string mbSettings = "";

            switch (device.HardwareType)
            {
                case HardwareType.Cabinet:
                    var cabinet = (CabinetDto)device;
                    header = cabinet.Position +
                        $"( зав.№{cabinet.SerialNumber}, " +
                        $"завершение монтажа {cabinet.Constructed?.ToShortDateString()}, " +
                        $"ответственный - {cabinet.ConstructedBy})";
                    break;

                case HardwareType.FlowComputer:
                    var flowcomputer = (FlowComputerDto)device;
                    header = $"{flowcomputer.HardwareType.GetDisplayName()} {flowcomputer.Position} " +
                        $"({flowcomputer.DeviceModel} " +
                        $"- №{flowcomputer.SerialNumber} " +
                        $"- IP={flowcomputer.IP} " +
                        $"- ASSEMBLY_VERSION={flowcomputer.AssemblyVersion} " +
                        $"- PRG_CRC32={flowcomputer.CRC32} " +
                        $"- LASTDATECFG={flowcomputer.LastConfigDate}):";
                    break;

                case HardwareType.Flowmeter:
                    var flowmeter = (FlowmeterDto)device;
                    var flowmeterMb = flowmeter.ModbusSettings;
                    mbSettings = flowmeter.SignalType == SignalType.RS485 ?
                        $"({flowmeterMb.Address}/{flowmeterMb.BoudRate}/Modbus RTU/{Enum.GetName(typeof(Parity), flowmeterMb.Parity)[0]}/{flowmeterMb.DataBits}/{flowmeterMb.StopBit})"
                        : "";

                    header = $"{flowmeter.HardwareType.GetDisplayName()} {flowmeter.Position}" +
                        $"(тип {flowmeter.DeviceType} " +
                        $"- серия {flowmeter.DeviceModel} " +
                        $"- зав.№{flowmeter.SerialNumber} " +
                        $"- диап. {flowmeter.MinValue}...{flowmeter.MaxValue} {flowmeter.EU} " +
                        $"- {flowmeter.Kfactor} имп/{flowmeter.EU} " +
                        $"- {flowmeter.SignalType.GetDisplayName()} " +
                        $"{mbSettings}):";
                    break;

                case HardwareType.Network:
                    var network = (NetworkHardwareDto)device;
                    header = $"{network.HardwareType.GetDisplayName()}: {network.Position}, " +
                        $"тип {network.DeviceType}, " +
                        $"модель {network.DeviceModel}, " +
                        $"зав.№{network.SerialNumber}:";
                    break;

                case HardwareType.PLC:
                    var plc = (PlcDto)device;
                    header = $"{plc.HardwareType.GetDisplayName()} {plc.Position} " +
                        $"({plc.DeviceModel} " +
                        $"- №{plc.SerialNumber} " +
                        $"- IP={plc.IP} " +
                        $"- ASSEMBLY={plc.AssemblyVersion}):";
                    break;

                case HardwareType.Pressure:
                case HardwareType.DiffPressure:
                case HardwareType.GasAnalyzer:
                case HardwareType.Temperature:
                    var measurement = (MeasurementDto)device;
                    header = $"{measurement.HardwareType.GetDisplayName()} {measurement.Position}" +
                        $"(тип {measurement.DeviceType} " +
                        $"- модель {measurement.DeviceModel} " +
                        $"- зав.№{measurement.SerialNumber} " +
                        $"- диап. {measurement.MinValue}...{measurement.MaxValue} {measurement.EU} " +
                        $"- {measurement.SignalType.GetDisplayName()} ):";
                    break;

                case HardwareType.InformPanel:
                    var panel = (InformPanelDto)device;
                    header = $"{panel.HardwareType.GetDisplayName()} {panel.Position}" +
                        $"(тип {panel.DeviceType} " +
                        $"- модель {panel.DeviceModel} " +
                        $"- зав.№{panel.SerialNumber} " +
                        $"- тип табло \"{panel.PanelType.GetDisplayName()}\" ):";
                    break;

                case HardwareType.APC:
                    var apc = (ApcDto)device;
                    header = $"{apc.HardwareType.GetDisplayName()} {apc.Position}" +
                        $"(тип {apc.DeviceType} " +
                        $"- модель {apc.DeviceModel} " +
                        $"- зав.№{apc.SerialNumber} ):";
                    break;

                case HardwareType.FireSensor:
                    var fireSensor = (FireSensorDto)device;
                    header = $"{fireSensor.HardwareType.GetDisplayName()} {fireSensor.Position}" +
                        $"(тип {fireSensor.DeviceType} " +
                        $"- модель {fireSensor.DeviceModel} " +
                        $"- зав.№{fireSensor.SerialNumber} ):";
                    break;

                case HardwareType.FireModule:
                    var fireModule = (FireModuleDto)device;
                    header = $"{fireModule.HardwareType.GetDisplayName()} {fireModule.Position}" +
                        $"(тип {fireModule.DeviceType} " +
                        $"- зав.№{fireModule.SerialNumber} ):";
                    break;

                case HardwareType.Valve:
                    var valve = (ValveDto)device;
                    var valveMb = valve.ModbusSettings;
                    mbSettings = valve.SignalType == SignalType.RS485 ?
                        $"({valveMb.Address}/{valveMb.BoudRate}/Modbus RTU/{Enum.GetName(typeof(Parity), valveMb.Parity)[0]}/{valveMb.DataBits}/{valveMb.StopBit})"
                        : "";

                    header = $"{valve.HardwareType.GetDisplayName()} {valve.Position} " +
                        $"(тип {valve.DeviceType}, " +
                        $"модель {valve.DeviceModel}, " +
                        $"зав.№{valve.SerialNumber}, " +
                        $"{valve.SignalType.GetDisplayName()} " +
                        $"{mbSettings}):";
                    break;
                case HardwareType.ARM:
                    var arm = (ArmDto)device;
                    var raid = arm.HasRAID ? "есть" : "отсутствует";

                    header = $"{arm.HardwareType.GetDisplayName()}: " +
                        $"{arm.Name} зав.№{arm.SerialNumber}, " +
                        $"монитор {arm.Monitor} зав.№{arm.MonitorSN}, " +
                        (!string.IsNullOrWhiteSpace(arm.Monitor2) ? $"монитор {arm.Monitor2} зав.№{arm.Monitor2SN}, " : "") +
                        $"клавиатура {arm.Keyboard} зав.№{arm.KeyboardSN}, " +
                        $"мышь {arm.Mouse} зав.№{arm.MouseSN}; " +
                        $"RAID-массив  - {raid}; " +
                        $"{arm.OS} ({arm.ProductKeyOS});";

                    foreach (var item in arm.NetworkAdapters)
                    {
                        header += $" {item.IP} - MAC-адрес {item.MacAddress},";
                    }
                    header = header.Replace(header.Last(), ';');
                    break;
            };

            return header;
        }
        private double MeasureTextHeight(string text, ExcelFont font, double width)
        {
            if (string.IsNullOrEmpty(text)) return 0.0;

            var bitmap = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(bitmap);

            var pixelWidth = Convert.ToInt32(width * 7);  //7 pixels per excel column width
            var fontSize = font.Size * 1.01f;
            var drawingFont = new Font(font.Name, fontSize);
            var size = graphics.MeasureString(text, drawingFont, pixelWidth, new StringFormat { FormatFlags = StringFormatFlags.MeasureTrailingSpaces });

            //72 DPI and 96 points per inch.  Excel height in points with max of 409 per Excel requirements.
            return Math.Min(Convert.ToDouble(size.Height) * 72 / 96, 409);
        }
    }
}
