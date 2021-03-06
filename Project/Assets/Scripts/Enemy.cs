﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    private static Enemy instance;

    public static Enemy Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Enemy>();
            }
            return instance;
        }
    }

    public GameObject Target { get; set; }

    private IEnemyState currentState;
    public bool attack;

    public bool Melee { get; set; }

    [SerializeField]
    private float meleeRange;

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
           
                return false;
        }
    }



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
        currentState.Execute();
        LookAtTarget();
    }

    void FixedUpdate()
    {
        HandleAttacks();
        ResetValues();
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

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            //MyAnimator.SetTrigger("attack");
            attack = true;
        }
    }

    private void HandleAttacks()
    {
        if (attack)
        {
            MyAnimator.SetTrigger("attack");
        }
    }

   private void ResetValues()
    {
        attack = false;
    }
}
