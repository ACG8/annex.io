using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	private bool active = false;
	private float health = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (active) {
			Cell_MoveCell moveCell = GetComponent<Cell_MoveCell> ();
			//activate whichever cell behaviors are attached
			if (moveCell != null)
				moveCell.activate ();
		}
	}

	public void toggleActive() {
		active = !active; 
	}

	public void LinkJointToNeighbor ( GameObject neighbor ) {
		FixedJoint2D joint = transform.gameObject.AddComponent<FixedJoint2D> ();
		joint.connectedBody = neighbor.GetComponent<Rigidbody2D> ();
		Debug.Log("From " + joint.transform.gameObject + " to " + neighbor);
	}

	GameObject GetNeighborAtPosition(Vector2 pos) {
		Vector2 worldOffset = transform.rotation * pos;
		Vector2 worldPosition = new Vector2 (transform.position.x, transform.position.y) + worldOffset;

		RaycastHit2D hit = Physics2D.Raycast (worldPosition, Vector2.zero, 0f);

		if (hit)
			return hit.transform.gameObject;
		else
			return null;
	}

	public void LinkJointsToNeighbors () {
		Vector2 rt_pos = new Vector2 (1f, 0f);
		Vector2 lt_pos = new Vector2 (-1f, 0f);
		Vector2 up_pos = new Vector2 (0f, 1f);
		Vector2 dn_pos = new Vector2 (0f, -1f);

		if (GetNeighborAtPosition (rt_pos) != null)
			LinkJointToNeighbor ( GetNeighborAtPosition (rt_pos) );
		if (GetNeighborAtPosition (lt_pos) != null)
			LinkJointToNeighbor ( GetNeighborAtPosition (lt_pos) );
		if (GetNeighborAtPosition (up_pos) != null)
			LinkJointToNeighbor ( GetNeighborAtPosition (up_pos) );
		if (GetNeighborAtPosition (dn_pos) != null) {
			LinkJointToNeighbor ( GetNeighborAtPosition (dn_pos) );
		}
	}
}
