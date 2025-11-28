using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DijkstraFlightFinder
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cboStart;
        private ComboBox cboEnd;
        private Button btnFind;
        private Button btnReset;
        private ListBox lstSteps;
        private Label lblResult;
        private Panel pnlGraph;
        private Label lblTitle;
        private Label lblStart;
        private Label lblEnd;
        private Label lblSteps;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cboStart = new ComboBox();
            this.cboEnd = new ComboBox();
            this.btnFind = new Button();
            this.btnReset = new Button();
            this.lstSteps = new ListBox();
            this.lblResult = new Label();
            this.pnlGraph = new Panel();
            this.lblTitle = new Label();
            this.lblStart = new Label();
            this.lblEnd = new Label();
            this.lblSteps = new Label();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkBlue;
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(400, 26);
            this.lblTitle.Text = "TÌM CHUYẾN BAY RẺ NHẤT";

            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new Point(20, 60);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new Size(60, 13);
            this.lblStart.Text = "Điểm đi:";

            // 
            // cboStart
            // 
            this.cboStart.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboStart.FormattingEnabled = true;
            this.cboStart.Location = new Point(100, 57);
            this.cboStart.Name = "cboStart";
            this.cboStart.Size = new Size(150, 21);

            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new Point(270, 60);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new Size(70, 13);
            this.lblEnd.Text = "Điểm đến:";

            // 
            // cboEnd
            // 
            this.cboEnd.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboEnd.FormattingEnabled = true;
            this.cboEnd.Location = new Point(350, 57);
            this.cboEnd.Name = "cboEnd";
            this.cboEnd.Size = new Size(150, 21);

            // 
            // btnFind
            // 
            this.btnFind.BackColor = Color.LightGreen;
            this.btnFind.Font = new Font("Arial", 9F, FontStyle.Bold);
            this.btnFind.Location = new Point(520, 55);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new Size(120, 30);
            this.btnFind.Text = "Tìm Đường Bay";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new EventHandler(this.BtnFind_Click);

            // 
            // btnReset
            // 
            this.btnReset.BackColor = Color.LightCoral;
            this.btnReset.Font = new Font("Arial", 9F, FontStyle.Bold);
            this.btnReset.Location = new Point(650, 55);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new Size(100, 30);
            this.btnReset.Text = "Làm Mới";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new EventHandler(this.BtnReset_Click);

            // 
            // lblResult
            // 
            this.lblResult.BackColor = Color.LightYellow;
            this.lblResult.BorderStyle = BorderStyle.FixedSingle;
            this.lblResult.Font = new Font("Arial", 10F, FontStyle.Bold);
            this.lblResult.ForeColor = Color.DarkGreen;
            this.lblResult.Location = new Point(20, 100);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new Size(750, 60);
            this.lblResult.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // lblSteps
            // 
            this.lblSteps.AutoSize = true;
            this.lblSteps.Font = new Font("Arial", 10F, FontStyle.Bold);
            this.lblSteps.Location = new Point(800, 60);
            this.lblSteps.Name = "lblSteps";
            this.lblSteps.Size = new Size(160, 16);
            this.lblSteps.Text = "Các bước thực hiện:";

            // 
            // lstSteps
            // 
            this.lstSteps.Font = new Font("Courier New", 9F);
            this.lstSteps.FormattingEnabled = true;
            this.lstSteps.ItemHeight = 15;
            this.lstSteps.Location = new Point(800, 85);
            this.lstSteps.Name = "lstSteps";
            this.lstSteps.Size = new Size(370, 550);

            // 
            // pnlGraph
            // 
            this.pnlGraph.BackColor = Color.White;
            this.pnlGraph.BorderStyle = BorderStyle.FixedSingle;
            this.pnlGraph.Location = new Point(20, 170);
            this.pnlGraph.Name = "pnlGraph";
            this.pnlGraph.Size = new Size(750, 470);
            this.pnlGraph.Paint += new PaintEventHandler(this.PnlGraph_Paint);

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 700);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.cboStart);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.cboEnd);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblSteps);
            this.Controls.Add(this.lstSteps);
            this.Controls.Add(this.pnlGraph);
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Tìm Chuyến Bay Rẻ Nhất - Thuật Toán Dijkstra";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
