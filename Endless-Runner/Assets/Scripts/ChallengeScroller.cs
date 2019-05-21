using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeScroller : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public GameObject[] obstacles;
    GameObject Player;
    public float obstacleFrequency = 0.5f;
    public Transform obstacleSpawnPoint;
    float counter = 0.0f;
    float destroyLocationOffset = -15.0f;

    bool isGameOver = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isGameOver)
        {
            return;
        }
        //Generate new obstacles
        if (counter <= 0.0f)
        {
            GenerateRandomObstacle();
        }
        else
        {
            counter -= Time.deltaTime * obstacleFrequency;
        }

        //Scrolling
        GameObject currentChild;

        for(int i =0; i < transform.childCount; i++)
        {
            currentChild = transform.GetChild(i).gameObject;
            scrollObstacles(currentChild);
            ////Score increase after cleared obstatcle.
            //if (currentChild.transform.position.x < Player.transform.position.x)
            //{
            //    transform.GetComponent<GameController>().IncreaseScore();
            //}
            //Destroys object if offscreen.
            if (currentChild.transform.position.x <= destroyLocationOffset)
            {
                Destroy(currentChild);
            }
        }

        
    }

  public void setGameOver()
    {
        isGameOver = true;
        transform.GetComponent<GameController>().InvokeGameOver();
    }

    void scrollObstacles (GameObject currentChallenege)
    {
        currentChallenege.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
    }

    void GenerateRandomObstacle()
    {
        Instantiate(obstacles[Random.Range(0, obstacles.Length)], obstacleSpawnPoint.position, Quaternion.identity, this.transform);
        counter = 1.0f;
    }

}
