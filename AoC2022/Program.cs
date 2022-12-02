// See https://aka.ms/new-console-template for more information

Console.WriteLine((await File.OpenText("input.txt")
        .ReadToEndAsync()).Split("\n\n")
    .Select(s => s.Trim())
    .Select(s => s.Split("\n")
        .Select(int.Parse)
        .Sum())
    .Max());
