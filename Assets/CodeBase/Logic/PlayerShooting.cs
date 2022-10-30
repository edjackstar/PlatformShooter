using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _projectilePrefab;

    private InputService _inputService;


    private void Start()
    {
        _inputService = new InputService();
    }

    private void Update()
    {
        if (_inputService.IsShootButton())
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(_inputService.MousePosition);

            if (Physics.Raycast(cameraRay, out RaycastHit hit, 1000f))
            {
                Vector3 direction = (hit.point - _gunPoint.position).normalized;

                GameObject projectile = GameObject.Instantiate(_projectilePrefab, _gunPoint.position, Quaternion.identity);
                
                projectile.transform.LookAt(hit.point);
                projectile.GetComponent<Projectile>().Direction = direction;
            }
        }
    }
}
