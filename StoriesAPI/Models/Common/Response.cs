using StoriesAPI.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoriesAPI.Models
{
  public class APIResponse<T>
  {

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public string ApiResponseStatus { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public int? HttpStatus { set; get; }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    /// <value>
    /// The content.
    /// </value>
    public T Data { get; set; }

    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>
    /// The errors.
    /// </value>
    public List<Error> Errors { get; set; }
  }
}
