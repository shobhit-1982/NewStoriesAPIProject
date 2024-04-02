using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using StoriesAPI.Controller;
using StoriesAPI.Models;
using StoriesAPI.Services.StoriesServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Stories.Controller;
using Xunit;

namespace Tests.Stories
{
  public class StoriesControllerTest
  {
    public class TheGetStoriesList
    {
     /// <summary>
     /// Shoulds the return list of Stories if request parameters is provided.
     /// </summary>
     /// <returns></returns>
      [Fact]
      public async Task Should_Return_List_Of_Stories_If_Request_Params_Provided()
      {
          //Given
       var storiesService = Substitute.For<IStoriesService>();
                storiesService.GetTopAllStories(Arg.Any<PagingParameters>()).Returns(new APIResponse<StoriesDetailsDto>(){ Data= new StoriesDetailsDto() { TotalRecords = 1 } });

                StoriesController sut = new StoriesControllerFixture().WithStoriesService(storiesService);

        //When
        var okResult = await sut.GetTopAllStories(new PagingParameters() { PageNumber =1 ,PageSize = 1});
        var result = ((APIResponse<StoriesDetailsDto>)(okResult as OkObjectResult).Value);
        //Then
               
          result.Should().BeOfType<APIResponse<StoriesDetailsDto>>();

            }


      /// <summary>
      /// Shoulds the return list of Stories if request parameter is not provided set default value (PageNumber/PazeSize).
      /// </summary>
      /// <returns></returns>
      [Fact]
      public async Task Should_Return_List_Of_Stories_If_Request_Params_isnull()
      {
                //Given
                var storiesService = Substitute.For<IStoriesService>();
                storiesService.GetTopAllStories(Arg.Any<PagingParameters>()).Returns(new APIResponse<StoriesDetailsDto>() { Data = new StoriesDetailsDto() { TotalRecords = 1 } });

                StoriesController sut = new StoriesControllerFixture().WithStoriesService(storiesService);
                //When
                var okResult = await sut.GetTopAllStories(null);
        var result = ((APIResponse<StoriesDetailsDto>)(okResult as OkObjectResult).Value);
        //Then

        result.Should().BeOfType<APIResponse<StoriesDetailsDto>>();

      }
    }

    public class TheGetSearchStories
    {

      /// <summary>
      /// Shoulds the return null of Stories if search value not found.
      /// </summary>
      /// <param name="searchValue"></param>
      /// <returns></returns>
      [Theory]
      [InlineData("123dfv")]
      [InlineData("df56")]
      public async Task Should_Return_SearchStories_Info_Not_Found(string searchValue)
      {
        //Given
          var storiesService = Substitute.For<IStoriesService>();
          storiesService.GetSearchStories(Arg.Any<string>()).Returns(new APIResponse<StoriesDetailsDto>() { Data = null});
          StoriesController sut = new StoriesControllerFixture().WithStoriesService(storiesService);

        //When
        var okResult = await sut.GetSearchStories(searchValue);
        var result = ((APIResponse<StoriesDetailsDto>)(okResult as OkObjectResult).Value).Data;
        //Then
        result.Should().BeNull();
      }

      /// <summary>
      /// Shoulds the return List of Stories if search value founded.
      /// </summary>
      /// <param name="searchValue"></param>
      /// <returns></returns>
            [Theory]
      [InlineData("abc")]
      [InlineData("abcd")]
      public async Task Should_Return_SearchStories_Info(string searchValue)
      {
        //Given
        var storiesService = Substitute.For<IStoriesService>();
        storiesService.GetSearchStories(Arg.Any<string>()).Returns(new APIResponse<StoriesDetailsDto>() { Data = new StoriesDetailsDto() { TotalRecords = 1 } });
                StoriesController sut = new StoriesControllerFixture().WithStoriesService(storiesService);

        //When
        var okResult = await sut.GetSearchStories(searchValue);
        var result = ((APIResponse<StoriesDetailsDto>)(okResult as OkObjectResult).Value);
        //Then
        result.Should().BeOfType<APIResponse<StoriesDetailsDto>>();
      }

    }
  } 
}
