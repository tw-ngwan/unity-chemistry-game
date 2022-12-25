using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCollider : MonoBehaviour
{
    // There's a slight problem. If ninja gets killed when it attacks, then the timescale won't go back to 1. 
    // This needs to be changed somehow... Maybe get the timeScale factor in another script like boss? 

    private Collider2D myCollider;

    private Animator myAnim;
    private SpriteRenderer sr; 
    private string SLASH_ANIMATION = "Slash";

    private string HEADQUARTERS_TAG = "Headquarters";
    private string PLAYER_TAG = "Player";
    private string PLAYER_ATTACK_TAG = "Flame";


    private float ninjaAttack;
    private float ninjaHitpoints;

    [SerializeField]
    private GameObject ninja;
    [SerializeField]
    private GameObject attackedPanel;


    public float slowDownFactor = 0.25f;
    public float slowDownLength = 2f;

    private void Awake()
    {
        myCollider = GetComponent<CapsuleCollider2D>();
        myAnim = GetComponentInParent<Animator>();
        sr = GetComponentInParent<SpriteRenderer>();
        //Debug.Log(myAnim);

    }


    // Start is called before the first frame update
    void Start()
    {
        ninjaAttack = InvasionSceneManager.initialNinjaAttack;
        ninjaHitpoints = InvasionSceneManager.initialNinjaHitpoints;
        Debug.Log("ninjaHitpoints is " + ninjaHitpoints);
        Debug.Log("ninjaAttack is " + ninjaAttack);
        Debug.Log("totalStrength is " + InvasionSceneManager.totalStrength);

    }

    // Update is called once per frame
    void Update()
    {
        //Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        //Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(HEADQUARTERS_TAG))
        {
            Debug.Log("hit headquarters");
            DealDamageToHeadquartersWrapper();
        }

        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            Debug.Log("hit player");
            DealDamageToPlayerWrapper();
        }

        if (collision.gameObject.CompareTag(PLAYER_ATTACK_TAG))
        {
            Debug.Log("ninja attacked!");
            NinjaAttacked();
        }
    }


    private void DealDamageToHeadquartersWrapper()
    {
        StartCoroutine("DealDamageToHeadquarters");
    }

    private void DealDamageToPlayerWrapper()
    {
        StartCoroutine("DealDamageToPlayer");
    }


    IEnumerator DealDamageToHeadquarters()
    {
        myAnim.SetBool(SLASH_ANIMATION, true);
        Headquarters.numberOfHitsRemaining -= 1;
        StartCoroutine("IonBotAttackedPanel");
        CameraFollowIonBot.headquartersAttacked = true;
        Debug.Log("Hit headquarters");
        yield return new WaitForSeconds(0.5f);
        CameraFollowIonBot.headquartersAttacked = false;
        Destroy(gameObject.transform.parent.gameObject);
        myAnim.SetBool(SLASH_ANIMATION, false);
    }

    IEnumerator DealDamageToPlayer()
    {
        myAnim.SetBool(SLASH_ANIMATION, true);
        StartCoroutine("IonBotAttackedPanel");
        SlowMotionWrapper();
        IonBot.hitpoints -= ninjaAttack;
        Debug.Log("Hit player");
        yield return new WaitForSeconds(0.5f);
        myAnim.SetBool(SLASH_ANIMATION, false);
    }

    private void NinjaAttacked()
    {
        Debug.Log("Ninja attacked!");
        sr.color = new Color(2, 0, 0);
        ninjaHitpoints -= IonBot.attackPower;
        Debug.Log("Ninja remaining hitpoints: " + ninjaHitpoints);
        if (ninjaHitpoints < 1)
        {
            Destroy(ninja);
        }
    }


    public void SlowMotionWrapper()
    {
        Debug.Log("Slowed down!");
        //Time.timeScale = slowDownFactor;
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
        StartCoroutine("SlowMotion");

    }

    IEnumerator SlowMotion()
    {
        TimeManager.timeSlowed = true;
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        yield return new WaitForSeconds(0.3f);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }


    IEnumerator IonBotAttackedPanel()
    {
        attackedPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        attackedPanel.SetActive(false);
    }
}
