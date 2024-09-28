using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScriptManager : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void LoadSettings()
    {
        for(int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject currChild = parent.transform.GetChild(i).gameObject;
            if(currChild.activeSelf == true)
            {
                currChild.SetActive(false);
            }
            else if(currChild.activeSelf == false)
            {
                currChild.SetActive(true);
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
