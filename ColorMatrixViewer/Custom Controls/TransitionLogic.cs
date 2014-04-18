//Copyright (c) 2014 Melvyn Laily
//http://arcanesanctum.net

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.

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
