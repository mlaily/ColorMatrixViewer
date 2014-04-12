using System;
using System.Collections.Generic;
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
	/// Interaction logic for MatrixBox.xaml
	/// </summary>
	public partial class MatrixBox : UserControl
	{

		//private float[,] _Matrix = null;
		//public float[,] Matrix
		//{
		//	get { return _Matrix; }
		//	private set
		//	{
		//		_Matrix = value;
		//		RefreshMatrixOrTextBoxes(RefreshDirection.FromMatrix);
		//	}
		//}

		//private TextBox[,] textboxes;

		//public MatrixBox()
		//{
		//	InitializeComponent();

		//	for (int i = 0; i < 5; i++)
		//	{
		//		parentGrid.RowDefinitions.Add(new RowDefinition());
		//		parentGrid.ColumnDefinitions.Add(new ColumnDefinition());
		//		for (int j = 0; j < 5; j++)
		//		{
		//			var textBox = new TextBox();
		//			textBox.TextAlignment = TextAlignment.Center;
		//			Grid.SetColumn(textBox, j);
		//			Grid.SetRow(textBox, i);
		//			textboxes[i,j] = textBox;
		//			parentGrid.Children.Add(textBox);
		//		}
		//	}
		//}

		private float[,] _Matrix = null;
		public float[,] Matrix
		{
			get { return _Matrix; }
			private set
			{
				_Matrix = value;
				RefreshMatrixOrTextBoxes(RefreshDirection.FromMatrix);
			}
		}

		private TextBox[,] textboxes;

		private bool suspendAutoRefresh = false;

		private struct UndoAction
		{
			public TextBox TextBox { get; private set; }
			public string Text { get; private set; }

			public UndoAction(TextBox textBox, string text)
				: this()
			{
				this.TextBox = textBox;
				this.Text = text;
			}
		}

		private bool IsUndoRedoTextChange = false;
		private Stack<UndoAction> UndoStack = new Stack<UndoAction>();
		private Stack<UndoAction> RedoStack = new Stack<UndoAction>();

		public event EventHandler MatrixChanged;
		protected void OnMatrixChanged()
		{
			var handler = MatrixChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		public MatrixBox()
		{
			InitializeComponent();
			InitializeMatrixTextboxes(this, new Point(0, 0));
			ResetMatrix();
			this.KeyDown += MatrixBox_KeyDown;
		}

		void MatrixBox_KeyDown(object sender, KeyEventArgs e)
		{
			//if (true) //todo: check for ctl modifier
			//{
			//	switch (e.Key)
			//	{
			//		case Key.Z:
			//			if (UndoStack.Count > 0)
			//			{
			//				//remove current action
			//				var action = UndoStack.Pop();
			//				//save the text
			//				var redoText = action.TextBox.Text;
			//				//indicate that the next text changed event must not create an undo action
			//				IsUndoRedoTextChange = true;
			//				action.TextBox.Text = action.Text;
			//				IsUndoRedoTextChange = false;
			//				action.TextBox.Focus();
			//				action.TextBox.SelectAll();
			//				//add a redo action
			//				RedoStack.Push(new UndoAction(action.TextBox, redoText));
			//			}
			//			e.Handled = true;
			//			//e.SuppressKeyPress = true;
			//			break;
			//		case Key.Y:
			//			if (RedoStack.Count > 0)
			//			{
			//				var action = RedoStack.Pop();
			//				var undoText = action.TextBox.Text;
			//				IsUndoRedoTextChange = true;
			//				action.TextBox.Text = action.Text;
			//				IsUndoRedoTextChange = false;
			//				action.TextBox.Focus();
			//				action.TextBox.SelectAll();
			//				UndoStack.Push(new UndoAction(action.TextBox, undoText));
			//			}
			//			e.Handled = true;
			//			//e.SuppressKeyPress = true;
			//			break;
			//	}
			//}
		}

		private void InitializeMatrixTextboxes(Control control, Point location)
		{
			this.suspendAutoRefresh = true;
			textboxes = new TextBox[5, 5];
			for (int i = 0; i < 5; i++)
			{
				parentGrid.RowDefinitions.Add(new RowDefinition());
				parentGrid.ColumnDefinitions.Add(new ColumnDefinition());
				for (int j = 0; j < 5; j++)
				{
					var newTextBox = new TextBox();
					newTextBox.TextAlignment = TextAlignment.Center;
					Grid.SetColumn(newTextBox, j);
					Grid.SetRow(newTextBox, i);
					parentGrid.Children.Add(newTextBox);
					newTextBox.Tag = ""; //will always contain the previous text before a text changed event
					newTextBox.KeyDown += MatrixBox_KeyDown;
					newTextBox.TextChanged += (o, e) =>
					{
						//if (e.whatever == ",") { e.Handled = true; /*e = ".";*/ } 
					};
					//Capture the i and j variables for the closure to work correctly...
					int iCopy = i, jCopy = j;
					newTextBox.TextChanged += (o, e) =>
					{
						//newTextBox.ClearUndo();
						if (!IsUndoRedoTextChange)
						{
							RedoStack.Clear(); //any user change reset the redo stack
							UndoStack.Push(new UndoAction(newTextBox, (string)newTextBox.Tag));
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
						decimal parsed = 0; //decimal type for exact decimal rounding
						if (!decimal.TryParse(newTextBox.Text,
							System.Globalization.NumberStyles.Float,
							System.Globalization.CultureInfo.InvariantCulture, out parsed))
							parsed = 0;
						decimal increment = 1;
						//if (ModifierKeys == Keys.Control)
						//{
						//	increment = .1m;
						//}
						parsed += increment * (e.Delta / (Math.Abs(e.Delta)));
						//10 significan figures
						newTextBox.Text = parsed.ToString("g10", System.Globalization.CultureInfo.InvariantCulture);
					};
					textboxes[i, j] = newTextBox;
				}
			}
			this.suspendAutoRefresh = false;
		}

		/// <summary>
		/// Reset the matrix to the identity matrix
		/// </summary>
		public void ResetMatrix()
		{
			this.suspendAutoRefresh = true;
			Matrix = new float[5, 5];
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
				RefreshTextBox(i, j);
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
			return different;
		}

		/// <summary>
		/// Refresh a single text box.
		/// Returns true if any change was actually made
		/// </summary>
		private bool RefreshTextBox(int i, int j)
		{
			string text = Matrix[i, j].ToString();
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
