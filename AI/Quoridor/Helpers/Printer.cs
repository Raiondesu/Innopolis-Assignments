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
		private static List<(Player player, ConsoleColor color)> players = new List<(Player player, ConsoleColor color)>();
		private static List<Wall> walls = new List<Wall>();
		
	///Public:
		static Printer()
		{
			Console.OutputEncoding = Encoding.UTF8;
			Printer.boardSize = (Settings.BoardSize << 1);
			if (Settings.PrintFiledInline)
				Printer.PrintFieldInline(Settings.FieldColor);
			else
				Printer.PrintField(Settings.FieldColor);
		}

		public static void Print(this Wall w)
		{
			if (w.IsVertical)
			{
				Printer.PrintLiteral(" │ ", w.Start, w.Color);
				Printer.PrintLiteral(" • ", w.Center, w.Color);
				Printer.PrintLiteral(" │ ", w.End, w.Color);
			}
			else
			{
				Printer.PrintLiteral("───", w.Start, w.Color);
				Printer.PrintLiteral("╴•╶", w.Center, w.Color);
				Printer.PrintLiteral("───", w.End, w.Color);
			}
		}

		public static void Print(this Player p)
		{
			if (Printer.players?.Any(t => t.player == p) ?? false)
			{
				var playerId = Printer.players.IndexOf(Printer.players.First(t => t.player == p));
				Printer.players[playerId] = (p, p.Color);
			}
			else Printer.players.Add((p, p.Color));

			Printer.PrintLiteral($" {p.Name[0]} ", p.Location, p.Color);
		}
		
	///Private:
		private static void PrintLiteral(string line, Vector2D location, ConsoleColor color)
		{
			Console.ForegroundColor = color;

			if (line.Length == 1) line = $" {line} ";
			if (line.Length == 2) line = $"{line[0]} {line[1]}";

			var internalLocation = (X: location.X * 3 - 2, Y: location.Y);
			Printer.cursorPosition = (initialPosition.X + internalLocation.X, initialPosition.Y + internalLocation.Y);
			
			Console.Write(line);
			Console.ResetColor();
			Printer.cursorPosition = finalPosition;
		}

		private static void PrintField(ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.Write("\n  ╔════");
			for (int x = 0; x <= boardSize; x++)
			{
				Console.Write(x % 2 == 0 ? "═╤═" : "═══");
			}
			Console.Write("════╗\n");
			Console.Write("  ║    ");
			for (int x = 0; x <= boardSize; x++)
				Console.Write(x % 2 == 0 ? " │ " : x >> 1 < 10 ? $" {x >> 1} " : $"{x >> 1} ");
			Console.Write("    ║\n");
			for (int y = 0; y <= boardSize; y++, Console.WriteLine())
			{
				Console.Write(y % 2 == 0 ? "  ╟───" : y >> 1 < 10 ? $"  ║  {y >> 1}" : $"  ║ {y >> 1}");
				for (int x = 0; x <= boardSize; x++)
				{
					if (x == 0 || x == boardSize)
					{
						if (y != 0 && y != boardSize && y % 2 != 0)
							Console.Write(x == 0 ? "  │ " : " │  ");
						else if (y % 2 == 0 && !(y == 0 || y == boardSize))
							Console.Write(x == 0 ? "──┤ " : " ├──");
						else if (y == boardSize && x == 0)
							Console.Write("──┼─");
						else if ((y == 0 || y == boardSize) && x == boardSize)
							Console.Write("─┼──");
						else
						{
							Console.Write("──┼─");
							// Console.Write(result);
							Printer.initialPosition = (Console.CursorLeft - 1, Console.CursorTop);
							// result = "";
						}
					}
					else
					{
						if (y == 0)
							Console.Write(x % 2 == 0 ? "─┴─" : "───");
						else if (y == boardSize)
							Console.Write(x % 2 == 0 ? "─┬─" : "───");
						else if (y % 2 == 0 && x % 2 == 0)
							Console.Write(" · ");
						else
							Console.Write("   ");
					}
				}
				Console.Write(y % 2 == 0 ? "───╢" : "   ║");
			}
			Console.Write("  ║    ");
			for (int x = 0; x <= boardSize; x++)
				Console.Write(x % 2 == 0 ? " │ " : "   ");
			Console.Write("    ║\n");

			Console.Write("  ╚════");
			for (int x = 0; x <= boardSize; x++)
			{
				Console.Write(x % 2 == 0 ? "═╧═" : "═══");
			}
			Console.Write("════╝\n");
			// Console.Write(result);
			Printer.finalPosition = (0, Console.CursorTop + 1);
			Console.ResetColor();
		}

		private static void PrintFieldInline(ConsoleColor color)
		{
			Console.ForegroundColor = color;

			var result = "\n  ╔════";
			for (int x = 0; x <= boardSize; x++)
				result += x % 2 == 0 ? "═╤═" : "═══";

			result += "════╗\n";
			result += "  ║    ";
			for (int x = 0; x <= boardSize; x++)
				result += x % 2 == 0 ? " │ " : x >> 1 < 10 ? $" {x >> 1} " : $"{x >> 1} ";
			result += "    ║\n";

			for (int y = 0; y <= boardSize; y++, result += "\n")
			{
				if (y % 2 == 0)
					result += "  ╟───";
				else
					result += y >> 1 < 10 ? $"  ║  {y >> 1}" : $"  ║ {y >> 1}";

				for (int x = 0; x <= boardSize; x++)
				{
					if (x == 0 || x == boardSize)
					{
						if (y != 0 && y != boardSize && y % 2 != 0)
							result += x == 0 ? "  │ " : " │  ";
						else if (y % 2 == 0 && !(y == 0 || y == boardSize))
							result += x == 0 ? "──┤ " : " ├──";
						else if (y == boardSize && x == 0)
							result += "──┼─";
						else if ((y == 0 || y == boardSize) && x == boardSize)
							result += "─┼──";
						else
						{
							result += "──┼─";
							Console.Write(result);
							Printer.initialPosition = (Console.CursorLeft - 1, Console.CursorTop);
							result = "";
						}
					}
					else
					{
						if (y == 0)
							result += x % 2 == 0 ? "─┴─" : "───";
						else if (y == boardSize)
							result += x % 2 == 0 ? "─┬─" : "───";
						else if (y % 2 == 0 && x % 2 == 0)
							result += " · ";
						else
							result += "   ";
					}
				}
				result += y % 2 == 0 ? "───╢" : "   ║";
			}
			result += "  ║    ";
			for (int x = 0; x <= boardSize; x++)
				result += x % 2 == 0 ? " │ " : "   ";
			result += "    ║\n";

			result += "  ╚════";
			for (int x = 0; x <= boardSize; x++)
				result += x % 2 == 0 ? "═╧═" : "═══";
			result += "════╝\n";

			Console.Write(result);
			Printer.finalPosition = (0, Console.CursorTop + 1);

			Console.ResetColor();
		}
	}
}