using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Slider))]
public class SetVol_Sensitivity : MonoBehaviour,IPointerUpHandler
{
    private Slider slide; //reference to the slider
    [SerializeField] private AudioMixer audioM; //the audio mixer
    [SerializeField] private string nameParam; //parameter to change

    public void SetSensitivity(float val)
    {
        slide.value = val;
        PlayerPrefs.SetFloat("Sensitivity", val);
        CameraControls.mouseSensitivity = val;
    }
    public void SetVolume(float val)
    {
        slide.value = val;
        audioM.SetFloat(nameParam, val);
        PlayerPrefs.SetFloat(nameParam, val);
    }

    // Start is called before the first frame update
    
    void Start()
    {
        slide = GetComponent<Slider>();
        if (nameParam == "")
        {
            float v = PlayerPrefs.GetFloat("Sensitivity", 5);
            SetSensitivity(v);
        }
        else
        {
            float v = PlayerPrefs.GetFloat(nameParam, 0);
            SetVolume(v);
        }


    }
  
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        slide.GetComponent<AudioSource>().Play();
    }
}
