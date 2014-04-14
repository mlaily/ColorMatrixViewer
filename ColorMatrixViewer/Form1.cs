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
			AddMatrixBox();
			splitContainer1.SplitterDistance = splitContainer1.Height - 100;
		}

		void matrixBox_MatrixChanged(object sender, EventArgs e)
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
				if (!matrixControl.MatrixBox.Enabled)
				{
					continue;
				}
				else
				{
					finalMatrix = Transform.Multiply(finalMatrix, matrixControl.MatrixBox.Matrix);
				}
			}
			resultMatrixBox.SetMatrix(finalMatrix);
			ApplyMatrix();
		}

		private void ApplyMatrix()
		{
			if (imageDiff1.FirstImage != null)
			{

				imageDiff1.SetImages(second: Util.ApplyColorMatrix(imageDiff1.FirstImage, resultMatrixBox.Matrix));
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
			AddMatrixBox();
		}
		private void removeMatrixBtn_Click(object sender, EventArgs e)
		{
			RemoveMatrixBox();
		}

		private void AddMatrixBox()
		{
			var newMatrix = new InListMatrixBox();
			newMatrix.MatrixBox.MatrixChanged += matrixBox_MatrixChanged;
			newMatrix.RemoveButtonClicked += newMatrix_RemoveButtonClicked;
			tableLayoutPanel1.Controls.Add(newMatrix);
			RefreshScrollBar();
		}

		void newMatrix_RemoveButtonClicked(object sender, EventArgs e)
		{
			RemoveMatrixBox((InListMatrixBox)sender);
		}
		private void RemoveMatrixBox(InListMatrixBox control = null)
		{
			if (control == null)
			{
				if (tableLayoutPanel1.Controls.Count > 0)
				{
					var last = (InListMatrixBox)tableLayoutPanel1.Controls[tableLayoutPanel1.Controls.Count - 1];
					control = last;
				}
			}
			control.MatrixBox.MatrixChanged -= matrixBox_MatrixChanged;
			control.RemoveButtonClicked -= newMatrix_RemoveButtonClicked;
			tableLayoutPanel1.Controls.Remove(control);
			RefreshScrollBar();
			ApplyMatrix();
		}

		private void RefreshScrollBar()
		{
			tableLayoutPanel1.Height = tableLayoutPanel1.PreferredSize.Height;
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			RefreshScrollBar();
		}

		private void loadTheDefaultImageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			imageDiff1.LoadDefaultImage();
			ApplyMatrix();
		}

		private void showResultMatrixBtn_Click(object sender, EventArgs e)
		{
			splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
			if (splitContainer1.Panel2Collapsed)
			{
				showResultMatrixBtn.Text = "Show resulting matrix";
			}
			else
			{
				showResultMatrixBtn.Text = "Hide resulting matrix";
			}
		}

		private void splitContainer1_Panel2_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				resultMatrixContextMenu.Show(splitContainer1.Panel2, e.Location);
			}
		}

	}
}
