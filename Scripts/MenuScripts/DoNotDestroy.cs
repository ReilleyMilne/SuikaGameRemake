using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("GameMusic");
        if(list.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
