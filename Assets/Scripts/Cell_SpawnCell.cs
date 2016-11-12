using UnityEngine;
using System.Collections;
using System;

public class Cell_SpawnCell : MonoBehaviour {

	public GameObject childPrefab;

	public GameObject CreateNewCell (Vector2 localOffset) {
		Vector2 worldOffset = transform.rotation * localOffset;
		Vector2 worldPosition = new Vector2 (transform.position.x, transform.position.y) + worldOffset;
		Quaternion newfacing = Quaternion.LookRotation (Vector3.forward, (Vector3) worldOffset);
		GameObject newcell = PhotonNetwork.Instantiate("Cell", worldPosition, newfacing, 0);
		//GameObject newcell = Instantiate (cell, worldPosition, newfacing) as GameObject;

		if (newcell != null) {
			newcell.GetComponent<Cell> ().LinkJointsToNeighbors ();
			transform.GetComponent<Cell> ().LinkJointToNeighbor (newcell);
		}
		return newcell;
	}

}
