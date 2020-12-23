using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject sensei;
    [SerializeField] private GameObject cutscenecam;
    private bool cutsceneplayed = false;
    private GameManager code;
    private bool pausesensei;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private AudioSource music;
    private void Start()
    {
        code = GameManager.instance;
    }
    private void EndCutScene()
    {
        player.SetActive(true);
        sensei.SetActive(false);
        cutscenecam.SetActive(false);
        PlaySmokeSound();
        music.UnPause();
    }
    private void StartCutScene()
    {
        player.SetActive(false);
        sensei.SetActive(true);
        cutscenecam.SetActive(true);
        music.Pause();
    }
    private void PlaySmokeSound()
    {
        smoke.GetComponent<AudioSource>().Play();
    }
    private void PlaySmoke()
    {
        smoke.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (cutsceneplayed == false)
            {
                cutsceneplayed = true;
                StartCutScene();
                Invoke("PlaySmoke", 15.3f);
                
                Invoke("EndCutScene", 16.3f);
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
