using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClear : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;
    [SerializeField] private int leveltoload;
    void Start()
    {
        code = GameManager.instance;
    }
    private void LoadNextLevel()
    {
        gameObject.GetComponent<LoadScene>().SetSceneToLoad(leveltoload);
        //gameObject.GetComponent<LoadScene>().BtnLoadScene("LoadingScreen");
        code.levelclear = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            LoadNextLevel();
            
            //Invoke("LoadNextLevel", .5f);
           
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
