using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
public class Loading : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] private Image loadingbar;
    [SerializeField] private Text txtPercent;
    [SerializeField] private Image Image;
    private bool ready = false;
    private float speed = .7f;//for the fill speed of the image
    [SerializeField]private Text confTxt;
    [SerializeField] public static int sceneToLoad = -1; //so static allows other scripts to access this variable and change it, god damnit
    private bool activated = false;
    [SerializeField] private bool waitForUserInput = true;//If true, the user has to press a key
    // Start is called before the first frame update
    void Start()
    {
        
        Image.fillAmount = 0;
        confTxt.GetComponent<Text>().enabled = false;
        Time.timeScale = 1;//Reset timescale
        Input.ResetInputAxes();//Reset the input (for 1 frame)
        System.GC.Collect();//Call the garbage collector
        Scene currentScene = SceneManager.GetActiveScene();//current scene 
        if (sceneToLoad == -1)
        {
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);//load next scene
        }
        else
        {
            async = SceneManager.LoadSceneAsync(sceneToLoad);
        }
        async.allowSceneActivation = false;//Dont go to next scene right away
        
    }
    
    public void Activate()
    {
        ready = true;
    }
   
    public IEnumerator BlinkyText(Text txt)
    {
        while (true)//infinite loop 
        {
            yield return new WaitForSeconds(.5f);
            txt.GetComponent<Text>().enabled = true;
            yield return new WaitForSeconds(.5f);
            txt.GetComponent<Text>().enabled = false;
           
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(async.progress>.89f && activated==false)
        {  
            StartCoroutine(BlinkyText(confTxt));
            activated = true;
        }
        if (waitForUserInput && Input.anyKey)
        {
            ready = true;
        }
        if (loadingbar)
        {
            loadingbar.fillAmount = async.progress + .1f;
        }
        if (txtPercent)
        {
            txtPercent.text = ((async.progress + .1f) * 100).ToString("f2") + " %";
        }
        if (Image)
        {
            Image.fillAmount = Mathf.Lerp(Image.fillAmount, 1+Image.fillAmount, Time.deltaTime * speed);
            if (Image.fillAmount>=1)
            {
                Image.fillAmount = 0;
            }
        }
       
        if (async.progress > 0.89f && SplashScreen.isFinished && ready)
        {
          
            async.allowSceneActivation = true;
           
        }
    }
}
