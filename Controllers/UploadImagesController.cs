using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Contants;
using LienPhatERP.Helper;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace LienPhatERP.Controllers
{
    public class UploadImagesController : Controller
    {
        private readonly IFileProvider _fileProvider;
        private readonly IDistributedCache _cache;
        public UploadImagesController(



          
            IHostingEnvironment env, IDistributedCache cache

            /*IHubContext<ChatHub> hubContext*/)
        {

        
            _fileProvider = env.WebRootFileProvider;
            _cache = cache;
        }
        public IActionResult InsertImage()
        {
            return View();
        }
        [HttpGet("ReviewImage")]
        public IActionResult ReviewImage(string urls)
        {
            var listUrl = JsonHelper.DeserializeObject<List<UploadFileApiResult>>(urls);
            return View(listUrl);
        }
        [HttpGet("/image/{width}/{height}/{*url}")]
        public async Task<IActionResult> ResizeImageAsync(string url, int width, int height)
        {

            if (width < 0 || height < 0) { return BadRequest(); }

            var key = $"/{width}/{height}/{url}";
            var data = await _cache.GetAsync(key);

            if (data == null)
            {
                var imagePath = PathString.FromUriComponent("/" + url);

                var fileInfo = _fileProvider.GetFileInfo(imagePath);
                if (!fileInfo.Exists)
                {

                    return NotFound();
                }

                var outputStream = new MemoryStream();
                using (var inputStream = fileInfo.CreateReadStream())
                using (var image = Image.Load(inputStream))
                {
                    using (Image<Rgba32> destRound = image.Clone(x => x.ConvertToAvatar(new Size(width, height), 1)))
                    {

                        destRound.SaveAsJpeg(outputStream);

                    }
                    data = outputStream.ToArray();
                }

                await _cache.SetAsync(key, data);
            }


            return File(data, "image/jpg");
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(ICollection<IFormFile> file)
        {

            try
            {
                var listUpload = await UploadFiles(file);

                
                var urls = JsonHelper.SerializeObject(listUpload);


                return RedirectToAction("ReviewImage", new { urls });
            }
            catch 
            {

               
                string message = $"file / upload failed!";
                return Json(message);
            }
        }
        public static async Task<List<UploadFileApiResult>> UploadFiles(ICollection<IFormFile> files,
            string f = "UploadFiles")
        {
            string folder = $"{f}/{DateTime.Now:yyyy}/{DateTime.Now:MM}/{DateTime.Now:dd}/";
            // full path to file in temp location
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",
                folder);

            bool folderExists = Directory.Exists(filePath);
            if (!folderExists)
                Directory.CreateDirectory(filePath);
            var listUpload = new List<UploadFileApiResult>();
            var url = "";
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = formFile.FileName.Replace(" ", "");
                    var id = Guid.NewGuid();
                    using (var stream = new FileStream(filePath + $"{id}_{fileName}", FileMode.Create))
                    {

                        await formFile.CopyToAsync(stream);
                        url = folder + $"{id}_{fileName}";
                        var upload = new UploadFileApiResult() { Url = url,FileName = fileName};
                        listUpload.Add(upload);
                    }
                }
            }

            return listUpload;
        }
    }
}