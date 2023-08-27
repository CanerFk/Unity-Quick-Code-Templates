using UnityEditor;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Animator handAnimator;
    [SerializeField] Transform firePoint;
    [SerializeField] Camera playerCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject wallHitDecalPrefab;
    [SerializeField] GameObject projectilePrefab;

    public Gun equippedWeapon;
    private PlayerController playerController;

    private float lastTimeShot = 0f;
    private int currentRecoilIndex = 0;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        equippedWeapon = new Phantom();
    }
    private void Update()
    {
        bool isTryingShoot = Input.GetKey(KeyCode.Mouse0);
        if(isTryingShoot)
        {
            HandleShooting();
        }
    }
    void HandleShooting()
    {
        if(Time.time - lastTimeShot >= 1/equippedWeapon.fireRatePerSecond)
        {
            RaycastHit hit;
            if(Physics.Raycast(
                firePoint.transform.position,
                firePoint.transform.TransformDirection(transform.forward),
                out hit,
                equippedWeapon.fallOfDistance)){
                Instantiate(wallHitDecalPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            }
            lastTimeShot = Time.time;
        }
    }
}
