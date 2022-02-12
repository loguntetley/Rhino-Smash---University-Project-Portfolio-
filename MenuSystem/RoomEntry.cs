using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomEntry : MonoBehaviour
{
    public Text m_RoomText;
    public string m_RoomName;

    public void JoinRoom()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.JoinRoom(m_RoomName);
    }
}
