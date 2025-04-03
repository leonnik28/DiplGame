using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector2 Size => _size;
    public UnityEngine.GameObject DoorUp => _doorUp;
    public UnityEngine.GameObject DoorDown => _doorDown;
    public UnityEngine.GameObject DoorRight => _doorRight;
    public UnityEngine.GameObject DoorLeft => _doorLeft;

    [SerializeField] private Vector2 _size;
    [SerializeField] private UnityEngine.GameObject _doorUp;
    [SerializeField] private UnityEngine.GameObject _doorDown;
    [SerializeField] private UnityEngine.GameObject _doorRight;
    [SerializeField] private UnityEngine.GameObject _doorLeft;

    public void RotateRandomly(System.Random random)
    {
        int randomRotation = random.Next(0, 4);

        for (int i = 0; i < randomRotation; i++)
        {
            RotateClockwise();
        }
    }

    private void RotateClockwise()
    {
        transform.Rotate(0, 90, 0);

        UnityEngine.GameObject tmp = _doorLeft;
        _doorLeft = _doorDown;
        _doorDown = _doorRight;
        _doorRight = _doorUp;
        _doorUp = tmp;
    }
}
