using System;
using System.Collections.Generic;

namespace Quoridor
{
	public partial class Game
	{
	///Private:
		private (int X, int Y) cursorPosition
		{
			get => (Console.CursorLeft, Console.CursorTop);
			set
			{
				Console.CursorLeft = value.X;
				Console.CursorTop = value.Y;
			}
		}
		private (int X, int Y) initialPosition;
		private (int X, int Y) finalPosition;
		private Dictionary<string, (int loc, int idx)> scoreLocs = new Dictionary<string, (int loc, int idx)>();

		private void Print(Wall w)
		{
			if (Settings.Log)
			{
				var color = Settings.ColorizeWalls ? w.Owner.Color : Settings.FieldColor;

				void printWall(string start, string center, string end)
				{
					PrintLiteral(start, w.Start, color);
					PrintLiteral(center, w.Center, color);
					PrintLiteral(end, w.End, color);
				}

				if (w.IsVertical) printWall(" │ ", " • ", " │ ");
				else printWall("───", "╴•╶", "───");
				// else printWall("───", " • ", "───");
				UpdateCounters(w.Owner);
			}
		}

		private void Print(Player p)
		{
			if (Settings.Log)
			{
				if (p.Steps > 0 && p.OldLocation != 0)
				{
					PrintLiteral($"   ", p.OldLocation, p.Color);
					PrintLiteral($"   ", (this.board.Size + 1, p.OldLocation.Y), p.Color);
				}
				PrintLiteral(p.Name[0], p.Location, p.Color);
				PrintLiteral(p.Name[0], (this.board.Size + 1, p.Location.Y), p.Color);
				UpdateCounters(p);
			}
		}

		private void PrintLiteral(char line, Vector2D location, ConsoleColor color = Settings.FieldColor)
			=> PrintLiteral(line.ToString(), location, color);
			
		private void PrintLiteral(string line, Vector2D location, ConsoleColor color = Settings.FieldColor)
		{
			if (line.Length == 1) line = $" {line} ";
			if (line.Length == 2) line = $"{line[0]} {line[1]}";

			var internalLocation = (X: location.X * 3 - 2, Y: location.Y);
			PrintInternal(line, internalLocation, color);
		}

		private void PrintInternal(string line, (int X, int Y) position, ConsoleColor color = Settings.FieldColor)
		{
			cursorPosition = (initialPosition.X + position.X, initialPosition.Y + position.Y);
			Console.ForegroundColor = color;
			Console.Write(line);
			Console.ResetColor();
			cursorPosition = finalPosition;
		}

		private void UpdateCounters(Player p)
		{
			void printScore(string line, int xoffset)
				=> PrintLiteral(line, (xoffset, this.board.Size + 1), p.Color);

			(int loc, int idx) l;

			if (!scoreLocs.ContainsKey(p.Name))
			{
				scoreLocs.Add(p.Name, ((scoreLocs.Count % 2 == 0 ? -1 : ((this.board.Size / 2) + 1)), scoreLocs.Count));
				l = scoreLocs[p.Name];
			}

			l = scoreLocs[p.Name];
			
			printScore(p.Name[0].ToString(), l.loc);
			printScore("│", l.loc + 1);
			printScore($"{(p.WallsAmount)} Walls ", l.loc + 2);
			printScore("│", l.loc + 5);
			printScore($"{(p.Steps)} Steps", l.loc + 6);
		}

		private void PrintFieldInline(ConsoleColor color)
		{
			Console.ForegroundColor = color;

			var result = "\n\n       ╔════";
			for (int x = 0; x <= this.board.Size; x++)
				result += x % 2 == 0 ? "═╤═" : "═══";

			result += "════╗\n";
			result += "       ║    ";
			for (int x = 0; x <= this.board.Size; x++)
				result += x % 2 == 0 ? " │ " : x >> 1 < 10 ? $" {x >> 1} " : $"{x >> 1} ";
			result += "    ║\n";

			for (int y = 0; y <= this.board.Size; y++, result += "\n")
			{
				if (y % 2 == 0)
					result += "       ╟───";
				else
					result += y >> 1 < 10 ? $"       ║  {y >> 1}" : $"       ║ {y >> 1}";

				for (int x = 0; x <= this.board.Size; x++)
				{
					if (x == 0 || x == this.board.Size)
					{
						if (y != 0 && y != this.board.Size && y % 2 != 0)
							result += x == 0 ? "  │ " : " │  ";
						else if (y % 2 == 0 && !(y == 0 || y == this.board.Size))
							result += x == 0 ? "──┤ " : " │  ";
						else if (y == 0 && x == this.board.Size)
							result += "─┼──";
						else if (y == this.board.Size && x == 0)
							result += "──┴─";
						else if (y == this.board.Size && x == this.board.Size)
							result += "─┴──";
						else
						{
							result += "──┼─";
							Console.Write(result);
							initialPosition = (Console.CursorLeft - 1, Console.CursorTop);
							result = "";
						}
					}
					else
					{
						if (y == 0)
							result += x % 2 == 0 ? "─┴─" : "───";
						else if (y == this.board.Size)
							result += x == this.board.Size / 2 ? "─┬─" : "───";
						else if (y % 2 == 0 && x % 2 == 0)
							result += " · ";
						else
							result += "   ";
					}
				}
				result += y == 0 || y == this.board.Size ? "───╢" : "   ║";
			}
			result += "       ║    ";
			for (int x = 0; x <= this.board.Size; x++)
				result += x == this.board.Size / 2 ? " │ " : "   ";
			result += "    ║\n";

			result += "       ╚════";
			for (int x = 0; x <= this.board.Size; x++)
				result +=  x == this.board.Size / 2 ? "═╧═" : "═══";
			result += "════╝\n";

			Console.Write(result);
			finalPosition = (0, Console.CursorTop + 1);

			Console.ResetColor();
		}
	}
}