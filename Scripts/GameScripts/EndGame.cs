using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class EndGame : MonoBehaviour
{
    public GameObject blankBackground;
    public GameObject scoresCanvas;
    public GameObject endCanvas;
    public GameObject inGameObjs;
    public GameObject cloud;
    public TMP_Text score;
    public TMP_Text finalScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        StartCoroutine(waitToDestroy());
        StartCoroutine(wait());
    }

    public void freezeObjs(Rigidbody2D rigid2D)
    {
        rigid2D.freezeRotation = true;
        rigid2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    IEnumerator wait()
    {
        cloud.SetActive(false);
        yield return new WaitForSecondsRealtime(3.5f);
        blankBackground.SetActive(true);
        endCanvas.SetActive(true);
        finalScore.SetText(score.text);
        scoresCanvas.SetActive(false);
        Destroy(inGameObjs);
    }

    IEnumerator waitToDestroy()
    {
        Transform[] inGameObjsChildren = inGameObjs.GetComponentsInChildren<Transform>();
        int count = 0;
        foreach (Transform i in inGameObjsChildren)
        {
            if (count == 0)
            {
                count++;
            }
            else
            {
                Debug.Log(i);
                freezeObjs(i.GetComponent<Rigidbody2D>());
            }
        }
        yield return new WaitForSecondsRealtime(2f);
        int count2 = 0;
        foreach (Transform i in inGameObjsChildren)
        {
            if (count2 == 0)
            {
                count2++;
            }
            else
            {
                StartCoroutine(i.GetComponent<Combiner>().DestroyAndFade(gameObject));
            }
        }
    }
}
