using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratulatoryPhraseManager : MonoBehaviour
{

    private Transform phraseTransform;
    private Vector3 movementSpeed = new Vector3(0.1f, 12, 0f);
    // Start is called before the first frame update
    void Start()
    {
        phraseTransform = GetComponent<Transform>();
        Debug.Log("Got transform");
    }

    // Update is called once per frame
    void Update()
    {
        phraseTransform.transform.position += movementSpeed * Time.deltaTime;
    }
}
