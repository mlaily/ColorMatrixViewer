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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.imageContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.loadAnImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.loadTheDefaultImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.leftColumnPanel = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.resultMatrixBox = new ColorMatrixViewer.MatrixBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.showResultMatrixBtn = new System.Windows.Forms.Button();
			this.AddMatrixBtn = new System.Windows.Forms.Button();
			this.resultMatrixContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageDiff1 = new ColorMatrixViewer.ImageDiff();
			this.imageContextMenu.SuspendLayout();
			this.leftColumnPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.resultMatrixContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageContextMenu
			// 
			this.imageContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadAnImageToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadTheDefaultImageToolStripMenuItem});
			this.imageContextMenu.Name = "imageContextMenu";
			this.imageContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.imageContextMenu.Size = new System.Drawing.Size(197, 54);
			// 
			// loadAnImageToolStripMenuItem
			// 
			this.loadAnImageToolStripMenuItem.Name = "loadAnImageToolStripMenuItem";
			this.loadAnImageToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.loadAnImageToolStripMenuItem.Text = "Load an image...";
			this.loadAnImageToolStripMenuItem.Click += new System.EventHandler(this.loadAnImageToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
			// 
			// loadTheDefaultImageToolStripMenuItem
			// 
			this.loadTheDefaultImageToolStripMenuItem.Name = "loadTheDefaultImageToolStripMenuItem";
			this.loadTheDefaultImageToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.loadTheDefaultImageToolStripMenuItem.Text = "Load the default image";
			this.loadTheDefaultImageToolStripMenuItem.Click += new System.EventHandler(this.loadTheDefaultImageToolStripMenuItem_Click);
			// 
			// leftColumnPanel
			// 
			this.leftColumnPanel.Controls.Add(this.splitContainer1);
			this.leftColumnPanel.Controls.Add(this.panel2);
			this.leftColumnPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftColumnPanel.Location = new System.Drawing.Point(0, 0);
			this.leftColumnPanel.Name = "leftColumnPanel";
			this.leftColumnPanel.Size = new System.Drawing.Size(313, 421);
			this.leftColumnPanel.TabIndex = 4;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.AutoScroll = true;
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Panel1MinSize = 0;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.resultMatrixBox);
			this.splitContainer1.Panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel2_MouseClick);
			this.splitContainer1.Panel2MinSize = 0;
			this.splitContainer1.Size = new System.Drawing.Size(313, 391);
			this.splitContainer1.SplitterDistance = 253;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AllowDrop = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(293, 391);
			this.tableLayoutPanel1.TabIndex = 3;
			this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
			this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
			this.tableLayoutPanel1.DragLeave += new System.EventHandler(this.tableLayoutPanel1_DragLeave);
			// 
			// resultMatrixBox
			// 
			this.resultMatrixBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.resultMatrixBox.Enabled = false;
			this.resultMatrixBox.Location = new System.Drawing.Point(36, 42);
			this.resultMatrixBox.Name = "resultMatrixBox";
			this.resultMatrixBox.Size = new System.Drawing.Size(238, 88);
			this.resultMatrixBox.TabIndex = 0;
			this.resultMatrixBox.Text = "matrixBox1";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.showResultMatrixBtn);
			this.panel2.Controls.Add(this.AddMatrixBtn);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 391);
			this.panel2.MinimumSize = new System.Drawing.Size(4, 30);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(313, 30);
			this.panel2.TabIndex = 4;
			// 
			// showResultMatrixBtn
			// 
			this.showResultMatrixBtn.Location = new System.Drawing.Point(171, 3);
			this.showResultMatrixBtn.Name = "showResultMatrixBtn";
			this.showResultMatrixBtn.Size = new System.Drawing.Size(118, 23);
			this.showResultMatrixBtn.TabIndex = 1;
			this.showResultMatrixBtn.Text = "Hide resulting matrix";
			this.showResultMatrixBtn.UseVisualStyleBackColor = true;
			this.showResultMatrixBtn.Click += new System.EventHandler(this.showResultMatrixBtn_Click);
			// 
			// AddMatrixBtn
			// 
			this.AddMatrixBtn.Location = new System.Drawing.Point(12, 3);
			this.AddMatrixBtn.Name = "AddMatrixBtn";
			this.AddMatrixBtn.Size = new System.Drawing.Size(118, 23);
			this.AddMatrixBtn.TabIndex = 0;
			this.AddMatrixBtn.Text = "Add a matrix";
			this.AddMatrixBtn.UseVisualStyleBackColor = true;
			this.AddMatrixBtn.Click += new System.EventHandler(this.AddMatrixBtn_Click);
			// 
			// resultMatrixContextMenu
			// 
			this.resultMatrixContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
			this.resultMatrixContextMenu.Name = "contextMenuStrip1";
			this.resultMatrixContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.resultMatrixContextMenu.Size = new System.Drawing.Size(108, 26);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.saveToolStripMenuItem.Text = "Save...";
			// 
			// imageDiff1
			// 
			this.imageDiff1.ContextMenuStrip = this.imageContextMenu;
			this.imageDiff1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageDiff1.Location = new System.Drawing.Point(313, 0);
			this.imageDiff1.Name = "imageDiff1";
			this.imageDiff1.Size = new System.Drawing.Size(519, 421);
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
			this.Controls.Add(this.leftColumnPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "ColorMatrix Viewer";
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.imageContextMenu.ResumeLayout(false);
			this.leftColumnPanel.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.resultMatrixContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ImageDiff imageDiff1;
		private System.Windows.Forms.ContextMenuStrip imageContextMenu;
		private System.Windows.Forms.ToolStripMenuItem loadAnImageToolStripMenuItem;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel leftColumnPanel;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button AddMatrixBtn;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem loadTheDefaultImageToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button showResultMatrixBtn;
		private MatrixBox resultMatrixBox;
		private System.Windows.Forms.ContextMenuStrip resultMatrixContextMenu;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
	}
}

