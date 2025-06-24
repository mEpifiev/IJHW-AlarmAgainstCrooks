using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float _distance;

    private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        if (_inputReader.IsInteract)
            TryInteract();
    }

    private void TryInteract()
    {
        Ray ray = Camera.main.ScreenPointToRay(_inputReader.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance))
            if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
                interactable.Interact();
    }        
}
