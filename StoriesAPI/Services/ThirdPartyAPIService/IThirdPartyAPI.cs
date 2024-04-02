using StoriesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoriesAPI.Services.ThirdPartyAPIService
{
    public interface IThirdPartyAPI
    {
        /// <summary>
        /// Get all stories ides
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<int>> GetAllStoriesIds();
        /// <summary>
        /// get story detail
        /// </summary>
        /// <param name="StoryId">StoryId</param>
        /// <returns></returns>
        Task<StoryDetailDto> GetStorieDetail(int StoryId);
    }
}
