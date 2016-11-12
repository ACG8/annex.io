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
				selectedCell = target;
		}
		if (Input.GetMouseButtonUp (0) ) {
			if (selectedCell != null && selectedCell.GetPhotonView().isMine) {
				Vector2 mousePos = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
				Vector2 localDisplacement = selectedCell.transform.InverseTransformPoint (mousePos);

				if (localDisplacement.magnitude > 0.5) {
					Vector2 unitVector = localDisplacement / localDisplacement.magnitude;
					//round to nearest 60 degrees
					float angle = Vector2.Angle(selectedCell.transform.up, localDisplacement);
					if (localDisplacement.x < 0)
						angle = -angle;
					angle = Mathf.Deg2Rad * Mathf.Round (angle / 60) * 60;
					Debug.Log ("angle: " + angle);
					Vector2 gridVector = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle));
					Debug.Log ("gridvector: " + gridVector);
					//Vector2 gridVector = new Vector2 (Mathf.Round (unitVector.x), Mathf.Round (unitVector.y));
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
		Vector2 rayPos = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		RaycastHit2D hit = Physics2D.Raycast (rayPos, Vector2.zero, 0f);

		if (hit != null && hit.transform.GetComponent<Cell>() != null)
			return hit.collider.transform.gameObject;
		else
			return null;
	}
}
