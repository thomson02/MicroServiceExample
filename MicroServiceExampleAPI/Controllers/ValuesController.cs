using System;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceExampleAPI.Controllers
{
    /// <summary>
    /// Values controller.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class ValuesController : ApiControllerBase
    {
        /// <summary>
        /// Gets the v1.
        /// </summary>
        /// <returns>The v1.</returns>
        [HttpGet, MapToApiVersion("1.0")]
        [Obsolete]
        public ActionResult<string> GetV1()
        {
            return "VERSION 1";
        }

        /// <summary>
        /// Gets the v2.
        /// </summary>
        /// <returns>The v2.</returns>
        [HttpGet, MapToApiVersion("2.0")]
        public ActionResult<string> GetV2()
        {
            return "VERSION 2";
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <returns>The by identifier.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            return "VERSION 2";
        }
    }
}
