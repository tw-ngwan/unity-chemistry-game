using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowIonBot : MonoBehaviour
{

    private Transform player;

    private Vector3 tempPos;
    private string PLAYER_TAG = "Player";


    [SerializeField]
    private float minX = -32;
    [SerializeField]
    private float maxX = 15;
    [SerializeField]
    private float maxY = 17f;

    // Moving of camera for other purposes
    public static bool headquartersAttacked = false;
    public static bool bossRoaring = false;

    private int elapsedFrames = 0;
    private int interpolationFramesCount = 50;

    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag(PLAYER_TAG).transform;
        Debug.Log(player);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // If player is dead, camera stops moving 
        if (!player)
        {
            return;
        }

        // Else, camera follows player's position, until borders of map 
        else
        {
            tempPos = transform.position;
            tempPos.x = player.position.x;
            tempPos.y = player.position.y + 2;

            if (tempPos.x < minX)
            {
                tempPos.x = minX;
            }
            else if (tempPos.x > maxX)
            {
                tempPos.x = maxX;
            }

            if (tempPos.y > maxY)
            {
                tempPos.y = maxY;
            }

        }

        transform.position = tempPos;

        // LateUpdate calls after everything in Update() is called. This prevents jittery camera.
    }
} //class
