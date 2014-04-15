using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorMatrixViewer
{
	public class GripPanel : Panel
	{
		public GripPanel()
		{
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.DoubleBuffered = true;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			int height = this.Height;
			int width = this.Width;
			for (int i = 0; i < height; i += 3)
			{
				e.Graphics.DrawLine(SystemPens.ControlDark, 0, i, width, i);
			}
		}
	}
}
