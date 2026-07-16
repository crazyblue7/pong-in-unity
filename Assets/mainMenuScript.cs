using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
	void Update() {}
	void Start()  {}
	public void play() {
		Debug.Log("Wok");
		SceneManager.LoadScene("game");
	}
	public void quit() {
		Application.Quit();
	}
}
