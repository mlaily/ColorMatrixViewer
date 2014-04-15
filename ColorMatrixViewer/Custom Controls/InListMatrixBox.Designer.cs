namespace ColorMatrixViewer
{
	partial class InListMatrixBox
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.plusBtn = new System.Windows.Forms.Button();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disableEnableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.matrixBox1 = new ColorMatrixViewer.MatrixBox();
			this.gripPanel = new ColorMatrixViewer.GripPanel();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// plusBtn
			// 
			this.plusBtn.Location = new System.Drawing.Point(262, 35);
			this.plusBtn.Name = "plusBtn";
			this.plusBtn.Size = new System.Drawing.Size(25, 25);
			this.plusBtn.TabIndex = 2;
			this.plusBtn.Text = "...";
			this.plusBtn.UseVisualStyleBackColor = true;
			this.plusBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.plusBtn_MouseClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.disableEnableToolStripMenuItem,
            this.toolStripSeparator2,
            this.removeToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.contextMenuStrip1.Size = new System.Drawing.Size(118, 76);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.loadToolStripMenuItem.Text = "Load";
			// 
			// disableEnableToolStripMenuItem
			// 
			this.disableEnableToolStripMenuItem.Name = "disableEnableToolStripMenuItem";
			this.disableEnableToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.disableEnableToolStripMenuItem.Text = "Disable";
			this.disableEnableToolStripMenuItem.Click += new System.EventHandler(this.disableEnableToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(114, 6);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem1});
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.removeToolStripMenuItem.Text = "Remove";
			// 
			// removeToolStripMenuItem1
			// 
			this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
			this.removeToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
			this.removeToolStripMenuItem1.Text = "Remove!";
			this.removeToolStripMenuItem1.Click += new System.EventHandler(this.removeToolStripMenuItem1_Click);
			// 
			// matrixBox1
			// 
			this.matrixBox1.Location = new System.Drawing.Point(21, 3);
			this.matrixBox1.Name = "matrixBox1";
			this.matrixBox1.Size = new System.Drawing.Size(238, 88);
			this.matrixBox1.TabIndex = 0;
			this.matrixBox1.Text = "matrixBox1";
			// 
			// gripPanel
			// 
			this.gripPanel.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.gripPanel.Location = new System.Drawing.Point(3, 3);
			this.gripPanel.Name = "gripPanel";
			this.gripPanel.Size = new System.Drawing.Size(15, 88);
			this.gripPanel.TabIndex = 3;
			this.gripPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gripPanel_MouseDown);
			// 
			// InListMatrixBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gripPanel);
			this.Controls.Add(this.plusBtn);
			this.Controls.Add(this.matrixBox1);
			this.Name = "InListMatrixBox";
			this.Size = new System.Drawing.Size(290, 95);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private MatrixBox matrixBox1;
		private System.Windows.Forms.Button plusBtn;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem disableEnableToolStripMenuItem;
		private GripPanel gripPanel;
	}
}
