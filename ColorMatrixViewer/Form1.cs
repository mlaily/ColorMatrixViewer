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

		private Bitmap input;
		private Bitmap displayed;

	

		private bool autoRefresh = true;

		public Form1()
		{
			//ResetMatrix();
			InitializeComponent();
			//RefreshMatrixOrTextBoxes(RefreshDirection.FromMatrix);
		}

		private void ApplyMatrix(bool force = false)
		{
			if (input == null) return;
			//if (RefreshMatrixOrTextBoxes(RefreshDirection.FromTextboxes) || force)
			//{
			//	displayed = Util.ApplyColorMatrix(input, Matrix);
			//	imageDiff1.SetImages(input, displayed);
			//}
		}

		private void loadAnImageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var dialog = new OpenFileDialog())
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					input = displayed = (Bitmap)Bitmap.FromFile(dialog.FileName);
					ApplyMatrix(force: true);
				}
			}
		}

		private void resetMatrixToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//ResetMatrix();
			//RefreshMatrixOrTextBoxes(RefreshDirection.FromMatrix);
			ApplyMatrix(force: true);
		}

	}
}
