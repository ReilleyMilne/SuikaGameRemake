using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolScript : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer effMixer;

    public void SetMusicVol(float sliderVal)
    {
        VolValues.musicVol = sliderVal;
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderVal) * 20);
    }

    public void SetEffVol(float sliderVal)
    {
        VolValues.effVol = sliderVal;
        effMixer.SetFloat("EffVol", Mathf.Log10(sliderVal) * 20);
    }
}
