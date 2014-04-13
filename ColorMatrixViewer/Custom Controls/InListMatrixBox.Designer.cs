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
			this.matrixTemplateList = new System.Windows.Forms.ListBox();
			this.matrixBox1 = new ColorMatrixViewer.MatrixBox();
			this.plusBtn = new System.Windows.Forms.Button();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// matrixTemplateList
			// 
			this.matrixTemplateList.FormattingEnabled = true;
			this.matrixTemplateList.Location = new System.Drawing.Point(3, 0);
			this.matrixTemplateList.Name = "matrixTemplateList";
			this.matrixTemplateList.Size = new System.Drawing.Size(238, 95);
			this.matrixTemplateList.TabIndex = 3;
			this.matrixTemplateList.Visible = false;
			this.matrixTemplateList.DoubleClick += new System.EventHandler(this.matrixTemplateList_DoubleClick);
			// 
			// matrixBox1
			// 
			this.matrixBox1.Location = new System.Drawing.Point(3, 3);
			this.matrixBox1.Name = "matrixBox1";
			this.matrixBox1.Size = new System.Drawing.Size(238, 88);
			this.matrixBox1.TabIndex = 0;
			this.matrixBox1.Text = "matrixBox1";
			// 
			// plusBtn
			// 
			this.plusBtn.Location = new System.Drawing.Point(245, 36);
			this.plusBtn.Name = "plusBtn";
			this.plusBtn.Size = new System.Drawing.Size(25, 25);
			this.plusBtn.TabIndex = 2;
			this.plusBtn.Text = "+";
			this.plusBtn.UseVisualStyleBackColor = true;
			this.plusBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.plusBtn_MouseClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.toolStripSeparator1,
            this.resetToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.contextMenuStrip1.Size = new System.Drawing.Size(110, 54);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.loadToolStripMenuItem.Text = "Load...";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(106, 6);
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
			this.resetToolStripMenuItem.Text = "Reset";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
			// 
			// InListMatrixBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.matrixTemplateList);
			this.Controls.Add(this.plusBtn);
			this.Controls.Add(this.matrixBox1);
			this.Name = "InListMatrixBox";
			this.Size = new System.Drawing.Size(274, 95);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.InListMatrixBox_MouseClick);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private MatrixBox matrixBox1;
		private System.Windows.Forms.ListBox matrixTemplateList;
		private System.Windows.Forms.Button plusBtn;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
	}
}
