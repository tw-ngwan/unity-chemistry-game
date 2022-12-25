using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // Things to do: 
    // 1. This scene is about done and dusted. Now, in intro scene, refer to Quiz button and do the same thing. And for gold, yeah, add a condition

    public static float bossHealth;

    private SpriteRenderer sr;
    Color activeColor;

    // For coroutine
    Coroutine routine;
    Coroutine movementRoutine;
    private bool cycleActive = true;

    private string PLAYER_ATTACK_TAG = "Flame";

    // For Spawning Ninjas 
    private int numberOfNinjas = 3;
    public GameObject ninja;

    // For bomb attack
    public GameObject ionBotBomb;

    // For Scorched Earth attack
    private string ENEMY_TAG = "Enemy";


    // To display animation 
    private Animator bossAnim;

    // To move boss
    private Rigidbody2D bossBody;
    private float speed = 6f;
    private int direction;
    private int elapsedFrames = 0;
    private int interpolationFramesCount = 50;
    private bool bossPositionCorrect = true;

    // To move player after stun 
    private GameObject ionBot;
    private Vector3 ionBotOriginalPosition = new Vector3(-32.364f, -2.010f, 0);
    private string PLAYER_TAG = "Player";
    private bool afterStunAttack = false;

    public static List<float> bossYPositions = new List<float>()
        {
            16.89f, 11.39f, 5.89f, -0.7f
        };
    private int currentPositionIndex = 5;
    private int pastPositionIndex;


    // To display boss healthbar 
    private float bossMaxHealth;
    public Slider healthbar;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        bossAnim = GetComponent<Animator>();
        bossBody = GetComponent<Rigidbody2D>();
        ionBot = GameObject.FindGameObjectWithTag(PLAYER_TAG);
    }

    // Start is called before the first frame update
    void Start()
    {
        bossMaxHealth = InvasionSceneManager.initialBossHitpoints;
        bossHealth = bossMaxHealth;
        healthbar.maxValue = bossMaxHealth;
        healthbar.value = bossMaxHealth;
        routine = StartCoroutine("BossFunctionCycle");
        movementRoutine = StartCoroutine("MoveBossPosition");
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = bossHealth;
        // May have to solve problem of ninja getting stuck 
        if (bossPositionCorrect != true)
        {
            Debug.Log("Boss moved");
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            Vector3 interpolatedPosition = Vector3.Lerp(new Vector3(21.91f, bossYPositions[pastPositionIndex], 0),
                new Vector3(21.91f, bossYPositions[currentPositionIndex], 0), 
                interpolationRatio);
            transform.position = interpolatedPosition;
            elapsedFrames += 1;
            if (elapsedFrames == interpolationFramesCount) // To reset stuff so that boss no longer moves
            {
                elapsedFrames = 0;
                bossPositionCorrect = true;
            }
           
        }


        if (Shield.shieldActive == false && cycleActive == true)
        {
            cycleActive = false;
            StopCoroutine(routine);
            StopCoroutine(movementRoutine);
            Debug.Log("Coroutine stopped.");
            StartCoroutine("BossStunned");
        }

        else if (Shield.shieldActive == false && cycleActive == false)
        {
            sr.color = Color.Lerp(sr.color, Color.grey, Time.deltaTime / 0.5f);
        }

        
    }



    IEnumerator MoveBossPosition()
    {
        Debug.Log("MoveBossPosition called");
        
        if (currentPositionIndex == 5)
        {
            currentPositionIndex = 0;
        }
        yield return new WaitForSeconds(9);
        int numCycles = 0;
        while (true)
        {
            Debug.Log("Commencing one cycle");
            pastPositionIndex = currentPositionIndex;
            currentPositionIndex = Random.Range(0, bossYPositions.Count);
            if (currentPositionIndex == pastPositionIndex)
            {
                continue;
            }
            int posIndexDiff = System.Math.Abs(currentPositionIndex - pastPositionIndex);
            interpolationFramesCount = 100 * posIndexDiff;


            //if (currentPositionIndex > pastPositionIndex) // This is because bossYPositions is in decreasing order, such that later in list, it needs to move down
            //{
            //    direction = -1;
            //}
            //else if (currentPositionIndex < pastPositionIndex)
            //{
            //    direction = 1;
            //}
            //else if (currentPositionIndex == pastPositionIndex)
            //{
            //    continue;
            //}

            //float pastPosition = bossYPositions[pastPositionIndex];
            //float currentPosition = bossYPositions[currentPositionIndex];

            //if (transform.position != new Vector3(21.91f, bossYPositions[positionIndex], 0f))
            //{
            //    bossBody.velocity = new Vector2(bossBody.velocity.x, speed * direction);
            //}
            //else
            //{
            //    bossBody.velocity = new Vector2(bossBody.velocity.x, 0);
            //}
            bossPositionCorrect = false;
            numCycles += 1;
            Debug.Log("Movement cycle " + numCycles);
            float timeToWait = Random.Range(6, 12);

            yield return new WaitForSeconds(timeToWait);
        }
        


    }

    
    IEnumerator BossFunctionCycle()
    {
        int j = 0;
        Debug.Log("Start");
        while (true)
        {
            ShieldHealthBoost();
            
            j += 1;
            Debug.Log("Boosting shield health " + j);
            Debug.Log("Cycle complete: " + j);
            yield return new WaitForSeconds(10f);
            while (!bossPositionCorrect) // This is supposedly to delay the next move until boss is in position
            {
                yield return new WaitForSeconds(0.1f);
            }

            SummonNinjasWrapper();
            j += 1;
            Debug.Log("Summoning Ninjas " + j);
            Debug.Log("Cycle complete: " + j);
            yield return new WaitForSeconds(10f);
            while (!bossPositionCorrect)
            {
                yield return new WaitForSeconds(0.1f);
            }

            //SelectRandomFunction();
            //j += 1;
            //Debug.Log("Cycle complete: " + j);

            for (int i = 0; i < 3; i++)
            {
                SelectRandomFunction();
                j += 1;
                Debug.Log("Cycle complete: " + j);
                yield return new WaitForSeconds(10f);
                while (!bossPositionCorrect)
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }

            ScorchedEarthAttack();
            j += 1;
            Debug.Log("Cycle complete: " + j);
            yield return new WaitForSeconds(5f);
            while (!bossPositionCorrect)
            {
                yield return new WaitForSeconds(0.1f);
            }

            
        }
    }


    // This seems to work... 
    // Ok now it works. I think...
    IEnumerator BossStunned()
    {
        Debug.Log("Boss is stunned!");
        DestroyAll(ENEMY_TAG);
        sr.color = new Color(0.5f, 0.5f, 0.5f);
        yield return new WaitForSeconds(5f);
        sr.color = Color.white;
        Shield.shieldActive = true;
        cycleActive = true;
        afterStunAttack = true;
        routine = StartCoroutine("BossFunctionCycle");
        movementRoutine = StartCoroutine("MoveBossPosition");
        yield return routine;
        yield return movementRoutine;
        
        
    }

    private void SelectRandomFunction()
    {
        int functionChoice = Random.Range(0, 3);
        Debug.Log("functionChoice is " + functionChoice);
        switch (functionChoice)
        {
            case 0:
                ShieldHealthBoost();
                break;
            case 1:
                SummonNinjasWrapper();
                break;
            case 2:
                TargetedBombAttack();
                break;
        }
    }
    
    // Remember to change these to private after you're done testing the functions 
    public void ShieldHealthBoost()
    {
        StartCoroutine("BossRoar");
        StartCoroutine("ShieldHealthBoostTimer");
    }

    public void SummonNinjasWrapper()
    {
        StartCoroutine("BossRoar");
        StartCoroutine("SummonNinjas");
    }

    public void TargetedBombAttack()
    {
        StartCoroutine("BossRoar");
        Debug.Log("Boss used Targeted Bomb Attack!");
        StartCoroutine("TargetedBomb");

    }


    public void ScorchedEarthAttack()
    {
        StartCoroutine("BossRoar");
        StartCoroutine("ScorchedEarth");
    }


    IEnumerator ShieldHealthBoostTimer()
    {
        Shield.shieldActive = true;
        Debug.Log("Boss using Ice Shield");
        yield return new WaitForSeconds(1f);
        GameObject shield = gameObject.transform.GetChild(0).gameObject;
        GameObject bossCollider = gameObject.transform.GetChild(1).gameObject;
        shield.SetActive(true);
        bossCollider.SetActive(false);
        if (afterStunAttack)
        {
            ionBot.transform.position = ionBotOriginalPosition;
            afterStunAttack = false;
        }
        float maxShieldHealth = InvasionSceneManager.initialShieldHitpoints * 4;
        Shield.shieldHealth += InvasionSceneManager.initialShieldHitpoints;
        if (Shield.shieldHealth > maxShieldHealth)
        {
            Shield.shieldHealth = maxShieldHealth;
        }

    }

    // This seems to work. Check back. 
    IEnumerator SummonNinjas()
    {
        Transform spawnLocation = gameObject.transform.GetChild(2);
        spawnLocation.gameObject.SetActive(true);
        Debug.Log("Boss summoning Ninjas!");
        for (int i = 0; i < numberOfNinjas; i++)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(ninja, spawnLocation.position, Quaternion.Euler(0, 180, 0), transform.parent);
            yield return new WaitForSeconds(1f);
        }
        Animator shrinkPortal = spawnLocation.gameObject.GetComponentInChildren<Animator>();
        shrinkPortal.SetBool("NinjaSpawned", true);
        spawnLocation.gameObject.SetActive(false);
    }

    IEnumerator TargetedBomb()
    {
        float missileDamage = 0.01111f * IonBot.maxHitpoints + 377.78f; 
        ionBotBomb.SetActive(true);

        yield return new WaitForSeconds(3f);
        GameObject explosion = ionBotBomb.gameObject.transform.GetChild(0).gameObject;
        explosion.SetActive(true);
        IonBot.hitpoints -= missileDamage;
        yield return new WaitForSeconds(0.5f);
        explosion.SetActive(false);
        ionBotBomb.SetActive(false);

        Debug.Log("Bomb exploded");

    }


    IEnumerator ScorchedEarth()
    {
        float scorchedEarthDamage = 0.50f * IonBot.maxHitpoints + 2000f; 
        GameObject scorchedEarth = gameObject.transform.GetChild(3).gameObject;
        scorchedEarth.SetActive(true);
        DestroyAll(ENEMY_TAG);
        IonBot.hitpoints -= scorchedEarthDamage;
        yield return new WaitForSeconds(1.5f);
        scorchedEarth.SetActive(false);
        

        Debug.Log("Bomb exploded");

    }

    void DestroyAll(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }


    IEnumerator BossRoar()
    {
        sr.color = new Color(2, 0, 0);
        bossAnim.SetBool("SpecialMove", true);
        CameraFollowIonBot.bossRoaring = true;
        yield return new WaitForSeconds(1f);
        bossAnim.SetBool("SpecialMove", false);
        CameraFollowIonBot.bossRoaring = false;
        sr.color = Color.white;
    }
}
