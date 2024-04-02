using Microsoft.AspNetCore.Mvc;
using StoriesAPI.Services.StoriesServices.Interface;
using System.Threading.Tasks;
using StoriesAPI.Base;
using StoriesAPI.Models;

namespace StoriesAPI.Controller
{
  [Route("api/Stories")]
  [ApiController]
  public class StoriesController : BaseController
  {
    /// <summary>
    /// story service
    /// </summary>
    private readonly IStoriesService _storiesService;

        /// <summary>
        ///Initializes a new instance of the <see cref="StoriesController"/> class.
        /// </summary>
        /// <param name="storiesService"></param>
        public StoriesController(IStoriesService storiesService)
    {
      _storiesService = storiesService;
    }



        /// <summary>
        /// Get all top stories
        /// </summary>
        /// <param name="pagingParameters">see cref="PagingParameters"</param>
        /// <returns></returns>
        [HttpGet]
    [Route("GetAllStories")]
    public async Task<IActionResult> GetTopAllStories([FromQuery] PagingParameters pagingParameters)
    {
      return StoriesOk(await _storiesService.GetTopAllStories(pagingParameters));
    }
    
    /// <summary>
    /// Get all searched stories 
    /// </summary>
    /// <param name="searchValue"> search value</param>
    /// <returns></returns>
    [HttpGet]
    [Route("SearchStories")]
    public async Task<IActionResult> GetSearchStories([FromQuery] string searchValue)
    {
      return StoriesOk(await _storiesService.GetSearchStories(searchValue));
    }

    
  }
}
