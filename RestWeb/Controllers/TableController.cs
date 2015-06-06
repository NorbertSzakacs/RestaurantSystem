using RestWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestWeb.Controllers
{
    public class TableController : ApiController
    {
        [HttpGet]
        [Route("api/Table/Get/{id}", Name = "GetTableUrl")]
        public IHttpActionResult Get(int id)
        {
            using (restDB context = new restDB())
            {
                var table = context.Tables.SingleOrDefault(p => p.TableID == id);
                if (table == null)
                {
                    return NotFound();
                }
                return Ok(table.DTO);
            }
        }

        [HttpGet]
        [Route("api/Table/Get/All", Name = "GetTablesUrl")]
        public IHttpActionResult GetAll()
        {
            using (restDB context = new restDB())
            {
                var tables = context.Tables.ToList();
                if (tables == null)
                {
                    return NotFound();
                }
                return Ok(toDTOList(tables));
            }
        }

        // POST: api/Table
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Table/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Table/5
        public void Delete(int id)
        {
        }

        private List<TableDTO> toDTOList(List<Table> tables)
        {
            var res = new List<TableDTO>();
            foreach (var table in tables)
            {
                res.Add(table.DTO);
            }
            return res;
        }
    }
}
