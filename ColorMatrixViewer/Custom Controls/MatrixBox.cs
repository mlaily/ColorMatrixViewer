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
		}

		private TextBox[,] textboxes;

		//magic numbers, from trial and error...
		protected override Size DefaultSize { get { return new Size(238, 88); } }
		protected override Size DefaultMaximumSize { get { return DefaultSize; } }
		protected override Size DefaultMinimumSize { get { return DefaultSize; } }
		public override Size MaximumSize { get { return DefaultSize; } }
		public override Size MinimumSize { get { return DefaultSize; } }

		#region Undo/Redo declarations

		/// <summary>
		/// When set to true, prevent the text boxes to fire MatrixChanged events.
		/// </summary>
		private bool suspendAutoRefresh = false;
		/// <summary>
		/// When set to true, prevent the creation of a redo action for the current operations.
		/// </summary>
		private bool IsUndoRedoTextChange = false;

		private Stack<ITransition> UndoStack = new Stack<ITransition>();
		private Stack<ITransition> RedoStack = new Stack<ITransition>();

		/// <summary>
		/// Used to undo/redo a textbox text changed
		/// </summary>
		/// <param name="textBox"></param>
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

		/// <summary>
		/// Used to undo/redo the complete matrix
		/// </summary>
		private Action<float[,]> GetMatrixSetter()
		{
			return (x) =>
			{
				IsUndoRedoTextChange = true;
				_Matrix = new float[5, 5];
				for (int i = 0; i < Matrix.GetLength(0); i++)
				{
					for (int j = 0; j < Matrix.GetLength(1); j++)
					{
						Matrix[i, j] = x[i, j];
						textboxes[i, j].Tag = FloatToString(Matrix[i, j]);
					}
				}
				RefreshMatrixOrTextBoxes(RefreshDirection.FromMatrix);
				IsUndoRedoTextChange = false;
			};
		}

		public event EventHandler MatrixChanged;
		protected void OnMatrixChanged()
		{
			var handler = MatrixChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		#endregion

		public MatrixBox()
			: base()
		{
			InitializeMatrixTextboxes(this, new Point(0, 0));
			ResetMatrix();
			this.KeyDown += MatrixBox_KeyDown;
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
					//event handlers
					newTextBox.KeyDown += MatrixBox_KeyDown;
					newTextBox.KeyPress +=
						(o, e) => { if (e.KeyChar == ',') { e.Handled = true; newTextBox.SelectedText = "."; } };
					newTextBox.TextChanged += GetTextBoxTextChangedHandler(newTextBox, i, j);
					newTextBox.MouseWheel += GetTextBoxMouseWheelHandler(newTextBox);

					textboxes[i, j] = newTextBox;
				}
			}
			this.suspendAutoRefresh = false;
		}

		#region Event Handlers

		void MatrixBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				//Perform the undo redo operations on the appropriate stacks when pressing CTRL+Z or CTRL+Y
				Stack<ITransition> stack, otherStack;
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

		#region Text Boxes Events

		private EventHandler GetTextBoxTextChangedHandler(TextBox textbox, int i, int j)
		{
			return (o, e) =>
			{
				textbox.ClearUndo();
				if (!IsUndoRedoTextChange)
				{
					RedoStack.Clear(); //any user change reset the redo stack
					UndoStack.Push(new Transition<string>((string)textbox.Tag, textbox.Text, GetTextBoxSetter(textbox)));
					textbox.Tag = textbox.Text;
				}
				if (!this.suspendAutoRefresh)
				{
					//try to refresh the corresponding matrix cell
					if (RefreshMatrix(i, j, throwsException: false))
					{
						OnMatrixChanged();
					}
				}
			};
		}

		private MouseEventHandler GetTextBoxMouseWheelHandler(TextBox textbox)
		{
			return (o, e) =>
			{
				if (ModifierKeys != Keys.None)
				{
					decimal parsed = 0; //decimal type for exact decimal rounding
					if (!decimal.TryParse(textbox.Text,
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
					textbox.Text = FloatToString(parsed);
				}
			};
		}

		#endregion

		#endregion

		#region Public Methods

		/// <summary>
		/// Reset the matrix to the identity matrix
		/// </summary>
		public void ResetMatrix()
		{
			this.suspendAutoRefresh = true;
			bool firstInitialization = this.Matrix == null;
			var previous = Matrix;
			//do not use the Matrix setter to avoid firing a matrix changed event for nothing each time
			//(the event will be fired at the end of the reset)
			_Matrix = new float[5, 5];
			GetMatrixSetter()(BuiltinMatrices.Identity);
			if (!firstInitialization)
			{
				RedoStack.Clear(); //any user change reset the redo stack
				UndoStack.Push(new Transition<float[,]>(previous, BuiltinMatrices.Identity, GetMatrixSetter()));
			}
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
			var previous = this.Matrix ?? BuiltinMatrices.Identity;
			//actually change the matrix (and update the text boxes)
			GetMatrixSetter()(matrix);
			RedoStack.Clear(); //any user change reset the redo stack
			UndoStack.Push(new Transition<float[,]>(previous, matrix, GetMatrixSetter()));
			this.suspendAutoRefresh = false;
		}

		//TODO: choose between the indexer and the Matrix property...
		//public float this[int i, int j]
		//{
		//	set
		//	{
		//		if (i >= 5 || j >= 5)
		//		{
		//			throw new IndexOutOfRangeException("i or j was less than 0 or greater than 4!");
		//		}
		//		this.suspendAutoRefresh = true;
		//		Matrix[i, j] = value;
		//		if (RefreshTextBox(i, j))
		//		{
		//			OnMatrixChanged();
		//		}
		//		this.suspendAutoRefresh = false;
		//	}
		//}

		public void ToggleEnabled()
		{
			this.Enabled = !this.Enabled;
			OnMatrixChanged();
		}

		public void ClearUndoRedo()
		{
			UndoStack.Clear();
			RedoStack.Clear();
		}

		#endregion

		#region Matrix <-> Text Boxes refresh logic

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
			string text = FloatToString(Matrix[i, j]);
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

		#endregion

		private static string FloatToString(float value)
		{
			return FloatToString((decimal)value);
		}

		private static string FloatToString(decimal value)
		{
			return value.ToString("g10", System.Globalization.CultureInfo.InvariantCulture);
		}

	}
}
