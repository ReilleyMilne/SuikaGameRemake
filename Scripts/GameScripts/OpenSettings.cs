using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenSettings : MonoBehaviour
{
    public GameObject canvas;
    public GameObject scores;
    public GameObject background;
    public Slider musicVolSlider;
    public Slider effVolSlider;

    private bool onSettingsScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !onSettingsScreen)
        {
            musicVolSlider.value = VolValues.musicVol;
            effVolSlider.value = VolValues.effVol;
            canvas.GetComponent<Canvas>().enabled = true;
            background.SetActive(true);
            scores.SetActive(false);
            onSettingsScreen = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && onSettingsScreen)
        {
            canvas.GetComponent<Canvas>().enabled = false;
            background.SetActive(false);
            scores.SetActive(true);
            onSettingsScreen = false;
        }
    }

    public void settingsControl()
    {
        onSettingsScreen = false;
        canvas.GetComponent<Canvas>().enabled = false;
        background.SetActive(false);
        scores.SetActive(true);
    }
}
