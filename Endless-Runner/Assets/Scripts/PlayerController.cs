using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    public float jumpPower = 10f;
    public float maxVelocity = 10f;
        
    
    float sqrMaxVelocity;
    
    
    //Off screen variable.
    float posX = -10.00f;
    public Animator animator;
    Rigidbody2D myRigidBody;
    bool isGrounded = false;
    bool isGameOver = false;
    //bool jump = false;
    ChallengeScroller myChallengeScroller;
    GameController myGameController;

    private void Awake()
    {
        myRigidBody = transform.GetComponent<Rigidbody2D>();
        myChallengeScroller = GameObject.FindObjectOfType<ChallengeScroller>();
        myGameController = GameObject.FindObjectOfType<GameController>();
        sqrMaxVelocity = maxVelocity * maxVelocity;
    }



    private void FixedUpdate()
    {
        //TO CLAMP MOVEMENT SPEED
        var v = myRigidBody.velocity;

        if (v.SqrMagnitude() > sqrMaxVelocity)
        {
            //Normalised converts vector length to 1
            myRigidBody.velocity = v.normalized * maxVelocity;
        }

        if(Input.touchCount > 0 && isGrounded && !isGameOver || Input.GetKey(KeyCode.Space) && isGrounded && !isGameOver)
        {
            animator.SetBool("jump", true);
            myRigidBody.AddForce( Vector3.up * (jumpPower * myRigidBody.mass * myRigidBody.gravityScale * 20.0f));
        }

        //Hit in face check
        if (transform.position.x < posX)
        {
           GameOver();
        }

        animator.SetFloat("speed", myChallengeScroller.scrollSpeed);
        
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
            animator.SetBool("jump", false);
            //myRigidBody.SetRotation(0);
            myRigidBody.MoveRotation(-myRigidBody.rotation * Time.fixedDeltaTime);
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
