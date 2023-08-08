using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float nextTimeToFire = 0;
    private int ammo;
    private GameController gameController;
    private GameObject player;

    private void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot(bulletPrefab.GetComponent<Bullet>().ammoType);
            player.GetComponent<Animator>().SetBool("Shoot", true);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            player.GetComponent<Animator>().SetBool("Shoot", false);
        }
    }

    private void Shoot(string ammoType)
    {
        ammo = gameController.UseAmmo(ammoType);

        if(ammo > 0 || bulletPrefab.GetComponent<Bullet>().ammoType == "Base")
        {
            nextTimeToFire = Time.time + 1f / bulletPrefab.GetComponent<Bullet>().fireRate;

            if (!bulletPrefab.gameObject.GetComponent<Bullet>().isChild)
            {
                //shooting logic
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
            else
            {
                //shooting logic
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, transform);
            }
        }
    }
}
