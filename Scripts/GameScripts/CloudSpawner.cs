using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject SpawnerLocation;
    public GameObject DropperLine;
    public GameObject InGameObjs;

    private GameObject currObj;

    public AudioSource spawnSoundEffect;

    private bool checkTime;

    private NextFruit nextFruit;
    // Start is called before the first frame update
    void Start()
    {
        GameObject nextFruitSpawner = GameObject.Find("NextObjSpawner");
        nextFruit = nextFruitSpawner.GetComponent<NextFruit>();
        currObj = Instantiate(nextFruit.getRandomPrefab(), SpawnerLocation.transform.position, Quaternion.identity);
        currObj.transform.parent = SpawnerLocation.transform;
        checkTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") && checkTime)
        {
            spawnSoundEffect.Play();
            DropperLine.GetComponent<SpriteRenderer>().enabled = false;
            currObj.GetComponent<CircleCollider2D>().enabled = true;
            currObj.transform.parent = InGameObjs.transform;
            currObj.GetComponent<Rigidbody2D>().gravityScale = 1;
            checkTime = false;
            StartCoroutine(counter());
        }
    }

    IEnumerator counter()
    {
        currObj = null;
        //Wait for half a second
        currObj = Instantiate(nextFruit.getNext(), SpawnerLocation.transform.position, Quaternion.identity);
        currObj.transform.parent = SpawnerLocation.transform;
        //Disable obj.
        currObj.GetComponent<SpriteRenderer>().enabled = false;
        currObj.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSecondsRealtime(1f);
        currObj.transform.position = SpawnerLocation.transform.position;
        //currObj = Instantiate(prefab, this.transform.Find("SpawnerLocation").gameObject.transform.position, Quaternion.identity);
        //Enable obj.
        DropperLine.GetComponent<SpriteRenderer>().enabled = true;
        currObj.GetComponent<SpriteRenderer>().enabled = true;
        checkTime = true;
    }

    public GameObject getCurrObj()
    {
        return currObj;
    }
}
