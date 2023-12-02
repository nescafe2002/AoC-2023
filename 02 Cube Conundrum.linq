<Query Kind="Statements">
  <Reference Relative="02 input.txt">C:\Drive\Challenges\AoC 2023\02 input.txt</Reference>
</Query>

var lines = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green".Split("\r\n");

lines = File.ReadAllLines("02 input.txt");

var data =
  from l in lines
  let g = l.Split(": ")
  select (Game: int.Parse(g[0].Split(' ')[1]), Draws: g[1].Split("; ").Select(y => y.Split(", ").Select(z => z.Split(' ')).Select(z => (Count: int.Parse(z[0]), Color: z[1])).ToArray()).ToArray());

var constraints = new Dictionary<string, int>{
  {"red", 12},
  {"green", 13},
  {"blue", 14},
};

data.Where(x => x.Draws.All(y => y.All(z => !constraints.TryGetValue(z.Color, out var w) || z.Count <= w))).Sum(x => x.Game).Dump("Answer 1");

data.Select(x => x.Draws.SelectMany(y => y).GroupBy(y => y.Color, y => y.Count).Select(y => y.Max())).Select(x => x.Aggregate((y, z) => y * z)).Sum().Dump("Answer 2");