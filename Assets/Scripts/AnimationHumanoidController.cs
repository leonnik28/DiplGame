using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHumanoidController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


}
