using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grapple : MonoBehaviour
{
    //--------------STILL TO DO 
    //Gotta change the color of the cursor when you can hook shot
    [SerializeField] private GameObject blackDot;
    [SerializeField] private GameObject greenDot;
    private GameManager code;
    [SerializeField] private Toggle hookcdicon;
    [SerializeField] private float hookrange = 50f;
    [SerializeField] private GameObject player;
    private RaycastHit pull;
   [SerializeField] public bool hitconfirm = false;
    private bool onlyonce = false;
    private float timepassed = 0;
    [SerializeField] private float pullspeed = 2f;
    [SerializeField] private float cooldown = 5f;
    private bool hookOnCD = false;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;
    LineRenderer lineRenderer;
    [SerializeField] private float linewidth = .5f;
    public Vector3 momentum;
    public bool airmomentum = false;
    private Movement movement;
    private float x1;
    private float x2;
    private float y1;
    private float y2;
    private float z1;
    private float z2;
    private float velx;
    private float vely;
    private float velz;
    [SerializeField] private AudioSource[] sounds;
    private void ScanForHit()
    {
        int layermask = 1 << 9;

        RaycastHit icondetail;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out icondetail, hookrange, layermask))
        {
            blackDot.SetActive(false);
            greenDot.SetActive(true);
        }
        else
        {
            blackDot.SetActive(true);
            greenDot.SetActive(false);
        }
    }
    private void PlaySounds()
    {
        int sound = Random.Range(0, 6);
        sounds[sound].Play();
    }
    private RaycastHit ShootHookShot()
    {
        int layermask = 1 << 9;

        RaycastHit grapplehit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out grapplehit, hookrange, layermask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * grapplehit.distance, Color.yellow);
            hitconfirm = true;
            PlaySounds();
        }
        else
        {
            hitconfirm = false;
        }
        return grapplehit;
    }
    private void DelayAirMomentum()
    {
        airmomentum = true;
    }
    private void storemomentum()
    {
        x1 = transform.position.x;
        y1 = transform.position.y;
        z1= transform.position.z;
        Debug.Log("HELLO");
    }
    private void storemomentum2()
    {
        x2 = transform.position.x;
        y2 = transform.position.y;
        z2 = transform.position.z;
    }
    private void calcvelocities ()
    {
       //ok so the math started out clean but just works best with the times 5 at the end... alrighty
       //and the math for the damn y dont i wanna reset the velocity to 0 thats WHY!!!!
            velx = -((x1 - x2) / .9f)*5 ;
        // vely = ((y1 - y2) / .9f);
       // vely = ((y1 - y2) / .3f);
        vely = 19.62f;
        velz = -((z1 - z2) / .9f)*5 ;
            Debug.Log(velz);
           movement.velocity = new Vector3(velx, vely, velz);
        
    }
    private void CancelHitConfirm()
    {
        hitconfirm = false;
    }
    
    private void AddGrav()
    {
        vely += movement.defaultgrav;
        movement.velocity = new Vector3(velx, vely, velz);
    }
    private void resetvel()
    {
       
       movement.velocity = new Vector3(0, 0,0);
        Debug.Log("uhh oh Stinky");
    }
    private void HookCoolDown()
    {
        hookOnCD = false;
    }
   private void ReenableMove()
    {
        hitconfirm = false;

    }
    private void DelayMovementDisable()
    {
        movement.enabled = false;
    }

    // Start is called before the first frame update
    //Line renderer stuff is of the unity documentation
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth=linewidth;
        lineRenderer.endWidth = linewidth;
        code = GameManager.instance;
        movement = player.GetComponent<Movement>();
    }
    private void RenderLine(RaycastHit pull,GameObject player)
    {
        lineRenderer.positionCount=2;
       lineRenderer.SetPosition(0, new Vector3(player.transform.position.x,player.transform.position.y+.61f,player.transform.position.z));
       //lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, pull.point);
    }
   

    // Update is called once per frame
    void Update()
    {
        ScanForHit();
        if (Input.GetButtonDown("Fire1")&&!hookOnCD)
        {
           pull= ShootHookShot();
            //hookOnCD = true;
            //Invoke("HookCoolDown", cooldown);

        }
        if (hitconfirm==true)
        {
            hookOnCD = true;
            Invoke("HookCoolDown", cooldown);
            lineRenderer.enabled = true;
            //player.GetComponent<Movement>().enabled = false;
            Invoke("DelayMovementDisable", .03f);
            storemomentum();
            x2 = pull.point.x;
            y2 = pull.point.y;
            z2 = pull.point.z;
            player.GetComponent<Movement>().Isjumping = true;
           // Invoke("storemomentum2", .9f);

            if (!onlyonce)
            {
                timepassed = 0;
                //Invoke("calcvelocities",.9f);
                calcvelocities();
               
            }
            timepassed += Time.deltaTime;
            Invoke("CancelHitConfirm", .89f);
            RenderLine(pull, player);
            AddGrav();
             player.transform.position = Vector3.Lerp(player.transform.position, pull.point, timepassed / pullspeed);
            Invoke("ReenableMove", .9f);
            Invoke("DelayAirMomentum", .1f);
        }
        else
        {
            movement.enabled = true;
            lineRenderer.enabled = false;
           
        }
        if(hookOnCD)
        {
            hookcdicon.isOn = false;
        }
        else
        {
            hookcdicon.isOn = true;
        }
        //if(player.GetComponent<CharacterController>().isGrounded)
        //{
        //   player.GetComponent<Movement>().enabled = true;
        //    Debug.Log("ass");
        //}
        if ((movement.isGrounded|| movement.iswallrunning || movement.isWallClimbing) && airmomentum)
        {
            resetvel();
            //Invoke("resetvel", .9f);
            Debug.Log("ass");
            airmomentum = false;
            movement.Sprinttoggle = true;
        }
       
    }
}
