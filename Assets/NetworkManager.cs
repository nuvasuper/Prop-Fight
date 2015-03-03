using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private string gameName = "Prop Fight";
	private string roomName = "Room 1";
	private int maxPlayers = 8;
	private HostData[] hostList;

	// Use this for initialization
	void Start () {
		//MasterServer.ipAddress = "127.0.0.1″;	
	}


	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)//if no network things are happening, provide some options
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))//option 1: host a server
				StartServer();
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))//option 2: check the host list
				RefreshHostList();
			
			if (hostList != null)//If there is a host or hosts, provide buttons to connect to them
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}

	private void RefreshHostList()//asks the master server for a list of our hosted games
	{
		Debug.Log ("Requesting Host List");
		MasterServer.RequestHostList(gameName);
	}
	void OnMasterServerEvent(MasterServerEvent msEvent)//if the master server responds, store the list of hosted games
	{
		if (msEvent == MasterServerEvent.HostListReceived) {
			Debug.Log ("Recieving Host List");
			hostList = MasterServer.PollHostList ();
		}
	}
	
	private void JoinServer(HostData hostData) //use to connect to a server with the given hostData
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()//If we connect, log it.
	{
		Debug.Log("Server Joined");
	}
	
	private void StartServer() {//To host a game, tell the master server what's up
		Network.InitializeServer(maxPlayers, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(roomName, gameName, "a game about props");
	}
	
	void OnServerInitialized()//If initializing the server worked, log it
	{
		Debug.Log("Server Initializied");
	}
}
