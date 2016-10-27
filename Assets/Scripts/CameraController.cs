using UnityEngine;
using System.Collections;

public class CameraController : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent <Camera> ().enabled = photonView.isMine;
	}
}
