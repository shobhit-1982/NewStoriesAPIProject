using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace StoriesAPI.Base
{
    /// <summary>
    /// BaseController
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        ///  the stories ok.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        [NonAction]
        public OkObjectResult StoriesOk(object result) => Ok(result);

        /// <summary>
        /// stories the created.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [NonAction]
        public CreatedResult StoriesCreated(Uri uri, [ActionResultObjectValue] object value) => Created(uri, value);

        /// <summary>
        /// the stories content of the no.
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public NoContentResult StoriesNoContent() => NoContent();

        /// <summary>
        /// stories the not found.
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public NotFoundResult StoriesNotFound() => NotFound();

    
    }
}
