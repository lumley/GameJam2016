using UnityEngine;
using System.Collections.Generic;
using System;

public class KillingRegion : MonoBehaviour {

    private static string COLLIDING_TAG = "player";
	public float secondsToKill = 3.0f;
    
    public int sparePlayerWithId = 0;
    
    private Dictionary<string, float> collidingPlayers = new Dictionary<string, float>();
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag.StartsWith(COLLIDING_TAG)){
            var playerScript = other.GetComponent<Player>();
            if(playerScript.playerId != sparePlayerWithId && !collidingPlayers.ContainsKey(other.tag)){
                collidingPlayers.Add(other.tag, 0.0f);
            }
        }
    }
    
    void FixedUpdate(){
        List<string> playersKilled = null;
        foreach(var key in collidingPlayers.Keys){
            if(ShouldKillPlayer(key, Time.fixedDeltaTime)){
                if(playersKilled == null){
                    playersKilled = new List<string>();
                }
                
                playersKilled.Add(key);
            }        
        }
        
        if(playersKilled != null){
            foreach(var key in playersKilled){
                var otherPlayer = GameObject.FindGameObjectWithTag(key);
                if(otherPlayer != null){
                    otherPlayer.GetComponent<Player>().KillPlayer();
                }
                collidingPlayers.Remove(key);
            }
        }
    }

    private bool ShouldKillPlayer(string tag, float deltaTime)
    {        
        collidingPlayers[tag] += deltaTime;
        return collidingPlayers[tag] > secondsToKill;
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag.StartsWith(COLLIDING_TAG) && collidingPlayers.ContainsKey(other.tag)){
            collidingPlayers.Remove(tag);
        }
    }
}
