using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject _projectilePrefab;

    [SerializeField] private Transform _gunPoint;
    [SerializeField] private LayerMask _ignoredLayers;

    private InputService _inputService;

    private void Start()
    {
        _inputService = new InputService();
    }

    private void Update()
    {
        if (_inputService.GetShootButton())
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(_inputService.MousePosition);

            if (Physics.Raycast(cameraRay, out RaycastHit hit, 1000f, ~_ignoredLayers) == true)
            {
                Vector3 direction = (hit.point - _gunPoint.position).normalized;

                GameObject projectile = GameObject.Instantiate(_projectilePrefab, _gunPoint.position, Quaternion.identity);
                projectile.transform.LookAt(hit.point);
                projectile.GetComponent<Projectile>().Direction = direction;
            }
        }
    }
}
