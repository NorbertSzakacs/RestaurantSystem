using RestWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Newtonsoft.Json;

namespace RestWeb.Controllers
{
    public class ItemsController : ApiController
    {
        [HttpPost]
        [Route("api/Items/Post/ItemIDs", Name = "PostItemItemIDUrl")]
        public IHttpActionResult Post2([FromBody]string jsonList)
        {
            List<int> iIDs = (List<int>)JsonConvert.DeserializeObject(jsonList, typeof(List<int>));
            using (restDB context = new restDB())
            {
                var items = from item in context.Items.Include(p=>p.Category)
                            where iIDs.Contains(item.ItemID)
                            select item;
                var asd = toDTOList(items.ToList());
                if (items == null)
                {
                    return NotFound();
                }
                return Created(Url.Link("GetItemUrl", new { id = -2 }), asd);
            }
        }

        public IHttpActionResult Post([FromBody]ItemDTO newItem)
        {
            Item asd = new Item(newItem);
            var inserted = InsertItem(asd);
            return Created(Url.Link("GetItemUrl", new { id = inserted.ItemID }), inserted.DTO);
        }

        [HttpGet]
        [Route("api/Items/Get/{id}", Name = "GetItemUrl")]
        public IHttpActionResult Get(int id)
        {
            using (restDB context = new restDB())
            {               
                var item = context.Items.Include(p => p.Category).SingleOrDefault(p => p.ItemID == id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item.DTO);
            }
        }
        
        [HttpGet]
        [Route("api/Items/Get/All", Name = "GetItemsUrl")]
        public IHttpActionResult GetAll()
        {
            using (restDB context = new restDB())
            {
                var items = context.Items.Include(p => p.Category).ToList();
                if (items == null)
                {
                    return NotFound();
                }
                return Ok(toDTOList(items));
            }
        }

        [HttpGet]
        [Route("api/Items/Search/CatID/{catID}", Name = "GetItemsCatIDUrl")]
        public IHttpActionResult GetAll(int catID)
        {
            using (restDB context = new restDB())
            {
                var items = context.Items.Include(p => p.Category).Where(p => p.Category.CategoryID == catID).ToList();
                if (items == null)
                {
                    return NotFound();
                }
                return Ok(toDTOList(items));
            }
        }

        // PUT: api/Items/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        [Route("api/Items/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (DeleteItem(id))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Enrolled");
                }
               
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        private List<ItemDTO> toDTOList(List<Item> items)
        {
            var res = new List<ItemDTO>();
            foreach (var item in items)
            {
                res.Add(item.DTO);
            }
            return res;
        }

        private static Item InsertItem(Item newItem)
        {
            using (restDB context = new restDB())
            {
                newItem.ItemID = context.Items.Max(p => p.ItemID) + 1;
                var catName = (from p in context.Categories
                               where p.CategoryID == newItem.CategoryID
                               select p).First();

                newItem.Category = catName;
                context.Items.Add(newItem);
                context.SaveChanges();
            }
            return newItem;
        }

        private bool DeleteItem(int id)
        {
            using (restDB context=new restDB())
            {
                var itemToDel = context.Items.Include(p => p.OrderDetails).SingleOrDefault(i => i.ItemID == id);
                if (itemToDel == null)
                {
                    throw new ArgumentException("Delete: Item id not exist!");
                }
                if (itemToDel.OrderDetails == null)
                {
                    context.Items.Remove(itemToDel);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
