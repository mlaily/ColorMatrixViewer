using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorMatrixViewer
{
	public partial class InListMatrixBox : UserControl
	{

		public MatrixBox MatrixBox { get; private set; }

		public InListMatrixBox()
		{
			InitializeComponent();
			this.MatrixBox = matrixBox1;
			this.matrixTemplateList.DataSource = BuiltinMatrices.All;
			this.matrixTemplateList.DisplayMember = "Key";
		}

		private void plusBtn_MouseClick(object sender, MouseEventArgs e)
		{
			contextMenuStrip1.Show(plusBtn, e.Location);
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			matrixTemplateList.Show();
		}

		private void resetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			matrixBox1.ResetMatrix();
		}

		private void InListMatrixBox_MouseClick(object sender, MouseEventArgs e)
		{
			matrixTemplateList.Hide();
		}

		private void matrixTemplateList_DoubleClick(object sender, EventArgs e)
		{
			matrixTemplateList.Hide();
			var matrix = matrixTemplateList.SelectedItem;
			if (matrix != null)
			{
				var cast = (KeyValuePair<string, float[,]>)matrix;
				this.matrixBox1.SetMatrix(cast.Value);
			}
		}

	}
}
