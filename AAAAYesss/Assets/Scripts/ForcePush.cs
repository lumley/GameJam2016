using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class ForcePush : MonoBehaviour {
    public float range;
    public float strength;
    public int cooldown; //Cooldown in frames
    public int drainSpeed; //Drain speed per frame

    private bool isForceEnabled;
    private int playerNumber;
    private int cooldownCounter;
    private int playerCount;

    // Use this for initialization
    void Start () {
        playerNumber = GetComponent<Player>().playerId;
        isForceEnabled = false;
        cooldownCounter = cooldown;
        var gameManager = GameManager.Instance;
        playerCount = gameManager != null ? gameManager.PlayerCount : 2;
    }

    // Update is called once per frame
    void Update()
    {
        string playerIdentifier = "player" + playerNumber;
        //Debug.Log(Input.GetButton(playerIdentifier + "Submit"));
        isForceEnabled = Input.GetButton(playerIdentifier + "Submit");
        if (Input.GetButton(playerIdentifier + "Submit") && cooldownCounter >= 0)
        {
            if (cooldownCounter == cooldown)
            {
                Debug.Log("playercount " + playerCount);
                for (int i = 1; i <= playerCount; i++)
                {
                    if (!(i == playerNumber))
                        forceField(i);
                }
            }
            if (cooldownCounter >= drainSpeed)
                cooldownCounter -= drainSpeed;
            else
                cooldownCounter = 0;
        }
        else if (cooldownCounter < cooldown)
        {
            cooldownCounter++;
        }
    }

    void forceField (int player) {
        //volatile GameObject target;
        var target = GameObject.FindGameObjectsWithTag("player" + player);
        if (target[0] != null)
        {   
            Rigidbody2D targetBody = target[0].GetComponent<Rigidbody2D>();
            Vector2 direction = target[0].transform.position - this.transform.position;
            Vector3 direction3d = new Vector3(direction.x, direction.y);
            float magnitude = direction.magnitude;
            if (magnitude <= range && targetBody != null)
            {
                Debug.Log("Ranger " + playerNumber + player);
                direction.Normalize();
                targetBody.MovePosition((target[0].transform.position + direction3d) * strength);
            }
            
        }
    }
}
