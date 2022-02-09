using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damge = 10f;
    public float fireRate = 25f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzelFlash;
    public GameObject impactEffect;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    private float nextTimeToFire = 0f;

    public Animator reloadAnim;
    
    
    void Start()
    {
        if (currentAmmo == -1)
            currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        reloadAnim.SetBool("reloading", false);
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        muzzelFlash.Play();

        currentAmmo--;
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(100f);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1.5f);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("A player is reloading");
        
        reloadAnim.SetBool("reloading", true);

        yield return new WaitForSeconds(reloadTime);
        
        reloadAnim.SetBool("reloading", false);
        
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
