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

		public event EventHandler RemoveButtonClicked;
		public event MouseEventHandler GripMouseDown;

		public InListMatrixBox()
		{
			InitializeComponent();
			this.MatrixBox = matrixBox1;

			foreach (var item in BuiltinMatrices.All)
			{
				var toolStripItem = new ToolStripMenuItem();
				toolStripItem.Text = item.Key;
				toolStripItem.Click += (o, e) =>
				{
					//WARNING: the closure on foreach correctly works only in .Net 4.5/C# 5
					this.matrixBox1.SetMatrix(item.Value);
				};
				this.loadToolStripMenuItem.DropDownItems.Add(toolStripItem);
			}
		}

		private void plusBtn_MouseClick(object sender, MouseEventArgs e)
		{
			contextMenuStrip1.Show(plusBtn, e.Location);
		}

		private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			var handler = RemoveButtonClicked;
			if (handler != null)
			{
				RemoveButtonClicked(this, EventArgs.Empty);
			}
		}

		private void toggleEnabledChk_CheckedChanged(object sender, EventArgs e)
		{
			matrixBox1.ToggleEnabled();
		}

		private void gripPanel_MouseDown(object sender, MouseEventArgs e)
		{
			var handler = GripMouseDown;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var resultString = Util.MatrixToString(matrixBox1.Matrix);
			Clipboard.SetText(resultString);
		}

		private void loadFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string raw = Clipboard.GetText();
			if (!string.IsNullOrWhiteSpace(raw))
			{
				try
				{
					var matrix = Util.ParseMatrix(raw);
					this.matrixBox1.SetMatrix(matrix);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error! " + ex.Message);
				}
			}

		}

	}
}
