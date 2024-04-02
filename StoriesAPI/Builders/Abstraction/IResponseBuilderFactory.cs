using System;
using System.Collections.Generic;
using System.Text;

namespace StoriesAPI.Builders
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResponseBuilderFactory
    {
        /// <summary>
        /// Gets the builder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IResponseBuilder<T> GetBuilder<T>();
    }
}
