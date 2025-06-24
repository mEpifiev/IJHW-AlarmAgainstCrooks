using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(InputReader))]
public class PlayerMovement : MonoBehaviour
{
    private const float MaxLookAngle = 90f;
    private const float MinLookAngle = -90f;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private bool _isCursorHidden;

    private Rigidbody _rigidbody;
    private InputReader _inputReader;

    private float _xRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputReader = GetComponent<InputReader>();

        if (_isCursorHidden)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        RotateCamera();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = _inputReader.VerticalDirection * transform.forward + _inputReader.HorizontalDirection * transform.right;

        _rigidbody.velocity = direction * _moveSpeed;
    }

    private void RotateCamera()
    {
        float mouseX = _inputReader.MouseXDirection * _mouseSensitivity;
        float mouseY = _inputReader.MouseYDirection * _mouseSensitivity;

        transform.Rotate(mouseX * Vector3.up);

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, MinLookAngle, MaxLookAngle);

        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}
