using RestWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace RestWeb.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        [Route("api/Order/Get/{id}", Name = "GetOrderUrl")]
        public IHttpActionResult Get(int id)
        {
            using (restDB context = new restDB())
            {
                var order = context.Orders.SingleOrDefault(p => p.OrderID == id);
                
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order.DTO);
            }
        }

        [HttpGet]
        [Route("api/Order/Get/Table/{id}", Name = "GetOrderByTableUrl")]
        public IHttpActionResult GetByTable(int id)
        {
            using (restDB context = new restDB())
            {
                var orders = context.Orders.Where(p=>p.TableID == id).ToList();
                if (orders == null)
                {
                    return NotFound();
                }
                return Ok(toDTOList(orders));
            }
        }

        [HttpGet]
        [Route("api/Order/Get/Date/{selectedDate}", Name = "GetOrderByDateUrl")]
        public IHttpActionResult GetByTable(long selectedDate)
        {
            using (restDB context = new restDB())
            {
                DateTime shortDate = DateTime.FromBinary(selectedDate);
                shortDate = shortDate.Date;

                var orders = context.Orders.Where(p => DbFunctions.TruncateTime(p.OrderDate) == shortDate).ToList();               
                if (orders == null)
                {
                    return NotFound();
                }
                return Ok(toDTOList(orders));
            }
        }

        [HttpGet]
        [Route("api/Order/Get/All", Name = "GetOrdersUrl")]
        public IHttpActionResult GetAll()
        {
            using (restDB context = new restDB())
            {
                
                var orders = context.Orders.ToList();
                if (orders == null)
                {
                    return NotFound();
                }
                return Ok(toDTOList(orders));
            }
        }

        [HttpGet]
        [Route("api/Order/Get/AllDetails", Name = "GetOrdersDetUrl")]
        public IHttpActionResult GetAllDet()
        {
            using (restDB context = new restDB())
            {
                var orders = context.Orders.ToList();
                var orderDets = context.OrderDetails.ToList();
                var ListOfItems = context.Items.Include(p => p.Category).ToList();

                //var selectedItems = from item in items
                //                    where orderDets.Any(order => order.ItemID == item.ItemID)
                //                    select item.ItemName;

                //var selectedItems = from order in orderDets
                //                    where items.Any(i => i.ItemID == order.ItemID)
                //                    select order.Item.ItemName;

                //var selectedItems = from order in orderDets
                //                    select order.Item.ItemName;

                var selectedItems = from order in orderDets
                                    join items in ListOfItems on order.ItemID equals items.ItemID
                                    //where order.ItemID == 5
                                    select new OrderDetDTO()
                                    {
                                        OrderID = order.OrderID,
                                        ItemID = order.ItemID,
                                        //UnitPrice = order.UnitPrice,
                                        UnitPrice = items.ItemID,
                                        Quantity = order.Quantity,
                                        //ItemName = items.ItemName
                                    };

                if (selectedItems == null)
                {
                    return NotFound();
                }
                return Ok(selectedItems);

            }
        }

        // POST: api/Order
        [HttpPost]
        [Route("api/Order/Add/", Name = "AddOrder")]
        public IHttpActionResult Post([FromBody]OrderDTO newOrder)
        {
            Order asd = new Order(newOrder);
            var inserted = InsertOrder(asd);
            return Created(Url.Link("GetOrderUrl", new { id = inserted.OrderID }), inserted.DTO);
        }

        // PUT: api/Order/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Order/5
        [HttpDelete]
        [Route("api/Order/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                DeleteOrder(id);
                return Ok();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        private void DeleteOrder(int id)
        {
            using (restDB context = new restDB())
            {
                var OrdertoDel = context.Orders.SingleOrDefault(o => o.OrderID == id);
                if (OrdertoDel == null)
                {
                    throw new ArgumentException("Delete: Order id not exist!");
                }
                context.Orders.Remove(OrdertoDel);
                context.SaveChanges();
            }
        }

        private static Order InsertOrder(Order newOrder)
        {
            using(restDB context = new restDB())
            {
                newOrder.OrderID = context.Orders.Max(p => p.OrderID) + 1;
                var table = (from p in context.Tables
                             where p.TableID == newOrder.TableID
                             select p).First();
                newOrder.Table = table;
                context.Orders.Add(newOrder);
                context.SaveChanges();
                return newOrder;
            }
        }

        private List<OrderDTO> toDTOList(List<Order> orders)
        {
            var res = new List<OrderDTO>();
            foreach (var order in orders)
            {
                res.Add(order.DTO);
            }
            return res;
        }
    }
}
