using UnityEngine;

public struct Paddle {
	public GameObject go; // gameobject
	public Vector2    position;
	public Vector2    scale;
	public float      velocity;
}
public class playercontroller : MonoBehaviour {
	public GameObject camXY;
	public GameObject sprite; // the reason the sprite is here is so we can rotate it w/o rotating the hitbox.
	public GameObject playerGameObject;
	public GameObject nenemyGameObject;
	public Vector2    velocity = new Vector2(-20,1);
	public float      spriteRotateSpeed = 30;

	float  radius;
	float  dt;
	Paddle player;
	Paddle nenemy;
	
	void Die() {
		Debug.Log("You Died");
	}
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		radius            = transform.localScale.x/2;

		player.go         = playerGameObject;
		player.scale      = player.go.transform.localScale;
		player.position.y = 0;
		player.position.x = player.go.transform.position.x+player.scale.x/2;

		nenemy.go         = nenemyGameObject;
		nenemy.scale      = nenemy.go.transform.localScale;
		nenemy.position.y = 0;
		nenemy.position.x = nenemy.go.transform.position.x-nenemy.scale.x/2;
	}

	// Update is called once per frame
	void Update() {
		dt = Time.deltaTime;

		// are we hitting the player paddle HORIZONTALLY?
		if ( transform.position.x - radius < player.position.x && velocity.x < 0 ) {
			// are we hitting it VERTICALLY?
			if ( transform.position.y + radius > player.position.y - player.scale.y/2 &&
			     transform.position.y - radius < player.position.y + player.scale.y/2) {
				velocity.x = 0 - velocity.x;
			}
		}
		// are we hitting the enemy paddle HORIZONTALLY?
		if ( transform.position.x + radius > nenemy.position.x && velocity.x > 0 ) {
			if ( transform.position.y + radius > nenemy.position.y - nenemy.scale.y/2 &&
			     transform.position.y - radius < nenemy.position.y + nenemy.scale.y/2) {
				velocity.x = 0 - velocity.x;
			}
		}

		// are we hitting the edge of the screen?
		if ( transform.position.y - radius < -camXY.transform.position.y && velocity.y < 0 ||
		     transform.position.y + radius >  camXY.transform.position.y && velocity.y > 0 ) {
			velocity.y = 0 - velocity.y;
		}

		if ( velocity.x < 0 ) {
			sprite.transform.Rotate(0,0,spriteRotateSpeed*dt);
		} else {
			sprite.transform.Rotate(0,0,spriteRotateSpeed*dt*-1);
		}
		transform.position = new Vector3(
			transform.position.x + velocity.x*dt,
			transform.position.y - velocity.y*dt,
			transform.position.z
		);

		player.go.transform.position = new Vector3(player.go.transform.position.x,-player.position.y, player.go.transform.position.z);
		nenemy.go.transform.position = new Vector3(nenemy.go.transform.position.x,-nenemy.position.y, nenemy.go.transform.position.z);
	}
}
