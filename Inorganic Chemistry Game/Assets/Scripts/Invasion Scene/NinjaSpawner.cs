using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaSpawner : MonoBehaviour
{

    public GameObject ninja;

    private bool allNinjasSpawned;
    private Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        allNinjasSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (allNinjasSpawned)
        {
            allNinjasSpawned = false;
            StartCoroutine(SummonNinjas());
        }
    }

    IEnumerator SummonNinjas()
    {
        int numberOfNinjas = Random.Range(3, 6);
        Debug.Log("Number of ninjas: " + numberOfNinjas);
        // Start animation or something 
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < numberOfNinjas; i++)
        {
            float timeToWait = Random.Range(1f, 2f);
            Instantiate(ninja, transform.position, Quaternion.Euler(0, 180, 0), transform.parent);
            Debug.Log("Ninja summoned from portal");
            yield return new WaitForSeconds(timeToWait);
        }
        myAnim.SetBool("NinjaSpawned", true);
        yield return new WaitForSeconds(0.5f);
        allNinjasSpawned = true;
        gameObject.SetActive(false);
        
    }

}
