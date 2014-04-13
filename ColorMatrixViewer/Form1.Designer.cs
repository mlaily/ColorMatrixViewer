namespace ColorMatrixViewer
{
	partial class Form1
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
			this.imageContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.loadAnImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel3 = new ColorMatrixViewer.CustomPanel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.removeMatrixBtn = new System.Windows.Forms.Button();
			this.AddMatrixBtn = new System.Windows.Forms.Button();
			this.imageDiff1 = new ColorMatrixViewer.ImageDiff();
			this.imageContextMenu.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageContextMenu
			// 
			this.imageContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadAnImageToolStripMenuItem});
			this.imageContextMenu.Name = "imageContextMenu";
			this.imageContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.imageContextMenu.Size = new System.Drawing.Size(162, 26);
			// 
			// loadAnImageToolStripMenuItem
			// 
			this.loadAnImageToolStripMenuItem.Name = "loadAnImageToolStripMenuItem";
			this.loadAnImageToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.loadAnImageToolStripMenuItem.Text = "Load an image...";
			this.loadAnImageToolStripMenuItem.Click += new System.EventHandler(this.loadAnImageToolStripMenuItem_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(278, 366);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(295, 421);
			this.panel1.TabIndex = 4;
			// 
			// panel3
			// 
			this.panel3.AutoScroll = true;
			this.panel3.Controls.Add(this.tableLayoutPanel1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(295, 366);
			this.panel3.TabIndex = 5;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.removeMatrixBtn);
			this.panel2.Controls.Add(this.AddMatrixBtn);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 366);
			this.panel2.MinimumSize = new System.Drawing.Size(0, 50);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(295, 55);
			this.panel2.TabIndex = 4;
			// 
			// removeMatrixBtn
			// 
			this.removeMatrixBtn.Location = new System.Drawing.Point(130, 24);
			this.removeMatrixBtn.Name = "removeMatrixBtn";
			this.removeMatrixBtn.Size = new System.Drawing.Size(75, 23);
			this.removeMatrixBtn.TabIndex = 1;
			this.removeMatrixBtn.Text = "Remove";
			this.removeMatrixBtn.UseVisualStyleBackColor = true;
			this.removeMatrixBtn.Click += new System.EventHandler(this.removeMatrixBtn_Click);
			// 
			// AddMatrixBtn
			// 
			this.AddMatrixBtn.Location = new System.Drawing.Point(38, 24);
			this.AddMatrixBtn.Name = "AddMatrixBtn";
			this.AddMatrixBtn.Size = new System.Drawing.Size(75, 23);
			this.AddMatrixBtn.TabIndex = 0;
			this.AddMatrixBtn.Text = "Add";
			this.AddMatrixBtn.UseVisualStyleBackColor = true;
			this.AddMatrixBtn.Click += new System.EventHandler(this.AddMatrixBtn_Click);
			// 
			// imageDiff1
			// 
			this.imageDiff1.ContextMenuStrip = this.imageContextMenu;
			this.imageDiff1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageDiff1.Location = new System.Drawing.Point(295, 0);
			this.imageDiff1.Name = "imageDiff1";
			this.imageDiff1.Size = new System.Drawing.Size(537, 421);
			this.imageDiff1.SplitterPosition = 0.5D;
			this.imageDiff1.TabIndex = 1;
			this.imageDiff1.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.ClientSize = new System.Drawing.Size(832, 421);
			this.Controls.Add(this.imageDiff1);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "ColorMatrix Viewer";
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.imageContextMenu.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ImageDiff imageDiff1;
		private System.Windows.Forms.ContextMenuStrip imageContextMenu;
		private System.Windows.Forms.ToolStripMenuItem loadAnImageToolStripMenuItem;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button removeMatrixBtn;
		private System.Windows.Forms.Button AddMatrixBtn;
		private CustomPanel panel3;
	}
}

