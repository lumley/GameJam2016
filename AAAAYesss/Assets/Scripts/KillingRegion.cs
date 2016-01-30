using UnityEngine;
using System.Collections.Generic;
using System;

public class KillingRegion : MonoBehaviour
{

    private static string COLLIDING_TAG = "player";
    public float secondsToKill = 3.0f;

    public int sparePlayerWithId = 0;

    private float[] collidingPlayerValueArray = new float[4];
    private bool[] isPlayerCollidingArray = new bool[4];

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.StartsWith(COLLIDING_TAG))
        {
            var playerScript = other.GetComponent<Player>();
            if (playerScript.playerId != sparePlayerWithId && !isPlayerCollidingArray[playerScript.playerId - 1])
            {
                isPlayerCollidingArray[playerScript.playerId - 1] = true;
            }
        }
    }

    void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime;
        for (int i = 0; i < isPlayerCollidingArray.Length; ++i)
        {
            if (isPlayerCollidingArray[i])
            {
                collidingPlayerValueArray[i] += deltaTime;
                if(collidingPlayerValueArray[i] > secondsToKill){
                    isPlayerCollidingArray[i] = false;
                    collidingPlayerValueArray[i] = 0;
                    
                    var dyingTarget = GameObject.FindGameObjectWithTag(COLLIDING_TAG + (i + 1));
                    if(dyingTarget != null){
                        var playerScript = dyingTarget.GetComponent<Player>();
                        playerScript.KillPlayer();
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.StartsWith(COLLIDING_TAG))
        {
            var playerScript = other.GetComponent<Player>();
            isPlayerCollidingArray[playerScript.playerId - 1] = false;
            collidingPlayerValueArray[playerScript.playerId - 1] = 0;
        }
    }
}
