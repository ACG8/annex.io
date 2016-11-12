using UnityEngine;
using System.Collections;

public class Cell_MoveCell : MonoBehaviour {

	public float thrust = 1f;

	public void activate() {
		this.GetComponent<Rigidbody2D> ().AddRelativeForce (Vector2.up * thrust);
	}
}
