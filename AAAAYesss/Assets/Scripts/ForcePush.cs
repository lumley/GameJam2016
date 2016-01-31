using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Player))]
public class ForcePush : MonoBehaviour
{
    public float range;
    public float strength;
    public int cooldown = 100; //Cooldown in frames
    public int drainSpeed = 1; //Drain speed per frame

    private int playerNumber;
    private int cooldownCounter;

    private int playerCount;
    private bool isRecharging;
    
    private GameObject forcePushObject;

    // Use this for initialization
    void Start()
    {
        playerNumber = GetComponent<Player>().playerId;
        isRecharging = false;
        cooldownCounter = cooldown;
        var gameManager = GameManager.Instance;
        playerCount = gameManager != null ? gameManager.PlayerCount : 2;
        
        for(int i=0; i<transform.childCount; ++i){
            var child = transform.GetChild(i).gameObject;
            if(child.name.Contains("Force")){
                forcePushObject = child;
                break;
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        string playerIdentifier = "player" + playerNumber;
        //Debug.Log(Input.GetButton(playerIdentifier + "Submit"));
        if (cooldownCounter == cooldown)
            isRecharging = false;
        if (Input.GetButton(playerIdentifier + "Submit"))
        {
            if (!isRecharging)
                pushForce();
            else
                playTiltAnimation();
        }
        else
        {
            forcePushObject.SetActive(false);
            if (cooldownCounter < cooldown)
                isRecharging = true;
        }

        if (cooldownCounter < cooldown)
        {
            cooldownCounter++;
        }
    }

    private void playTiltAnimation()
    {
        //TODO: Implement it
    }

    private void pushForce()
    {
        if (cooldownCounter > 0)
        {
            if(cooldownCounter >= cooldown){
                var soundManager = GameObject.FindGameObjectWithTag(GameSoundManager.TAG);
                if(soundManager != null){
                    soundManager.GetComponent<GameSoundManager>().PlayAttackSoundForPlayer(playerNumber);
                }
            }
            
            for (int i = 1; i <= playerCount; i++)
            {
                if (!(i == playerNumber))
                    forceField(i);
            }
            
            if (cooldownCounter >= drainSpeed)
            {
                forcePushObject.SetActive(true);
                cooldownCounter -= drainSpeed;
            }
            else
            {
                cooldownCounter = 0;
                isRecharging = true;
                forcePushObject.SetActive(false);
            }
        }

    }

    void forceField(int player)
    {
        var target = GameObject.FindGameObjectWithTag("player" + player);
        if (target != null)
        {
            Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
            Vector2 direction = target.transform.position - this.transform.position;
            Vector3 direction3d = new Vector3(direction.x, direction.y);
            float magnitude = direction.magnitude;
            if (magnitude <= range && targetBody != null)
            {
                Debug.Log("Ranger " + playerNumber + player);
                direction3d.Normalize();
                Vector3 test = target.transform.position + (direction3d * strength);
                targetBody.MovePosition(test);
                if (target.transform.position == test)
                    Debug.Log("Succesful pushed");
            }

        }
    }
}