using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductCalatog.Controllers
{
    //routing it to piccontroller alternatively it can be written as "api/Pic"
    [Route("api/[controller]")]         //whatever is inside the square brackets will be replaced by name of controller
    [ApiController]                     //indicates i am an Api not a normal controller

    public class PicController : ControllerBase

    {
        //its route will be api/pic/getimage/1  ie value of [controller] is pic and value of {id } 
        //will be some no and whatever no will be there will be stored in id variable
        //whenver user enter api/pic/1 he will be automatically directed to GetImage function because it is the only function which has  int id part

            private readonly IWebHostEnvironment _env;    // declared it globally so that it can used by all functions

        //to know where is wwwroot folder we will insert dependency Injection in contructor of this class 
        public PicController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpGet("{id}")]                            //marked rendering image as Get request because it will not have any body from user
        public IActionResult GetImage(int id)
        {
            var webRoot = _env.WebRootPath;           //get the path of wwwrootfolder
           var path= Path.Combine($"{webRoot}/Pics", $"Ring{id}.jpg");  //ie {wwwrootpath}/Pics/Ring{id}
            var buffer = System.IO.File.ReadAllBytes(path);         // to read image in byte from selected path
            return File(buffer, "image/jpeg");    // cant return directly buffer because client wont understand byte

        }
    }
}