using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using StoriesAPI.Controller;
using StoriesAPI.Models;
using StoriesAPI.Services.StoriesServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Stories.Controller
{
  public class StoriesControllerFixture : ITestFixtureBuilder
  {
    /// <summary>
    /// the stories service
    /// </summary>
    private IStoriesService _storiesService = null;
    /// <summary>
    /// fixture
    /// </summary>
    public StoriesControllerFixture()
    {
       _storiesService = Substitute.For<IStoriesService>();
    }
    /// <summary>
    /// Withes the stories service.
    /// </summary>
    /// <param name="StoriesService">The stories service.</param>
    /// <returns></returns>
    public StoriesControllerFixture WithStoriesService(IStoriesService storiesService)
    {
      return this.With(field: ref _storiesService, value: storiesService);
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="StoriesControllerFixture"/> to <see cref="StoriesController"/>.
    /// </summary>
    /// <param name="fixture">The fixture.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator StoriesController(StoriesControllerFixture fixture) => fixture.Build();

    /// <summary>
    /// Builds this instance.
    /// </summary>
    /// <returns></returns>
    public StoriesController Build() => new StoriesController(_storiesService);

  }
}
