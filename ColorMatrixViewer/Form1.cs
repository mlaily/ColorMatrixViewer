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
			//vScrollBar1.MouseWheel += vScrollBar1_MouseWheel;
			//vScrollBar1.MouseEnter += vScrollBar1_MouseEnter;
		}

		//void vScrollBar1_MouseEnter(object sender, EventArgs e)
		//{
		//	vScrollBar1.Focus();
		//}

		//void vScrollBar1_MouseWheel(object sender, MouseEventArgs e)
		//{
		//	int maxValue = vScrollBar1.Maximum - vScrollBar1.LargeChange;
		//	var newValue = vScrollBar1.Value + e.Delta;
		//	if (newValue >= vScrollBar1.Minimum && newValue <= maxValue)
		//	{
		//		vScrollBar1.Value = newValue;
		//	}
		//}

		void matrixBox1_MatrixChanged(object sender, EventArgs e)
		{
			ApplyMatrix();
		}

		private void ApplyMatrix()
		{
			if (imageDiff1.FirstImage != null)
			{
				var finalMatrix = new float[5, 5];
				for (int i = 0; i < 5; i++)
				{
					for (int j = 0; j < 5; j++)
					{
						finalMatrix[i, j] = BuiltinMatrices.Identity[i, j];
					}
				}
				foreach (InListMatrixBox matrixControl in tableLayoutPanel1.Controls)
				{
					finalMatrix = Transform.Multiply(finalMatrix, matrixControl.MatrixBox.Matrix);
				}
				imageDiff1.SetImages(second: Util.ApplyColorMatrix(imageDiff1.FirstImage, finalMatrix));
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

		private void AddMatrixBtn_Click(object sender, EventArgs e)
		{
			var newMatrix = new InListMatrixBox();
			newMatrix.MatrixBox.MatrixChanged += matrixBox1_MatrixChanged;
			tableLayoutPanel1.Controls.Add(newMatrix);
			RefreshScrollBar();
		}

		private void removeMatrixBtn_Click(object sender, EventArgs e)
		{
			if (tableLayoutPanel1.Controls.Count > 0)
			{
				var last = (InListMatrixBox)tableLayoutPanel1.Controls[tableLayoutPanel1.Controls.Count - 1];
				last.MatrixBox.MatrixChanged -= matrixBox1_MatrixChanged;
				tableLayoutPanel1.Controls.Remove(last);
			}
			RefreshScrollBar();
		}

		private void RefreshScrollBar()
		{
			int panelHeight = vScrollBar1.Height;
			int contentHeight = tableLayoutPanel1.PreferredSize.Height;

			tableLayoutPanel1.Height = contentHeight;

			vScrollBar1.LargeChange = panelHeight;

			vScrollBar1.Enabled = contentHeight > panelHeight;
			if (contentHeight > panelHeight) vScrollBar1.Maximum = contentHeight;
			else vScrollBar1.Value = vScrollBar1.Minimum;
		}
		private void vScrollBar1_ValueChanged(object sender, EventArgs e)
		{
			tableLayoutPanel1.Location = new Point(0, -1 * vScrollBar1.Value);
		}

		private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
		{
			ForceMatrixListFocus();
		}

		private void ForceMatrixListFocus()
		{
			tableLayoutPanel1.Focus();
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			RefreshScrollBar();
		}



	}
}
