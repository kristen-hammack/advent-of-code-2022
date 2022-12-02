// See https://aka.ms/new-console-template for more information

var elfCalories = (await File.OpenText("Day1input.txt")
        .ReadToEndAsync()).Split("\n\n")
    .Select(s => s.Trim())
    .Select(s => s.Split("\n")
        .Select(int.Parse)
        .Sum()).ToList();
var sum = 0;
for (int i = 0; i < 3; i++)
{
    var max = elfCalories.Max();
    sum += max;
    elfCalories.Remove(max);
}
Console.WriteLine(sum);
