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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cboStart = new System.Windows.Forms.ComboBox();
            this.cboEnd = new System.Windows.Forms.ComboBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lstSteps = new System.Windows.Forms.ListBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.pnlGraph = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblSteps = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboStart
            // 
            this.cboStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStart.FormattingEnabled = true;
            this.cboStart.Location = new System.Drawing.Point(133, 70);
            this.cboStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboStart.Name = "cboStart";
            this.cboStart.Size = new System.Drawing.Size(199, 28);
            this.cboStart.TabIndex = 2;
            // 
            // cboEnd
            // 
            this.cboEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEnd.FormattingEnabled = true;
            this.cboEnd.Location = new System.Drawing.Point(467, 70);
            this.cboEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboEnd.Name = "cboEnd";
            this.cboEnd.Size = new System.Drawing.Size(199, 28);
            this.cboEnd.TabIndex = 4;
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.LightGreen;
            this.btnFind.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnFind.Location = new System.Drawing.Point(693, 68);
            this.btnFind.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(160, 37);
            this.btnFind.TabIndex = 5;
            this.btnFind.Text = "Tìm Đường Bay";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.LightCoral;
            this.btnReset.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnReset.Location = new System.Drawing.Point(867, 68);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(133, 37);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Làm Mới";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // lstSteps
            // 
            this.lstSteps.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.lstSteps.FormattingEnabled = true;
            this.lstSteps.ItemHeight = 22;
            this.lstSteps.Location = new System.Drawing.Point(1055, 123);
            this.lstSteps.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstSteps.Name = "lstSteps";
            this.lstSteps.Size = new System.Drawing.Size(492, 664);
            this.lstSteps.TabIndex = 9;
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.LightYellow;
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblResult.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblResult.Location = new System.Drawing.Point(27, 123);
            this.lblResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(999, 73);
            this.lblResult.TabIndex = 7;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlGraph
            // 
            this.pnlGraph.BackColor = System.Drawing.Color.White;
            this.pnlGraph.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlGraph.BackgroundImage")));
            this.pnlGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGraph.Location = new System.Drawing.Point(27, 209);
            this.pnlGraph.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlGraph.Name = "pnlGraph";
            this.pnlGraph.Size = new System.Drawing.Size(999, 578);
            this.pnlGraph.TabIndex = 10;
            this.pnlGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlGraph_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(27, 25);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(379, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TÌM CHUYẾN BAY RẺ NHẤT";
            // 
            // lblStart
            // 
            this.lblStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.Location = new System.Drawing.Point(27, 70);
            this.lblStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(98, 23);
            this.lblStart.TabIndex = 1;
            this.lblStart.Text = "Điểm đi:";
            // 
            // lblEnd
            // 
            this.lblEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnd.Location = new System.Drawing.Point(361, 70);
            this.lblEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(98, 23);
            this.lblEnd.TabIndex = 3;
            this.lblEnd.Text = "Điểm đến:";
            // 
            // lblSteps
            // 
            this.lblSteps.AutoSize = true;
            this.lblSteps.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblSteps.Location = new System.Drawing.Point(1051, 100);
            this.lblSteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSteps.Name = "lblSteps";
            this.lblSteps.Size = new System.Drawing.Size(173, 19);
            this.lblSteps.TabIndex = 8;
            this.lblSteps.Text = "Các bước thực hiện:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1283, 752);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm Chuyến Bay Rẻ Nhất - Thuật Toán Dijkstra";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
