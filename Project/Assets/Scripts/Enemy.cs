using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

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
