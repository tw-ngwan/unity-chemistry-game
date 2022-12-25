using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{


    private Rigidbody2D myBody;
    private Animator myAnim;
    private CapsuleCollider2D myCollider;

    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string PLAYER_TAG = "Player";

    private string HEADQUARTERS_TAG = "Headquarters";

    private bool isGrounded = true;

    [SerializeField]
    private float speed = 4f;
    private int direction = -1;

    private SpriteRenderer sr;    






    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        //Debug.Log(myAnim);
        //Debug.Log(myBody);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.5f); // Slowly linear interpolate to white. Takes 3 sec
    }

    // Setting the walk animation of the Ninja, depending on whether it's grounded. 
    private void FixedUpdate()
    {
        if (myBody.velocity.y != 0)
        {
            //Debug.Log("Not grounded!");
            isGrounded = false;
            myAnim.SetBool(WALK_ANIMATION, false);
        }
        else if (myBody.velocity.y == 0)
        {
            isGrounded = true;
            myAnim.SetBool(WALK_ANIMATION, true);
        }

        if (isGrounded)
        {
            Debug.Log("Ninja walking");
            myAnim.SetBool(WALK_ANIMATION, true);
            myBody.velocity = new Vector2(speed * direction, myBody.velocity.y);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            Debug.Log("Landed on ground");
            isGrounded = true;
        }

    }





    

    
    
}
