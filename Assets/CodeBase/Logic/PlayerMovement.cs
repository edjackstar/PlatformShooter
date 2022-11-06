using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController CharacterController;
    [SerializeField] private float _speed;

    private InputService _inputService;
    private Vector3 _verticalVelocity;

    private const float Gravity = -9.81f;

    private void Start()
    {
        _inputService = new InputService();
        _verticalVelocity = Vector3.up * Gravity;
    }

    private void Update()
    {
        Vector3 inputVector = GetInputVector();
        MovePlayer(TranslateCameraDirection(inputVector) * _speed);

        ApplyGravity();
        MovePlayer(_verticalVelocity);
    }
    private Vector3 TranslateCameraDirection(Vector3 inputDirection)
    {
        Vector3 movementVector = Vector3.zero;

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            //Трансформируем экранныые координаты вектора в мировые
            movementVector = Camera.main.transform.TransformDirection(inputDirection);
            movementVector.y = 0;
        }

        return movementVector;
    }
    private void ApplyGravity()
    {
        if (CharacterController.isGrounded == true)
            _verticalVelocity.y = 0f;
        else
            _verticalVelocity.y += Gravity * Time.deltaTime;
    }

    private void MovePlayer(Vector3 movement) 
        => CharacterController.Move(movement * Time.deltaTime);

    private Vector3 GetInputVector()
        => _inputService.GetInputVector();
}
