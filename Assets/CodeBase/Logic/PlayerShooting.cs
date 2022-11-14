using Assets.CodeBase.Infrastructure;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject _projectilePrefab;

    [SerializeField] private Transform _gunPoint;
    [SerializeField] private LayerMask _ignoredLayers;
    [SerializeField] private float _shootingColdown;
    [SerializeField] private float _spreadRange;

    private AllServices _services = AllServices.Instance;
    private float _coldown = 0f;

    private InputService _inputService;
    private RaycastHit _hit;

    private void Start()
    {
        _inputService = _services.GetService<InputService>();
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (_inputService.GetShootButton() == false)
            return;

        Vector3 mousePosition = _inputService.MousePosition;
        Vector3 spreadShift = generateSpreadShift();

        Ray cameraSpreadRay = Camera.main.ScreenPointToRay(mousePosition + spreadShift);

        if (HaveEndpoint(cameraSpreadRay) == true && _coldown + Time.deltaTime > _shootingColdown)
            TakeAShot(_hit);

        IncreaseColdown();
    }

    private void IncreaseColdown()
    {
        _coldown = (_coldown + Time.deltaTime) % _shootingColdown;
    }

    private void TakeAShot(RaycastHit hit)
    {
        Vector3 direction = (hit.point - _gunPoint.position).normalized;
        CreateBullet(hit.point, direction);
    }

    private void CreateBullet(Vector3 endPoint, Vector3 direction)
    {
        GameObject projectile = GameObject.Instantiate(_projectilePrefab, _gunPoint.position, Quaternion.identity);
        projectile.transform.LookAt(endPoint);
        projectile.GetComponent<Projectile>().Direction = direction;
    }

    private Vector3 generateSpreadShift() 
        => new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f) * _spreadRange;

    private bool HaveEndpoint(Ray cameraSpreadRay)
        => Physics.Raycast(cameraSpreadRay, out _hit, 1000f, ~_ignoredLayers);
}
