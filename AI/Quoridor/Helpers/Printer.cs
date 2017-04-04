using System;
using System.Collections.Generic;
using System.Text;

namespace Quoridor
{
	public static class Printer
	{
	///Private:
		private static int boardSize;
		private static (int X, int Y) cursorPosition
		{
			get => (Console.CursorLeft, Console.CursorTop);
			set
			{
				Console.CursorLeft = value.X;
				Console.CursorTop = value.Y;
			}
		}
		private static (int X, int Y) initialPosition;
		private static (int X, int Y) finalPosition;
		private static Dictionary<string, (int loc, int idx)> scoreLocs = new Dictionary<string, (int loc, int idx)>();
		
	///Public:
		static Printer()
		{
			Console.OutputEncoding = Encoding.UTF8;
			Console.CursorVisible = false;
			boardSize = (Settings.BoardSize << 1);
			PrintFieldInline(Settings.FieldColor);
		}

		public static void Print(this Player p, Wall w)
		{
			var color = Settings.ColorizeWalls ? p.Color : Settings.FieldColor;

			void printWall(string start, string center, string end)
			{
				PrintLiteral(start, w.Start, color);
				PrintLiteral(center, w.Center, color);
				PrintLiteral(end, w.End, color);
			}

			if (w.IsVertical) printWall(" │ ", " • ", " │ ");
			else printWall("───", "╴•╶", "───");
			UpdateCounters(p);
		}

		public static void Print(this Player p)
		{
			if (p.OldLocation != 0)
			{
				PrintLiteral($"   ", p.OldLocation, p.Color);
				PrintLiteral($"   ", (boardSize + 1, p.OldLocation.Y), p.Color);
			}
			PrintLiteral(p.Name[0], p.Location, p.Color);
			PrintLiteral(p.Name[0], (boardSize + 1, p.Location.Y), p.Color);
			UpdateCounters(p);
		}
		
	///Private:
		private static void PrintLiteral(char line, Vector2D location, ConsoleColor color = Settings.FieldColor)
			=> PrintLiteral(line.ToString(), location, color);
			
		private static void PrintLiteral(string line, Vector2D location, ConsoleColor color = Settings.FieldColor)
		{
			Console.ForegroundColor = color;

			if (line.Length == 1) line = $" {line} ";
			if (line.Length == 2) line = $"{line[0]} {line[1]}";

			var internalLocation = (X: location.X * 3 - 2, Y: location.Y);
			cursorPosition = (initialPosition.X + internalLocation.X, initialPosition.Y + internalLocation.Y);
			
			Console.Write(line);
			Console.ResetColor();
			cursorPosition = finalPosition;
		}

		private static void UpdateCounters(Player p)
		{
			void printScore(string line, int xoffset)
				=> PrintLiteral(line, (xoffset, boardSize + 1), p.Color);

			(int loc, int idx) l;

			if (!scoreLocs.ContainsKey(p.Name))
			{
				scoreLocs.Add(p.Name, ((scoreLocs.Count % 2 == 0 ? -1 : boardSize + 1), scoreLocs.Count));
				l = scoreLocs[p.Name];
				printScore(p.Name[0].ToString(), l.loc);
				printScore("│", l.loc + (l.idx % 2 == 0 ? 1 : -1));
			}

			l = scoreLocs[p.Name];
			
			printScore($" {(p.WallsAmount)} Walls ", l.loc + (l.idx % 2 == 0 ? 2 : -8));				
			printScore($" {(p.Steps)} Steps ", l.loc + (l.idx % 2 == 0 ? 6 : -4));
		}

		private static void PrintFieldInline(ConsoleColor color)
		{
			Console.ForegroundColor = color;

			var result = "\n       -----";
			for (int i = 0; i <= boardSize; i++)
				result += "---";
			result += "-----\n\n       ╔════";
			for (int x = 0; x <= boardSize; x++)
				result += x % 2 == 0 ? "═╤═" : "═══";

			result += "════╗\n";
			result += "       ║    ";
			for (int x = 0; x <= boardSize; x++)
				result += x % 2 == 0 ? " │ " : x >> 1 < 10 ? $" {x >> 1} " : $"{x >> 1} ";
			result += "    ║\n";

			for (int y = 0; y <= boardSize; y++, result += "\n")
			{
				if (y % 2 == 0)
					result += "       ╟───";
				else
					result += y >> 1 < 10 ? $"       ║  {y >> 1}" : $"       ║ {y >> 1}";

				for (int x = 0; x <= boardSize; x++)
				{
					if (x == 0 || x == boardSize)
					{
						if (y != 0 && y != boardSize && y % 2 != 0)
							result += x == 0 ? "  │ " : " │  ";
						else if (y % 2 == 0 && !(y == 0 || y == boardSize))
							result += x == 0 ? "──┤ " : " │  ";
						else if (y == 0 && x == boardSize)
							result += "─┼──";
						else if (y == boardSize && x == 0)
							result += "──┴─";
						else if (y == boardSize && x == boardSize)
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
						else if (y == boardSize)
							result += x == boardSize / 2 ? "─┬─" : "───";
						else if (y % 2 == 0 && x % 2 == 0)
							result += " · ";
						else
							result += "   ";
					}
				}
				result += y == 0 || y == boardSize ? "───╢" : "   ║";
			}
			result += "       ║    ";
			for (int x = 0; x <= boardSize; x++)
				result += x == boardSize / 2 ? " │ " : "   ";
			result += "    ║\n";

			result += "       ╚════";
			for (int x = 0; x <= boardSize; x++)
				result +=  x == boardSize / 2 ? "═╧═" : "═══";
			result += "════╝\n";

			Console.Write(result);
			finalPosition = (0, Console.CursorTop + 1);

			Console.ResetColor();
		}
	}
}