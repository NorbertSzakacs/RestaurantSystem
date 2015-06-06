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
    public class OrderDetController : ApiController
    {
        [HttpGet]
        [Route("api/OrderDet/Get/{id}", Name = "GetOrderDetUrl")]
        public IHttpActionResult Get(int id)
        {
            using (restDB context = new restDB())
            {
                var order = context.OrderDetails.SingleOrDefault(p => p.OrderID == id);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order.DTO);
            }
        }

        [HttpGet]
        [Route("api/OrderDet/Get/All", Name = "GetOrderDetsUrl")]
        public IHttpActionResult GetAll()
        {
            using (restDB context = new restDB())
            {                
                var orders = context.OrderDetails.ToList();
                if (orders == null)
                {
                    return NotFound();
                }
                return Ok(toDTOList(orders));
            }
        }

        // POST: api/OrderDet
        [HttpPost]
        [Route("api/OrderDet/Get/ByOrders", Name = "OrderDetGetVyOrdersURL")]
        public IHttpActionResult Post([FromBody]string jsonList)
        {
            List<int> oIDs = (List<int>)JsonConvert.DeserializeObject(jsonList,typeof(List<int>));
            using (restDB context = new restDB())
            {
                var itemIDs = from oDet in context.OrderDetails
                              where oIDs.Contains(oDet.OrderID)
                              select oDet;
                var itemList = toDTOList(itemIDs.ToList());
                if (itemIDs == null)
                {
                    return NotFound();
                }
                return Created(Url.Link("GetItemUrl", new { id = -1 }), itemList);
            }
        }

        // POST: api/Order
        [HttpPost]
        [Route("api/OrderDet/Add/", Name = "AddOrderDet")]
        public IHttpActionResult PostAdd([FromBody]OrderDetDTO newOrderDet)
        {
            OrderDetail inserted = new OrderDetail(newOrderDet);

            using (restDB context = new restDB())
            {
                var item = (from p in context.Items.Include(q=>q.Category)
                           where p.ItemID == inserted.ItemID
                           select p).First();
                inserted.Item = item;

                var order = (from p in context.Orders
                            where p.OrderID == inserted.OrderID
                            select p).First();

                order.OrderDetails.Add(inserted);
                inserted.Order = order;

                context.OrderDetails.Add(inserted);
                context.SaveChanges();
            }
            return Created(Url.Link("GetOrderDetUrl", new { id = inserted.OrderID }), inserted.DTO);
        }

        // PUT: api/OrderDet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/OrderDet/5
        [HttpDelete]
        [Route("api/OrderDet/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                DeleteOrderDet(id);
                return Ok();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        private void DeleteOrderDet(int id)
        {
            using (restDB context = new restDB())
            {
                var orderDettoDel = context.OrderDetails.SingleOrDefault(o => o.OrderID == id);
                if (orderDettoDel == null)
                {
                    throw new ArgumentException("Delete: OrderDet id not exist!");
                }
                context.OrderDetails.Remove(orderDettoDel);
                context.SaveChanges();
            }
        }


        private List<OrderDetDTO> toDTOList(List<OrderDetail> orders)
        {
            var res = new List<OrderDetDTO>();
            foreach (var orderDet in orders)
            {
                res.Add(orderDet.DTO);
            }
            return res;
        }
    }
}
