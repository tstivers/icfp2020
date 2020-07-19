namespace ui
{
    partial class ContestForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.colorsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.btnCacheKill = new System.Windows.Forms.Button();
            this.pnlColors = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.ColorPicker = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.colorsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 654);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(929, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.colorsPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(929, 654);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // colorsPanel
            // 
            this.colorsPanel.Controls.Add(this.chkGrid);
            this.colorsPanel.Controls.Add(this.btnCacheKill);
            this.colorsPanel.Controls.Add(this.pnlColors);
            this.colorsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorsPanel.Location = new System.Drawing.Point(777, 3);
            this.colorsPanel.Name = "colorsPanel";
            this.colorsPanel.Size = new System.Drawing.Size(149, 539);
            this.colorsPanel.TabIndex = 0;
            // 
            // chkGrid
            // 
            this.chkGrid.AutoSize = true;
            this.chkGrid.Location = new System.Drawing.Point(3, 3);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.Size = new System.Drawing.Size(80, 19);
            this.chkGrid.TabIndex = 0;
            this.chkGrid.Text = "Show Grid";
            this.chkGrid.UseVisualStyleBackColor = true;
            this.chkGrid.CheckedChanged += new System.EventHandler(this.chkGrid_CheckedChanged);
            // 
            // btnCacheKill
            // 
            this.btnCacheKill.Location = new System.Drawing.Point(3, 28);
            this.btnCacheKill.Name = "btnCacheKill";
            this.btnCacheKill.Size = new System.Drawing.Size(75, 23);
            this.btnCacheKill.TabIndex = 1;
            this.btnCacheKill.Text = "Kill Cache";
            this.btnCacheKill.UseVisualStyleBackColor = true;
            this.btnCacheKill.Click += new System.EventHandler(this.btnCacheKill_Click);
            // 
            // pnlColors
            // 
            this.pnlColors.Location = new System.Drawing.Point(3, 57);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Size = new System.Drawing.Size(146, 248);
            this.pnlColors.TabIndex = 2;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(768, 539);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // ContestForm
            // 
            this.ClientSize = new System.Drawing.Size(929, 676);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Name = "ContestForm";
            this.Text = "Howdy";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.colorsPanel.ResumeLayout(false);
            this.colorsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel colorsPanel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.ColorDialog ColorPicker;
        private System.Windows.Forms.Button btnCacheKill;
        private System.Windows.Forms.FlowLayoutPanel pnlColors;
    }
}

