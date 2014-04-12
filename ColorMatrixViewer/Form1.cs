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
			editTabPage.Enabled = false;
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

		private void splitContainer1_DoubleClick(object sender, EventArgs e)
		{
			splitContainer1.SplitterDistance = 258; //250 + the tab page margins or whatever...
		}

	}
}
