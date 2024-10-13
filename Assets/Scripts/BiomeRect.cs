using UnityEngine;

public class BiomeRect
{
    public Rect Rect { get; set; }
    public string BiomeName { get; set; }

    public BiomeRect(Rect rect, string biomeName)
    {
        Rect = rect;
        BiomeName = biomeName;
    }
}
