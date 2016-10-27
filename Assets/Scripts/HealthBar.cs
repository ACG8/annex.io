using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// =BUG= Healthbar moves around on screen as player moves. It should not move about at all.

public class HealthBar : MonoBehaviour {

	public float maxHealth = 10.0f;
	public float health;
	public Image currentHealthbar;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		//debug hp
		health -= Time.deltaTime;
		if (health < 0)
			health = maxHealth;
		updateHealthBar ();
	}

	void OnGUI() {
		//Vector2 targetPos;
		//targetPos = Camera.main.WorldToScreenPoint (transform.position);
		//GUI.Box(new Rect(targetPos.x, Screen.height - 5, 60, 20), health)
	}

	private void updateHealthBar() {
		float ratio = health / maxHealth;
		currentHealthbar.rectTransform.localScale = new Vector3 (ratio, 1, 1);
	}

	private void takeDamage(float damage) {
	
	}
}
