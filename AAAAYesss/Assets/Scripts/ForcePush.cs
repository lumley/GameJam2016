using UnityEngine;
using System.Collections;

public class ForcePush : MonoBehaviour {
    public float range;
    public float strength;
    public int playerNumber;

    private Rigidbody2D playerRigidBody;

	// Use this for initialization
	void Start () {
        playerRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        string playerIdentifier = "player" + playerNumber;
        Debug.Log(Input.GetButton(playerIdentifier + "Submit"));
        if (Input.GetButtonDown(playerIdentifier + "Submit"))
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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range);
            if (hit.collider != null)
            {
                Rigidbody2D targetRigidBody = target.GetComponent<Rigidbody2D>();
                if(targetRigidBody != null)
                {
                    targetRigidBody.AddForce(direction * strength, ForceMode2D.Impulse);
                }
            }
        }
    }
}
