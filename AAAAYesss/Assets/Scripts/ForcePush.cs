using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class ForcePush : MonoBehaviour {
    public float range;
    public float strength;
    private int playerNumber;

    private Rigidbody2D playerRigidBody;

	// Use this for initialization
	void Start () {
        playerNumber = GetComponent<Player>().playerId;
        playerRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        string playerIdentifier = "player" + playerNumber;
        //Debug.Log(Input.GetButton(playerIdentifier + "Submit"));
        if (Input.GetButton(playerIdentifier + "Submit"))
        {
            for(int i = 1; i <= 4; i++)
            {
                if (playerNumber != i)
                    forceField(i);
            }
        }
	}

    void forceField (int player) {
        string targetName = "player" + player;
        var target = GameObject.FindGameObjectWithTag(targetName);
        if (target != null)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, player + 7);
            if (hit.collider != null)
            {
                Rigidbody2D targetRigidBody = target.GetComponent<Rigidbody2D>();
                //Rigidbody2D targetRigidBody = hit.rigidbody;
                if (targetRigidBody != null)
                {
                    Vector3 direction3d = new Vector3(direction.x, direction.y);
                    targetRigidBody.MovePosition((target.transform.position + direction3d) * strength);
                }
            }
        }
    }
}
