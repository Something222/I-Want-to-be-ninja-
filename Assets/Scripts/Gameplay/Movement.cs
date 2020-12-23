using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    #region Shittodo
    #region shitdone
    //make a basic movement script (Done with help from brackeys) (directional controls also done)
    //make a crouch (shouldnt be too hard) done
    //make a sprint (shouldnt be too hard) done
    //make a slide (this may be a bit complex) if sprinting and hit crouch you will slide instead(Done)
    //make a wallrun (try using the raycast of earlier but use x directions maybe) (and stick to the wall) (good) (FINALLY)
    //screw with the camera like pull it back when you sprint and stuff to make it more exicting (also learn to spell)(done)
    //MENUS ARE IMPORTANT (STILL TO DO)
    //head raycast so you cant uncrounch into object(done)
    //lean the camera when wallrunning possibly lock rotation of character too. (done camera leans)
    //make a double jump (done)
    //make a hookshot (to do maybe last)
    //finish up tutorial level with cutscenes 
    //MENUS ARE IMPORTANT 
    //level select (cause you gonna wanna skip that tutorial)
    //add a timer for levels and track best time (so ill need player prefs for that) 
    //make sound footsteps jump and music  (footsteps done just need like a wallrun and climb effect)
    //make purty (animation) kinda done with the tutorial man (i may need a walk cycle to get the footsteps woking)
    //replace the place holder loading screen 
    //make levels (more) maybe 5 full levels would feel good.
    #endregion shitdone
 
    //THE WRITE UP
    #endregion shittodo
    [SerializeField] private AudioSource[] jumpsounds;
    [SerializeField] private AudioSource[] deathsounds;
    [SerializeField] private Toggle sprintVisual;
    private GameManager code;
    [SerializeField] private float walkspeed=6f;
    public GameObject player;
     [SerializeField]private  float gravity= -9.81f;
   [SerializeField] public bool isGrounded;
    [SerializeField]public Vector3 velocity;
    [SerializeField]private float jumpheight=1f;
    private bool dead;
    public float defaultgrav = -9.81f*2;
    [SerializeField] private GameObject hookercam;
    //private float forwardspeed = 6f;
    //private float backwardsspeed = 3f;
    //private float strafespeed = 4.5f;
    //private float desiredspeedx;
    //private float desiredspeedz;
    public CharacterController controller;
    [SerializeField]private bool secondjump = false;
    [SerializeField]private float sprintspeed=2f;
    [SerializeField]private float crouchspeed = .5f;
    private bool sprinttoggle;
    private bool shifttoggle;
    private bool crouched;
    private bool crouchtoggle;
    private bool sliding;
    private bool slidetoggle;
  [SerializeField]  private bool isjumping;
    private bool isWallJumping;
    private bool rightwallrun;
    private bool leftwallrun;
    private bool backClimb;
    private bool frontClimb;
    public bool isWallClimbing;
    private bool fallingjump;
    private float x = 0;
    public float z = 0;
    private float altmovementsprintspeed;
    private Vector3 movedirection = Vector3.zero;
    [SerializeField]private float initialslideslowdown = .1f;
    [SerializeField]private float slideslowdown = .01f;
   [SerializeField] public bool iswallrunning=false;
    private int wallid;
    [SerializeField]private float walljumpheight = 7f;
    [SerializeField]private float walljumpdistance = 10f;
    private bool zforwardwallrun;
    private bool zbackwardwallrun;
    private GameObject currentwall;
    private bool canuncrouch;
    public bool leftwalljumping;
    public bool rightwalljumping;
    private Grapple grapple;
    [SerializeField] private float airspeed=.09f;
    private float walljumptimer = .45f;
    //private bool isDoubleJumping;
    public bool Sprinttoggle { get => sprinttoggle; set => sprinttoggle = value; }
    public bool Rightwallrun { get => rightwallrun; set => rightwallrun = value; }
    public bool Leftwallrun { get => leftwallrun; set => leftwallrun = value; }
    public bool IsWallJumping { get => isWallJumping; set => isWallJumping = value; }
    public bool Isjumping { get => isjumping; set => isjumping = value; }

    //k so it aint that simple this only works if the wall is pperfectly flush
    //private void WalljumpX (float walljump)
    //{
    //    velocity.x = walljump;
    //    velocity.y = walljumpheight;
    //    Invoke("ResetXVelocity", .5f);
    //}
    //need to use uler angles to directly get the angles if not you get whatever the hell a quartiernian is
    public void NoLongerWallJumping()
    {
        IsWallJumping = false;
        leftwalljumping = false;
        rightwalljumping = false;
    }
   
    private void JumpSound()
    {
        int sound = Random.Range(1, 3);
        jumpsounds[sound].Play();
    }
    private void PlayDeath()
    {
        int sound = Random.Range(0, 2);
        deathsounds[sound].Play();
    }
    public void Delayedsprint()
    {
        sprinttoggle = true;
    }
    private void AngledWallJumpPlayer(float walljumptotal,bool left)
    {
        float vx;
        float vz;
        float initialangle=transform.eulerAngles.y;
        if (left)
        {
            leftwalljumping = true;
           initialangle -= 90;
        }
        else
        {
            rightwalljumping = true;
           initialangle +=  90;
        }
        initialangle += 180;
        Debug.Log(initialangle);
        initialangle = initialangle * (Mathf.PI / 180);
        //Last semester math comes in handy! HOLY SHIZA
        vz = walljumptotal * Mathf.Cos(initialangle);
        vx = walljumptotal * Mathf.Sin(initialangle);
        velocity.z = vz;
        velocity.x = vx;
        velocity.y = walljumpheight;
        IsWallJumping = true;
        Invoke("NoLongerWallJumping", walljumptimer);
        Invoke("ResetZVelocity", walljumptimer);
        Invoke("ResetXVelocity", walljumptimer);
        JumpSound();
    }
    private void WallClimbJump(float walljumptotal,bool front)
    {
        float vx;
        float vz;
        float initialangle = transform.eulerAngles.y;
        if (frontClimb)
        {
            initialangle += 180;
        }
        Debug.Log(initialangle);
        initialangle = initialangle * (Mathf.PI / 180);
        vz = walljumptotal * Mathf.Cos(initialangle);
        vx = walljumptotal * Mathf.Sin(initialangle);
        velocity.z = vz;
        velocity.x = vx;
        velocity.y = walljumpheight;
        IsWallJumping = true;
        Invoke("NoLongerWallJumping", .5f);
        Invoke("ResetZVelocity", .5f);
        Invoke("ResetXVelocity", .5f);
        JumpSound();
    }

    //so the using the wall as a reference offers more consistancy but if your on the wrong side of the wall youll just hop up it
    //i dont really know how i would fix that using the player as a reference still works nice for now so if theres time do this
    //private void AngledWallJumpWall(float walljumptotal, bool left)
    //{
    //    float vx;
    //    float vz;
    //    float initialangle = currentwall.transform.eulerAngles.y;


    //    Debug.Log(initialangle);
    //    initialangle = initialangle * (Mathf.PI / 180);
    //    //Last semester math comes in handy! HOLY SHIZA
    //    vz = walljumptotal * Mathf.Cos(initialangle);
    //    vx = walljumptotal * Mathf.Sin(initialangle);
    //    velocity.z = vz;
    //    velocity.x = vx;
    //    velocity.y = walljumpheight;
    //    Invoke("ResetZVelocity", .5f);
    //    Invoke("ResetXVelocity", .5f);
    //}
    //private void WalljumpZ(float walljump)
    //{
    //    velocity.z = walljump;
    //    velocity.y = walljumpheight;
    //    Invoke("ResetZVelocity", .5f);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Checkpoint")
        {
            code.respawnloc = other.transform.position;
            isGrounded = true;
        }
        if (other.tag=="DeathZone")
        {
            dead = true;
           
        }
        if(other.tag=="Trampoline")
        {
            TrampJump();
            isjumping = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Trampoline")
        {
            secondjump = false;
        }
    }

    void Start()
    {
        code = GameManager.instance;
        secondjump = false;
        grapple = hookercam.GetComponent<Grapple>();
     
    }
    #region groundchecknotes
    //so this is a ray cast that will shoot right under the foot of the capsule to see if its grounded.
    //physics.SphereCast a sphere based raycast
    //takes a position a radius direction, gives a hitinfo the distance, 
    //groundcheck() is largely taken from the unity standard asset rigidbodyfirstpersoncontroller script
    #endregion groundchecknotes
    #region Functions
    public void ResetXVelocity()
    {
        velocity.x = 0;
    }
  public void ResetZVelocity()
    {
        velocity.z = 0;
    }
    public void GroundCheckv2()
    {
        

        //well character controller already does this but hey its good to have as reference. ill keep it in cause for wallrun 
        //i may need something similar, and i did
        Debug.DrawRay(transform.position, Vector3.down, Color.black);
        if (Physics.Raycast(transform.position, Vector3.down, controller.height * .5f + .1f))
        {
            isGrounded = true;
            secondjump = false;
            Isjumping = false;
            wallid = 0;

            fallingjump = true;
            //issliidejumping
        }
        else
        {

            isjumping = true;
            isGrounded = false;
        }
    }
    public void GroundCheck()
    {
        RaycastHit hitInfo;

        //well character controller already does this but hey its good to have as reference. ill keep it in cause for wallrun 
        //i may need something similar, and i did
        Debug.DrawRay(transform.position, Vector3.down, Color.black);
        if (Physics.SphereCast(transform.position, controller.radius, Vector3.down, out hitInfo,
            ((controller.height / 2f) - controller.radius)+.1f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
            secondjump = false;
            Isjumping = false;
            wallid = 0;

            fallingjump = true;
            //issliidejumping
        }
        else
        {


            isGrounded = false;
        }
    }
    public void HeadCheck()
    {
        RaycastHit headinfo;
        Debug.DrawRay(transform.position, Vector3.up);
        if (Physics.SphereCast(transform.position, controller.radius, Vector3.up, out headinfo,
            ((controller.height / 2f) + controller.radius) + .5f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            canuncrouch = false;
        }
        else
        {
            canuncrouch = true;
        }
    }

    public void WallClimbCheck()
    {
        int layermask = 1 <<8;
        //layermask = ~layermask;
        RaycastHit wallclimbinfo;
        if (Physics.SphereCast(transform.position, controller.radius, transform.TransformDirection(Vector3.forward), out wallclimbinfo,
           controller.radius + .5f, layermask, QueryTriggerInteraction.Ignore) && Isjumping&&wallclimbinfo.collider.tag=="WallClimb")
        {
            isWallClimbing = true;
            frontClimb = true;
            backClimb = false;
            iswallrunning = false;
            if (wallid != wallclimbinfo.collider.GetInstanceID())
            {
                secondjump = true;
            }
            wallid = wallclimbinfo.collider.GetInstanceID();
        }
        else if( (Physics.SphereCast(transform.position, controller.radius, transform.TransformDirection(-Vector3.forward), out wallclimbinfo,
           controller.radius + .5f, layermask, QueryTriggerInteraction.Ignore) && Isjumping && wallclimbinfo.collider.tag == "WallClimb"))
        {
            isWallClimbing = true;
            backClimb = true;
            frontClimb = false;
            iswallrunning = false;

            if (wallid != wallclimbinfo.collider.GetInstanceID())
            {
                secondjump = true;
            }
            wallid = wallclimbinfo.collider.GetInstanceID();
        }
        else
        {
            frontClimb = false;
            backClimb = false;
            isWallClimbing = false;
        }
    }
    public void WallRunCheck()
    {
       // int layermask = 8;
        int layermask = 1 << 8;
        //layermask = ~layermask;
        RaycastHit wallruninfo;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right));
        Debug.DrawRay(transform.position,transform.TransformDirection(-Vector3.right));
        //right wall run
        if (Physics.SphereCast(transform.position, controller.radius, transform.TransformDirection(Vector3.right), out wallruninfo,
            controller.radius + .5f, layermask, QueryTriggerInteraction.Ignore)&&Isjumping&&!IsWallJumping/*&&wallruninfo.collider.tag!="WallClimb"*/)
            {
            iswallrunning = true;
         
            Rightwallrun = true;
           
            if (wallid!= wallruninfo.collider.GetInstanceID())
            {
                secondjump = true;
            }
            wallid = wallruninfo.collider.GetInstanceID();
            currentwall = wallruninfo.collider.gameObject;

        }
        else if ((Physics.SphereCast(transform.position, controller.radius, transform.TransformDirection(-Vector3.right)/*-Vector3.right*/, out wallruninfo,
            controller.radius + .5f, layermask, QueryTriggerInteraction.Ignore))&&Isjumping&&!IsWallJumping)
        {
            iswallrunning = true;
            Leftwallrun = true;
            if (wallid != wallruninfo.collider.GetInstanceID())
            {
                secondjump = true;
            }
            wallid = wallruninfo.collider.GetInstanceID();
            currentwall = wallruninfo.collider.gameObject;
        }
        else
        {
            iswallrunning = false;
            Leftwallrun = false;
            Rightwallrun = false;
        }
    }
    //brackeys provided this is the mathimatical formula for how to jump although looks fancier but does the same as just setting a jump height
    public void JumpFormula(bool candoublejump)
    {
        //damnit this one feels better though
        velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
       // velocity.y = jumpheight; //ya this is just less complicated
        secondjump = candoublejump;
        Isjumping = true;
        fallingjump = false;
        JumpSound();
    }
    private void invokeisjump()
    {
        isjumping = true;
    }
    private void invokenofalling()
    {
        fallingjump = false; ;
    }
    private void TrampJump()
    {
        //damnit this one feels better though
        velocity.y = Mathf.Sqrt(jumpheight * -1.75f*gravity);
        // velocity.y = jumpheight; //ya this is just less complicated
        
        Invoke("invokenofalling", .1f);
        Isjumping = true;
        Invoke("invokeisjump", .1f);
       
        fallingjump = false;
        JumpSound();
    }
    #endregion Functions
    // Update is called once per frame
    void Update()
    {
        WallRunCheck();
         GroundCheck();
        //GroundCheckv2();
        WallClimbCheck();
        if (isGrounded && velocity.y < 0)
        {
            //so when i crouch i shoot to the ground still
            velocity.y = -3f;
        }
        #region Arialdriftattempt
        //float x;
        //if (Input.GetAxisRaw("Horizontal")>0)
        //     {
        //     x += .05f;
        // }
        //else if (Input.GetAxisRaw("Horizontal") < 0)
        // {
        //     x -= .05f;
        // }
        //else if (Input.GetAxisRaw("Horizontal") == 0)
        // {
        //     x = 0;
        // }
        // if (Input.GetAxisRaw("Vertical") > 0)
        // {
        //     z += .05f;
        // }
        // else if (Input.GetAxisRaw("Vertical") < 0)
        // {
        //     z -= .05f;
        // }
        // else if (Input.GetAxisRaw("Vertical") == 0)
        // {
        //     z = 0;
        // }

        if (isGrounded)
        {
            x = Input.GetAxis("Horizontal");
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                x += airspeed;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                x -= airspeed;
            }

        }
        #endregion Arialdriftattempt
        //HOT DAMN finally got directional movement only took two extra lines
        if (!IsWallJumping)
        {
            x = Mathf.Clamp(x, -.75f, .75f);
        }
        else
        {
            x = Mathf.Clamp(x, 0, 0);
        }


        //float z;
        if (isGrounded)
        {
            z = Input.GetAxis("Vertical");
        }
        else
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                z += airspeed;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                z -= airspeed;
            }

        }
        if (isWallClimbing)
        {
            z = Mathf.Clamp(z, -.1f, .05f);
        }
        else
        {
            z = Mathf.Clamp(z, -.5f, 1f);
        }


        #region notes
        //this allows for different movement values based on direction but feels really choppy for some reason
        //i think what im doing here is moving a set number of units and thats why its not stopping very nicely
        //imma keep these here for fun
        #endregion notes
        #region tryingothermovement
        //if (z > 0)
        //{
        //    desiredspeedz = forwardspeed;
        //    if (sprinttoggle)
        //    {
        //        desiredspeedz = sprintspeed * forwardspeed;
        //    }
        //    if (crouched)
        //    {
        //        desiredspeedz = forwardspeed * crouchspeed;
        //    }
        //}
        //if (z == 0)
        //{

        //    desiredspeedz = 0;
        //    sprinttoggle = false;
        //}
        //if (z < 0)
        //{
        //    desiredspeedz = -backwardsspeed;
        //    if (crouched)
        //    {
        //        desiredspeedz = -backwardsspeed * crouchspeed;
        //    }
        //}
        //if (x > 0)
        //{
        //    desiredspeedx = strafespeed;
        //}
        //if (x == 0)
        //{
        //    desiredspeedx = 0;
        //}
        //if (x < 0)
        //{
        //    desiredspeedx = -strafespeed;
        //}
        // Vector3 alternatemovement = transform.right * desiredspeedx +transform.forward * desiredspeedz;
        // controller.Move(alternatemovement * Time.deltaTime);

        #endregion tryingothermovement 
        #region directionalcontrols_try2
        //this is what you did in class maybe instead of increasing the units moved like before try to influence the 
        //x and z get axis parts limit them to .5 (backwards) and .75 for strafing then increase the *speed by sprint speed if toggled
        //private Vector3 moveDirection = Vector3.zero;
        //moveDirection = Vector3.zero;
        //moveDirection.z = Input.GetAxis("Vertical");
        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotatespeed, 0);
        //moveDirection = transform.TransformDirection(moveDirection);
        //moveDirection *= speed;
        //characterController.Move(moveDirection * Time.deltaTime);



        #endregion directionalcontrols_try2
        float speed = walkspeed;

        #region toggles
        if (Input.GetButtonDown("Crouch") && !crouchtoggle && !crouched && !sliding && !Sprinttoggle)
        {
            crouched = true;
            crouchtoggle = true;
        }
        if (Input.GetButtonDown("Crouch") && !crouchtoggle && crouched && canuncrouch)
        {
            crouched = false;
        }
        if (Input.GetButton("Sprint") && !Sprinttoggle && !shifttoggle && z > 0 && !Isjumping)
        {
            Sprinttoggle = true;
            shifttoggle = true;
        }
        if (Input.GetButtonDown("Sprint") && Sprinttoggle && !shifttoggle && !Isjumping)
        {
            Sprinttoggle = false;
        }
        if (z <= 0.1f)
        {
            Sprinttoggle = false;
        }
        if (Sprinttoggle)
        {
            speed = sprintspeed * walkspeed;
            crouched = false;
            if (Input.GetButtonDown("Crouch"))
            {
                sliding = true;
            }
            sprintVisual.isOn = true;
        }
        else
        {
            speed = walkspeed;
            sprintVisual.isOn = false;
        }
        if (sliding)
        {
            player.transform.localScale = new Vector3(1, .7f, 1);
            HeadCheck();
            speed = sprintspeed * walkspeed;
            z -= initialslideslowdown;
            initialslideslowdown += slideslowdown;
            if (Input.GetButtonDown("Jump") && canuncrouch)
            {
                JumpFormula(true);
                sliding = false;
                //isslidejumping

            }
            if (z <= .3f && !canuncrouch)
            {
                z = .3f;
            }
            if (z <= .3f && canuncrouch)
            {
                sliding = false;
                //feels better coming out into sprint
                // sprinttoggle = false;
            }
        }
        if (crouched)
        {
            player.transform.localScale = new Vector3(1, .75f, 1);
            speed = crouchspeed * walkspeed;
            Sprinttoggle = false;
            HeadCheck();
        }
        if (!sliding && !crouched)
        {
            player.transform.localScale = new Vector3(1, 1, 1);
            initialslideslowdown = .1f;
        }
        #endregion toggles


        /////////////The Actual movement call//////////
        Vector3 movement = transform.right * x + transform.forward * z;
        if (!grapple.airmomentum)
        {
            controller.Move(movement * speed * Time.deltaTime);
        }
        else
        {
            controller.Move(.5f * movement * speed * Time.deltaTime);
        }
        if (Input.GetButtonDown("Jump") && secondjump && !iswallrunning && !isWallClimbing/* &&!isWallJumping*/)
        {

            JumpFormula(false);
            //ResetXVelocity();
            //ResetZVelocity();
            isjumping = true;
        }
        if (Input.GetButtonDown("Jump") && grapple.airmomentum)
        {
            JumpFormula(false);
            ResetZVelocity();
            ResetXVelocity();
            grapple.airmomentum = false;
            Invoke("Delayedsprint", .03f);

        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            JumpFormula(true);
            isjumping = true;
        }
        if (Input.GetButtonDown("Jump") && !isGrounded && fallingjump &&!iswallrunning && !isWallClimbing /*&& !isWallJumping*/)
        {
            JumpFormula(false);
        }
        if (iswallrunning)
        {
            gravity = defaultgrav * .60f;
            if (Input.GetButtonDown("Jump") && Leftwallrun)
            {
                // AngledWallJumpWall(walljumpdistance, true);
                AngledWallJumpPlayer(walljumpdistance, true);
            }
            if (Input.GetButtonDown("Jump") && Rightwallrun)
            {
                // AngledWallJumpWall(walljumpdistance, false);
                AngledWallJumpPlayer(walljumpdistance, false);
            }

        }
        if (isWallClimbing)
        {

            gravity = 0;
            velocity.y = 6;
            if (Input.GetButtonDown("Jump"))
            {
                WallClimbJump(walljumpdistance, true);
            }


        }
        if (!iswallrunning && !isWallClimbing)
        {
            gravity = defaultgrav;
        }
        velocity.y += gravity * Time.deltaTime;
        if (Input.GetButtonDown("Respawn"))
        {
            
            dead = true;
        }

        controller.Move(velocity * Time.deltaTime);
        shifttoggle = false;
        crouchtoggle = false;
        if (dead)
        {
            PlayDeath();
            transform.position = code.respawnloc;
            dead = false;
        }
        //gets rid of a weird clipping issue
        if (Isjumping)
        {
            controller.stepOffset = .0f;
            controller.slopeLimit = 90;
        }
        else
        {
            controller.stepOffset = 0.1f;
            controller.slopeLimit = 45;
        }
    }
    }

