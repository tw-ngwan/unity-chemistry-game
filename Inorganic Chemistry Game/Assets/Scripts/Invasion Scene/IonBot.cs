using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IonBot : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 13f;

    private float movementX;

    private Rigidbody2D myBody;
    private CapsuleCollider2D myCollider;
    private Animator myAnim;
    private string WALK_ANIMATION = "Walk";
    private SpriteRenderer sr;

    private bool isGrounded = true;
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    public GameObject flyingFlames;
    public GameObject ionBot;
    public GameObject attackFlames;

    private int objectScale = 5;

    public static float attackPower;
    public static float hitpoints;
    public static float maxHitpoints;

    public Slider healthbar;

    public static bool playerAlive = true;


    // Moving player using UI Buttons 
    private int direction;
    private bool moveIonBot = false;

    private void Awake()
    {
        playerAlive = true;
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();


        // Not directly referencing attackPower in DataAcrossScenes because we account for what happens when numIons < 5
        if (DataAcrossScenes.numIons < 5)
        {
            attackPower = 200;
            maxHitpoints = 2000;
        }
        else
        {
            attackPower = DataAcrossScenes.ionBotAttackPower;
            maxHitpoints = DataAcrossScenes.ionBotHitpoints;
        }
        hitpoints = maxHitpoints;

    }

    //Start is called before the first frame update
    void Start()
    {
        healthbar.maxValue = maxHitpoints;
        healthbar.value = hitpoints;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = hitpoints;

        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
        //Debug.Log(isGrounded);

        sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.5f);

        CheckIfAlive();
        // So if you want to create left, right buttons, use EventTrigger and PointerDown or something 

    }



    void PlayerMoveKeyboard()
    {
        if (moveIonBot)
        {
            ionBot.transform.position += new Vector3(1 * direction, 0, 0) * Time.deltaTime * moveForce;
            ionBot.transform.localScale = new Vector3(objectScale * direction, objectScale, 1f);
        }
        
        else
        {
            movementX = Input.GetAxisRaw("Horizontal");
            //if (movementX != 0)
            //    Debug.Log("movementX is " + movementX);

            ionBot.transform.position += new Vector3(movementX, 0, 0) * Time.deltaTime * moveForce;

            //myBody.transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;

            if (movementX == -1 || movementX == 1)
            {
                myBody.transform.localScale = new Vector3(objectScale * movementX, objectScale, 1f);
            }
        }

    }

    void AnimatePlayer()
    {

        if (movementX == 1 || movementX == -1 || !isGrounded || moveIonBot)
        {
            flyingFlames.SetActive(true);
        }
        
        else
        {
            flyingFlames.SetActive(false);
        }

    }


    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); //ForceMode2D.Impulse gives an instant force based on body's mass. .Force gives force, not instant
            //Debug.Log("Jump is pressed");

            //GetButtonDown detects when any key is pressed down. "Jump" goes across platforms, from spacebar on com to X on XBOX, for eg
            //GetButtonUp detects when key is released. GetButton detects when key is held down
        }
    }

    public void PlayerJumpButton()
    {
        if (isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }


    public void OnPressMoveLeft()
    {
        direction = -1;
        moveIonBot = true;


    }
    public void OnPressMoveRight()
    {
        direction = 1;
        moveIonBot = true;

    }

    public void OnRelease()
    {
        moveIonBot = false;
        direction = 0;
    }

    // This is not working for some reason
    // Ok I see the problem. Apparently it's coz this relates to the script gameObject since I made it a separate object. I need to compare collision of ionBot instead
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
            Debug.Log("Landed on ground");
            //Asking: Game object that has a tag that was landed on, is the tag of the object GROUND_TAG? 
            //In-built function in MonoBehaviour 
        }

    }


    public void AttackWrapper()
    {
        StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        attackFlames.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackFlames.SetActive(false);
    }


    public void CheckIfAlive()
    {
        if (hitpoints < 1 && playerAlive)
        {
            playerAlive = false;
            Debug.Log("Player dead!");
            StartCoroutine("HalfSecondToDeath");
        }
    }

    IEnumerator HalfSecondToDeath()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
