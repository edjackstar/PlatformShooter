using UnityEngine;

public class InputService
{
    private Vector3 _inputVector = Vector3.zero;

    public Vector3 MousePosition => Input.mousePosition;

    public Vector3 GetInputVector()
    {
        _inputVector.x = Input.GetAxis("Horizontal");
        _inputVector.z = Input.GetAxis("Vertical");

        return _inputVector;
    }

    // Input.GetAxis("Jump");

    public bool GetShootButton() 
        => Input.GetMouseButtonDown(0);
}