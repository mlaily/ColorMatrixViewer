using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorMatrixViewer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void imageDiff_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				imageDiffContextMenu.IsOpen = true;

			}
		}

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == true)
			{
				imageDiff.SetImages(Bitmap.FromFile(dialog.FileName));
				ApplyMatrix();
			}
		}

		private void ApplyMatrix()
		{
			if (imageDiff.FirstImage != null)
			{
				imageDiff.SetImages(second: Util.ApplyColorMatrix(imageDiff.FirstImage, matrixBox.Matrix));
			}
		}

		private void matrixBox_MatrixChanged(object sender, EventArgs e)
		{
			ApplyMatrix();
		}

		private void GridSplitter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			tabControlColumn.Width = new GridLength(258);
		}

	}
}
