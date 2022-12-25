using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] musics = GameObject.FindGameObjectsWithTag("Music");
        if (musics.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GameObject[] musics = GameObject.FindGameObjectsWithTag("OtherMusic");
        if (musics.Length >= 1)
        {
            Destroy(gameObject);
        }
    }
}
