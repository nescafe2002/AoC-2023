<Query Kind="Statements">
  <Reference Relative="04 input.txt">C:\Drive\Challenges\AoC 2023\04 input.txt</Reference>
</Query>

var input = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11".Split("\r\n");

input = File.ReadAllLines("04 input.txt");

var cards = input.Select(x => x.Split(": ")[1].Split(" | ").Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)).ToArray()).Select(x => x[0].Intersect(x[1]).Count()).ToArray();

cards.Where(x => x > 0).Sum(x => Math.Pow(2, x - 1)).Dump("Answer 1");

var counts = Enumerable.Range(0, cards.Length).Select(x => 1).ToArray();

for (int i = 0; i < cards.Length; i++)
{
  for (int j = 1; j <= cards[i]; j++)
  {
    counts[i + j] += counts[i];
  }
}

counts.Sum().Dump("Answer 2");