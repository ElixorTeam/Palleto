﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDeviceControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            Console.WriteLine("UploadController");
            _environment = environment;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> MultipleAsync(IFormFile[] files, string currentDirectory)
        {
            Console.WriteLine("MultipleAsync");
            try
            {
                if (HttpContext.Request.Form.Files.Any())
                {
                    foreach (var file in HttpContext.Request.Form.Files)
                    {
                        // reconstruct the path to ensure everything 
                        // goes to uploads directory
                        string requestedPath = currentDirectory.ToLower()
                            .Replace(_environment.WebRootPath.ToLower(), "");
                        if (requestedPath.Contains("\\uploads\\"))
                        {
                            requestedPath = requestedPath.Replace("\\uploads\\", "");
                        }
                        else
                        {
                            requestedPath = "";
                        }
                        string path = Path.Combine(_environment.WebRootPath, "uploads", requestedPath, file.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream).ConfigureAwait(true);
                        }
                    }
                }
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
