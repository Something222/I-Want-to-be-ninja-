using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunSound : MonoBehaviour
{
    // Start is called before the first frame update
    private Movement move;
    [SerializeField]private AudioSource[] clip;
    private bool onlyonce = false;
    private bool pausecheck = false;
    private GameManager code;
    void Start()
    {
        move = gameObject.GetComponent<Movement>();
        code = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (move.iswallrunning)
        {
            if (!onlyonce)
            {
                clip[0].Play();
                
            }
            onlyonce = true;
            if(code.isPaused)
            {
                pausecheck = true;
                clip[0].Pause();
            }
            if (!code.isPaused &&pausecheck==true)
            {
                pausecheck = false;
                clip[0].UnPause();
            }
        }
        if (move.isWallClimbing)
        {
            if (!onlyonce)
            {
                clip[1].Play();
            }
            onlyonce = true;
            if (code.isPaused)
            {
                pausecheck = true;
                clip[1].Pause();
            }
            if (!code.isPaused && pausecheck == true)
            {
                pausecheck = false;
                clip[1].UnPause();
            }
        }
        if (!move.isWallClimbing&&!move.iswallrunning)
        {
            clip[1].Stop();
            clip[0].Stop();
            onlyonce = false;
        }
       
    }
}
