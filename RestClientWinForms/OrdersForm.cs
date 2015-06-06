using Newtonsoft.Json;
using RestClientWinForms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace RestClientWinForms
{
    public partial class OrdersForm : Form
    {
        Dictionary<int, int> itemIDCount;
        Form oldForm;
        int orderIDCell = 0;
        int reserved = 2;

        public OrdersForm(Form oldFormT)
        {
            InitializeComponent();
            oldForm = oldFormT;
            dateTimePickerOrder.Enabled = false;
            comBoxTable.Enabled = false;
            btnShowAll.Enabled = false;
            itemIDCount = new Dictionary<int, int>();
            dateTimePickerNew.MinDate = DateTime.Now;
            dateTimePickerNew.MaxDate = DateTime.Now.AddDays(30);
            numericQuant.Minimum = 1;
            numericQuant.Maximum = 100;            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            oldForm.Location = this.Location;
            oldForm.StartPosition = FormStartPosition.Manual;
            oldForm.Show();
            this.Hide();
        }

        private async void Orders_Load(object sender, EventArgs e)
        {
            itemDataGridView.Columns[3].DefaultCellStyle.Format = "c";

            loadAllOrderData();            

            var resultTables = await Task.Run(() =>
            {
                return taskTableGetAll();
            });

            comBoxTable.DataSource = resultTables;
            comBoxTable.DisplayMember = "TableID";
            comBoxTable.ValueMember = "TableID";
            comBoxTable.SelectedItem = null;
            this.comBoxTable.SelectedValueChanged += new System.EventHandler(this.comBoxTable_SelectedValueChanged);

            comBoxNewTable.DataSource = resultTables;
            comBoxNewTable.DisplayMember = "TableID";
            comBoxNewTable.ValueMember = "TableID";
            comBoxNewTable.SelectedItem = null;

            var resultItems = await Task.Run(() =>
            {
                return taskItemGetAll();
            });

            comBoxNewItem.DataSource = resultItems;
            comBoxNewItem.DisplayMember = "ItemName";
            comBoxNewItem.ValueMember = "ItemID";
            comBoxNewItem.SelectedItem = null;                       
        }

        private async void orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (orderDataGridView.SelectedRows.Count == 1)
                {
                    itemIDCount.Clear();
                    
                    int id = Int32.Parse(orderDataGridView.SelectedRows[0].Cells[orderIDCell].Value.ToString());

                    var resultOrderDet = await Task.Run(() =>
                        {
                            return taskOrderDetGetID(id);
                        });

                    var resultItem = await Task.Run(() =>
                    {
                        return taskItemGetID(resultOrderDet.ItemID);
                    });

                    var itemBindingList = new BindingList<Item>();
                    itemBindingList.Add((Item)resultItem);
                    itemDataGridView.DataSource = itemBindingList;
                    itemDataGridView.Rows[0].Cells[2].Value = resultOrderDet.Quantity;
                    itemDataGridView.Rows[0].Cells[orderIDCell].Value = resultOrderDet.OrderID;

                    textBoxDelete.DataBindings.Clear();
                    textBoxDelete.DataBindings.Add("Text", resultOrderDet, "OrderID");
                } 
        }

        private void comBoxTable_SelectedValueChanged(object sender, EventArgs e)
        {
            int id = Int32.Parse(comBoxTable.SelectedValue.ToString());
            
            refreshGridsByTable(id);
        }

        private async void refreshGridsByTable(int id)
        {
            itemDataGridView.DataSource = null;
            orderDataGridView.DataSource = null;
            textBoxDelete.Clear();
            textBoxReserv.Text = "";

            var table = await Task.Run(() =>
            {
                return taskTableGetID(id);
            });            
            restaurantControll1.maxValue = table.Capacity;

            if (table.Reserved.Value)
            {
                reserved = 1;
            }
            else
            {
                reserved = 0;
            }

            var orders = await Task.Run(() =>
            {
                return taskOrderGetTableID(id);
            });
            restaurantControll1.actValue = orders.Count;

            var items = await Task.Run(() =>
            {
                return taskItemGetItemIDs(orders);
            });

            itemDataGridView.DataSource = items;
            orderDataGridView.DataSource = orders;

            int i = 0;
            foreach (var item in items)
            {
                itemDataGridView.Rows[i].Cells[2].Value = itemIDCount[item.ItemID];
                itemDataGridView.Rows[i].Cells[orderIDCell].Value = orderDataGridView.Rows[i].Cells[orderIDCell].Value;
                i++;
            }
        }

        private async void dateTimePickerOrder_ValueChanged(object sender, EventArgs e)
        {
            long dateObj = dateTimePickerOrder.Value.ToBinary();
            var result = await Task.Run(() =>
            {
                return taskOrderGetDateString(dateObj);
            });

            orderDataGridView.DataSource = result;
            itemDataGridView.DataSource = null;
        }

        private async void loadAllOrderData()
        {
            comBoxTable.Enabled = false;
            comBoxNewTable.Enabled = false;
            dateTimePickerOrder.Enabled = false;
            btnNewOrder.Enabled = false;
            btnShowAll.Enabled = false;
            itemDataGridView.DataSource = null;

            var resultOrders = await Task.Run(() =>
            {
                return taskOrderGetAll();
            });
            orderDataGridView.DataSource = resultOrders;
            comBoxTable.Enabled = true;
            comBoxTable.SelectedItem = null;
            comBoxNewTable.Enabled = true;
            comBoxNewTable.SelectedItem = null;
            dateTimePickerOrder.Enabled = true;
            btnNewOrder.Enabled = true;
            btnShowAll.Enabled = true;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {            
            loadAllOrderData();
        }

        private async void btnNewOrder_Click(object sender, EventArgs e)
        {
            if ((comBoxNewTable.SelectedValue != null) && (comBoxNewItem.SelectedValue != null))
            {
                btnNewOrder.Enabled = false;

                OrderDTO newOrderDTO = new OrderDTO
                {
                    OrderID = -1,
                    TableID = Int32.Parse(comBoxNewTable.SelectedValue.ToString()),
                    OrderDate = dateTimePickerNew.Value
                };

                var selItemID = Int32.Parse(comBoxNewItem.SelectedValue.ToString());

                var selItem = await taskItemGetID(selItemID);

                OrderDetail newOrderDetDTO = new OrderDetail
                {
                    OrderID = -1,
                    ItemID = selItemID,
                    UnitPrice = Int32.Parse(selItem.UnitPrice.ToString()),
                    Quantity = (short)numericQuant.Value
                };

                using (HttpClient client = new HttpClient())
                {
                    var resp = await client.PostAsJsonAsync("http://localhost:41400/api/Order/Add/", newOrderDTO);
                    var addedOrder = await resp.Content.ReadAsAsync<Order>();
                    newOrderDetDTO.OrderID = addedOrder.OrderID;
                    var finished = await client.PostAsJsonAsync("http://localhost:41400/api/OrderDet/Add/", newOrderDetDTO);
                }

                refreshGridsByTable(newOrderDTO.TableID);

                comBoxNewItem.SelectedItem = null;
                comBoxNewTable.SelectedItem = null;
                numericQuant.Value = 1;
                btnNewOrder.Enabled = true;
            }
            else
            {
                const string message = "Some critical field not filled correctly. Try again!";
                const string caption = "Error!";

                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult diaRes = folderBrowserDialog1.ShowDialog();

            if (diaRes == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath != "")
                {
                    textBoxBrowse.Text = folderBrowserDialog1.SelectedPath;
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (textBoxDelete.TextLength > 0)
            {
                const string message = "Are you sure that you would like to delete this order?";
                const string caption = "Deleting...";

                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        int tableID = Int32.Parse(orderDataGridView.SelectedRows[0].Cells[1].Value.ToString());
                        int orderId = Int32.Parse(orderDataGridView.SelectedRows[0].Cells[orderIDCell].Value.ToString());
                        await client.DeleteAsync(string.Format("http://localhost:41400/api/Order/Delete/{0}", orderId));
                        await client.DeleteAsync(string.Format("http://localhost:41400/api/OrderDet/Delete/{0}", orderId));

                        refreshGridsByTable(tableID);
                    }
                }
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if ((int)itemDataGridView.Rows.Count > 0)
            {
                if (textBoxBrowse.Text.Length > 0)
                {
                    PdfPTable pdfTable = new PdfPTable(itemDataGridView.ColumnCount);
                    pdfTable.DefaultCell.Padding = 3;
                    pdfTable.WidthPercentage = 100;
                    pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfTable.DefaultCell.BorderWidth = 1;

                    int[] columnWidths = { 25, 25, 25, 50, 30, 45, 55 };
                    pdfTable.SetWidths(columnWidths);

                    foreach (DataGridViewColumn column in itemDataGridView.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                        pdfTable.AddCell(cell);
                    }

                    foreach (DataGridViewRow row in itemDataGridView.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            pdfTable.AddCell(cell.Value.ToString());
                        }
                    }

                    string folderPath = textBoxBrowse.Text.ToString();
                    //string folderPath = @"E:\BME\8. félév\dotNET\RestaurantSystem\PDFs\";
                    string pdfName = string.Format("{0}_{1:yyyy-MM-dd_HH-mm-ss}.pdf", comBoxTable.SelectedValue.ToString(), DateTime.Now);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    using (FileStream stream = new FileStream(folderPath + pdfName, FileMode.Create))
                    {
                        var pdfDoc = new Document();
                        var text = new Chunk("Exported Order data ", FontFactory.GetFont("arial"));
                        text.SetUnderline(0.5f, -1.5f);
                        var table = new Chunk(string.Format("from table: {0}", comBoxTable.SelectedValue.ToString()));
                        var time = new Chunk(string.Format("Export time: {0}", DateTime.Now.ToString()));

                        var p1 = new Paragraph();
                        p1.Add(text);
                        p1.Add(table);
                        var p2 = new Paragraph();
                        p2.Add(pdfTable);

                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        pdfDoc.Add(p1);
                        pdfDoc.Add(Chunk.NEWLINE);
                        pdfDoc.Add(p2);
                        pdfDoc.Add(Chunk.NEWLINE);
                        pdfDoc.Add(time);

                        pdfDoc.Close();
                        stream.Close();
                    }

                    const string message = "PDF created!";
                    const string caption = "Success!";
                    var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    const string message = "File path not selected!";
                    const string caption = "Error!";
                    var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                const string message = "No order selected!";
                const string caption = "Error!";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region Asynchron Tasks Region

        public async Task<BindingList<Item>> taskItemGetItemIDs(BindingList<Order> orders)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var orderIDs = new List<int>();
                foreach (var item in orders)
                {
                    orderIDs.Add(item.OrderID);
                }
                var jsonOrderIDs = JsonConvert.SerializeObject(orderIDs);

                var resp = await client.PostAsJsonAsync("http://localhost:41400/api/OrderDet/Get/ByOrders", jsonOrderIDs);
                var orderDets = await resp.Content.ReadAsAsync<BindingList<OrderDetail>>();
                               
                var itemIDs = new List<int>();
                itemIDCount.Clear();

                foreach (var orderDet in orderDets)
                {
                    itemIDs.Add(orderDet.ItemID);
                    if (itemIDCount.ContainsKey(orderDet.ItemID))
                    {
                        itemIDCount[orderDet.ItemID]++;
                    }
                    else
                    {
                        itemIDCount.Add(orderDet.ItemID, orderDet.Quantity);
                    }                        
                }
                var jsonItemIDs = JsonConvert.SerializeObject(itemIDs);

                var resp2 = await client.PostAsJsonAsync("http://localhost:41400/api/Items/Post/ItemIDs/", jsonItemIDs);
                var items = await resp2.Content.ReadAsAsync<BindingList<Item>>();

                return items;
            }
        }

        public async Task<BindingList<Order>> taskOrderGetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var resp = await client.GetAsync("http://localhost:41400/api/Order/Get/All");
                var orders = await resp.Content.ReadAsAsync<BindingList<Order>>();
                return orders;
            }
        }

        public async Task<BindingList<Table>> taskTableGetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var resp = await client.GetAsync("http://localhost:41400/api/Table/Get/All");
                var tables = await resp.Content.ReadAsAsync<BindingList<Table>>();
                return tables;
            }
        }

        public async Task<Table> taskTableGetID(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var resp = await client.GetAsync(string.Format("http://localhost:41400/api/Table/Get/{0}",id));
                var table = await resp.Content.ReadAsAsync<Table>();
                return table;
            }
        }

        public async Task<BindingList<Item>> taskItemGetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var resp = await client.GetAsync("http://localhost:41400/api/Items/Get/All");
                var products = await resp.Content.ReadAsAsync<BindingList<Item>>();
                return products;
            }
        }
        
        public async Task<Item> taskItemGetID(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var respItem = await client.GetAsync(string.Format("http://localhost:41400/api/Items/Get/{0}", id));
                var result = await respItem.Content.ReadAsAsync<Item>();
                return result;
            }
        }

        public async Task<OrderDetail> taskOrderDetGetID(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var respOrderDet = await client.GetAsync(string.Format("http://localhost:41400/api/OrderDet/Get/{0}", id));
                var orderDet = await respOrderDet.Content.ReadAsAsync<OrderDetail>();
                return orderDet;
            }
        }

        public async Task<BindingList<Order>> taskOrderGetTableID(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var resp = await client.GetAsync(string.Format("http://localhost:41400/api/Order/Get/Table/{0}", id));
                var orders = await resp.Content.ReadAsAsync<BindingList<Order>>();
                return orders;
            }
        }

        public async Task<BindingList<Order>> taskOrderGetDateString(long dateObj)
        {
            using (HttpClient client = new HttpClient())
            {
                var resp = await client.GetAsync(string.Format("http://localhost:41400/api/Order/Get/Date/{0}", dateObj));
                var orders = await resp.Content.ReadAsAsync<BindingList<Order>>();
                return orders;
            }
        }
        #endregion

        private void restaurantControll1_Click(object sender, EventArgs e)
        {
                if (reserved == 1)
                {
                    textBoxReserv.Text = "Reserved.";
                }
                if (reserved == 0)
                {
                    textBoxReserv.Text = "Free.";
                }
                if ((restaurantControll1.actValue > 0) && (reserved != 2))
                {
                    textBoxReserv.Text = "In use.";
                }
        }
    }
}
