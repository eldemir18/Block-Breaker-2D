//using System;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{   
    // Config params
    [SerializeField] Paddle paddle;
    [SerializeField] float  startPushY;
    [SerializeField] float  startPushX;

    // State
    Vector2 paddleToBallVector;
    bool    hasStarted = false;
    float   secondsX = 0.0f;
    float   timerX   = 0.0f;
    float   secondsY = 0.0f;
    float   timerY   = 0.0f;
    float   currentPositionOfBallY  = 0.0f;
    float   previousPositionOfBallY = 0.0f;
    float   currentPositionOfBallX  = 0.0f;
    float   previousPositionOfBallX = 0.0f;

    // Cached reference
    Rigidbody2D myRigidbod2D;

    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        myRigidbod2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            LockBallToPedal();
            LaunchBall();
        }
        
        else if (hasStarted)
        {
            if (IsBallStuckX())
            {
                 
                if(transform.position.y > 8)
                {
                    startPushY = myRigidbod2D.velocity.x > 0 ? startPushY : -startPushY;
                    myRigidbod2D.velocity = new Vector2(startPushY, -startPushX);
                }
                else if (transform.position.y < 8)
                {
                    startPushY = myRigidbod2D.velocity.x > 0 ? startPushY : -startPushY;
                    myRigidbod2D.velocity = new Vector2(startPushY, startPushX);
                }
            }
            
            else if (IsBallStuckY())
            {
                if (transform.position.x > 8)
                {
                    startPushY = myRigidbod2D.velocity.y > 0 ? startPushY : -startPushY;
                    myRigidbod2D.velocity = new Vector2(-Mathf.Abs(startPushX), startPushY);
                }
                else if (transform.position.x < 8)
                {
                    startPushY = myRigidbod2D.velocity.y > 0 ? startPushY : -startPushY;
                    myRigidbod2D.velocity = new Vector2(Mathf.Abs(startPushX), startPushY);
                }
            }
        }
    }

    private bool IsBallStuckY()
    {
        currentPositionOfBallX = transform.position.x;
        if (Mathf.Abs(currentPositionOfBallX - previousPositionOfBallX) < 0.2)
        {
            timerX += Time.deltaTime;
            secondsX = timerX % 60;
            
            if (secondsX > 5)
                return true;
        }
        
        else
        {
            previousPositionOfBallX = currentPositionOfBallX;
            timerX = 0.0f;
        }

        return false;
    }

    private bool IsBallStuckX()
    {
        currentPositionOfBallY = transform.position.y;
        if(Mathf.Abs(currentPositionOfBallY - previousPositionOfBallY) < 0.2)
        {
            timerY += Time.deltaTime;
            secondsY = timerY % 60;
            
            if(secondsY > 5)
                return true;  
        }
        
        else
        {
            previousPositionOfBallY = currentPositionOfBallY;
            timerY = 0.0f;
        }
        
        return false;
    }

    private void LaunchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            startPushX = (Random.value < 0.5f) ? -2f : 2f;             
            startPushY = 8f;
            myRigidbod2D.velocity = new Vector2(startPushX, startPushY);
        }  
    }

    private void LockBallToPedal()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddleToBallVector + paddlePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted) 
            GetComponent<AudioSource>().Play();
    }
}
