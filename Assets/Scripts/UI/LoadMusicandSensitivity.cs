using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadMusicandSensitivity : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM;
    // Start is called before the first frame update
    void Start()
    {
      
        float sens = PlayerPrefs.GetFloat("Sensitivity", 5);
        float volMusic = PlayerPrefs.GetFloat("VolMusic", 0);
        float volSFX = PlayerPrefs.GetFloat("VolSFX", 0);
        float volVoice = PlayerPrefs.GetFloat("VolVoice", 0);
        audioM.SetFloat("VolMusic", volMusic);
        audioM.SetFloat("VolVoice", volVoice);
        audioM.SetFloat("VolSfx", volSFX);
        CameraControls.mouseSensitivity = sens;
        
      
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
