using CheckerApp.Application.Checks.Queries.GetCheckResultFile;
using System.Threading.Tasks;

namespace CheckerApp.Application.Interfaces
{
    public interface IFileService
    {
        Task<byte[]> BuildExcelFileAsync(CheckResultDto checkResult);
    }
}
