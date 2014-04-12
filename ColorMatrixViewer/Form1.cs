using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorMatrixViewer
{
	public partial class Form1 : Form
	{

		public Form1()
		{
			InitializeComponent();
			tableLayoutPanel1.RowCount = 0;
			tableLayoutPanel1.RowStyles.Clear();
			//http://stackoverflow.com/questions/2197452/how-to-disable-horizontal-scrollbar-for-table-panel-in-winforms
			tableLayoutPanel1.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);

			matrixBox1.MatrixChanged += matrixBox1_MatrixChanged;
		}

		void matrixBox1_MatrixChanged(object sender, EventArgs e)
		{
			ApplyMatrix();
		}

		private void ApplyMatrix()
		{
			if (imageDiff1.FirstImage != null)
			{
				imageDiff1.SetImages(second: Util.ApplyColorMatrix(imageDiff1.FirstImage, matrixBox1.Matrix));
			}
		}

		private void loadAnImageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var dialog = new OpenFileDialog())
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					imageDiff1.SetImages(Bitmap.FromFile(dialog.FileName));
					ApplyMatrix();
				}
			}
		}

		private void resetMatrixToolStripMenuItem_Click(object sender, EventArgs e)
		{
			matrixBox1.ResetMatrix();
			ApplyMatrix();
		}

		private void AddMatrixBtn_Click(object sender, EventArgs e)
		{
			tableLayoutPanel1.Controls.Add(new MatrixBox());
		}

		private void removeMatrixBtn_Click(object sender, EventArgs e)
		{
			var last = tableLayoutPanel1.Controls[tableLayoutPanel1.Controls.Count - 1];
			if (last != matrixBox1)
			{
				tableLayoutPanel1.Controls.Remove(last);
			}
		}

		private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
		{
			ForceMatrixListFocus();
		}
		private void tableLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
		{
			ForceMatrixListFocus();
		}

		private void ForceMatrixListFocus()
		{
			tableLayoutPanel1.Focus();

		}


	}
}
