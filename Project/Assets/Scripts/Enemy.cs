using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public GameObject Target { get; set; }

    private IEnemyState currentState;
    public bool attack;

    // Use this for initialization
    public override void Start ()
    {

        base.Start();
        ChangeState(new IdleState());
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandleInput();
        ResetValues();
        currentState.Execute();
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if(Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0  && facingRight ||xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        MyAnimator.SetFloat("speed", 1);
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other); 
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    private void HandleAttacks()
    {
        if (attack)
        {
            MyAnimator.SetTrigger("attack");
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }
    }

    private void ResetValues()
    {
        attack = false;
    }
}
