using RestWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestWeb.Controllers
{
    public class CategoryController : ApiController
    {
        // GET: api/Category
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Category/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("api/Category/Get/All", Name = "GetCategoriesUrl")]
        public IHttpActionResult GetAll()
        {
            using (restDB context = new restDB())
            {
                var cats = context.Categories.ToList();
                if (cats == null)
                {
                    return NotFound();
                }
                return Ok(toDTOList(cats));
            }
        }

        // POST: api/Category
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Category/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
        }

        private List<CategoryDTO> toDTOList (List<Category> cats)
        {
            var res = new List<CategoryDTO>();
            foreach (var item in cats)
            {
                res.Add(item.DTO);
            }
            return res;
        }
    }
}
