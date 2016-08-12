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
			this.customPanel1 = new ColorMatrixViewer.CustomPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.resultMatrixBox = new ColorMatrixViewer.MatrixBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ClearMatricesBtn = new System.Windows.Forms.Button();
			this.showResultMatrixBtn = new System.Windows.Forms.Button();
			this.AddMatrixBtn = new System.Windows.Forms.Button();
			this.resultMatrixContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageDiff1 = new ColorMatrixViewer.ImageDiff();
			this.imageContextMenu.SuspendLayout();
			this.leftColumnPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.customPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.resultMatrixContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageContextMenu
			// 
			this.imageContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.imageContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadAnImageToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadTheDefaultImageToolStripMenuItem});
			this.imageContextMenu.Name = "imageContextMenu";
			this.imageContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.imageContextMenu.Size = new System.Drawing.Size(240, 62);
			// 
			// loadAnImageToolStripMenuItem
			// 
			this.loadAnImageToolStripMenuItem.Name = "loadAnImageToolStripMenuItem";
			this.loadAnImageToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
			this.loadAnImageToolStripMenuItem.Text = "Load an image...";
			this.loadAnImageToolStripMenuItem.Click += new System.EventHandler(this.loadAnImageToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
			// 
			// loadTheDefaultImageToolStripMenuItem
			// 
			this.loadTheDefaultImageToolStripMenuItem.Name = "loadTheDefaultImageToolStripMenuItem";
			this.loadTheDefaultImageToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
			this.loadTheDefaultImageToolStripMenuItem.Text = "Load the default image";
			this.loadTheDefaultImageToolStripMenuItem.Click += new System.EventHandler(this.loadTheDefaultImageToolStripMenuItem_Click);
			// 
			// leftColumnPanel
			// 
			this.leftColumnPanel.Controls.Add(this.splitContainer1);
			this.leftColumnPanel.Controls.Add(this.panel2);
			this.leftColumnPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.leftColumnPanel.Location = new System.Drawing.Point(0, 0);
			this.leftColumnPanel.Margin = new System.Windows.Forms.Padding(4);
			this.leftColumnPanel.Name = "leftColumnPanel";
			this.leftColumnPanel.Size = new System.Drawing.Size(417, 518);
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
			this.splitContainer1.Panel1.Controls.Add(this.customPanel1);
			this.splitContainer1.Panel1MinSize = 0;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.resultMatrixBox);
			this.splitContainer1.Panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel2_MouseClick);
			this.splitContainer1.Panel2MinSize = 0;
			this.splitContainer1.Size = new System.Drawing.Size(417, 481);
			this.splitContainer1.SplitterDistance = 376;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 0;
			// 
			// customPanel1
			// 
			this.customPanel1.AutoScroll = true;
			this.customPanel1.Controls.Add(this.tableLayoutPanel1);
			this.customPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.customPanel1.Location = new System.Drawing.Point(0, 0);
			this.customPanel1.Margin = new System.Windows.Forms.Padding(4);
			this.customPanel1.Name = "customPanel1";
			this.customPanel1.Size = new System.Drawing.Size(417, 376);
			this.customPanel1.TabIndex = 4;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AllowDrop = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(391, 481);
			this.tableLayoutPanel1.TabIndex = 3;
			this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
			this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
			this.tableLayoutPanel1.DragLeave += new System.EventHandler(this.tableLayoutPanel1_DragLeave);
			// 
			// resultMatrixBox
			// 
			this.resultMatrixBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.resultMatrixBox.Enabled = false;
			this.resultMatrixBox.Location = new System.Drawing.Point(48, 2);
			this.resultMatrixBox.Margin = new System.Windows.Forms.Padding(4);
			this.resultMatrixBox.Name = "resultMatrixBox";
			this.resultMatrixBox.Size = new System.Drawing.Size(317, 108);
			this.resultMatrixBox.TabIndex = 0;
			this.resultMatrixBox.Text = "matrixBox1";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ClearMatricesBtn);
			this.panel2.Controls.Add(this.showResultMatrixBtn);
			this.panel2.Controls.Add(this.AddMatrixBtn);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 481);
			this.panel2.Margin = new System.Windows.Forms.Padding(4);
			this.panel2.MinimumSize = new System.Drawing.Size(5, 37);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(417, 37);
			this.panel2.TabIndex = 4;
			// 
			// ClearMatricesBtn
			// 
			this.ClearMatricesBtn.Location = new System.Drawing.Point(120, 4);
			this.ClearMatricesBtn.Margin = new System.Windows.Forms.Padding(4);
			this.ClearMatricesBtn.Name = "ClearMatricesBtn";
			this.ClearMatricesBtn.Size = new System.Drawing.Size(103, 28);
			this.ClearMatricesBtn.TabIndex = 2;
			this.ClearMatricesBtn.Text = "Clear list...";
			this.ClearMatricesBtn.UseVisualStyleBackColor = true;
			this.ClearMatricesBtn.Click += new System.EventHandler(this.ClearMatricesBtn_Click);
			// 
			// showResultMatrixBtn
			// 
			this.showResultMatrixBtn.Location = new System.Drawing.Point(277, 4);
			this.showResultMatrixBtn.Margin = new System.Windows.Forms.Padding(4);
			this.showResultMatrixBtn.Name = "showResultMatrixBtn";
			this.showResultMatrixBtn.Size = new System.Drawing.Size(133, 28);
			this.showResultMatrixBtn.TabIndex = 1;
			this.showResultMatrixBtn.Text = "Hide result matrix";
			this.showResultMatrixBtn.UseVisualStyleBackColor = true;
			this.showResultMatrixBtn.Click += new System.EventHandler(this.showResultMatrixBtn_Click);
			// 
			// AddMatrixBtn
			// 
			this.AddMatrixBtn.Location = new System.Drawing.Point(7, 4);
			this.AddMatrixBtn.Margin = new System.Windows.Forms.Padding(4);
			this.AddMatrixBtn.Name = "AddMatrixBtn";
			this.AddMatrixBtn.Size = new System.Drawing.Size(103, 28);
			this.AddMatrixBtn.TabIndex = 0;
			this.AddMatrixBtn.Text = "Add a matrix";
			this.AddMatrixBtn.UseVisualStyleBackColor = true;
			this.AddMatrixBtn.Click += new System.EventHandler(this.AddMatrixBtn_Click);
			// 
			// resultMatrixContextMenu
			// 
			this.resultMatrixContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.resultMatrixContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem});
			this.resultMatrixContextMenu.Name = "contextMenuStrip1";
			this.resultMatrixContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.resultMatrixContextMenu.Size = new System.Drawing.Size(207, 30);
			// 
			// copyToClipboardToolStripMenuItem
			// 
			this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
			this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
			this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
			this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
			// 
			// imageDiff1
			// 
			this.imageDiff1.ContextMenuStrip = this.imageContextMenu;
			this.imageDiff1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageDiff1.Location = new System.Drawing.Point(417, 0);
			this.imageDiff1.Margin = new System.Windows.Forms.Padding(4);
			this.imageDiff1.Name = "imageDiff1";
			this.imageDiff1.Size = new System.Drawing.Size(692, 518);
			this.imageDiff1.SplitterPosition = 0.5D;
			this.imageDiff1.TabIndex = 1;
			this.imageDiff1.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1109, 518);
			this.Controls.Add(this.imageDiff1);
			this.Controls.Add(this.leftColumnPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "ColorMatrix Viewer";
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.imageContextMenu.ResumeLayout(false);
			this.leftColumnPanel.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.customPanel1.ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
		private CustomPanel customPanel1;
		private System.Windows.Forms.Button ClearMatricesBtn;
	}
}

