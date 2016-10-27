using UnityEngine;
//using System.Collections;
using System;

public class NetworkManager : MonoBehaviour {

	private const string roomName = "RoomName";
	private TypedLobby lobbyname = new TypedLobby ("North America", LobbyType.Default);
	private RoomInfo[] roomsList;
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("0.1");
	}

	void OnGUI() {
		if (!PhotonNetwork.connected) {
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		} else if (PhotonNetwork.room == null) {
			//Create Room
			if (GUI.Button (new Rect (100, 100, 250, 100), "Start Server"))
				PhotonNetwork.CreateRoom (
					roomName + System.Guid.NewGuid ().ToString ("N"), 
					new RoomOptions() { MaxPlayers = 5, IsOpen = true, IsVisible = true }, 
					lobbyname
				);
			//Join Room
			if (roomsList != null) {
				for (int i=0; i < roomsList.Length; i++) {
					if (GUI.Button (new Rect (100, 250 + (110 * i), 250, 100), "Join " + roomsList [i].name))
						PhotonNetwork.JoinRoom (roomsList [i].name);
				}
			}
		}
	}

	void OnConnectedToMaster() {
		PhotonNetwork.JoinLobby (lobbyname);
	}

	void OnReceivedRoomListUpdate() {
		roomsList = PhotonNetwork.GetRoomList ();
	}

	void OnJoinedRoom() {
		Debug.Log ("Connected to Room");
		// Spawn Player
		PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
