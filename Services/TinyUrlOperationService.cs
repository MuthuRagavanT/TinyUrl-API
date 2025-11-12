using Microsoft.Data.SqlClient;
using System;
using System.Data;
using TinyUrl_Api.Db_Handler;
using TinyUrl_Api.Models;
using TinyUrl_Api.ViewModels;

namespace TinyUrl_Api.Repositories
{
    public class TinyUrlOperationService : ITinyUrlOperationService
    {
         private readonly DbHelper _dbHelper;
        public TinyUrlOperationService(DbHelper dbHelper) 
        {
            _dbHelper = dbHelper;
        }

        private static string GenerateShortCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        async Task<DataTable> ITinyUrlOperationService.InsertDetails(InsertViewModel viewModel)
        {
            TinyUrlModel model = new TinyUrlModel();
            model.Code = GenerateShortCode();
            model.ShortUrl = "https://tiny-rul-api.azurewebsites.net/" + model.Code;
            model.LongUrl = viewModel.originalUrl;
            model.IsPrivate = viewModel.IsPrivate;
            DataTable dataTable;
            var parameters = new SqlParameter[]
            {
                new("@LongUrl", model.LongUrl),
                new("@ShortUrl", model.ShortUrl),
                new("@IsPrivate", model.IsPrivate),
                new("@ShortCode", model.Code)
            };
            dataTable = await _dbHelper.ExcuteQueryTable("sp_InsertDetails", parameters);
            return dataTable;
        }

        async Task<DataTable> ITinyUrlOperationService.DeleteDetails(string code)
        {
            DataTable dataTable;
            var parameters = new SqlParameter[]
            {
                new("@ShortCode", code)
            };
            dataTable = await _dbHelper.ExcuteQueryTable("sp_DeleteTableRecords", parameters);
            return dataTable;
        }

        async Task<DataTable> ITinyUrlOperationService.UpdateDetails(string code)
        {
            TinyUrlModel model = new TinyUrlModel();
            DataTable dataTable;
            var parameters = new SqlParameter[]
            {
                new("@ShortCode", code)
            };
            dataTable = await _dbHelper.ExcuteQueryTable("sp_InsertDetails", parameters);
            return dataTable;
        }

        async Task<List<TinyUrlModel>> ITinyUrlOperationService.SelectDetails(string code)
        {
            DataTable dataTable;
            var parameters = new SqlParameter[]
            {
                new("@ShortCode", code)
            };
            dataTable = await _dbHelper.ExcuteQueryTable("sp_GetTinyUrlDetails", parameters);
            List<TinyUrlModel> modelList = new List<TinyUrlModel>();
            if (dataTable!=null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    modelList.Add(new TinyUrlModel
                    {
                        Code = Convert.ToString(row["code"])!,
                        ShortUrl = Convert.ToString(row["shortURL"])!,
                        LongUrl = Convert.ToString(row["originalURL"])!,
                        ClickCount = Convert.ToInt32(row["totalClicks"]),
                        IsPrivate = Convert.ToBoolean(row["isPrivate"])
                    });
                }

            }
            return modelList;
        }
    }
}
