using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ExplorerTree.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ExplorerController : Controller
    {
        private readonly string rootPath;

        public ExplorerController(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            rootPath = config.GetValue<string>("root");
        }

        [HttpGet]
        public TreeNode<SystemComponent> Graph([FromQuery]string root, [FromQuery]int width = 5, [FromQuery]int depth = 5)
        {
            return Explorer.GetGraph(root, width, depth);
        }

        [HttpGet]
        public TreeNode<string> Tree()
        {
            return Explorer.GetTree(rootPath);
        }
    }
}
