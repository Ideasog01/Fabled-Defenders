using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_R : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    public static bool isGrounded;
    public Transform feetPos;
    [SerializeField]
    private float checkRadius;
    private float moveInput;
    public LayerMask ground;

    public bool stunned;

    

    public bool onIce;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!stunned)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onIce)
        {
            moveInput = Input.GetAxis("Horizontal");
            speed = 8;
        }
        else
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            speed = 5;
        }

        if(this.transform.position.y < -500)
        {
            transform.position = new Vector3(GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().xInt, GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().yInt, 0);
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !stunned && !PlayerAttack.disableAttack)
        {
            rb.velocity = Vector2.up * jumpForce;
            Debug.Log("Jumped!");
        }

    }
}
