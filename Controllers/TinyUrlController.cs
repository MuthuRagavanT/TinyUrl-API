using Azure.Messaging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using TinyUrl_Api.Models;
using TinyUrl_Api.Repositories;
using TinyUrl_Api.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TinyUrl_Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TinyUrlController : ControllerBase
    {
        ITinyUrlOperationService _tinyUrlService;
        public TinyUrlController(ITinyUrlOperationService tinyUrlService) 
        { 
            _tinyUrlService = tinyUrlService;
        }

        [HttpPost,Route("add")]
        public async Task<IActionResult> InsertData(InsertViewModel model)
        {
            if (string.IsNullOrEmpty(model.originalUrl))
            {
                return BadRequest();
            }
            else
            {
                if (!(Uri.TryCreate(model.originalUrl.Trim(), UriKind.Absolute, out Uri? uriResult)&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
                {
                    return BadRequest();
                }
            }

            DataTable dataTable = new DataTable();
            try
            {
                dataTable = await _tinyUrlService.InsertDetails(model);
            }
            catch(Exception ex) 
            {
                throw;
            }
            return Ok(new { shortUrl = $"{dataTable.Rows[0]["ShortUrl"].ToString()}" });
        }

        [HttpDelete, Route("delete/{code}")]
        public async Task<IActionResult> DeleteRecord(string code)
        {

            if (string.IsNullOrEmpty(code))
                return NotFound();

            DataTable dataTable = new DataTable();
            try
            {
                dataTable = await _tinyUrlService.DeleteDetails(code);
            }
            catch (Exception ex)
            {
                throw;
            }
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut, Route("update/{code}")]
        public async Task<IActionResult> UpdateDetails(string code)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = await _tinyUrlService.UpdateDetails(code);
            }
            catch (Exception ex)
            {
                throw;
            }
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet, Route("/{code}")]
        public async Task<IActionResult> FetchCodeRecords(string code)
        {

            List<TinyUrlModel> model = new List<TinyUrlModel>();
            try
            {
                model = await _tinyUrlService.SelectDetails(code);
            }
            catch (Exception ex)
            {
                throw;
            }
            if(model.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Redirect(model[0].LongUrl);
            }
        }

        [HttpGet, Route("public")]
        public async Task<List<TinyUrlModel>> FetchAllRecords()
        {
            List<TinyUrlModel> model = new List<TinyUrlModel>();
            try
            {
                model = await _tinyUrlService.SelectDetails(null);
            }
            catch (Exception ex)
            {
                throw;
            }
            return model;
        }
    }
}
