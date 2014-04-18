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
	public partial class MatrixBox : Control
	{

		private float[,] _Matrix = null;
		public float[,] Matrix
		{
			get
			{
				return _Matrix;
			}
			private set
			{
				_Matrix = value;
				RefreshMatrixOrTextBoxes(RefreshDirection.FromMatrix);
			}
		}

		private TextBox[,] textboxes;

		private bool suspendAutoRefresh = false;

		public class Transition<T>
			where T : class
		{
			public T From { get; private set; }
			public T To { get; private set; }

			protected Action<T> Setter { get; set; }

			/// <summary>
			/// apply and return the undo action.
			/// </summary>
			public Transition<T> Undo()
			{
				Setter(From);
				return new Transition<T>(To, From, Setter);
			}

			public Transition(T from, T to, Action<T> setter)
			{
				this.From = from;
				this.To = to;
				this.Setter = setter;
			}
		}

		private Action<string> GetTextBoxSetter(TextBox textBox)
		{
			return (x) =>
			{
				IsUndoRedoTextChange = true;
				textBox.Text = x;
				IsUndoRedoTextChange = false;
				textBox.Focus();
				textBox.SelectAll();
			};
		}

		private bool IsUndoRedoTextChange = false;
		private Stack<Transition<string>> UndoStack = new Stack<Transition<string>>();
		private Stack<Transition<string>> RedoStack = new Stack<Transition<string>>();

		public event EventHandler MatrixChanged;
		protected void OnMatrixChanged()
		{
			var handler = MatrixChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		//magic numbers, from trial and error...
		protected override Size DefaultSize { get { return new Size(238, 88); } }
		protected override Size DefaultMaximumSize { get { return DefaultSize; } }
		protected override Size DefaultMinimumSize { get { return DefaultSize; } }
		public override Size MaximumSize { get { return DefaultSize; } }
		public override Size MinimumSize { get { return DefaultSize; } }

		public MatrixBox()
			: base()
		{
			InitializeMatrixTextboxes(this, new Point(0, 0));
			ResetMatrix();
			this.KeyDown += MatrixBox_KeyDown;
		}

		void MatrixBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				Stack<Transition<string>> stack, otherStack;
				switch (e.KeyCode)
				{
					case Keys.Z:
						stack = UndoStack;
						otherStack = RedoStack;
						break;
					case Keys.Y:
						stack = RedoStack;
						otherStack = UndoStack;
						break;
					default: return;
				}
				if (stack.Count > 0)
				{
					var action = stack.Pop();
					var undoAction = action.Undo();
					otherStack.Push(undoAction);
				}
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		private void InitializeMatrixTextboxes(Control control, Point location)
		{
			this.suspendAutoRefresh = true;
			textboxes = new TextBox[5, 5];
			const int xSpacing = 47, ySpacing = 17;
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					var newTextBox = new TextBox();
					newTextBox.Parent = control;
					newTextBox.Location = new Point(location.X + j * xSpacing, location.Y + i * ySpacing);
					newTextBox.Width = 50;
					newTextBox.Height = 20;
					newTextBox.TextAlign = HorizontalAlignment.Center;
					newTextBox.Tag = ""; //will always contain the previous text before a text changed event
					newTextBox.KeyDown += MatrixBox_KeyDown;
					newTextBox.KeyPress += (o, e) => { if (e.KeyChar == ',') { e.Handled = true; newTextBox.SelectedText = "."; } };
					//Capture the i and j variables for the closure to work correctly...
					int iCopy = i, jCopy = j;
					newTextBox.TextChanged += (o, e) =>
					{
						newTextBox.ClearUndo();
						if (!IsUndoRedoTextChange)
						{
							RedoStack.Clear(); //any user change reset the redo stack
							UndoStack.Push(new Transition<string>((string)newTextBox.Tag, newTextBox.Text, GetTextBoxSetter(newTextBox)));
							newTextBox.Tag = newTextBox.Text;
						}
						if (!this.suspendAutoRefresh)
						{
							//try to refresh the corresponding matrix cell
							if (RefreshMatrix(iCopy, jCopy, throwsException: false))
							{
								OnMatrixChanged();
							}
						}
					};
					newTextBox.MouseWheel += (o, e) =>
					{
						if (ModifierKeys != Keys.None)
						{
							decimal parsed = 0; //decimal type for exact decimal rounding
							if (!decimal.TryParse(newTextBox.Text,
								System.Globalization.NumberStyles.Float,
								System.Globalization.CultureInfo.InvariantCulture, out parsed))
								parsed = 0;
							decimal increment = 1;
							if (ModifierKeys == Keys.Control)
							{
								increment = .1m;
							}
							parsed += increment * (e.Delta / (Math.Abs(e.Delta)));
							//10 significan figures
							newTextBox.Text = parsed.ToString("g10", System.Globalization.CultureInfo.InvariantCulture);
						}
					};
					textboxes[i, j] = newTextBox;
				}
			}
			this.suspendAutoRefresh = false;
		}

		public void ToggleEnabled()
		{
			this.Enabled = !this.Enabled;
			OnMatrixChanged();
		}

		/// <summary>
		/// Reset the matrix to the identity matrix
		/// </summary>
		public void ResetMatrix()
		{
			this.suspendAutoRefresh = true;
			//do not use the setter to avoid firing a matrix changed event for nothing (the event will be fired at the end of the reset)
			_Matrix = new float[5, 5];
			for (int i = 0; i < Matrix.GetLength(0); i++)
			{
				for (int j = 0; j < Matrix.GetLength(1); j++)
				{
					Matrix[i, j] = BuiltinMatrices.Identity[i, j];
				}
			}
			RefreshMatrixOrTextBoxes(RefreshDirection.FromMatrix);
			this.suspendAutoRefresh = false;
		}

		/// <summary>
		/// Set the internal matrix of the control to a clone of the given matrix
		/// </summary>
		/// <param name="matrix"></param>
		public void SetMatrix(float[,] matrix)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			if (matrix.GetLength(0) != 5 || matrix.GetLength(1) != 5)
			{
				throw new ArgumentException("The matrix must be a 5x5 matrix!", "matrix");
			}
			this.suspendAutoRefresh = true;
			for (int i = 0; i < Matrix.GetLength(0); i++)
			{
				for (int j = 0; j < Matrix.GetLength(1); j++)
				{
					Matrix[i, j] = matrix[i, j];
				}
			}
			RefreshMatrixOrTextBoxes(RefreshDirection.FromMatrix);
			this.suspendAutoRefresh = false;
		}

		public float this[int i, int j]
		{
			set
			{
				if (i >= 5 || j >= 5)
				{
					throw new IndexOutOfRangeException("i or j was less than 0 or greater than 4!");
				}
				this.suspendAutoRefresh = true;
				Matrix[i, j] = value;
				if (RefreshTextBox(i, j))
				{
					OnMatrixChanged();
				}
				this.suspendAutoRefresh = false;
			}
		}

		private enum RefreshDirection
		{
			FromMatrix,
			FromTextboxes,
		}
		/// <summary>
		/// Returns true if any change was actually made
		/// </summary>
		private bool RefreshMatrixOrTextBoxes(RefreshDirection direction)
		{
			this.suspendAutoRefresh = true;
			bool different = false;
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					bool result = false;
					switch (direction)
					{
						case RefreshDirection.FromMatrix:
							result = RefreshTextBox(i, j);
							break;
						case RefreshDirection.FromTextboxes:
							try
							{
								result = RefreshMatrix(i, j);
							}
							catch (Exception)
							{
								//ResetMatrix();
								throw new Exception("Invalid matrix!");
							}
							break;
						default:
							throw new Exception("Fuck you!");
					}
					if (result) different = true;
				}
			}
			this.suspendAutoRefresh = false;
			if (different)
			{
				OnMatrixChanged();
			}
			return different;
		}

		/// <summary>
		/// Refresh a single text box.
		/// Returns true if any change was actually made
		/// </summary>
		private bool RefreshTextBox(int i, int j)
		{
			string text = ((decimal)Matrix[i, j]).ToString("g10", System.Globalization.CultureInfo.InvariantCulture);
			if (textboxes[i, j].Text != text)
			{
				textboxes[i, j].Text = text;
				return true;
			}
			return false;
		}
		/// <summary>
		/// Refresh a single matrix cell from the corresponding text box.
		/// Returns true if any change was actually made.
		/// If throwsException is false and the textbox value cannot be parsed, the procedure is aborted and false is returned.
		/// </summary>
		private bool RefreshMatrix(int i, int j, bool throwsException = true)
		{
			float parsed;
			if (throwsException)
			{
				parsed = float.Parse(textboxes[i, j].Text,
									System.Globalization.NumberStyles.Float,
									System.Globalization.CultureInfo.InvariantCulture);
			}
			else
			{
				if (!float.TryParse(textboxes[i, j].Text,
									System.Globalization.NumberStyles.Float,
									System.Globalization.CultureInfo.InvariantCulture,
									out parsed))
				{
					//on error, do not update the matrix!
					return false;
				}
			}

			if (Matrix[i, j] != parsed)
			{
				Matrix[i, j] = parsed;
				return true;
			}
			return false;
		}

	}
}
