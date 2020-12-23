using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int currentlevel;
    public Vector3 respawnloc;
    public bool levelclear = false;
    [SerializeField]private GameObject levelclearpanel;
    public bool isPaused;
    [SerializeField] private GameObject pausePanel;
    private bool onlyonce=false;
    [SerializeField] private Text timer;
    private float timepassed;
    [SerializeField] private Text bestTime;
    private bool sethigh = false;
  //  public bool dead;
    //public void respawn()
    //{
    //    transform.position = respawnloc;
    //    dead = false;
    //}
   
    // Start is called before the first frame update
    public void UnPause()
    {
        isPaused = false;
    }
    void Start()
    {
        currentlevel = SceneManager.GetActiveScene().buildIndex;
    }
   
    private void Awake()
    {
       
        //only one instance exists
        if (instance == null)//if there is no instance this becomes instance
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


    }
    // Update is called once per frame
    //some references made here to a tutorial on timers https://www.youtube.com/watch?v=x-C95TuQtf0
    void Update()
    {
        timepassed += Time.deltaTime;
        int minutes = (int)timepassed / 60;
        int seconds = ((int)timepassed % 60);
        timer.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        if (levelclear==true)
        {
            Time.timeScale = 0;
            float previousbest= PlayerPrefs.GetFloat("LevelSpeed" + currentlevel, 999999999);
            timer.enabled = false;
            if (timepassed<previousbest&&sethigh==false)
            {
                PlayerPrefs.SetFloat("LevelSpeed" + currentlevel, timepassed);
                sethigh = true;
                bestTime.text= "New Best Time:" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
            }
            else if (timepassed > previousbest && sethigh == false)
            {
                minutes = (int)previousbest / 60;
                sethigh = true;
                seconds= ((int)previousbest % 60);
                bestTime.text = "Best Time:"+minutes.ToString("D2") + ":" + seconds.ToString("D2");
            }
           
            
            levelclearpanel.SetActive(true);
        }
        if (!levelclear &&Input.GetButtonDown("Cancel")&&!isPaused)
        {
            isPaused = true;
            onlyonce = false;
        }
        else if (!levelclear && Input.GetButtonDown("Cancel")&&pausePanel.activeInHierarchy==true)
        {
            isPaused = false;
        }
        if(isPaused==true)
        {
            Time.timeScale = 0;
            if (!onlyonce)
            {
                onlyonce = true;
                pausePanel.SetActive(true);
            }
        }
        if (!isPaused&& !levelclear)
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            onlyonce = false;
            timer.enabled = true;
        }
        //if (!isPaused &&!levelclear)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}
    }
}
