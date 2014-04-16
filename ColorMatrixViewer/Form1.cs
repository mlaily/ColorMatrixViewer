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

		private int draggingPositionY;
		private int dropAtIndex = 0;
		private Pen dragDropIndicationPen = new Pen(SystemColors.ControlDarkDark, 4);

		public Form1()
		{
			InitializeComponent();
			var exampleMatrix = AddMatrixBox();
			exampleMatrix.MatrixBox.SetMatrix(BuiltinMatrices.NegativeHueShift180Variation1);
			splitContainer1.SplitterDistance = splitContainer1.Height - 96;
		}

		void matrixBox_MatrixChanged(object sender, EventArgs e)
		{
			ApplyMatrix();
		}

		private void ApplyMatrix()
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

		private InListMatrixBox AddMatrixBox()
		{
			var newMatrix = new InListMatrixBox();
			newMatrix.MatrixBox.MatrixChanged += matrixBox_MatrixChanged;
			newMatrix.RemoveButtonClicked += newMatrix_RemoveButtonClicked;
			newMatrix.GripMouseDown += newMatrix_GripMouseDown;
			tableLayoutPanel1.Controls.Add(newMatrix);
			RefreshScrollBar();
			return newMatrix;
		}

		void newMatrix_GripMouseDown(object sender, MouseEventArgs e)
		{
			var cast = (InListMatrixBox)sender;
			cast.DoDragDrop(cast, DragDropEffects.Move);
		}

		void newMatrix_RemoveButtonClicked(object sender, EventArgs e)
		{
			RemoveMatrixBox((InListMatrixBox)sender);
		}
		private void RemoveMatrixBox(InListMatrixBox control = null, bool refreshUI = true)
		{
			if (control == null)
			{
				if (tableLayoutPanel1.Controls.Count > 0)
				{
					var last = (InListMatrixBox)tableLayoutPanel1.Controls[tableLayoutPanel1.Controls.Count - 1];
					control = last;
				}
				else
				{
					return;
				}
			}
			control.MatrixBox.MatrixChanged -= matrixBox_MatrixChanged;
			control.RemoveButtonClicked -= newMatrix_RemoveButtonClicked;
			tableLayoutPanel1.Controls.Remove(control);
			if (refreshUI)
			{
				RefreshScrollBar();
				ApplyMatrix();
			}
		}

		private void RefreshScrollBar()
		{
			var preferredHeight = tableLayoutPanel1.PreferredSize.Height;
			//the smallest possible to avoid scrollbars but still big enough to fill space and allow seamless drag and drop
			if (preferredHeight < splitContainer1.Panel1.Height)
			{
				preferredHeight = splitContainer1.Panel1.Height;
			}
			tableLayoutPanel1.Height = preferredHeight;
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
				showResultMatrixBtn.Text = "Show result matrix";
			}
			else
			{
				showResultMatrixBtn.Text = "Hide result matrix";
			}
		}

		private void splitContainer1_Panel2_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				resultMatrixContextMenu.Show(splitContainer1.Panel2, e.Location);
			}
		}

		private InListMatrixBox RefreshDragDropMatrix(DragEventArgs e)
		{
			var draggedMatrix = e.Data.GetData(typeof(InListMatrixBox).FullName) as InListMatrixBox;
			if (draggedMatrix == null) return null;

			var draggedOverMatrix = tableLayoutPanel1.GetChildAtPoint(
						   tableLayoutPanel1.PointToClient(new Point(e.X, e.Y))) as InListMatrixBox;
			if (draggedOverMatrix == null)
			{
				//abort refreshing
				return draggedMatrix;
			}
			var point = draggedOverMatrix.PointToClient(new Point(e.X, e.Y));

			int indexOfDragging = tableLayoutPanel1.Controls.IndexOf(draggedMatrix);
			int indexOfDraggedOver = tableLayoutPanel1.Controls.IndexOf(draggedOverMatrix);

			//TODO: maybe try to simplify this a bit?
			if (point.Y <= draggedMatrix.Height / 2)
			{
				//before
				draggingPositionY = draggedOverMatrix.Location.Y - 2;//pen width: 4
				if (indexOfDragging < indexOfDraggedOver)
				{
					//dragging is before dragged over
					dropAtIndex = tableLayoutPanel1.Controls.IndexOf(draggedOverMatrix) - 1;
					if (dropAtIndex < 0) dropAtIndex = 0;
				}
				else
				{
					//dragging is after dragged over (indexes cannot be equal)
					dropAtIndex = tableLayoutPanel1.Controls.IndexOf(draggedOverMatrix);
				}
			}
			else
			{
				//after
				draggingPositionY = draggedOverMatrix.Location.Y + draggedOverMatrix.Height + 2;//pen width: 4
				if (indexOfDragging < indexOfDraggedOver)
				{
					//dragging is before dragged over
					dropAtIndex = tableLayoutPanel1.Controls.IndexOf(draggedOverMatrix);
				}
				else
				{
					//dragging is after dragged over (indexes cannot be equal)
					dropAtIndex = tableLayoutPanel1.Controls.IndexOf(draggedOverMatrix) + 1;
					if (dropAtIndex > tableLayoutPanel1.Controls.Count - 1) dropAtIndex = tableLayoutPanel1.Controls.Count - 1;
				}
			}
			if (draggedMatrix == draggedOverMatrix)
			{
				dropAtIndex = indexOfDragging;
			}
			return draggedMatrix;
		}

		private void tableLayoutPanel1_DragOver(object sender, DragEventArgs e)
		{
			if (RefreshDragDropMatrix(e) == null) return;

			e.Effect = DragDropEffects.Move;

			var g = tableLayoutPanel1.CreateGraphics();
			g.Clear(tableLayoutPanel1.BackColor);
			//take the margins into account so that the line is perfectly aligned with the matrix box
			g.DrawLine(dragDropIndicationPen, 6, draggingPositionY, tableLayoutPanel1.Width - 3, draggingPositionY);
		}

		private void tableLayoutPanel1_DragDrop(object sender, DragEventArgs e)
		{
			var draggedMatrix = RefreshDragDropMatrix(e);

			tableLayoutPanel1.Controls.SetChildIndex(draggedMatrix, dropAtIndex);
			ApplyMatrix();//matrix multiplication is order dependant

			CleanDragDropIndicator();
		}

		private void tableLayoutPanel1_DragLeave(object sender, EventArgs e)
		{
			CleanDragDropIndicator();
		}

		private void CleanDragDropIndicator()
		{
			var g = tableLayoutPanel1.CreateGraphics();
			g.Clear(tableLayoutPanel1.BackColor);
		}

		private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var resultString = Util.MatrixToString(resultMatrixBox.Matrix);
			Clipboard.SetText(resultString);
		}

		private void ClearMatricesBtn_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Remove all the matrices in the list?", "Please confirm...", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
			{
				while (tableLayoutPanel1.Controls.Count > 1)
				{
					RemoveMatrixBox(refreshUI: false);
				}
				RemoveMatrixBox(refreshUI: true);
			}
		}

	}
}
