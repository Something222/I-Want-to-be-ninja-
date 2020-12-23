using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Selectable[] defaultButtons;
    // Start is called before the first frame update
    public void PanelToggle(int position)
    {
        Input.ResetInputAxes();//avoid double inputs
        for (int i=0;i<panels.Length;i++)
        {
            panels[i].SetActive(position == i);
            if(position==i)
            {
                defaultButtons[i].Select();
            }      
        }
    }
    
    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }
    void PanelToggle()
    { PanelToggle(0); }
    void Start()
    {
        Invoke("PanelToggle", 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
