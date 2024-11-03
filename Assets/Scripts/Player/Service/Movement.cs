using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;

    [Header("Properties")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 0.1f;

    public void Move(Vector2 inputVector)
    {
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        if (moveDirection != Vector3.zero)
        {
            Rotate(moveDirection);
            _animator.SetInteger("animation", 10);
        }
        else
        {
            _animator.SetInteger("animation", 1);
        }

        Vector3 velocity = moveDirection * _moveSpeed;
        _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
    }

    private void Rotate(Vector3 moveDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }
}
