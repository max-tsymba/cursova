using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Mounight.Models;
using SimpleImageGallery.Data;
using SimpleImageGallery.Services;

namespace Mounight.Controllers
{
    public class ImageController : Controller
    {
        private IConfiguration _config;
        private IImage _imageService;
        private string AzureConnectionString { get; }

        public ImageController(IConfiguration config, IImage imageService)
        {
            _config = config;
            _imageService = imageService;
            AzureConnectionString = _config["AzureStorageConnectionString"];
        }
        public IActionResult Upload()
        {
            var model = new UploadImageModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadNewImage(IFormFile file, string tags, string title) 
        {
            var container = _imageService.GetBlobContainer(AzureConnectionString, "images");

            var content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var filename = content.FileName.Trim('"');

            var blockBlob = container.GetBlockBlobReference(filename);
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            await _imageService.SetImage(title, tags, blockBlob.Uri);

            return RedirectToAction("Index", "Gallery");
        }
    }
}
