using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    private IEnemyState currentState;
    public bool attack;

    // Use this for initialization
    void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
        HandleInput();
        ResetValues();
    }

    private void HandleAttacks()
    {
        if (attack)
        {
            myAnimator.SetTrigger("attack");
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
