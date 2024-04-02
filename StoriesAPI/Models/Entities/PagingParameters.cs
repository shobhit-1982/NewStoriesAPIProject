using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoriesAPI.Models
{ 
    /// <summary>
    /// 
    /// </summary>
  public class PagingParameters
  {
    /// <summary>
    /// set max page size
    /// </summary>
    const int maxPageSize = 200;
    /// <summary>
    /// page number
    /// </summary>
    public int PageNumber { get; set; } = 1;
    /// <summary>
    /// page size
    /// </summary>
    private int _pageSize = 200;

    /// <summary>
    /// set page size
    /// </summary>
    public int PageSize
    {
      get
      {
        return _pageSize;
      }
      set
      {
        _pageSize = (value > maxPageSize) ? maxPageSize : value;
      }
    }
  }
}
