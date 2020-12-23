using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene3 : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;
    [SerializeField] private GameObject sensei;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject cam2;
    private bool cutsceneplayed = false;
    private bool pausesensei = false;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private AudioSource music;
    void Start()
    {
        cutsceneplayed = false;
        code = GameManager.instance;
    }
    private void PlaySmoke()
    {
        smoke.Play();
    }
    private void PlaySmokeSound()
    {
        smoke.GetComponent<AudioSource>().Play();
    }
    private void SetSensei()
    {
        sensei.SetActive(true);
    }
    private void StartCutscene()
    {
        player.SetActive(false);
        Invoke("SetSensei", 1.4f);
        cam.SetActive(true);
        smoke.Play();
        Invoke("PlaySmokeSound", 1f);
        music.Pause();
    }
    private void StartCam2()
    {
        cam2.SetActive(true);
        cam.SetActive(false);
    }
    private void EndCutscene()
    {
        cam2.SetActive(false);
        sensei.SetActive(false);
        player.SetActive(true);
        PlaySmokeSound();
        music.UnPause();
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (cutsceneplayed == false)
        {
            if (other.tag == "Player")
            {
                cutsceneplayed = true;
                StartCutscene();
                Invoke("StartCam2", 10.4f);
                Invoke("PlaySmoke", 27.4f);
                Invoke("EndCutscene", 28.4f);
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
