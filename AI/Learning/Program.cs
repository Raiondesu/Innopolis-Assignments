namespace Learning
{
	using System;
	using Engine;

    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                var map = new Map((4, 4));
                var player = new Player((0, 0));
                var apple = new Collectable((2, 3), 200);
                var cake = new Collectable((2, 3), 900);
                var lava = new Collectable((2, 3), -100);
                var pits = new [] {
                    new Collectable((1, 1), -1000),
                    new Collectable((1, 2), -1000),
                    new Collectable((1, 3), -1000),
                    new Collectable((3, 3), -1000)
                };
                map.Actors.Add(apple);
                map.Actors.Add(cake);
                map.Actors.AddRange(pits);
                map.Actors.Add(player);
                
                player.PlayOn(ref map);

                Console.WriteLine($"Score: {player.Score}; \"Enter\" to continue...");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }
    }
}
