using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private GameObject selectedCell = null;
	
	// Update is called once per frame
	void Update () {
		if (Camera.main != null)
			inputCommands ();
	}

	private void inputCommands() {
		

		//Right clicking on a cell you control toggles its activity
		if (Input.GetMouseButtonDown (1)) {
			// Toggle cell behavior depending on attached scripts
			GameObject target = ClickSelectCell ();
			//take behavior based on scripts
			if (target != null && target.GetPhotonView().isMine)
				target.GetComponent<Cell> ().toggleActive ();
		}

		//Dragging on a cell you control spawns a new one
		if (Input.GetMouseButtonDown (0)) {
			GameObject target = ClickSelectCell ();
			if (target != null && target.GetPhotonView ().isMine)
				Debug.Log ("target: " + target);
				selectedCell = target;
		}
		if (Input.GetMouseButtonUp (0) ) {
			Debug.Log ("Selected cell: " + selectedCell);
			if (selectedCell != null && selectedCell.GetPhotonView().isMine) {
				Vector2 mousePos = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
				Vector2 displacement = selectedCell.transform.InverseTransformPoint (mousePos);

				if (displacement.magnitude > 0.5) {
					Vector2 unitVector = displacement / displacement.magnitude;
					Vector2 gridVector = new Vector2 (Mathf.Round (unitVector.x), Mathf.Round (unitVector.y));
					Cell_SpawnCell spawnscript = selectedCell.GetComponent<Cell_SpawnCell> ();
					if (spawnscript != null) {
						Debug.Log ("creating cell at " + gridVector);
						spawnscript.CreateNewCell (gridVector);
					}
				}
			}
		}


	}

	// Method for selecting a cell by clicking
	GameObject ClickSelectCell() {
		Debug.Log ("finding raypos");
		Vector2 rayPos = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Debug.Log (rayPos);
		RaycastHit2D hit = Physics2D.Raycast (rayPos, Vector2.zero, 0f);

		if (hit != null && hit.transform.GetComponent<Cell>() != null)
			return hit.collider.transform.gameObject;
		else
			return null;
	}
}
