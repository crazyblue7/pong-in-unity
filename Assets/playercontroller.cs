using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public struct Paddle {
	public GameObject go; // gameobject
	public Vector2    position;
	public Vector2    scale;
	public float      velocity;
}
public class playercontroller : MonoBehaviour {
	public GameObject camXY;
	public GameObject playerGameObject;
	public GameObject nenemyGameObject;
	public TMP_Text   pointsDisplay;
	public Vector2    points = new Vector2(0,0);
	public Vector2    startVelocity = new Vector2(20,1);
	public Vector2    velocity;
	public float      spriteRotateSpeed = 30;
	public float      paddleSpeed;
	public float      restitution = 1;

	float  nenemyAIoffset;
	float  idkOfThisSecond;
	float  radius;
	float  dt;
	Paddle player;
	Paddle nenemy;

	int frameCounterForThisSecond;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		velocity = startVelocity;
		radius   = transform.localScale.x/2;

		player.go         = playerGameObject;
		player.scale      = player.go.transform.localScale;
		player.position.y = 0;
		player.position.x = player.go.transform.position.x+player.scale.x/2;

		nenemy.go         = nenemyGameObject;
		nenemy.scale      = nenemy.go.transform.localScale;
		nenemy.position.y = 0;
		nenemy.position.x = nenemy.go.transform.position.x-nenemy.scale.x/2;

		frameCounterForThisSecond = 0;
		nenemyAIoffset = 0;
	}

	void Reset() {
		transform.position = new Vector3(0,0,transform.position.z);
		Start();
	}
	
	void LosePoint() {
		// well technically we arent losing a point but idk what to call this function so its gonna be lose point
		// nenemy gains point
		points.y++;
		Reset();
	}
	void GainPoint() {
		points.x++;
		Reset();
	}

	public void pause() {
		SceneManager.LoadScene("mainmenu");
	}
	// Update is called once per frame
	void Update() {
		dt = Time.deltaTime;
		frameCounterForThisSecond++;
		idkOfThisSecond += dt;

		if ( idkOfThisSecond > 1 ) {
			idkOfThisSecond = 0;
			frameCounterForThisSecond = 0;
			nenemyAIoffset = Random.Range(-4f,4f);
			Debug.Log(nenemyAIoffset);
		}

		if ( Input.GetAxisRaw("Pause") != 0f )
			pause();

		// update point display
		pointsDisplay.text = points.x+"            "+points.y;

		// to make it easier to win a point, we check the movement before checking if we are hitting the left of the screen.
		// input movement
		player.velocity = Input.GetAxisRaw("Vertical")*paddleSpeed*(-1);

		// are we hitting the player paddle HORIZONTALLY? (aka are we in the horizontal area in which we could hit the paddle?)
		if ( transform.position.x - radius < player.position.x && velocity.x < 0 ) {
			// are we hitting it VERTICALLY? (aka are we in the vertical area in which we could hit the paddle?)
			if ( transform.position.y + radius > player.position.y - player.scale.y/2 &&
			     transform.position.y - radius < player.position.y + player.scale.y/2) {
				// then we invert velocity.
				velocity.x = 0 - velocity.x;

				// now is the paddle moving? move in its direction
				if ( player.velocity != 0 )
					velocity.y = player.velocity;

				// apply friction
				velocity.y *= restitution;
			}
		}
		// are we hitting the enemy paddle HORIZONTALLY? (aka are we in the horizontal area in which we could hit the paddle?)
		if ( transform.position.x + radius > nenemy.position.x && velocity.x > 0 ) {
			// are we hitting it VERTICALLY? (aka are we in the vertical area in which we could hit the paddle?)
			if ( transform.position.y + radius > nenemy.position.y - nenemy.scale.y/2 &&
			     transform.position.y - radius < nenemy.position.y + nenemy.scale.y/2) {
				// then we invert velocity.
				velocity.x = 0 - velocity.x;

				// now is the paddle moving? move in its direction
				if ( nenemy.velocity != 0 )
					velocity.y = nenemy.velocity;

				// apply friction
				velocity.y *= restitution;
			}
		}

		// are we hitting the top or bottom of the screen and have the apropriate velocity so that we dont get stuck to the top/bottom of the screen due to precision errors?
		if ( transform.position.y - radius < -camXY.transform.position.y && velocity.y > 0 ||
		     transform.position.y + radius >  camXY.transform.position.y && velocity.y < 0 ) {
			// then we invert velocity.
			velocity.y = 0 - velocity.y;
		}

		// are we hitting the left of the screen?
		if ( transform.position.x - radius < -camXY.transform.position.x ) {
			// then we lose a point.
			LosePoint();
		}
		// are we hitting the right of the screen?
		if ( transform.position.x + radius > camXY.transform.position.x ) {
			// then we gain a point.
			GainPoint();
		}

		// temp ai
		nenemy.velocity = velocity.y + nenemyAIoffset;

		// add velocity to objects
		if ( velocity.x < 0 ) {
			transform.Rotate(0,0,spriteRotateSpeed*dt);
		} else {
			transform.Rotate(0,0,spriteRotateSpeed*dt*-1);
		}
		transform.position = new Vector3(
			transform.position.x + velocity.x*dt,
			transform.position.y - velocity.y*dt,
			transform.position.z
		);

		player.position.y -= player.velocity*dt;
		nenemy.position.y -= nenemy.velocity*dt;
		player.go.transform.position = new Vector3(player.go.transform.position.x,player.position.y, player.go.transform.position.z);
		nenemy.go.transform.position = new Vector3(nenemy.go.transform.position.x,nenemy.position.y, nenemy.go.transform.position.z);
	}
}
