using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    [Header("Properties")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 0.1f;

    public void Move(Vector2 inputVector)
    {
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        if (moveDirection != Vector3.zero)
        {
            Rotate(moveDirection);
        }

        moveDirection = _characterController.transform.forward * inputVector.y + _characterController.transform.right * inputVector.x;
        _characterController.Move(_moveSpeed * Time.deltaTime * moveDirection);
    }

    private void Rotate(Vector3 moveDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }
}
