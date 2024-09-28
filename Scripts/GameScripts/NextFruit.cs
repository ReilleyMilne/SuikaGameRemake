using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFruit : MonoBehaviour
{
    public GameObject berryPrefab;
    public GameObject strawberryPrefab;
    public GameObject grapePrefab;
    public GameObject dekoponPrefab;
    public GameObject orangePrefab;

    private GameObject nextObj;
    private GameObject currPrefab;

    // Start is called before the first frame update
    void Start()
    {
        currPrefab = getRandomPrefab();
        nextObj = Instantiate(currPrefab, gameObject.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getNext()
    {
        GameObject returnedCurrPrefab = currPrefab;
        currPrefab = getRandomPrefab();
        StartCoroutine(waiter());

        return returnedCurrPrefab;
    }

    public GameObject getRandomPrefab()
    {
        float randVal = Random.Range(1, 6);
        switch (randVal)
        {
            case 1:
                return berryPrefab;
            case 2:
                return strawberryPrefab;
            case 3:
                return grapePrefab;
            case 4:
                return dekoponPrefab;
            case 5:
                return orangePrefab;
            default:
                return null;
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(1f);
        Destroy(nextObj);
        nextObj = Instantiate(currPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
