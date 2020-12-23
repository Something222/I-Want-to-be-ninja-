using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutscene4 : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;
    [SerializeField] private GameObject sensei;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject cam2;
    [SerializeField] private GameObject cam3;
    private bool cutsceneplayed = false;
    private bool pausesensei = false;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private AudioSource music;
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
    private void Camera2()
    {
        cam.SetActive(false);
        cam2.SetActive(true);
    }
    private void Camera3()
    {
        cam2.SetActive(false);
        cam3.SetActive(true);
    }
    private void EndCutScene()
    {
        cam3.SetActive(false);
        sensei.SetActive(false);
        player.SetActive(true);
        PlaySmokeSound();
        music.UnPause();
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
                StartCutscene();
                Invoke("Camera2", 35.4f);
                Invoke("Camera3", 42.4f);
                Invoke("PlaySmoke", 46f);
                Invoke("EndCutScene", 47f);
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
