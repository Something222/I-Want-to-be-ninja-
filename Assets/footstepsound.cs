using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepsound : MonoBehaviour
{
    // Start is called before the first frame update
    private Movement movement;
    [SerializeField]private GameObject player;
    void Start()
    {
        movement = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movement.isGrounded)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
        if(!movement.isGrounded)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip); ;
    }
}
