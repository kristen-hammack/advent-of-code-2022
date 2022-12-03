using System.Data.SqlTypes;

namespace AoC2022;

public class Day2
{
    public static async Task Do()
    {
        var input = (await File.OpenText("Day2input.txt").ReadToEndAsync()).Split("\n",
            StringSplitOptions.RemoveEmptyEntries);
        //x=1, y=2, z=3
        //x>c, y>a, z>b
        Console.WriteLine(input.Select(s => ((RPS)s[0]).Points(s[2])).Sum());
    }
}

public class RPS
{
    private char C { get; set; }

    public static explicit operator RPS(char c) => new()
    {
        C = c switch { 'X' or 'A' => 'R', 'Y' or 'B' => 'P', 'Z' or 'C' => 'S', _ => '_' }
    };

    public int Points(char xyz)
    {
        return xyz switch { 'X' => Lose(), 'Y' => Draw(), 'Z' => Win(), _ => 0 };
    }

    private int Win()
    {
        return
            C switch
            {
                'R' => 2,
                'P' => 3,
                'S' => 1,
                _ => 0
            } + 6;
    }

    private int Lose()
    {
        return C switch
        {
            'R' => 3,
            'P' => 1,
            'S' => 2,
            _ => 0
        };
    }

    private int Draw()
    {
        return C switch
        {
            'R' => 1,
            'P' => 2,
            'S' => 3,
            _ => 0
        } + 3;
    }
    
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
