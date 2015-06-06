using Newtonsoft.Json;
using RestClientWinForms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Globalization;
using System.Resources;

namespace RestClientWinForms
{
    public partial class InvetoryForm : Form
    {
        Form oldForm;
        ResourceManager resMan;
        CultureInfo cultHU;
        CultureInfo cultEN;
        bool lang;
        string msgDel;
        string capDel;
        string msgDelProb;
        string capDelProb;
        string msgAddProb;
        string capError;
        string msgFileTxtProb;
        string msgFilePathProb;
        string msgFileXmlProb;
        string msgXmlError;

        public InvetoryForm(Form oldFormT)
        {
            InitializeComponent();
            oldForm = oldFormT;
            resMan = new ResourceManager("RestClientWinForms.Resource.Res", typeof(MainForm).Assembly);
            lang = Settings.Default.Language;
            if (lang)
            {
                radioHU.Checked = true;
            }
            else
            {
                radioEN.Checked = true;
            }             
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var resultItems = await Task.Run(() =>
            {
                return taskItemGetAll();
            });
            inventoryGrid.DataSource = resultItems;
            boxDelInfo.DataBindings.Add("Text", resultItems, "ItemName");

            var resultCategories = await Task.Run(() =>
            {
                return taskCategoryGetAll();
            });
            boxCategory.DataSource = resultCategories;
            boxCategory.DisplayMember = "CategoryName";
            boxCategory.ValueMember = "CategoryID"; 
        }

        private void btActForm2_Click(object sender, EventArgs e)
        {
            oldForm.Location = this.Location;
            oldForm.StartPosition = FormStartPosition.Manual;
            oldForm.Show();
            this.Hide();
        }

        private async void btnDelItem_Click(object sender, EventArgs e)
        {
            if (boxDelInfo.Text.Length > 0)
            {
                var result = MessageBox.Show(msgDel, capDel, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        int i = Int32.Parse(inventoryGrid.SelectedRows[0].Cells[0].Value.ToString());
                        var resp = await client.DeleteAsync(string.Format("http://localhost:41400/api/Items/Delete/{0}", i));
                        if (resp.IsSuccessStatusCode)
                        {
                            refreshGrid();
                        }
                        else
                        {
                            var result2 = MessageBox.Show(msgDelProb, capDelProb, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }                        
                    }
                }
            }
        }

