using System;
using System.Collections.Generic;
using System.Linq;
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
		private static List<Wall> walls = new List<Wall>();
		
	///Public:
		static Printer()
		{
			Console.OutputEncoding = Encoding.UTF8;
			boardSize = (Settings.BoardSize << 1);
			PrintFieldInline(Settings.FieldColor);
		}

		public static void Print(this Player p, Wall w)
		{
			var color = Settings.ColorizeWalls ? w.Color : Settings.FieldColor;
			if (w.IsVertical)
			{
				PrintLiteral(" │ ", w.Start, color);
				PrintLiteral(" • ", w.Center, color);
				PrintLiteral(" │ ", w.End, color);
			}
			else
			{
				PrintLiteral("───", w.Start, color);
				PrintLiteral("╴•╶", w.Center, color);
				PrintLiteral("───", w.End, color);
			}

			UpdateCounters(p);
		}

		public static void Print(this Player p)
		{
			if (p.OldLocation != 0)
				PrintLiteral($"   ", p.OldLocation, p.Color);
			PrintLiteral($" {p.Name[0]} ", p.Location, p.Color);
			UpdateCounters(p);
		}
		
	///Private:
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
			(int loc, int idx) l;

			if (!scoreLocs.ContainsKey(p.Name))
			{
				scoreLocs.Add(p.Name, ((scoreLocs.Count % 2 == 0 ? -1 : boardSize + 1), scoreLocs.Count));
				l = scoreLocs[p.Name];
				PrintLiteral(p.Name[0].ToString(), (l.loc, -3), p.Color);
				PrintLiteral("Walls", (l.loc + (l.idx % 2 == 0 ? -3 : 2), -1), p.Color);
				PrintLiteral("Steps", (l.loc + (l.idx % 2 == 0 ? -3 : 2), boardSize + 1), p.Color);
			}

			l = scoreLocs[p.Name];
			
			PrintLiteral($"{(p.WallsAmount)}", (l.loc, -1), p.Color);				
			PrintLiteral($"{(p.Steps)}", (l.loc, boardSize + 1), p.Color);
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
							result += "───";
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
				result += "   ";
			result += "    ║\n";

			result += "       ╚════";
			for (int x = 0; x <= boardSize; x++)
				result += "═══";
			result += "════╝\n";

			Console.Write(result);
			finalPosition = (0, Console.CursorTop + 1);

			Console.ResetColor();
		}
	}
}