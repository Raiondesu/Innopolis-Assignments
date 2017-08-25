using System;
using System.Collections.Generic;
using System.Linq;

namespace MultisetTask
{
	public class ConstrainedMultiset<T> : Multiset<T>
	{
		protected ConstrainedMultiset(Dictionary<T, uint> dictionary, uint MaxCapacity) : base(
			MaxCapacity < 0 ? new Dictionary<T, uint>() :
			new Dictionary<T, uint>(
				  dictionary?.Take(MaxCapacity > int.MaxValue ? int.MaxValue : (int)MaxCapacity)
			)
		) => this.MaxCapacity = MaxCapacity;
		
		public ConstrainedMultiset(ConstrainedMultiset<T> other, uint MaxCapacity = 0)
		 : this(other.store, MaxCapacity == 0 ? other.MaxCapacity : MaxCapacity) {}

		public ConstrainedMultiset(uint MaxCapacity) : this(new Dictionary<T, uint>(), MaxCapacity) {}

		public uint MaxCapacity { get; }


		public override void Add(T value, uint quantity = 1)
		{
			if (this[value] + quantity > this.MaxCapacity)
				quantity -= (this[value] + quantity - this.MaxCapacity);

			base.Add(value, quantity);
		}

		public override string ToString()
		{
			string result = "{ ";

			foreach (var pair in this.store)
				result += $"{pair.Key}" + (pair.Value > 1 ? $"({pair.Value}/{this.MaxCapacity}), " : ", ");

			result = result.Substring(0, result.Length - 2);
			result += " }";
			return result;
		}
	}
}