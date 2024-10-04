using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    public List<BiomeData> Biomes;

    [Serializable]
    public class BiomeData
    {
        public string BiomeName;
        public Vector3 Position;
    }
}
