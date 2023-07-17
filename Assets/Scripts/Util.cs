using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Util
{
    public static int ManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }
}