using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    [SerializeField]
    private float fireRate = 0.005f;
    private float nextFire = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot() {
        // Shooting object
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
