using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionHandler : MonoBehaviourPunCallbacks
{

    public GameObject VRRig;
    public float charactersOffset = 2f;

    public bool connectOnStart = true;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (connectOnStart)
        {
            Connect();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        Debug.Log("Am I the master: " + PhotonNetwork.IsMasterClient);

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom("Room1", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("On Room Joined");
        PhotonNetwork.LoadLevel("Arena");     
      

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "Arena")
        {
            var numPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            PhotonNetwork.Instantiate(VRRig.name, new Vector3(0,1.7f,numPlayers * charactersOffset), Quaternion.identity);
        }
        
    }
}