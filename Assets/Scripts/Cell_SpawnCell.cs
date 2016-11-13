using UnityEngine;
using System.Collections;
using System;

public class Cell_SpawnCell : MonoBehaviour {

	public GameObject childPrefab;

	public GameObject CreateNewCell (Vector2 localOffset) {
		Vector2 worldOffset = transform.rotation * localOffset;
		Vector2 worldPosition = new Vector2 (transform.position.x, transform.position.y) + worldOffset; //bug here
		Quaternion newfacing = Quaternion.LookRotation (Vector3.forward, (Vector3) worldOffset);
		RaycastHit2D hit = Physics2D.Raycast (worldPosition, Vector2.zero, 0f);
		if (hit.collider == null) {
			GameObject newcell = PhotonNetwork.Instantiate("Cell", worldPosition, newfacing, 0);
			newcell.GetComponent<Cell> ().LinkJointsToNeighbors ();
			transform.GetComponent<Cell> ().LinkJointToNeighbor (newcell);
			return newcell;
		}
		return null;
	}
}
