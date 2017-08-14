using System;
using System.Linq;
using System.Collections.Generic;

namespace MultisetTask
{
	public class Multiset<T>
	{
		private Dictionary<T, uint> store;
		
		private Multiset(Dictionary<T, uint> dictionary) => this.store = dictionary;
		public Multiset() : this(new Dictionary<T, uint>()) {}

		/// Get & Set for specific elements
		public uint this[T key]
		{
			get => this.store[key];
			set => this.store[key] = value;
		}


		/// Classic set operations

		public void Add(T value, uint quantity = 1)
		{
			if (store.ContainsKey(value))
				store[value] += quantity;
			else
				store.Add(value, quantity);
		}

		public void Remove(T value, uint quantity)
		{
			if (store.ContainsKey(value))
			{
				if (store[value] - quantity > 0)
					store[value] -= quantity;
				else
					store.Remove(value);
			}
		}

		public void Remove(T value)
		{
			if (store.ContainsKey(value))
				store.Remove(value);
		}


		/// Add & Substract for Multiset<T>

		public Multiset<T> Add(Multiset<T> other)
		{
			Multiset<T> result = new Multiset<T>(this.store);

			foreach (var pair in other.store)
				result.store.Add(pair.Key, pair.Value);
			
			return result;
		}

		public Multiset<T> Substract(Multiset<T> other)
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
			
			return (obj as Multiset<T>).store.Count == this.store.Count && !(obj as Multiset<T>).store.Except(this.store).Any();
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