using System.Collections.Generic;

namespace Quoridor
{
	public struct Tree<T>
	{
		public Tree(T root) => this.Root = new Node(root);

		public Node Root { get; }

		public struct Node
		{
			public Node(T value)
			{
				this.Value = value;
				this.Children = new List<Node>();
			}

			public T Value { get; set; }
			public List<Node> Children { get; }			
		}
	}
}