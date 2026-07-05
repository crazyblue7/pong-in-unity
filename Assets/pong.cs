using UnityEngine;

public class pong : MonoBehaviour {
	struct rect {
		float x;
		float y;
		float width;
		float height;
	};
	struct paddle {
		GameObject go;   // paddle GameObject
		rect       rect; // paddle rect
	};
	public Vector2 position;
	public struct paddle player = {};
	public struct paddle enemy = {};
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		position = new Vector2(0,0);
	}

	// Update is called once per frame
	void Update() {
		position.x++;
		position.y++;
	}
}
