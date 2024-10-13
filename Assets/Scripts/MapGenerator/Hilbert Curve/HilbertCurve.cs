using UnityEngine;

public static class HilbertCurve
{
    public static Vector2Int[] GenerateHilbertCurve(int order)
    {
        int n = (int)Mathf.Pow(2, order);
        Vector2Int[] points = new Vector2Int[n * n];
        GenerateHilbertCurveRecursive(order, 0, 0, n, points, 0);
        return points;
    }

    private static void GenerateHilbertCurveRecursive(int order, int x, int y, int size, Vector2Int[] points, int index)
    {
        if (order == 0)
        {
            points[index] = new Vector2Int(x, y);
            return;
        }

        int halfSize = size / 2;
        int nextOrder = order - 1;

        GenerateHilbertCurveRecursive(nextOrder, x, y, halfSize, points, index);
        GenerateHilbertCurveRecursive(nextOrder, x + halfSize, y, halfSize, points, index + halfSize * halfSize);
        GenerateHilbertCurveRecursive(nextOrder, x + halfSize, y + halfSize, halfSize, points, index + 2 * halfSize * halfSize);
        GenerateHilbertCurveRecursive(nextOrder, x, y + halfSize, halfSize, points, index + 3 * halfSize * halfSize);
    }
}