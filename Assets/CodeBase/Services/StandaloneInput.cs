using UnityEngine;

namespace Assets.CodeBase.Services
{
    // Will move to another file later. IInputService -> Standalone \ Mobile
    public interface IInputService
    {
        Vector3 GetMovementDirection();
    }

    public class StandaloneInput : IInputService
    {
        private Vector3 _movementDirection = Vector3.zero;

        public Vector3 GetMovementDirection()
        {
            _movementDirection.x = Input.GetAxis("Horizontal");
            _movementDirection.z = Input.GetAxis("Vertical");
            
            _movementDirection.Normalize();

            return _movementDirection;
        }
    }

    public class MobileInput : IInputService
    {
        public Vector3 GetMovementDirection()
        {
            throw new System.NotImplementedException();
        }
    }
}