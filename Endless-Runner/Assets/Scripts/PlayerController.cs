using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    public float jumpPower = 10f;
    //Off screen variable.
    float posX = -7.00f;
    Rigidbody2D myRigidBody;
    bool isGrounded = false;
    bool isGameOver = false;
    ChallengeScroller myChallengeScroller;
    GameController myGameController;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = transform.GetComponent<Rigidbody2D>();
        myChallengeScroller = GameObject.FindObjectOfType<ChallengeScroller>();
        myGameController = GameObject.FindObjectOfType<GameController>();
    }

    private void FixedUpdate()
    {
        //if (Input.GetKey(KeyCode.Space) && isGrounded && !isGameOver)
        //{
        //    myRigidBody.AddForce(Vector3.up * (jumpPower * myRigidBody.mass * myRigidBody.gravityScale * 20.0f));
        //}

        if(Input.touchCount > 0)
        {
            myRigidBody.AddForce(Vector3.up * (jumpPower * myRigidBody.mass * myRigidBody.gravityScale * 20.0f));
        }

        //Hit in face check
        if (transform.position.x < posX)
        {
            GameOver();
        }

       
    }

    private void Update()
    {
       
    }

    void GameOver()
    {
        isGameOver = true;
        myChallengeScroller.setGameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.collider.tag == "Enemy")
        {
            GameOver();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Score")
        {
            myGameController.IncreaseScore(5);
            Destroy(collision.gameObject);
        }
    }
}
