using System;

namespace AdventOfCode2024.Common;

class Coordinate
{
    public int Row;
    public int Col;

    public Coordinate() : this(0, 0) { }

    public Coordinate(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public Coordinate(Coordinate other)
    {
        Row = other.Row;
        Col = other.Col;
    }

    public static readonly Coordinate Up = new(-1, 0);
    public static readonly Coordinate Right = new(0, 1);
    public static readonly Coordinate Down = new(1, 0);
    public static readonly Coordinate Left = new(0, -1);
    public static readonly Coordinate[] Orthogonal = [Up, Right, Down, Left];

    public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.Row + b.Row, a.Col + b.Col);
    public static Coordinate operator -(Coordinate a, Coordinate b) => new(a.Row - b.Row, a.Col - b.Col);
    public static Coordinate operator *(Coordinate a, int coef) => new(a.Row * coef, a.Col * coef);

    public static bool operator ==(Coordinate left, Coordinate right) => left.Row == right.Row && left.Col == right.Col;

    public static bool operator !=(Coordinate left, Coordinate right) => !(left == right);
    public int DistanceFrom(Coordinate target) => Math.Abs(Row - target.Row) + Math.Abs(Col - target.Col); 
    
    public override bool Equals(object? obj)=> obj is Coordinate other && this == other;

    public override int GetHashCode() => HashCode.Combine(Row, Col);

    public override string ToString() => $"({Row}, {Col})";

    public IEnumerable<Coordinate> GetOrthogonals()
    {
        foreach(Coordinate offset in Orthogonal)
            yield return this + offset;
    }
}