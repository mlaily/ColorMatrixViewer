using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorMatrixViewer
{
	/// <summary>
	/// Inspiration source: http://www.codeproject.com/Articles/7827/Customizing-WinForm-s-System-Menu
	/// </summary>
	public class FormWithSystemMenu : Form
	{
		public const int WM_SYSCOMMAND = 0x112;
		public const int MF_SEPARATOR = 0x800;
		public const int MF_BYPOSITION = 0x400;
		public const int MF_STRING = 0x0;

		int currentId = 0x1000;

		private Dictionary<int, Action> customSystemMenuItems = new Dictionary<int, Action>();


		[DllImport("user32.dll")]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("user32.dll")]
		private static extern bool InsertMenu(IntPtr hMenu, int wPosition, int wFlags, int wIDNewItem, string lpNewItem);

		protected void AddCustomSystemMenuItem(string label, Action action)
		{
			IntPtr sysMenuHandle = GetSystemMenu(this.Handle, false);
			int id = currentId++;
			if (InsertMenu(sysMenuHandle, 0, MF_BYPOSITION, id, label))
			{
				customSystemMenuItems.Add(id, action);
			}
			else
			{
				throw new Exception();
			}
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_SYSCOMMAND)
			{
				int id = m.WParam.ToInt32();
				if (customSystemMenuItems.ContainsKey(id))
				{
					customSystemMenuItems[id]();
				}
			}
			base.WndProc(ref m);
		}
	}
}
