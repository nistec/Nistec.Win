using System;
using Nistec.SyntaxEditor.Trace;

namespace Nistec.SyntaxEditor.Document
{
	/// <summary>
	/// This interface is used to describe a span inside a text sequence
	/// </summary>
	public class AbstractSegment : ISegment
	{
		protected int offset = -1;
		protected int length = -1;
		
		#region Nistec.SyntaxEditor.Document.ISegment interface implementation
		public virtual int Offset {
			get {
				return offset;
			}
			set {
				offset = value;
			}
		}
		
		public virtual int Length {
			get {
				return length;
			}
			set {
				length = value;
			}
		}
		
		#endregion
		
		public override string ToString()
		{
			return String.Format("[AbstractSegment: Offset = {0}, Length = {1}]",
			                     Offset,
			                     Length);
		}
		
		
	}
}
