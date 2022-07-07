using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;

namespace ValantDemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MazeController : ControllerBase
    {
        private readonly ILogger<MazeController> _logger;
        private string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Mazes\");

        public MazeController(ILogger<MazeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetNextAvailableMoves")]
        public IEnumerable<string> GetNextAvailableMoves()
        {
          return new List<string> {"Up", "Down", "Left", "Right"};
        }

        [HttpGet]
        [Route("GetAllMazes")]
        public IEnumerable<string> GetAllMazes()
        {
            var mazes = Directory.GetFiles(filePath);
            List<string> mazeContents = new List<string>();
            foreach(var file in mazes)
            {
                mazeContents.Add(System.IO.File.ReadAllText(file));
            }

            return mazeContents; 

        }

        [HttpGet]
        [Route("GetMazeByName")]
        public string GetMazeByName(string mazeName)
        {
            var mazes = Directory.GetFiles(filePath);
            string mazeContent = string.Empty;
            foreach(var file in mazes)
            {
                if (file == mazeName)
                {
                    string fullFile = Path.Combine(filePath, mazeName);
                    mazeContent = System.IO.File.ReadAllText(fullFile);
                }
            }

            return mazeContent;            
        }
    }
}
