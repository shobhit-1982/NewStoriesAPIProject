using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StoriesAPI.Builders;
using StoriesAPI.Caching.Abstraction;
using StoriesAPI.Common.Enum;
using StoriesAPI.Models;
using StoriesAPI.Services.StoriesServices.Interface;
using StoriesAPI.Services.ThirdPartyAPIService;
using static StoriesAPI.Caching.Models.CacheResultModel;

namespace StoriesAPI.Services.StoriesServices.Service
{
    /// <summary>
    /// add class contain get stories related data functions
    /// </summary>
    public class StoriesService : IStoriesService
    {
        /// <summary>
        /// The http client 
        /// </summary>
        private readonly HttpClient _httpClient;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// The responseBuilderFactory
        /// </summary>
        private readonly IResponseBuilderFactory _responseBuilderFactory;
        /// <summary>
        /// The caching service
        /// </summary>
        private readonly ICachingService _cachingService;
        /// <summary>
        /// The third party api
        /// </summary>
        private readonly IThirdPartyAPI _thirdPartyAPI;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoriesService"/> class.
        /// </summary>
        /// <param name="responseBuilderFactory">responseBuilderFactory</param>
        /// <param name="httpclient">httpclient</param>
        /// <param name="mapper">mapper</param>
        /// <param name="cachingService">cachingService</param>
        /// <param name="thirdPartyAPI">thirdPartyAPI</param>
        public StoriesService(IResponseBuilderFactory responseBuilderFactory, HttpClient httpclient, IMapper mapper, ICachingService cachingService, IThirdPartyAPI thirdPartyAPI)
        {
          _httpClient = httpclient;
          _responseBuilderFactory = responseBuilderFactory;
          _mapper = mapper;
          _cachingService = cachingService;
          _thirdPartyAPI = thirdPartyAPI;
        }

        /// <summary>
        /// Get stories list that are mached by search title
        /// </summary>
        /// <param name="searchStoryTitle">searchStoryTitle</param>
        /// <returns></returns>
        public async Task<APIResponse<StoriesDetailsDto>> GetSearchStories(string searchStoryTitle)
        {
          StoriesDetailsDto storiesDetailsDto = new StoriesDetailsDto();
          var responseBuilder = _responseBuilderFactory.GetBuilder<StoriesDetailsDto>();
          var data = await GetDataForStoriesDetails();
          if (!string.IsNullOrEmpty(searchStoryTitle))
          {
                    storiesDetailsDto.Stories = data.Where(e => e.title.ToLower().Contains(searchStoryTitle.ToLower()));
                    storiesDetailsDto.TotalRecords = storiesDetailsDto.Stories.Count();
          }
                //storiesDetailsDto.TotalRecords = storiesDetailsDto.Stories.Count();
          return responseBuilder.AddSuccessData(storiesDetailsDto).Build();
        }

        /// <summary>
        /// Get All Stories service
        /// </summary>
        /// <param name="pagingParameters">see cref="PagingParameters"</param>
        /// <returns> StoriesDetailsDto </returns>
        public async Task<APIResponse<StoriesDetailsDto>> GetTopAllStories(PagingParameters pagingParameters)
        {
            var responseBuilder = _responseBuilderFactory.GetBuilder<StoriesDetailsDto>();
            StoriesDetailsDto storiesDetailsDto = new StoriesDetailsDto();
            Stopwatch stopw = new Stopwatch();
            stopw.Start();
            var data = await GetDataForStoriesDetails();
            storiesDetailsDto.Stories = data.OrderByDescending(on => on.id).Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).Take(pagingParameters.PageSize).ToList();
            storiesDetailsDto.TotalRecords = data.Count();
            stopw.Stop();
            var x = stopw.ElapsedMilliseconds;
            return responseBuilder.AddSuccessData(storiesDetailsDto).Build();
        }


        /// <summary>
        /// get data from thired party API and cached data
        /// </summary>
        /// <returns>List of StoryDetailDto</returns>
        private async Task<IEnumerable<Models.StoryDetailDto>> GetDataForStoriesDetails()
        {
          List<StoryDetailDto> storiesDetailstList = new List<StoryDetailDto>();
          IEnumerable<StoryDetailDto> enumerableCollection = null;
          // Check caching
          var cacheRecords = await _cachingService.RetrieveFromCacheAsync("AllStoriesDataKey");
          if (cacheRecords.CacheStatus == CacheStatusOption.DoesNotExists)
          {
              // Call First APi
              var storiesIDes = (await _thirdPartyAPI.GetAllStoriesIds()).Take(220);
                    var tasks = storiesIDes.Select(async storyId =>
                    {
                        storiesDetailstList.Add(await _thirdPartyAPI.GetStorieDetail(storyId));
                    });
                    await Task.WhenAll(tasks);
                    enumerableCollection = storiesDetailstList.Where(x=>x.type == "story" && x.url != null && x.url != "").Take(200).OrderByDescending(x => x.id);
                    await _cachingService.SaveToCacheAsync("AllStoriesDataKey", enumerableCollection);
            return enumerableCollection;
           }
            enumerableCollection = _mapper.Map<IEnumerable<StoryDetailDto>>(cacheRecords.CacheValue);
            return enumerableCollection;
        }

    }
}
