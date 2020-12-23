using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }
        private void OnTriggerExit(Collider other)
        {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
        }
    
}
