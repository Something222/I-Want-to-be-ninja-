using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // Start is called before the first frame update
    private Movement movement;
    [SerializeField] private GameObject player;
    
     private Animator animcontroller;

    void Start()
    {
        movement = player.GetComponent<Movement>();
        animcontroller=gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movement.isGrounded)
        {
            animcontroller.SetBool("Jumping", true);
           // gameObject.GetComponent<SphereCollider>().enabled = false;
        }
        if (movement.isGrounded)
        {
            animcontroller.SetBool("Jumping", false);
            //gameObject.GetComponent<SphereCollider>().enabled = true;
        }
        if (movement.z!=0&&!movement.Sprinttoggle&&movement.isGrounded)
        {
            animcontroller.SetBool("Walking", true);
            animcontroller.SetBool("Running", false);
            //animcontroller.SetBool("Jumping", false);
        }
       if(movement.Sprinttoggle&&movement.isGrounded)
        {
            animcontroller.SetBool("Running", true);
            animcontroller.SetBool("Walking", false);
            //animcontroller.SetBool("Jumping", false);
        }
       if (movement.z==0&&movement.isGrounded)
        {
            animcontroller.SetBool("Running", false);
            animcontroller.SetBool("Walking", false);
            //animcontroller.SetBool("Jumping", false);
        }
    
    }
}
