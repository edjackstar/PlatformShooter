using Assets.CodeBase.Services;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController CharacterController;

    private IInputService _inputService;
    private Vector3 _gravitySpeed = Vector3.zero;
    
    [SerializeField]
    private float _speed;

    private const float Gravity = -9.81f;

    // Will change later this dependency with DI (Dependency Injection)
    private void Awake() => _inputService = new StandaloneInput();

    private void Update()
    {
        Vector3 movementVector = _speed * _inputService.GetMovementDirection();

        MovePlayer(movementVector);
        MovePlayer(ComputeGravitySpeed());
    }

    private Vector3 ComputeGravitySpeed()
    {
        if (CharacterController.isGrounded == true)
            _gravitySpeed.y = 0f;

        _gravitySpeed.y += Gravity * Time.deltaTime;
        
        return _gravitySpeed;
    }

    private void MovePlayer(Vector3 movementVector) 
        => CharacterController.Move(movementVector * Time.deltaTime);
}
