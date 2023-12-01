<Query Kind="Statements">
  <Reference Relative="01 input.txt">C:\Drive\Challenges\AoC 2023\01 input.txt</Reference>
</Query>

var input = @"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";

input = File.ReadAllText("01 input.txt").Trim();

var re1 = new Regex("(one|two|three|four|five|six|seven|eight|nine)");
var re2 = new Regex("(one|two|three|four|five|six|seven|eight|nine)", RegexOptions.RightToLeft);

var dic = new Dictionary<string, string> {
  { "one", "1" },
  { "two", "2" },
  { "three", "3" },
  { "four", "4" },
  { "five", "5" },
  { "six", "6" },
  { "seven", "7" },
  { "eight", "8" },
  { "nine", "9" },
};

var lines =
  from line in input.Split("\n")
  let line1 = re1.Replace(line, m => dic[m.Value], 1)
  let line2 = re2.Replace(line, m => dic[m.Value], 1)
  let digits = line.Where(x => x > '0' && x <= '9').Select(x => (int)x - '0').ToArray()
  let digits1 = line1.Where(x => x > '0' && x <= '9').Select(x => (int)x - '0').ToArray()
  let digits2 = line2.Where(x => x > '0' && x <= '9').Select(x => (int)x - '0').ToArray()
  select (digits.First() * 10 + digits.Last(), digits1.First() * 10 + digits2.Last());

lines.Sum(x => x.Item1).Dump("Answer 1");
lines.Sum(x => x.Item2).Dump("Answer 2");