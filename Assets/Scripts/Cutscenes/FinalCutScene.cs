using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutScene : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject sensei;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject cam2;
    [SerializeField] private GameObject cam3;
    [SerializeField] private GameObject cam4;
    [SerializeField] private AudioSource music;
    private bool cutsceneplayed = false;
    private GameManager code;
    private bool pausesensei;
    [SerializeField] private ParticleSystem[] smoke;
    // Start is called before the first frame update
    private void PlaySmoke1()
    {
        smoke[0].Play();
        Invoke("PlaySmokeSound", 1f);
    }
    private void PlaySmoke2()
    {
        smoke[1].Play();
        Invoke("PlaySmokeSound", 1f);
    }
    private void PlaySmoke3()
    {
        smoke[2].Play();
        Invoke("PlaySmokeSound", 1f);
    }
    private void PlaySmoke4()
    {
        smoke[3].Play();
        Invoke("PlaySmokeSound", 1f);
    }
    private void PlaySmokeSound()
    {
        smoke[0].GetComponent<AudioSource>().Play();
    }
    private void StartCutScene()
    {
        player.SetActive(false);
        cam.SetActive(true);
        Invoke("SetSenseiActive", 1.4f);
        PlaySmoke1();
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
    private void StartAudio()
    {
        sensei.GetComponent<AudioSource>().Play();
    }
    private void ActivateCam4()
    {
        cam4.SetActive(true);
        cam3.SetActive(false);
        Invoke("StartAudio", .5f);
    }
    private void EndCutScene()
    {
        player.SetActive(true);
        sensei.SetActive(false);
        cam.SetActive(false);
        cam2.SetActive(false);
        cam3.SetActive(false);
        cam4.SetActive(false);
        music.UnPause();
    }
    private void StartAnim()
    {
        sensei.GetComponent<Animator>().SetBool("Start", true);
    }
    void Start()
    {
        code = GameManager.instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (cutsceneplayed == false)
        {
            if (other.tag == "Player")
            {
                cutsceneplayed = true;
                StartCutScene();
                Invoke("PlaySmoke1", 2f);
                Invoke("ActivateCam2", 3f);
                Invoke("PlaySmoke2", 3f);
                Invoke("ActivateCam3", 4f);
                Invoke("PlaySmoke3", 4f);
                Invoke("ActivateCam4", 5f);
                Invoke("StartAnim", 7.4f);
                Invoke("PlaySmoke4", 36f);
                Invoke("EndCutScene", 37f);
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
        if (!code.isPaused && pausesensei)
        {
            sensei.GetComponent<AudioSource>().UnPause();
            pausesensei = false;
        }
    }
}
