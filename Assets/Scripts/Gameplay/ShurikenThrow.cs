using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenThrow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject projectile;
    [SerializeField] private float strength = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            Vector3 throwpos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
            GameObject  shuriken= Instantiate(projectile, throwpos, transform.rotation);
            shuriken.GetComponent<Rigidbody>().AddForce(transform.forward * strength, ForceMode.Impulse);
        }
    }
}
