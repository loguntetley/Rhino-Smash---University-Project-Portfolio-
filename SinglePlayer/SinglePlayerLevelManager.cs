using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerLevelManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.OfflineMode = true;
        PhotonNetwork.LocalPlayer.NickName = "SinglePlayer";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.CreateRoom(null);
        


    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master server in OfflineMode");
        
    }
}
