<Query Kind="Statements">
  <Reference Relative="03 input.txt">C:\Drive\Challenges\AoC 2023\03 input.txt</Reference>
</Query>

var input = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..".Split("\r\n");

input = File.ReadAllLines("03 input.txt");

var grid = Enumerable.Range(0, input.Length).SelectMany(x => Enumerable.Range(0, input[x].Length).Select(y => (x, y))).ToDictionary(x => x, x => input[x.x][x.y]);

var re = new Regex(@"\d+");

var numbers = Enumerable.Range(0, input.Length).SelectMany(x => from m in re.Matches(input[x]) select (x, m.Index, m.Length, m.Value));

numbers.SelectMany(n =>
  from x in Enumerable.Range(n.x - 1, 3)
  from y in Enumerable.Range(n.Index - 1, n.Length + 2)
  where x >= 0 && x < input.Length && y >= 0 && y < input[0].Length
  let v = grid[(x, y)]
  where (v < '0' || v > '9') && v != '.'
  select int.Parse(n.Value)
).Sum().Dump("Answer 1");

var gears =
  from n in numbers
  from x in Enumerable.Range(n.x - 1, 3)
  from y in Enumerable.Range(n.Index - 1, n.Length + 2)
  where x >= 0 && x < input.Length && y >= 0 && y < input[0].Length
  let v = grid[(x, y)]
  where v == '*'
  group int.Parse(n.Value) by (x, y) into grp
  where grp.Count() > 1
  select grp;

gears.Select(x => x.Aggregate((y, z) => y * z)).Sum().Dump("Answer 2");