using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorMatrixViewer
{
	//http://stackoverflow.com/questions/17874043/disabling-scroll-on-condition
	public class CustomPanel : Panel
	{
		protected override void WndProc(ref Message m)
		{
			//prevent scrolling while holding alt, shift, of ctrl
			if (m.Msg == 0x20a && ModifierKeys != Keys.None) return; //WM_MOUSEWHEEL = 0x20a
			base.WndProc(ref m);
		}
	}
}
