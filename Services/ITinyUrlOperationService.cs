using System.Data;
using TinyUrl_Api.Models;
using TinyUrl_Api.ViewModels;

namespace TinyUrl_Api.Repositories
{
    public interface ITinyUrlOperationService
    {
        Task<DataTable> InsertDetails(InsertViewModel model);
        Task<DataTable> DeleteDetails(string code);
        Task<DataTable> UpdateDetails(string code);
        Task<List<TinyUrlModel>> SelectDetails(string code);
    }
}
