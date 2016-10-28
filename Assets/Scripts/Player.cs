using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {

	private Vector3 targetPos;
	public float movespeed = 5.0f;

	//Synchronization variables
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Only control my player object
		if (photonView.isMine)
			InputMovement ();
		else
			SyncedMovement ();
	}

	private void InputMovement() {
		// Update target location
		if (Input.GetMouseButton (1)) {
			targetPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			targetPos.z = transform.position.z;
		}
		//Move toward target location
		transform.position = Vector3.MoveTowards (transform.position, targetPos, movespeed * Time.deltaTime);
	}

	private void SyncedMovement() {
		syncTime += Time.deltaTime;
		transform.position = Vector3.Lerp (syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting)
			stream.SendNext (transform.position);
		else {
			syncEndPosition = (Vector3)stream.ReceiveNext ();
			syncStartPosition = transform.position;

			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
		}
	}
}