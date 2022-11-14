using Assets.CodeBase.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private float _mouseSpeed;
    [SerializeField] private LayerMask _ignoredLayers;

    public float RotationAngleX;
    public float RotationAngleY;
    public float MaxRotationAngleX;

    public float Distance;
    public float OffsetY;

    private InputService _inputService;

    private void Start()
    {
        _inputService = AllServices.Instance.GetService<InputService>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (Target == null)
            return;

        RotateAroundTarget();
        FollowTarget();
    }

    private void RotateAroundTarget()
    {
        Vector3 mouseMovement = _inputService.GetMouseMovement();
        RotationAngleX = Mathf.Clamp(RotationAngleX - mouseMovement.x * _mouseSpeed, -MaxRotationAngleX, MaxRotationAngleX);
        RotationAngleY += mouseMovement.y * _mouseSpeed;
    }

    private void FollowTarget()
    {
        Quaternion cameraRotation = Quaternion.Euler(RotationAngleX, RotationAngleY, 0f);
        Vector3 playerHeadPos = FollowingPointPosition();
        float calculatedDistance = CalculateDistance(cameraRotation, playerHeadPos);

        Vector3 position = cameraRotation * Vector3.back * calculatedDistance + playerHeadPos;

        transform.SetPositionAndRotation(position, cameraRotation);
    }

    private float CalculateDistance(Quaternion rotation, Vector3 playerPos)
    {
        Vector3 cameraDirection = CalculateCameraDirection(rotation);
        Ray cameraDistanceRay = new Ray(playerPos, cameraDirection - Target.position);

        Physics.Raycast(cameraDistanceRay, out RaycastHit _hit, Distance, ~_ignoredLayers);

        return Math.Clamp((playerPos - _hit.point).magnitude, 0f, Distance);
    }

    private Vector3 FollowingPointPosition()
    {
        Vector3 position = Target.position;
        position.y += OffsetY;

        return position;
    }

    private Vector3 CalculateCameraDirection(Quaternion rotation)
        => rotation * Vector3.back * Distance + Target.position;

    public void SetTarget(Transform cameraTarget)
         => Target = cameraTarget;
}
