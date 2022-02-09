using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 10f;
    public float force = 200;

    public GameObject explosionEffect;
    
    float countdown;
    bool hasExploded = false;

    private void Start()
    {
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Expload();
            hasExploded = true;
        }

        void Expload()
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);

          Collider[] collidersToDestroy =  Physics.OverlapSphere(transform.position, radius);

          foreach (Collider nearbyObject in collidersToDestroy)
          {
              // Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
              //
              // if (rb != null)
              // {
              //     rb.AddExplosionForce(force, transform.position, radius);
              // }
        }
          
          Collider[] collidersToMove =  Physics.OverlapSphere(transform.position, radius);

          foreach (Collider nearbyObject in collidersToMove)
          {
              Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

              if (rb != null)
              {
                  rb.AddExplosionForce(force, transform.position, radius);
              }
          }
          
          Destroy(gameObject);
        }
    }
}
