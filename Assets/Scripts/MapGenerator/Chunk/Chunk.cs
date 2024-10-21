using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector2 Size => _size;
    public GameObject DoorUp => _doorUp;
    public GameObject DoorDown => _doorDown;
    public GameObject DoorRight => _doorRight;
    public GameObject DoorLeft => _doorLeft;

    [SerializeField] private Vector2 _size;
    [SerializeField] private GameObject _doorUp;
    [SerializeField] private GameObject _doorDown;
    [SerializeField] private GameObject _doorRight;
    [SerializeField] private GameObject _doorLeft;

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

        GameObject tmp = _doorLeft;
        _doorLeft = _doorDown;
        _doorDown = _doorRight;
        _doorRight = _doorUp;
        _doorUp = tmp;
    }
}
