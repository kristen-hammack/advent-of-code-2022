namespace aoc;

public class Day2
{
    public async Task Do()
    {
        var input =(await File.OpenText("Day1input.txt").ReadToEndAsync()).Split("\n");
        //x=1, y=2, z=3
        //x>c, y>a, z>b
        
    }
    
}

public class rps
{
    public char C { get; set; }

    public static implicit operator rps(char c) => new() { C = c };
    
    public static bool operator >(rps r, rps p)
    {
        return r != p && (r.C == 'x' ? p.C == 'c' : r.C == 'y' ? p.C == 'a' : p.C == 'b');
    }
    public static bool operator <(rps r, rps p) => !(r > p) && r != p;
    public static bool operator ==(rps r, rps p) => r.C == p.C;
    public static bool operator !=(rps r, rps p) => r.C != p.C;
}
