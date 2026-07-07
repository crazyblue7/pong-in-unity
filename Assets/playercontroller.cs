using UnityEngine;

public struct Paddle {
	public GameObject go; // gameobject
	public float position;
	public float velocity;
}
public class playercontroller : MonoBehaviour {
	public GameObject sprite; // the reason the sprite is here is so we can rotate it w/o rotating the hitbox.
	public GameObject playerGameObject;
	public GameObject enemyGameObject;
	public Vector2    velocity = new Vector2(-20,0);
	public float      spriteRotateSpeed = 30;

	float  radius;
	float  dt;
	Paddle player;
	Paddle enemy;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		radius   = transform.localScale.x/2;
		player.go = playerGameObject;
		player.position = 0;
	}

	// Update is called once per frame
	void Update() {
		//Debug.Log("This work? " + radius + " for radius, and " + player.go.transform.position.x + " for the transform");
		dt = Time.deltaTime;

		if ( transform.position.x - radius < player.go.transform.position.x + player.go.transform.localScale.x/2 ) {
			Debug.Log(";D");
			if ( velocity.x < 0 ) {
				Debug.Log("INv");
				velocity.x = 10 - velocity.x;
				//transform.position = new Vector3(player.go.transform.position.x + player.go.transform.localScale.x/2 + radius+1,transform.position.y,transform.position.z);
			} else {
				Debug.Log("What.");
			}
		} else if ( transform.position.x + radius > player.go.transform.position.x - player.go.transform.localScale.x/2 ) {
			if ( velocity.x > 0 ) {
				velocity.x = 0 - velocity.x;
				//transform.position = new Vector3(player.go.transform.position.x - player.go.transform.localScale.x/2 - radius-1,transform.position.y,transform.position.z);
			}
		}

		sprite.transform.Rotate(0,0,spriteRotateSpeed*dt);
		transform.position = new Vector3(
			transform.position.x + velocity.x*dt,
			transform.position.y - velocity.y*dt,
			transform.position.z
		);

		player.go.transform.position = new Vector3(player.go.transform.position.x,-player.position, player.go.transform.position.z);
	}
}
