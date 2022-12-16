using System.Collections;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace AoC2022;

public class Day11
{
    public static async Task Do()
    {
        var input = (await File.OpenText("Day11input.txt").ReadToEndAsync());
        var monkeys = new List<Monkey>();
        string pattern = @"Monkey (?'monkey'\w):\n\s+Starting items: (?'startingItems'.+)\n\s+operation: (?'operation'new = (?'leftOp'.+) (?'op'.) (?'rightOp'.+))\n\s+test: (?'test'divisible by (?'divisibleBy'\d+))\n\s+if true: throw to monkey (?'ifTrue'.+)\n\s+if false: throw to monkey (?'ifFalse'.+)";
        RegexOptions options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
        foreach (Match m in Regex.Matches(input, pattern, options))
        {
            var monkey = new Monkey
            {
                Worries =
                    new Queue<int>(m.Groups["startingItems"].Value.Split(",", StringSplitOptions.TrimEntries)
                        .Select(int.Parse)),
                IfTrue = int.Parse(m.Groups["ifTrue"].Value),
                IfFalse = int.Parse(m.Groups["ifFalse"].Value),
                Test = int.Parse(m.Groups["divisibleBy"].Value),
                Operation = (old) =>
                {
                    var right = m.Groups["rightOp"].Value == "old" ? old : int.Parse(m.Groups["rightOp"].Value);
                    return m.Groups["op"].Value switch
                    {
                        "*" => old * right,
                        "+" => old + right,
                        _ => throw new ArgumentException()
                    };
                }
            };
            monkeys.Add(monkey);
        }

        for (int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                var count = monkey.Worries.Count;
                for (int j = 0; j < count; j++)
                {
                    monkey.Inspections++;
                    var worry = monkey.Worries.Dequeue();
                    worry = monkey.Operation(worry);
                    worry /= 3;
                    monkeys[worry % monkey.Test == 0 ? monkey.IfTrue : monkey.IfFalse].Worries.Enqueue(worry);
                }
            }
        }

        var total =monkeys.OrderByDescending(m => m.Inspections)
            .Take(2)
            .Select(m => m.Inspections)
            .Aggregate((a, b) => a * b);
        Console.WriteLine(total);
    }
    
}

public class Monkey
{
    public int Inspections { get; set; }
    public Func<int, int> Operation { get; set; }
    public Queue<int> Worries { get; set; }
    public int Test { get; set; }
    public int IfTrue { get; set; }
    public int IfFalse { get; set; }
}

