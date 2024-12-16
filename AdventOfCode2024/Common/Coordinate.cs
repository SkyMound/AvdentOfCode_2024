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

    public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.Row + b.Row, a.Col + b.Col);
    public static Coordinate operator -(Coordinate a, Coordinate b) => new(a.Row - b.Row, a.Col - b.Col);
    public static Coordinate operator *(Coordinate a, int coef) => new(a.Row * coef, a.Col * coef);

    public override bool Equals(object? obj)
    {
        if (obj is not Coordinate other)
            return false;

        return Row == other.Row && Col == other.Col;
    }

    public override int GetHashCode() => HashCode.Combine(Row, Col);

    public override string ToString() => $"({Row}, {Col})";
}