using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Player))]
public class ForcePush : MonoBehaviour
{
    public float range;
    public float strength;
    public int cooldown; //Cooldown in frames
    public int drainSpeed; //Drain speed per frame
    public GameObject forcePushAnim;

    private int playerNumber;
    private int cooldownCounter;
    private int playerCount;
    private bool isRecharging;

    // Use this for initialization
    void Start()
    {
        playerNumber = GetComponent<Player>().playerId;
        isRecharging = false;
        cooldownCounter = cooldown;
        var gameManager = GameManager.Instance;
        playerCount = gameManager != null ? gameManager.PlayerCount : 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        string playerIdentifier = "player" + playerNumber;
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
            if (cooldownCounter < cooldown)
            {
                isRecharging = true;
                playForceAnimation(false);
            }
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
            for (int i = 1; i <= playerCount; i++)
            {
                if (!(i == playerNumber))
                    forceField(i);
            }
            if (cooldownCounter >= drainSpeed)
            {
                cooldownCounter -= drainSpeed;
            }
            else
            {
                cooldownCounter = 0;
                isRecharging = true;
                playForceAnimation(false);
            }
        }

    }

    void playForceAnimation(Boolean playing)
    {
        forcePushAnim.SetActive(playing);
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
                direction3d.Normalize();
                playForceAnimation(true);
                targetBody.MovePosition(target.transform.position + (direction3d * strength));
            }

        }
    }
}