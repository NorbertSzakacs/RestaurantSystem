namespace RestClientWinForms
{
    partial class InvetoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvetoryForm));
            this.inventoryGrid = new System.Windows.Forms.DataGridView();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btActForm2 = new System.Windows.Forms.Button();
            this.headerLabel = new System.Windows.Forms.Label();
            this.inventLabel = new System.Windows.Forms.Label();
            this.newItemPanel = new System.Windows.Forms.TableLayoutPanel();
            this.boxPrice = new System.Windows.Forms.TextBox();
            this.boxDesc = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.boxName = new System.Windows.Forms.TextBox();
            this.boxCategory = new System.Windows.Forms.ComboBox();
            this.newItemLabel = new System.Windows.Forms.Label();
            this.deleteLabel = new System.Windows.Forms.Label();
            this.btnDelItem = new System.Windows.Forms.Button();
            this.boxDelInfo = new System.Windows.Forms.TextBox();
            this.btnNewFileBrowse = new System.Windows.Forms.Button();
            this.importLabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.textBoxFileContent = new System.Windows.Forms.TextBox();
            this.fileContLabel = new System.Windows.Forms.Label();
            this.btnImportItem = new System.Windows.Forms.Button();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnImportItemXML = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioHU = new System.Windows.Forms.RadioButton();
            this.radioEN = new System.Windows.Forms.RadioButton();
            this.itemNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.inventoryGrid)).BeginInit();
            this.newItemPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // inventoryGrid
            // 
            this.inventoryGrid.AllowUserToAddRows = false;
            this.inventoryGrid.AllowUserToDeleteRows = false;
            this.inventoryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inventoryGrid.AutoGenerateColumns = false;
            this.inventoryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inventoryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.itemNameDataGridViewTextBoxColumn,
            this.categoryNameDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.unitPriceDataGridViewTextBoxColumn});
            this.inventoryGrid.DataSource = this.itemBindingSource;
            this.inventoryGrid.Location = new System.Drawing.Point(364, 99);
            this.inventoryGrid.MultiSelect = false;
            this.inventoryGrid.Name = "inventoryGrid";
            this.inventoryGrid.ReadOnly = true;
            this.inventoryGrid.RowTemplate.Height = 24;
            this.inventoryGrid.Size = new System.Drawing.Size(606, 532);
            this.inventoryGrid.TabIndex = 0;
            // 
            // ItemID
            // 
            this.ItemID.DataPropertyName = "ItemID";
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            this.ItemID.Width = 50;
            // 
            // btActForm2
            // 
            this.btActForm2.Location = new System.Drawing.Point(12, 12);
            this.btActForm2.Name = "btActForm2";
            this.btActForm2.Size = new System.Drawing.Size(95, 23);
            this.btActForm2.TabIndex = 3;
            this.btActForm2.Text = "Go Back";
            this.btActForm2.UseVisualStyleBackColor = true;
            this.btActForm2.Click += new System.EventHandler(this.btActForm2_Click);
            // 
            // headerLabel
            // 
            this.headerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerLabel.AutoSize = true;
            this.headerLabel.Location = new System.Drawing.Point(113, 18);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(243, 17);
            this.headerLabel.TabIndex = 5;
            this.headerLabel.Text = "Here you can add or remove an Item.";
            // 
            // inventLabel
            // 
            this.inventLabel.AutoSize = true;
            this.inventLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.inventLabel.Location = new System.Drawing.Point(359, 65);
            this.inventLabel.Name = "inventLabel";
            this.inventLabel.Size = new System.Drawing.Size(169, 25);
            this.inventLabel.TabIndex = 3;
            this.inventLabel.Text = "Items in invetory";
            // 
            // newItemPanel
            // 
            this.newItemPanel.ColumnCount = 2;
            this.newItemPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.1536F));
            this.newItemPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.8464F));
            this.newItemPanel.Controls.Add(this.boxPrice, 1, 3);
            this.newItemPanel.Controls.Add(this.boxDesc, 1, 2);
            this.newItemPanel.Controls.Add(this.nameLabel, 0, 0);
            this.newItemPanel.Controls.Add(this.categoryLabel, 0, 1);
            this.newItemPanel.Controls.Add(this.descriptionLabel, 0, 2);
            this.newItemPanel.Controls.Add(this.priceLabel, 0, 3);
            this.newItemPanel.Controls.Add(this.btnAddItem, 1, 4);
            this.newItemPanel.Controls.Add(this.boxName, 1, 0);
            this.newItemPanel.Controls.Add(this.boxCategory, 1, 1);
            this.newItemPanel.Location = new System.Drawing.Point(2, 34);
            this.newItemPanel.Name = "newItemPanel";
            this.newItemPanel.RowCount = 5;
            this.newItemPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.newItemPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.newItemPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.newItemPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.newItemPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.newItemPanel.Size = new System.Drawing.Size(314, 212);
            this.newItemPanel.TabIndex = 6;
            // 
            // boxPrice
            // 
            this.boxPrice.Location = new System.Drawing.Point(94, 138);
            this.boxPrice.Name = "boxPrice";
            this.boxPrice.Size = new System.Drawing.Size(75, 22);
            this.boxPrice.TabIndex = 8;
            // 
            // boxDesc
            // 
            this.boxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDesc.Location = new System.Drawing.Point(94, 87);
            this.boxDesc.Name = "boxDesc";
            this.boxDesc.Size = new System.Drawing.Size(217, 22);
            this.boxDesc.TabIndex = 7;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(77, 17);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Item name:";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new System.Drawing.Point(3, 42);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(69, 17);
            this.categoryLabel.TabIndex = 1;
            this.categoryLabel.Text = "Category:";
            this.categoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(3, 84);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(83, 17);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "Description:";
            this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Location = new System.Drawing.Point(3, 135);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(44, 17);
            this.priceLabel.TabIndex = 3;
            this.priceLabel.Text = "Price:";
            this.priceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddItem.Location = new System.Drawing.Point(231, 178);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(80, 30);
            this.btnAddItem.TabIndex = 4;
            this.btnAddItem.Text = "Add";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // boxName
            // 
            this.boxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxName.Location = new System.Drawing.Point(94, 3);
            this.boxName.Name = "boxName";
            this.boxName.Size = new System.Drawing.Size(217, 22);
            this.boxName.TabIndex = 5;
            // 
            // boxCategory
            // 
            this.boxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxCategory.DisplayMember = "CategoryID";
            this.boxCategory.FormattingEnabled = true;
            this.boxCategory.Location = new System.Drawing.Point(94, 45);
            this.boxCategory.Name = "boxCategory";
            this.boxCategory.Size = new System.Drawing.Size(217, 24);
            this.boxCategory.TabIndex = 9;
            this.boxCategory.ValueMember = "CategoryID";
            // 
            // newItemLabel
            // 
            this.newItemLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.newItemLabel.AutoSize = true;
            this.newItemLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.newItemLabel.Location = new System.Drawing.Point(3, 0);
            this.newItemLabel.Name = "newItemLabel";
            this.newItemLabel.Size = new System.Drawing.Size(156, 25);
            this.newItemLabel.TabIndex = 7;
            this.newItemLabel.Text = "New Item Form";
            // 
            // deleteLabel
            // 
            this.deleteLabel.AutoSize = true;
            this.deleteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deleteLabel.Location = new System.Drawing.Point(2, 3);
            this.deleteLabel.Name = "deleteLabel";
            this.deleteLabel.Size = new System.Drawing.Size(212, 25);
            this.deleteLabel.TabIndex = 8;
            this.deleteLabel.Text = "Delete Selected Item";
            // 
            // btnDelItem
            // 
            this.btnDelItem.Location = new System.Drawing.Point(238, 38);
            this.btnDelItem.Name = "btnDelItem";
            this.btnDelItem.Size = new System.Drawing.Size(80, 30);
            this.btnDelItem.TabIndex = 9;
            this.btnDelItem.Text = "Delete";
            this.btnDelItem.UseVisualStyleBackColor = true;
            this.btnDelItem.Click += new System.EventHandler(this.btnDelItem_Click);
            // 
            // boxDelInfo
            // 
            this.boxDelInfo.Location = new System.Drawing.Point(14, 42);
            this.boxDelInfo.Name = "boxDelInfo";
            this.boxDelInfo.ReadOnly = true;
            this.boxDelInfo.Size = new System.Drawing.Size(151, 22);
            this.boxDelInfo.TabIndex = 10;
            // 
            // btnNewFileBrowse
            // 
            this.btnNewFileBrowse.Location = new System.Drawing.Point(238, 36);
            this.btnNewFileBrowse.Name = "btnNewFileBrowse";
            this.btnNewFileBrowse.Size = new System.Drawing.Size(80, 30);
            this.btnNewFileBrowse.TabIndex = 11;
            this.btnNewFileBrowse.Text = "Browse";
            this.btnNewFileBrowse.UseVisualStyleBackColor = true;
            this.btnNewFileBrowse.Click += new System.EventHandler(this.btnNewFileBrowse_Click);
            // 
            // importLabel
            // 
            this.importLabel.AutoSize = true;
            this.importLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.importLabel.Location = new System.Drawing.Point(5, 6);
            this.importLabel.Name = "importLabel";
            this.importLabel.Size = new System.Drawing.Size(164, 25);
            this.importLabel.TabIndex = 12;
            this.importLabel.Text = "Import new Item";
            // 
            // textBoxFileContent
            // 
            this.textBoxFileContent.Location = new System.Drawing.Point(127, 103);
            this.textBoxFileContent.Multiline = true;
            this.textBoxFileContent.Name = "textBoxFileContent";
            this.textBoxFileContent.ReadOnly = true;
            this.textBoxFileContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFileContent.Size = new System.Drawing.Size(212, 94);
            this.textBoxFileContent.TabIndex = 13;
            // 
            // fileContLabel
            // 
            this.fileContLabel.AutoSize = true;
            this.fileContLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fileContLabel.Location = new System.Drawing.Point(124, 82);
            this.fileContLabel.Name = "fileContLabel";
            this.fileContLabel.Size = new System.Drawing.Size(88, 18);
            this.fileContLabel.TabIndex = 14;
            this.fileContLabel.Text = "File content:";
            // 
            // btnImportItem
            // 
            this.btnImportItem.Location = new System.Drawing.Point(8, 103);
            this.btnImportItem.Name = "btnImportItem";
            this.btnImportItem.Size = new System.Drawing.Size(115, 30);
            this.btnImportItem.TabIndex = 15;
            this.btnImportItem.Text = "Import .TXT";
            this.btnImportItem.UseVisualStyleBackColor = true;
            this.btnImportItem.Click += new System.EventHandler(this.btnImportItem_Click);
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(8, 40);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.ReadOnly = true;
            this.textBoxFilePath.Size = new System.Drawing.Size(220, 22);
            this.textBoxFilePath.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.deleteLabel);
            this.panel1.Controls.Add(this.btnDelItem);
            this.panel1.Controls.Add(this.boxDelInfo);
            this.panel1.Location = new System.Drawing.Point(12, 541);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 90);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnImportItemXML);
            this.panel2.Controls.Add(this.importLabel);
            this.panel2.Controls.Add(this.btnNewFileBrowse);
            this.panel2.Controls.Add(this.textBoxFilePath);
            this.panel2.Controls.Add(this.textBoxFileContent);
            this.panel2.Controls.Add(this.btnImportItem);
            this.panel2.Controls.Add(this.fileContLabel);
            this.panel2.Location = new System.Drawing.Point(12, 326);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(344, 209);
            this.panel2.TabIndex = 18;
            // 
            // btnImportItemXML
            // 
            this.btnImportItemXML.Location = new System.Drawing.Point(8, 150);
            this.btnImportItemXML.Name = "btnImportItemXML";
            this.btnImportItemXML.Size = new System.Drawing.Size(115, 30);
            this.btnImportItemXML.TabIndex = 17;
            this.btnImportItemXML.Text = "Import .XML";
            this.btnImportItemXML.UseVisualStyleBackColor = true;
            this.btnImportItemXML.Click += new System.EventHandler(this.btnImportItemXML_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.newItemLabel);
            this.panel3.Controls.Add(this.newItemPanel);
            this.panel3.Location = new System.Drawing.Point(12, 65);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(344, 255);
            this.panel3.TabIndex = 19;
            // 
            // radioHU
            // 
            this.radioHU.AutoSize = true;
            this.radioHU.Location = new System.Drawing.Point(758, 18);
            this.radioHU.Name = "radioHU";
            this.radioHU.Size = new System.Drawing.Size(76, 21);
            this.radioHU.TabIndex = 21;
            this.radioHU.TabStop = true;
            this.radioHU.Text = "Magyar";
            this.radioHU.UseVisualStyleBackColor = true;
            this.radioHU.CheckedChanged += new System.EventHandler(this.radioHU_CheckedChanged);
            // 
            // radioEN
            // 
            this.radioEN.AutoSize = true;
            this.radioEN.Location = new System.Drawing.Point(758, 45);
            this.radioEN.Name = "radioEN";
            this.radioEN.Size = new System.Drawing.Size(75, 21);
            this.radioEN.TabIndex = 22;
            this.radioEN.TabStop = true;
            this.radioEN.Text = "English";
            this.radioEN.UseVisualStyleBackColor = true;
            this.radioEN.CheckedChanged += new System.EventHandler(this.radioEN_CheckedChanged);
            // 
            // itemNameDataGridViewTextBoxColumn
            // 
            this.itemNameDataGridViewTextBoxColumn.DataPropertyName = "ItemName";
            this.itemNameDataGridViewTextBoxColumn.HeaderText = "Item name";
            this.itemNameDataGridViewTextBoxColumn.Name = "itemNameDataGridViewTextBoxColumn";
            this.itemNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // categoryNameDataGridViewTextBoxColumn
            // 
            this.categoryNameDataGridViewTextBoxColumn.DataPropertyName = "CategoryName";
            this.categoryNameDataGridViewTextBoxColumn.HeaderText = "Category";
            this.categoryNameDataGridViewTextBoxColumn.Name = "categoryNameDataGridViewTextBoxColumn";
            this.categoryNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // unitPriceDataGridViewTextBoxColumn
            // 
            this.unitPriceDataGridViewTextBoxColumn.DataPropertyName = "UnitPrice";
            this.unitPriceDataGridViewTextBoxColumn.HeaderText = "Price";
            this.unitPriceDataGridViewTextBoxColumn.Name = "unitPriceDataGridViewTextBoxColumn";
            this.unitPriceDataGridViewTextBoxColumn.ReadOnly = true;
            this.unitPriceDataGridViewTextBoxColumn.Width = 60;
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(RestClientWinForms.Models.Item);
            // 
            // InvetoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(984, 653);
            this.Controls.Add(this.radioEN);
            this.Controls.Add(this.radioHU);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.inventoryGrid);
            this.Controls.Add(this.inventLabel);
            this.Controls.Add(this.btActForm2);
            this.Controls.Add(this.headerLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1050, 700);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "InvetoryForm";
            this.Text = "Inventory";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inventoryGrid)).EndInit();
            this.newItemPanel.ResumeLayout(false);
            this.newItemPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView inventoryGrid;
        private System.Windows.Forms.BindingSource itemBindingSource;
        private System.Windows.Forms.Button btActForm2;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label inventLabel;
        private System.Windows.Forms.TableLayoutPanel newItemPanel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Label newItemLabel;
        private System.Windows.Forms.TextBox boxPrice;
        private System.Windows.Forms.TextBox boxDesc;
        private System.Windows.Forms.TextBox boxName;
        private System.Windows.Forms.ComboBox boxCategory;
        private System.Windows.Forms.Label deleteLabel;
        private System.Windows.Forms.Button btnDelItem;
        private System.Windows.Forms.TextBox boxDelInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnNewFileBrowse;
        private System.Windows.Forms.Label importLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox textBoxFileContent;
        private System.Windows.Forms.Label fileContLabel;
        private System.Windows.Forms.Button btnImportItem;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnImportItemXML;
        private System.Windows.Forms.RadioButton radioHU;
        private System.Windows.Forms.RadioButton radioEN;






    }
}

