    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public partial class UploadController : Controller
    {

        private readonly DictionaryService _dictionaryService;

        public List<string> uploadedImagesPaths = new List<string>();

        public static  List<string> uploadedImagesPaths2 = new List<string>();

        public IHostingEnvironment HostingEnvironment { get; set; }

        public UploadController(DictionaryService dictionaryService, IHostingEnvironment hostingEnvironment)
        {
            _dictionaryService = dictionaryService;
            HostingEnvironment = hostingEnvironment;
        }

        
        public ActionResult Async()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Async_Save(IEnumerable<IFormFile> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {

                uploadedImagesPaths = new List<string>();

                foreach (var file in files)
                {
                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                    var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));

                    //var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);

                    var fileId = Guid.NewGuid().ToString();
                    var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, "images", fileId + Path.GetExtension(file.FileName));

                    using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    await file.CopyToAsync(fileStream);

                    //uploadedImagesPaths.Add(((fileId.ToString()) + Path.GetExtension(file.FileName)).ToString());
                    uploadedImagesPaths.Add((fileId + Path.GetExtension(file.FileName)).ToString());
                    uploadedImagesPaths2.Add((fileId + Path.GetExtension(file.FileName)).ToString());

                    foreach (string path in uploadedImagesPaths)
                    {

                        LandmarkImage img = new LandmarkImage
                        {

                            ImageURL = path

                        };

                        _dictionaryService.SaveUploadedImagesTemporaryDB(img);

                        //_dictionaryService.SaveUploadedImagesDB(img);
                    }


                }

            }

            return Content("");
        }


        [HttpPost]
        public void SaveUploadedImagesDB()
        {

            //int id = 3;
            //string path = "un path";
            foreach (string path in uploadedImagesPaths2)
            {
                LandmarkImage img = new LandmarkImage
                {

                    ImageURL = path

                };

                _dictionaryService.SaveUploadedImagesDB(img);
            }

        }

        public ActionResult Async_Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, "images", fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }




    }



        //public void SaveImageDB(int id, string imgpath)
        //{

        //    foreach (var path in imgpath)
        //    {
        //       _dictionaryService.SaveUploadImageDB(id, path.ToString());
        //    }

        //}


    }