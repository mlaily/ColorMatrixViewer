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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.combineTabPage = new System.Windows.Forms.TabPage();
			this.editTabPage = new System.Windows.Forms.TabPage();
			this.matrixBox1 = new ColorMatrixViewer.MatrixBox();
			this.imageDiff1 = new ColorMatrixViewer.ImageDiff();
			this.imageContextMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.editTabPage.SuspendLayout();
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
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.imageDiff1);
			this.splitContainer1.Size = new System.Drawing.Size(832, 421);
			this.splitContainer1.SplitterDistance = 258;
			this.splitContainer1.SplitterWidth = 8;
			this.splitContainer1.TabIndex = 3;
			this.splitContainer1.DoubleClick += new System.EventHandler(this.splitContainer1_DoubleClick);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.combineTabPage);
			this.tabControl1.Controls.Add(this.editTabPage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(258, 421);
			this.tabControl1.TabIndex = 3;
			// 
			// combineTabPage
			// 
			this.combineTabPage.Location = new System.Drawing.Point(4, 22);
			this.combineTabPage.Name = "combineTabPage";
			this.combineTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.combineTabPage.Size = new System.Drawing.Size(250, 395);
			this.combineTabPage.TabIndex = 1;
			this.combineTabPage.Text = "Combine";
			this.combineTabPage.UseVisualStyleBackColor = true;
			// 
			// editTabPage
			// 
			this.editTabPage.Controls.Add(this.matrixBox1);
			this.editTabPage.Location = new System.Drawing.Point(4, 22);
			this.editTabPage.Name = "editTabPage";
			this.editTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.editTabPage.Size = new System.Drawing.Size(250, 395);
			this.editTabPage.TabIndex = 0;
			this.editTabPage.Text = "Edit";
			this.editTabPage.UseVisualStyleBackColor = true;
			// 
			// matrixBox1
			// 
			this.matrixBox1.Location = new System.Drawing.Point(6, 6);
			this.matrixBox1.Name = "matrixBox1";
			this.matrixBox1.Size = new System.Drawing.Size(238, 88);
			this.matrixBox1.TabIndex = 2;
			this.matrixBox1.Text = "matrixBox1";
			// 
			// imageDiff1
			// 
			this.imageDiff1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.imageDiff1.ContextMenuStrip = this.imageContextMenu;
			this.imageDiff1.Location = new System.Drawing.Point(0, 20);
			this.imageDiff1.Name = "imageDiff1";
			this.imageDiff1.Size = new System.Drawing.Size(558, 401);
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
			this.Controls.Add(this.splitContainer1);
			this.Name = "Form1";
			this.Text = "ColorMatrix Viewer";
			this.imageContextMenu.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.editTabPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ImageDiff imageDiff1;
		private MatrixBox matrixBox1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage editTabPage;
		private System.Windows.Forms.TabPage combineTabPage;
		private System.Windows.Forms.ContextMenuStrip imageContextMenu;
		private System.Windows.Forms.ToolStripMenuItem loadAnImageToolStripMenuItem;
	}
}

