using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject sensei;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cam;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private GameObject cam2;
    [SerializeField] private GameObject cam3;
    [SerializeField] private GameObject cam4;
    private bool pausesensei = false;
    private GameManager code;
    private bool cutsceneplayed = false;
    [SerializeField] private AudioSource music;
    private void Start()
    {
        code = GameManager.instance;
    }
    private void EndCutScene()
    {
        player.SetActive(true);
        sensei.SetActive(false);
        cam.SetActive(false);
        cam2.SetActive(false);
        cam3.SetActive(false);
        cam4.SetActive(false);
        PlaySmokeSound();
        music.UnPause();
    }
  
   
    private void StartCutScene()
    {
        player.SetActive(false);
        cam.SetActive(true);
        Invoke("SetSenseiActive", 1.4f);
        smoke.Play();
        Invoke("PlaySmokeSound", 1f);
        music.Pause();
        
    }
    private void ActivateCam2()
    {
        cam.SetActive(false);
        cam2.SetActive(true);
    }
    private void ActivateCam3()
    {
        cam3.SetActive(true);
        cam2.SetActive(false);
    }
    private void SetSenseiActive()
    {
        sensei.SetActive(true);
    }
    private void ActivateCam4()
    {
        cam4.SetActive(true);
        cam3.SetActive(false);
    }
    private void PlaySmoke()
    {
        smoke.Play();
    }
    private void PlaySmokeSound()
    {
        smoke.GetComponent<AudioSource>().Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (cutsceneplayed == false)
        {
            if (other.tag == "Player")
            {
                cutsceneplayed = true;
                StartCutScene();
                Invoke("ActivateCam2", 13f);
                Invoke("ActivateCam3", 28f);
                Invoke("ActivateCam4", 35f);
                Invoke("PlaySmoke", 42f);
                Invoke("EndCutScene", 43f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (code.isPaused)
        {
            sensei.GetComponent<AudioSource>().Pause();
            pausesensei = true;
        }
        if(!code.isPaused&&pausesensei)
        {
            sensei.GetComponent<AudioSource>().UnPause();
            pausesensei = false;
        }

    }
}
