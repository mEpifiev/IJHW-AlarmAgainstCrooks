using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    public float HorizontalDirection { get; private set; }
    public float VerticalDirection { get; private set; }
    public float MouseXDirection { get; private set; }
    public float MouseYDirection { get; private set; }

    public Vector3 MousePosition { get; private set; }
    public bool IsInteract { get; private set; }

    private void Update()
    {
        HorizontalDirection = Input.GetAxis(Horizontal);
        VerticalDirection = Input.GetAxis(Vertical);

        MouseXDirection = Input.GetAxis(MouseX);
        MouseYDirection = Input.GetAxis(MouseY);

        IsInteract = Input.GetKeyDown(KeyCode.E);
        MousePosition = Input.mousePosition;
    }
}
