using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{

    public static float shieldHealth;
    public static bool shieldActive = true;
    private Collider2D myCollider;
    private SpriteRenderer sr;

    private string PLAYER_ATTACK_TAG = "Flame";

    public GameObject bossCollider;

    // Display shield healthbar
    private float shieldMaxHealth;
    public Slider healthbar;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shieldHealth = InvasionSceneManager.initialShieldHitpoints;
        shieldMaxHealth = shieldHealth * 4;
        healthbar.maxValue = shieldMaxHealth;
        healthbar.value = shieldHealth;
        Debug.Log("shieldHealth is " + shieldHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = shieldHealth;
        sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_ATTACK_TAG))
        {
            ShieldAttacked();
        }
    }


    void ShieldAttacked()
    {
        Debug.Log("Shield attacked");
        sr.color = new Color(2, 2, 0);
        shieldHealth -= IonBot.attackPower;
        Debug.Log("Shield remaining hitpoints: " + shieldHealth);

        if (shieldHealth < 1)
        {
            shieldHealth = 0;
            shieldActive = false;
            gameObject.SetActive(false);
            bossCollider.SetActive(true);
        }
    }
}
