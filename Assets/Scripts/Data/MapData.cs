using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class UnitData
{
    public string name;
    public int team;
    public int[] position;
}

[Serializable]
public class GroundData
{
    public string name;
    public int[] position;
}

[Serializable]
public class MapData
{
    public int version;
    public string name;
    public int[] size;
    public List<GroundData> ground;
    public List<UnitData> unit;
}
