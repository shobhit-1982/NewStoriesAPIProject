using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using StoriesAPI.Builders;
using StoriesAPI.Caching.Abstraction;
using StoriesAPI.Caching.Models;
using StoriesAPI.Models;
using StoriesAPI.Services.StoriesServices.Service;
using StoriesAPI.Services.ThirdPartyAPIService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Stories.Service
{
  class StoriesServiceFixture : ITestFixtureBuilder
  {
    
    /// <summary>
    /// IMapper
    /// </summary>
    IMapper _mapper;
    HttpClient _httpClient;
    IResponseBuilderFactory _responseBuilderFactory;
    IMemoryCache _cache;
    ICachingService _cachingService;
    IThirdPartyAPI _thirdPartyAPI;



    public StoriesServiceFixture()
    {
      _mapper = Substitute.For<IMapper>();
      _responseBuilderFactory = Substitute.For<IResponseBuilderFactory>();
      _httpClient = Substitute.For<HttpClient>();
      _cache = Substitute.For<IMemoryCache>();
      _cachingService = Substitute.For<ICachingService>();
      _thirdPartyAPI = Substitute.For<IThirdPartyAPI>();
            _thirdPartyAPI.GetAllStoriesIds().Returns(new List<int>() { 39773074 });

            _thirdPartyAPI.GetStorieDetail(Arg.Any<int>()).Returns(new StoryDetailDto() { id = 39773074, title = "Fujitsu spilled private client data,passwords into the open unnoticed for a year", type = "story", url = "https://www.thestack.technology/fujitsu-breach-cloud-buckets/" });

            _responseBuilderFactory.GetBuilder<IEnumerable<StoryDetailDto>>().Returns(new ResponseBuilder<IEnumerable<StoryDetailDto>>());

            _cachingService.RetrieveFromCacheAsync(Arg.Any<string>()).Returns(new CacheResultModel("AllStoriesDataKey") { CacheStatus = CacheResultModel.CacheStatusOption.Exists, CacheValue = new List<StoryDetailDto>() { new StoryDetailDto() { id = 10, title = "The unique algorithm behind rewind in Braid", type = "story", url = "https://twitter.com/jonathan_blow/status/1770277363848552734" } } });

       
           
            _mapper.Map<IEnumerable<StoryDetailDto>>(Arg.Any<IEnumerable<StoryDetailDto>>()).Returns(new List<StoryDetailDto>() { new StoryDetailDto() { id = 10, title = "The unique algorithm behind rewind in Braid", type = "story", url = "https://twitter.com/jonathan_blow/status/1770277363848552734" } });
         
           

        }

    /// <summary>
    /// With mapper
    /// </summary>
    /// <param name="mapper"></param>
    /// <returns></returns>
    public StoriesServiceFixture WithMapper(IMapper mapper) => this.With(ref _mapper, mapper);

    /// <summary>
    /// with IResponseBuilderFactory
    /// </summary>
    /// <param name="responseBuilderFactory"></param>
    /// <returns></returns>
    public StoriesServiceFixture WithResponseBuilderFactory(IResponseBuilderFactory responseBuilderFactory) =>
        this.With(ref _responseBuilderFactory, responseBuilderFactory);

    /// <summary>
    /// Performs an implicit conversion from <see cref="InventoryActionServiceFixture" /> to <see cref="InventoryActionService" />.
    /// </summary>
    /// <param name="fixture">The fixture.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator StoriesService(StoriesServiceFixture fixture) => fixture.Build();


     /// <summary>
     /// Withes the caching service.
     /// </summary>
     /// <param name="cachingService">The caching service.</param>
     /// <returns></returns>
    public StoriesServiceFixture WithCachingService(ICachingService cachingService) => this.With(ref _cachingService, cachingService);

     public StoriesServiceFixture WithThirdPartyAPIService(IThirdPartyAPI thirdPartyAPI) => this.With(ref _thirdPartyAPI, thirdPartyAPI);

        

        /// <summary>
        /// Build the instance
        /// </summary>
        /// <returns></returns>
        private StoriesService Build() => new StoriesService(_responseBuilderFactory, _httpClient, _mapper, _cache, _cachingService, _thirdPartyAPI);
  }
}
