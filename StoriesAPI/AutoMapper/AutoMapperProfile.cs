using AutoMapper;
using StoriesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoriesAPI.AutoMapper
{

  /// <summary>
  /// auto Mapper
  /// </summary>
  public class AutoMapperProfile : Profile
  {
    /// <summary>
    /// Register my storyDetail and dto class for mapper
    /// </summary>
    public AutoMapperProfile()
    {
      CreateMap<StoryDetail, StoryDetailDto>().ReverseMap();
    }
  }
}
