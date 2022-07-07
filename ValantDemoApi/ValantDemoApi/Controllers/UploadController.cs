using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ValantDemoApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UploadController : ControllerBase
  {
    private string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Mazes\");

    [HttpPost, DisableRequestSizeLimit]
    public ActionResult UploadFile()
    {
      try
      {
        var file = Request.Form.Files[0];
        if (!Directory.Exists(filePath))
        {
          Directory.CreateDirectory(filePath);
        }

        if (file.Length > 0)
        {
          string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
          string fullPath = Path.Combine(filePath, fileName);
          using (var stream = new FileStream(fullPath, FileMode.Create))
          {
            file.CopyTo(stream);
          }
        }
        return Ok("Upload Successful.");
      }
      catch (Exception ex)
      {

      }

      return BadRequest();
    }
  }
}
