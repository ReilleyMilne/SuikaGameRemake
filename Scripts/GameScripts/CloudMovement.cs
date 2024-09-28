using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{

    public float xSpeed;

    private GameObject currObj;
    
    private double leftLimit = -2.87;
    private double rightLimit = 2.69;
    // Start is called before the first frame update
    void Start()
    {
        currObj = GetComponent<CloudSpawner>().getCurrObj();
    }

    // Update is called once per frame
    void Update()
    {
        currObj = GetComponent<CloudSpawner>().getCurrObj();
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && currObj.transform.position.x < rightLimit)
        {
            transform.Translate(Vector2.right * Time.deltaTime * xSpeed);
        }
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && currObj.transform.position.x > leftLimit)
        {
            transform.Translate(Vector2.left * Time.deltaTime * xSpeed);
        }
    }

}
