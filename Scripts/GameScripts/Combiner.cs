using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Combiner : MonoBehaviour
{
    public GameObject nextPrefab;
    public GameObject whitePrefab;

    private ScoreTracker scoreTracker;

    public float fadeDuration;

    private bool beingPopped = false;
    private bool touchedGround = false;

    private float touchedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreText = GameObject.Find("Scores");
        scoreTracker = scoreText.GetComponent<ScoreTracker>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // ends the game once fruit collides with death wall
        if (collision.gameObject.tag == "DeathWall" && touchedGround)
        {
            touchedTime += Time.deltaTime;
            Debug.Log(touchedTime);
            if (touchedTime >= 1f)
            {
                endGameCheck(collision);
            }
        }
    }

  
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Wall")
        {
            touchedGround = true;
        }
        // called upon when two identical objects merge and are not already being merged 
        if (collision.gameObject.tag == this.gameObject.tag && !beingPopped && !collision.gameObject.GetComponent<Combiner>().isBeingPopped())
        {
            updateScore(collision.gameObject.tag);
            beingPopped = true;
            StartCoroutine(DestroyAndFade(collision.gameObject));
            Destroy(collision.gameObject);
            float xPosition = (gameObject.transform.position.x + collision.transform.position.x) / 2;
            float yPosition = (gameObject.transform.position.y + collision.transform.position.y) / 2;
            Vector2 midpointPosition = new Vector2(xPosition, yPosition);
            GameObject createdObj = Instantiate(nextPrefab, midpointPosition, Quaternion.identity);
            createdObj.transform.parent = GameObject.Find("InGameObjs").transform;
            createdObj.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void endGameCheck(Collider2D collision)
    {
        Debug.Log(gameObject.transform.position);
        collision.GetComponent<BoxCollider2D>().enabled = false;
        scoreTracker.saveScoresToCloud();
        GameObject deathWall = GameObject.Find("DeathWall");
        EndGame endGameScript = deathWall.GetComponent<EndGame>();
        endGameScript.GameOver();
    }

    private void updateScore(string tag)
    {
        switch (tag)
        {
            case "1Cherry":
                scoreTracker.updateScore(1);
                break;
            case "2Strawberry":
                scoreTracker.updateScore(3);
                break;
            case "3Grape":
                scoreTracker.updateScore(6);
                break;
            case "4Dekopon":
                scoreTracker.updateScore(10);
                break;
            case "5Orange":
                scoreTracker.updateScore(15);
                break;
            case "6Apple":
                scoreTracker.updateScore(21);
                break;
            case "7Pear":
                scoreTracker.updateScore(28);
                break;
            case "8Peach":
                scoreTracker.updateScore(36);
                break;
            case "9Pineapple":
                scoreTracker.updateScore(45);
                break;
            case "10Melon":
                scoreTracker.updateScore(55);
                break;
            case "11Watermelon":
                scoreTracker.updateScore(66);
                break;
        }
    }

    bool isBeingPopped()
    {
        return beingPopped;
    }

    public IEnumerator DestroyAndFade(GameObject collidedObj)
    {
        // Instantiate the white version
        GameObject whiteObject = Instantiate(whitePrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        // Set its initial scale or alpha to zero
        whiteObject.transform.localScale = Vector3.zero;

        // Gradually increase the scale or alpha over time
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            whiteObject.transform.localScale = Vector3.Lerp(Vector3.zero, transform.localScale, t);
            // Alternatively, if using materials with alpha, you can lerp the alpha value.
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the white object has the final scale
        whiteObject.transform.localScale = transform.localScale;

        // Destroy the original object and the white version
        Destroy(whiteObject);
        Destroy(gameObject);
    }
}