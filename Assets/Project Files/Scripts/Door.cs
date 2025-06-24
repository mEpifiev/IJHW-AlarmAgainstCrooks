using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour, IInteractable
{
    private readonly int IsOpen = Animator.StringToHash(nameof(IsOpen));

    private Animator _animator;

    private bool _isOpen = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        _isOpen = !_isOpen;

        _animator.SetBool(IsOpen, _isOpen);
    }
}