using UnityEngine;

public class PlayerMovement
{
    private Rigidbody _rigidbody;
    private Animator _animator;

    private PlayerMovementSettings _settings;

    public PlayerMovement(Rigidbody rigidbody, Animator animator, PlayerMovementSettings playerMovementSettings)
    {
        _rigidbody = rigidbody;
        _animator = animator;
        _settings = playerMovementSettings;
    }

    public void Move(Vector2 inputVector, Transform transform)
    {
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        if (moveDirection != Vector3.zero)
        {
            Rotate(moveDirection, transform);
            _animator.SetInteger("animation", 10);
        }
        else
        {
            _animator.SetInteger("animation", 1);
        }

        Vector3 velocity = moveDirection * _settings.MoveSpeed;
        _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
    }

    private void Rotate(Vector3 moveDirection, Transform transform)
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _settings.RotationSpeed);
    }
}
