using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D myRigidbody;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;
    private bool facingRight;
    private bool slide;

	// Use this for initialization
	void Start ()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}

    void Update()
    {
        HandleInput();
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);

        Flip(horizontal);

        ResetValues();
	}

    private void HandleMovement(float horizontal)
    {
        if (!myAnimator.GetBool("slide"))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", true);
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", false);
        }
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    private void ResetValues()
    {
        slide = false;
    }
}

