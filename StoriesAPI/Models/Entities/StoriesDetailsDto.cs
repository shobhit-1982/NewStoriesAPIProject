﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoriesAPI.Models
{
    public class StoriesDetailsDto
    {
        /// <summary>
        /// total records
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// story collection
        /// </summary>
        public IEnumerable<StoryDetailDto> Stories { get; set; }
    }
}
