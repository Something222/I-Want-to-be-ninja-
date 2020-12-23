using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    //Brackeys script remember to reference this part only the base of it 
    // Start is called before the first frame update
    public static float mouseSensitivity = 100f;
    [SerializeField] private GameObject player;
    public Transform body;
    private Movement move;
    float xRotation = 0f;
    private bool onlyonce;
    private bool onlyoncewalljump;
    float timepassed;
    [SerializeField] private float headtiltspeed = 3f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        move = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //if(move.Sprinttoggle)
        //{
        //    transform.localPosition = new Vector3(0, 0.63f, -.2f);
        //}
        //else
        //{
        //    transform.localPosition = new Vector3(0, 0.63f, 0f);
        //}
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //so mathf.clamp is what you did in the brick breaker to keep something within ranges just much simpler
        if (move.Rightwallrun&&!move.isWallClimbing)
        {
            if (!onlyonce)
            {
                timepassed = 0;
                onlyonce = true;
            }
            timepassed+=Time.deltaTime;
            transform.localRotation = Quaternion.Euler(xRotation, 0f, Mathf.LerpAngle(0,6,timepassed/headtiltspeed));
        }
        if (move.Leftwallrun&&!move.isWallClimbing)
        {
            if (!onlyonce)
            {
                timepassed = 0;
                onlyonce = true;
            }
            timepassed += Time.deltaTime;
            transform.localRotation = Quaternion.Euler(xRotation, 0f, Mathf.LerpAngle(0, -6, timepassed / headtiltspeed));

        }
        if (!move.iswallrunning)
        {
            onlyonce = false;
            if (transform.localRotation.z>0)
            {
                if (!onlyoncewalljump)
                       {
                           timepassed = 0;
                           onlyoncewalljump = true;
                        }
                    timepassed += Time.deltaTime;
                transform.localRotation = Quaternion.Euler(xRotation, 0f, Mathf.LerpAngle(6, 0, timepassed / headtiltspeed));
            }
            else if ( transform.localRotation.z < 0)
            {
                
                if (!onlyoncewalljump)
                {
                    timepassed = 0;
                    onlyoncewalljump = true;
                }
                timepassed += Time.deltaTime;
                transform.localRotation = Quaternion.Euler(xRotation, 0f, Mathf.LerpAngle(-6, 0, timepassed / headtiltspeed));
              
            }
            else if ( transform.localRotation.z == 0)
            {
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                onlyoncewalljump = false;
            }
            #region walljumprotationtry1
            //if (move.rightwalljumping)
            //{
            //    if (!onlyoncewalljump)
            //    {
            //        timepassed = 0;
            //        onlyoncewalljump = true;
            //    }
            //    timepassed += Time.deltaTime;
            //    transform.localRotation = Quaternion.Euler(xRotation, 0f, Mathf.LerpAngle(6, 0, timepassed / headtiltspeed));
            //}
            //else if (move.leftwalljumping)
            //{
            //    if (!onlyoncewalljump)
            //    {
            //        timepassed = 0;
            //        onlyoncewalljump = true;
            //    }
            //    timepassed += Time.deltaTime;
            //    transform.localRotation = Quaternion.Euler(xRotation, 0f, Mathf.LerpAngle(-6, 0, timepassed / headtiltspeed));
            //}
            //else if (!move.rightwalljumping&&!move.leftwalljumping)
            //{
            //    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //    onlyoncewalljump = false;
            //}
            #endregion walljumprotationtry1
        }
        body.Rotate(Vector3.up * mouseX);
        

    }
}
