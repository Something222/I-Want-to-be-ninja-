using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
public void NextLevel()
    {
        gameObject.GetComponent<LoadScene>().BtnLoadScene("LoadingScreen");
    }
    public void Mainmenu()
    {
        gameObject.GetComponent<LoadScene>().SetSceneToLoad(0);
        gameObject.GetComponent<LoadScene>().BtnLoadScene("LoadingScreen");
    }


}
