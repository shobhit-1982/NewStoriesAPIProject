using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoriesAPI.Models;

namespace StoriesAPI.Services.StoriesServices.Interface
{
  public interface IStoriesService
  {

        /// <summary>
        /// Get All Stories service
        /// </summary>
        /// <param name="pagingParameters">see cref="PagingParameters"</param>
        /// <returns> StoriesDetailsDto </returns>
        Task<APIResponse<StoriesDetailsDto>> GetTopAllStories(PagingParameters pagingParameters);
        /// <summary>
        /// Get stories list that are mached by search title
        /// </summary>
        /// <param name="searchtext">searchStoryTitle</param>
        /// <returns></returns>
        Task<APIResponse<StoriesDetailsDto>> GetSearchStories(String searchtext);
  }
}
