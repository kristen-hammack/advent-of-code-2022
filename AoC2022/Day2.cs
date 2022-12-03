namespace AoC2022;

public class Day2
{
    public static async Task Do()
    {
        var input = (await File.OpenText("Day2input.txt").ReadToEndAsync()).Split("\n",
            StringSplitOptions.RemoveEmptyEntries);
        //x=1, y=2, z=3
        //x>c, y>a, z>b
        Console.WriteLine(input.Select(s => (RPS)s[2] > (RPS)s[0] ? 6 : (RPS)s[0] == (RPS)s[2] ? 3 : 0).Sum() +
                          input.Select(s => s[2]).Select(c => c == 'Z' ? 3 : c == 'Y' ? 2 : 1).Sum());
    }
}

public class RPS
{
    private char C { get; set; }

    public static explicit operator RPS(char c) => new()
    {
        C = c switch { 'X' or 'A' => 'R', 'Y' or 'B' => 'P', 'Z' or 'C' => 'S', _ => '_' }
    };
    
    public static bool operator >(RPS r, RPS p)
    {
        return r != p && (r.C switch
        {
            'R' => p.C == 'S',
            'P' => p.C == 'R',
            'S' => p.C == 'P',
            _ => false
        });
    }
    public static bool operator <(RPS r, RPS p) => !(r > p) && r != p;
    public static bool operator ==(RPS r, RPS p) => r.C == p.C;
    public static bool operator !=(RPS r, RPS p) => r.C != p.C;
}
