using System;
using System.Linq;
using System.Collections.Generic;

namespace MultisetTask
{
	public class Multiset<T>
	{
		protected Dictionary<T, uint> store;
		
		protected Multiset(Dictionary<T, uint> dictionary) => this.store = new Dictionary<T, uint>(dictionary);

		public Multiset() : this(new Dictionary<T, uint>()) {}
		public Multiset(Multiset<T> other) : this(other.store) {}

		public int Count => this.store.Count;

		/// Get & Set for specific elements

		//The quantity of specific element
		public uint this[T key]
		{
			get => this.store.ContainsKey(key) ? this.store[key] : 0;
			set => this.store[key] = value;
		}

		//The set of all elements
		public IEnumerable<T> Contents => this.store.Keys;
		

		/// Classic set operations

		public virtual void Add(T value, uint quantity = 1)
		{
			if (store.ContainsKey(value))
				store[value] += quantity;
			else
				store.Add(value, quantity);
		}

		public virtual bool Remove(T value, uint quantity)
		{
			if (store.ContainsKey(value))
			{
				if (store[value] - quantity > 0)
					store[value] -= quantity;
				else
					store.Remove(value);
				
				return true;
			}
			return false;
		}

		public virtual void Remove(T value)
		{
			if (store.ContainsKey(value))
				store.Remove(value);
		}


		/// Add & Substract for Multiset<T>

		public virtual Multiset<T> Add(Multiset<T> other)
		{
			Multiset<T> result = new Multiset<T>(this.store);

			foreach (var pair in other.store)
				result.Add(pair.Key, pair.Value);
			
			return result;
		}

		public virtual Multiset<T> Substract(Multiset<T> other)
		{
			Multiset<T> result = new Multiset<T>(this.store);

			foreach (var pair in other.store)
				result.Remove(pair.Key, pair.Value);

			return result;
		}


		/// Operators:

		public static bool operator ==(Multiset<T> one, Multiset<T> another) => one.Equals(another);
		public static bool operator !=(Multiset<T> one, Multiset<T> another) => !one.Equals(another);
		public static Multiset<T> operator +(Multiset<T> one, Multiset<T> another) => one.Add(another);
		public static Multiset<T> operator -(Multiset<T> one, Multiset<T> another) => one.Substract(another);

		public static explicit operator Multiset<T>(Dictionary<T, uint> dictionary) => new Multiset<T>(dictionary);
		public static explicit operator Dictionary<T, uint>(Multiset<T> bag) => bag.store;


		/// Overrides:

		public override int GetHashCode() => this.store.GetHashCode();

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(Multiset<T>))
				return false;
			
			return (obj as Multiset<T>).Count == this.Count && !(obj as Multiset<T>).store.Except(this.store).Any();
		}

		public override string ToString()
		{
			string result = "{ ";

			foreach (var pair in this.store)
				result += $"{pair.Key}" + (pair.Value > 1 ? $"({pair.Value}), " : ", ");

			result = result.Substring(0, result.Length - 2);
			result += " }";
			return result;
		}
	}
}