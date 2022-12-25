using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{

    private SpriteRenderer sr;
    private string PLAYER_ATTACK_TAG = "Flame";

    public GameObject boss;
    public static bool bossAlive;


    private void Awake()
    {
        bossAlive = true;
        sr = GetComponentInParent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_ATTACK_TAG))
        {
            BossAttacked();
        }
    }


    void BossAttacked()
    {
        Debug.Log("Boss attacked");
        sr.color = new Color(2, 2, 0);
        Boss.bossHealth -= IonBot.attackPower;
        Debug.Log("Boss remaining hitpoints: " + Boss.bossHealth);

        if (Boss.bossHealth < 1)
        {
            bossAlive = false;
            Destroy(boss);
        }
    }
}
