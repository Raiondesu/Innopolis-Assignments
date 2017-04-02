using System;
using System.Linq;

namespace Quoridor
{
	public partial class QuoridorGame
	{
		public void Print()
		{
			var p2_walls = this.Players[1].WallsAmount;
			
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write($"\n\n\t{this.Players[1]} - {p2_walls}\n\n");
			Console.Write("\t");
			if (p2_walls >= 1) Console.Write("│ ");
			for (int i = 0; i <= this.BoardSize; i++)
				Console.Write(i % 2 != 0 && i < (p2_walls - 1) * 2 ? " │ " : "   ");
			Console.Write("\n\t");
			if (p2_walls >= 1) Console.Write("│ ");
			for (int i = 0; i <= this.BoardSize; i++)
				Console.Write(i % 2 != 0 && i < (p2_walls - 1) * 2 ? " │ " : "   ");
			Console.Write("\n\t");
			if (p2_walls >= 1) Console.Write("│ ");
			for (int i = 0; i <= this.BoardSize; i++)
				Console.Write(i % 2 != 0 && i < (p2_walls - 1) * 2 ? " │ " : "   ");
			Console.ResetColor();

			Console.Write($"\n{this.BoardSize + 1}\t╠═");
			for (int i = 0; i < this.BoardSize; i++)
				Console.Write(i % 2 == 0 ? "═══" : "═╩═");
			Console.Write("═╣\n");

			for (int y = this.BoardSize; y > 0; y--)
			{
				Console.Write(y + "\t");
				Console.Write("║ ");
				for (int x = 1; x <= this.BoardSize; x++)
				{
					if (this._board.Walls.Any(k => k.Value.Any(w => w.LiesOn(new Vector2D(x, y)))))
					{
						var wa = this._board.Walls.First(k => k.Value.Any(w => w.LiesOn(new Vector2D(x, y))));
						if (wa.Key == this.Players[1]) Console.ForegroundColor = ConsoleColor.Blue;
						else Console.ForegroundColor = ConsoleColor.Red;
						var wl = wa.Value.Find(w => w.LiesOn(new Vector2D(x, y)));
						if (wl.IsVertical)
							Console.Write(" │ ");
						else
						{
							Console.Write("─────────");
							x += 2;
						}
						Console.ResetColor();
					}
					else if (x % 2 != 0 && y % 2 != 0 && this.Players.Any(p => p.Location == new Vector2D(x, y)))
					{
						Player pl = this.Players.First(p => p.Location == new Vector2D(x, y));
						if (pl == this.Players[1]) Console.ForegroundColor = ConsoleColor.Blue;
						else Console.ForegroundColor = ConsoleColor.Red;
						Console.Write(" " + pl.Name[0] + " ");
						Console.ResetColor();
					}
					else if (x % 2 == 0)
						Console.Write((y % 2 == 0) ? "─┼─" : "   ");
					else 
						Console.Write("   ");
				}
				Console.Write(" ║\n");
			}
			
			Console.Write("0\t╠═");
			for (int i = 0; i < this.BoardSize; i++)
				Console.Write(i % 2 == 0 ? "═══" : "═╦═");
			Console.Write("═╣\n");
			
			var p1_walls = this.Players[0].WallsAmount;

			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("\t");
			if (p1_walls >= 1) Console.Write("│ ");
			for (int i = 0; i <= this.BoardSize; i++)
				Console.Write(i % 2 != 0 && i < (p1_walls - 1) * 2 ? " │ " : "   ");
			Console.Write("\n\t");
			if (p1_walls >= 1) Console.Write("│ ");
			for (int i = 0; i <= this.BoardSize; i++)
				Console.Write(i % 2 != 0 && i < (p1_walls - 1) * 2 ? " │ " : "   ");
			Console.Write("\n\t");
			if (p1_walls >= 1) Console.Write("│ ");
			for (int i = 0; i <= this.BoardSize; i++)
				Console.Write(i % 2 != 0 && i < (p1_walls - 1) * 2 ? " │ " : "   ");
			
			Console.Write("\n\n\t");
			for (int i = 0; i <= this.BoardSize + 1; i++)
				Console.Write(i + (i >= 10 ? " " : "  "));

			Console.Write($"\n\n\t{Players[0]} - {p1_walls}\n\n");
			Console.ResetColor();
		}

		public override string ToString()
		{
			var p2_walls = this.Players[1].WallsAmount;
			var result = $"\n\n\t{Players[1]} - {p2_walls}\n\n";

			result += "\t";
			if (p2_walls >= 1) result += "│ ";
			for (int i = 0; i <= BoardSize; i++)
				result += i % 2 != 0 && i < (p2_walls - 1) * 2 ? " │ " : "   ";
			result += "\n\t";
			if (p2_walls >= 1) result += "│ ";
			for (int i = 0; i <= BoardSize; i++)
				result += i % 2 != 0 && i < (p2_walls - 1) * 2 ? " │ " : "   ";
			result += "\n\t";
			if (p2_walls >= 1) result += "│ ";
			for (int i = 0; i <= BoardSize; i++)
				result += i % 2 != 0 && i < (p2_walls - 1) * 2 ? " │ " : "   ";

			result += $"\n{BoardSize + 1}\t╠═";
			for (int i = 0; i < BoardSize; i++)
				result += i % 2 == 0 ? "═══" : "═╩═";
			result += "═╣\n";

			for (int y = BoardSize; y > 0; y--)
			{
				result += y + "\t";
				result += "║ ";
				for (int x = 1; x <= BoardSize; x++)
				{
					if (this._board.Walls.Any(k => k.Value.Any(w => w.LiesOn(new Vector2D(x, y)))))
					{
						var wa = this._board.Walls.First(k => k.Value.Any(w => w.LiesOn(new Vector2D(x, y))));
						var wl = wa.Value.Find(w => w.LiesOn(new Vector2D(x, y)));
						if (wl.IsVertical)
							result += " │ ";
						else
						{
							result += "─────────";
							x += 2;
						}
					}
					else if (x % 2 != 0 && y % 2 != 0 && this.Players.Any(p => p.Location == new Vector2D(x, y)))
					{
						Player pl = this.Players.First(p => p.Location == new Vector2D(x, y));
						result += " " + pl.Name[0] + " ";
					}
					else if (x % 2 == 0)
						result += (y % 2 == 0) ? "─┼─" : "   ";
					else 
						result += "   ";
				}
				result += " ║\n";
			}
			
			result += "0\t╠═";
			for (int i = 0; i < BoardSize; i++)
				result += i % 2 == 0 ? "═══" : "═╦═";
			result += "═╣\n";
			
			var p1_walls = this.Players[0].WallsAmount;

			result += "\t";
			if (p1_walls >= 1) result += "│ ";
			for (int i = 0; i <= BoardSize; i++)
				result += i % 2 != 0 && i < (p1_walls - 1) * 2 ? " │ " : "   ";
			result += "\n\t";
			if (p1_walls >= 1) result += "│ ";
			for (int i = 0; i <= BoardSize; i++)
				result += i % 2 != 0 && i < (p1_walls - 1) * 2 ? " │ " : "   ";
			result += "\n\t";
			if (p1_walls >= 1) result += "│ ";
			for (int i = 0; i <= BoardSize; i++)
				result += i % 2 != 0 && i < (p1_walls - 1) * 2 ? " │ " : "   ";
			
			result += "\n\n\t";
			for (int i = 0; i <= BoardSize + 1; i++)
				result += i + (i >= 10 ? " " : "  ");

			result += $"\n\n\t{Players[0]} - {p1_walls}\n";

			return result;
		}
	}
}