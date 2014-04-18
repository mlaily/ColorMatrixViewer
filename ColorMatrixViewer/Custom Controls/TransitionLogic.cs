using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMatrixViewer
{

	/// <summary>
	/// Represent a transition, that is, going from a state to another, with the ability to go back to the previous state.
	/// </summary>
	public interface ITransition
	{
		ITransition Undo();
	}

	public class Transition<T> : ITransition
		where T : class
	{
		/// <summary>
		/// Source state.
		/// </summary>
		public T From { get; private set; }
		/// <summary>
		/// Destination state.
		/// </summary>
		public T To { get; private set; }

		/// <summary>
		/// Setter method, used to set the state (the value).
		/// Might also implement custom logic besides setting a value.
		/// </summary>
		private Action<T> Setter;

		/// <summary>
		/// Undo the current transition, and return an object reprensenting the undo transition.
		/// To cancel the undo, call Undo() on the returned object.
		/// </summary>
		public Transition<T> Undo()
		{
			Setter(From);
			return new Transition<T>(To, From, Setter);
		}
		ITransition ITransition.Undo()
		{
			return Undo();
		}

		/// <param name="from">Source state</param>
		/// <param name="to">Destination state</param>
		/// <param name="setter">
		/// Setter method, used to set the state (the value).
		/// Might also implement custom logic besides setting a value.
		/// </param>
		public Transition(T from, T to, Action<T> setter)
		{
			this.From = from;
			this.To = to;
			this.Setter = setter;
		}
	}

}