        private async void btnAddItem_Click(object sender, EventArgs e)
        {
            if ((boxName.Text.Length > 0) && (boxDesc.Text.Length > 0))
            {
                int newPrice = Int32.Parse(boxPrice.Text.ToString());
                if (newPrice < 0) newPrice = 0;
                ItemDTO newItem = new ItemDTO
                {
                    ItemID = -1,
                    ItemName = boxName.Text,
                    CategoryID = Int32.Parse(boxCategory.SelectedValue.ToString()),
                    CategoryName = boxCategory.SelectedText.ToString(),
                    Description = boxDesc.Text,
                    UnitPrice = newPrice
                };

                //WriteXML(newItem);

                using (HttpClient client = new HttpClient())
                {
                    var addedItem = await client.PostAsJsonAsync("http://localhost:41400/api/Items/Get/", newItem);
                }

                refreshGrid();
                boxName.Clear();
                boxDesc.Clear();
                boxPrice.Clear();
            }
            else
            {
                var result = MessageBox.Show(msgAddProb, capError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewFileBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Text Files |*.txt| XML Files |*.xml";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFileContent.Text = "";
                textBoxFilePath.Text = openFileDialog.FileName;
                using (StreamReader reader = new StreamReader(textBoxFilePath.Text, Encoding.Default, true))
                {
                    while (true)
                    {
                        string line = reader.ReadLine();
                        if (line == null) break;
                        textBoxFileContent.Text += string.Format("{0}\r\n", line);
                    }
                }
            }
        }

        private async void btnImportItem_Click(object sender, EventArgs e)
        {
            if (textBoxFilePath.Text.Length > 0)
            {
                if (openFileDialog.FileName.ToString().Substring(openFileDialog.FileName.ToString().Length - 4).Equals(".txt"))
                {
                    var resultCategories = await Task.Run(() =>
                    {
                        return taskCategoryGetAll();
                    });

                    using (StreamReader reader = new StreamReader(textBoxFilePath.Text, Encoding.Default, true))
                    {
                        while (true)
                        {
                            string line = reader.ReadLine();
                            if (line == null) break;

                            char[] delimiterChars = { ';' };
                            string[] fields = line.Split(delimiterChars);

                            int catID = Int32.Parse(fields[1]);
                            int price = Int32.Parse(fields[3]);
                            var catName = (from cat in resultCategories
                                           where cat.CategoryID == catID
                                           select cat.CategoryName).First();

                            ItemDTO newItem = new ItemDTO
                            {
                                ItemID = -1,
                                ItemName = fields[0],
                                CategoryID = catID,
                                CategoryName = catName,
                                Description = fields[2],
                                UnitPrice = price
                            };
                            using (HttpClient client = new HttpClient())
                            {
                                var addedItem = await client.PostAsJsonAsync("http://localhost:41400/api/Items/Get/", newItem);
                            }
                        }
                        textBoxFilePath.Text = "";
                        textBoxFileContent.Text = "";
                        refreshGrid();
                    }
                }
                else 
                {
                    var result = MessageBox.Show(msgFileTxtProb, capError, MessageBoxButtons.OK, MessageBoxIcon.Error);             
                }                
            }
            else
            {
                var result = MessageBox.Show(msgFilePathProb, capError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnImportItemXML_Click(object sender, EventArgs e)
        {
            if (textBoxFilePath.Text.Length > 0)
            {
                if (openFileDialog.FileName.ToString().Substring(openFileDialog.FileName.ToString().Length - 4).Equals(".xml"))
                {
                    var resultCategories = await Task.Run(() =>
                    {
                        return taskCategoryGetAll();
                    });

                    XmlReaderSettings readSett = new XmlReaderSettings();
                    readSett.Schemas.Add(null, "itemDTOschema.xsd");
                    readSett.ValidationType = ValidationType.Schema;
                    readSett.ValidationEventHandler += new ValidationEventHandler(readSet_ValidationEventHandler);

                    using (XmlReader reader = XmlReader.Create(textBoxFilePath.Text.ToString(), readSett))
                    {
                        var xmlDoc = new XmlDocument();
                        xmlDoc.Load(textBoxFilePath.Text.ToString());
                        var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                        nsmgr.AddNamespace("ItemDTO", "ItemDTO-schema");
                        foreach (XmlNode node in xmlDoc.SelectNodes("//ItemDTO:ItemName", nsmgr))
                        {
                            int catID = Int32.Parse(node.NextSibling.NextSibling.InnerText.ToString());
                            string desc = node.NextSibling.NextSibling.NextSibling.InnerText;
                            int price = Int32.Parse(node.NextSibling.NextSibling.NextSibling.NextSibling.InnerText.ToString());
                            var catName = (from cat in resultCategories
                                           where cat.CategoryID == catID
                                           select cat.CategoryName).First();

                            ItemDTO newItem = new ItemDTO
                            {
                                ItemID = -1,
                                ItemName = node.InnerText,
                                CategoryID = catID,
                                CategoryName = catName,
                                Description = desc,
                                UnitPrice = price
                            };
                            using (HttpClient client = new HttpClient())
                            {
                                var addedItem = await client.PostAsJsonAsync("http://localhost:41400/api/Items/Get/", newItem);
                            }
                        }
                        textBoxFileContent.Text = "";
                        textBoxFilePath.Text = "";
                        refreshGrid();
                    }
                }
                else
                {
                    var result = MessageBox.Show(msgFileXmlProb, capError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var result = MessageBox.Show(msgFilePathProb, capError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void readSet_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            var result = MessageBox.Show(msgXmlError, capError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }        

        public static void WriteXML(ItemDTO item)
        {
            var writer = new XmlSerializer(typeof(ItemDTO));
            var file = new StreamWriter(@"C:\Users\Norbert\Desktop\deser\item.xml", false, Encoding.GetEncoding("ISO-8859-2"));
            writer.Serialize(file, item);
            file.Close();
        }

        private async void refreshGrid()
        {
            var resultItems = await Task.Run(() =>
            {
                return taskItemGetAll();
            });

            inventoryGrid.DataSource = resultItems;
            boxDelInfo.DataBindings.Clear();
            boxDelInfo.DataBindings.Add("Text", resultItems, "ItemName");
        }

        #region Asynchron Tasks Region
        public async Task<BindingList<Category>> taskCategoryGetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var resp = await client.GetAsync("http://localhost:41400/api/Category/Get/All");
                var categories = await resp.Content.ReadAsAsync<BindingList<Category>>();
                return categories;
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
        #endregion

        private void radioHU_CheckedChanged(object sender, EventArgs e)
        {
            if (radioHU.Checked)
            {
                setLanguage(true);
                radioEN.Checked = false;
            }            
        }

        private void radioEN_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEN.Checked)
            {
                setLanguage(false);
                radioHU.Checked = false;
            }
        }

        private void setLanguage(bool hun)
        {
            if (hun)
            {
                cultHU = CultureInfo.CreateSpecificCulture("hu");
                btActForm2.Text = resMan.GetString("btActForm2", cultHU);
                headerLabel.Text = resMan.GetString("headerLabel", cultHU);
                newItemLabel.Text = resMan.GetString("newItemLabel", cultHU);
                nameLabel.Text = resMan.GetString("nameLabel", cultHU);
                categoryLabel.Text = resMan.GetString("categoryLabel", cultHU);
                descriptionLabel.Text = resMan.GetString("descriptionLabel", cultHU);
                priceLabel.Text = resMan.GetString("priceLabel", cultHU);
                btnAddItem.Text = resMan.GetString("btnAddItem", cultHU);
                importLabel.Text = resMan.GetString("importLabel", cultHU);
                btnNewFileBrowse.Text = resMan.GetString("btnNewFileBrowse", cultHU);
                fileContLabel.Text = resMan.GetString("fileContLabel", cultHU);
                btnImportItem.Text = resMan.GetString("btnImportItem", cultHU);
                btnImportItemXML.Text = resMan.GetString("btnImportItemXML", cultHU);
                deleteLabel.Text = resMan.GetString("deleteLabel", cultHU);
                btnDelItem.Text = resMan.GetString("btnDelItem", cultHU);
                inventLabel.Text = resMan.GetString("inventLabel", cultHU);
                radioHU.Text = resMan.GetString("radioHU", cultHU);
                radioEN.Text = resMan.GetString("radioEN", cultHU);
                inventoryGrid.Columns[0].HeaderText = resMan.GetString("invGridItemID", cultHU);
                inventoryGrid.Columns[1].HeaderText = resMan.GetString("invGridItemName", cultHU);
                inventoryGrid.Columns[2].HeaderText = resMan.GetString("invGridCat", cultHU);
                inventoryGrid.Columns[3].HeaderText = resMan.GetString("invGridDesc", cultHU);
                inventoryGrid.Columns[4].HeaderText = resMan.GetString("invGridPrice", cultHU);
                this.Text = resMan.GetString("formName", cultHU);
                msgDel = resMan.GetString("msgDel", cultHU);
                capDel = resMan.GetString("capDel", cultHU);
                msgXmlError = resMan.GetString("msgXmlError", cultHU);
                msgFileXmlProb = resMan.GetString("msgFileXmlProb", cultHU);
                msgFileTxtProb = resMan.GetString("msgFileTxtProb", cultHU);
                msgFilePathProb = resMan.GetString("msgFilePathProb", cultHU);
                msgDelProb = resMan.GetString("msgDelProb", cultHU);
                msgAddProb = resMan.GetString("msgAddProb", cultHU);
                capError = resMan.GetString("capError", cultHU);
                capDelProb = resMan.GetString("capDelProb", cultHU);
                
            }
            else
            {
                cultEN = CultureInfo.CreateSpecificCulture("en");
                btActForm2.Text = resMan.GetString("btActForm2", cultEN);
                headerLabel.Text = resMan.GetString("headerLabel", cultEN);
                newItemLabel.Text = resMan.GetString("newItemLabel", cultEN);
                nameLabel.Text = resMan.GetString("nameLabel", cultEN);
                categoryLabel.Text = resMan.GetString("categoryLabel", cultEN);
                descriptionLabel.Text = resMan.GetString("descriptionLabel", cultEN);
                priceLabel.Text = resMan.GetString("priceLabel", cultEN);
                btnAddItem.Text = resMan.GetString("btnAddItem", cultEN);
                importLabel.Text = resMan.GetString("importLabel", cultEN);
                btnNewFileBrowse.Text = resMan.GetString("btnNewFileBrowse", cultEN);
                fileContLabel.Text = resMan.GetString("fileContLabel", cultEN);
                btnImportItem.Text = resMan.GetString("btnImportItem", cultEN);
                btnImportItemXML.Text = resMan.GetString("btnImportItemXML", cultEN);
                deleteLabel.Text = resMan.GetString("deleteLabel", cultEN);
                btnDelItem.Text = resMan.GetString("btnDelItem", cultEN);
                inventLabel.Text = resMan.GetString("inventLabel", cultEN);
                radioHU.Text = resMan.GetString("radioHU", cultEN);
                radioEN.Text = resMan.GetString("radioEN", cultEN);
                inventoryGrid.Columns[0].HeaderText = resMan.GetString("invGridItemID", cultEN);
                inventoryGrid.Columns[1].HeaderText = resMan.GetString("invGridItemName", cultEN);
                inventoryGrid.Columns[2].HeaderText = resMan.GetString("invGridCat", cultEN);
                inventoryGrid.Columns[3].HeaderText = resMan.GetString("invGridDesc", cultEN);
                inventoryGrid.Columns[4].HeaderText = resMan.GetString("invGridPrice", cultEN);
                this.Text = resMan.GetString("formName", cultEN);
                msgDel = resMan.GetString("msgDel", cultEN);
                capDel = resMan.GetString("capDel", cultEN);
                msgXmlError = resMan.GetString("msgXmlError", cultEN);
                msgFileXmlProb = resMan.GetString("msgFileXmlProb", cultEN);
                msgFileTxtProb = resMan.GetString("msgFileTxtProb", cultEN);
                msgFilePathProb = resMan.GetString("msgFilePathProb", cultEN);
                msgDelProb = resMan.GetString("msgDelProb", cultEN);
                msgAddProb = resMan.GetString("msgAddProb", cultEN);
                capError = resMan.GetString("capError", cultEN);
                capDelProb = resMan.GetString("capDelProb", cultEN);
            }
            Settings.Default.Language = hun;
            Settings.Default.Save();
        }
    }
}
