using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public Transform groundCheck;
    public float groundCheckRadius;

    public float speed;  
    public float jumpForce;

    public float jumpTime;
    float jumpTimeCounter;
    
    bool ground = false; 
    LayerMask isGround;
    Rigidbody2D _rb;
    Collider2D _col;
    Animator _anim;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        isGround = 1 << 8; //Ground layer

        jumpTimeCounter = jumpTime;
    }

    private void Update()
    { 
        ground = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGround);

        _rb.velocity = new Vector2(speed, _rb.velocity.y);

        if (ground && (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(1)))
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //_rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }

        if (jumpTimeCounter > 0 && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(1)))
        { 
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(1)))
        {
            jumpTimeCounter = 0;
        }
        if (ground)
        {
            jumpTimeCounter = jumpTime;
        }

        _anim.SetFloat("Speed", _rb.velocity.x);
        _anim.SetBool("Ground", ground);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KillBox")
        { 
            GameManager.instance.Restart();
        } 
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Coint")
        {
            GameManager.instance.UpdateCointText(++GameManager.instance.coint);
            Destroy(col.gameObject);
        }
    }
} 