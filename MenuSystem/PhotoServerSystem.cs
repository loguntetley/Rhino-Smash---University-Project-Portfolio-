using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoServerSystem : MonoBehaviourPunCallbacks
{
    private string m_PlayerName;
    [SerializeField] private InputField m_PlayerNameInputField;
    [SerializeField] private GameObject m_StartGameButton, m_TextPrefab;
    [SerializeField] private Transform m_InsideRoomPanelContent, m_ListRoomPanelContent, m_RoomEntryPrefab;
    private Dictionary<string, RoomInfo> m_RoomList;

    private UtilityFunctions m_UtilityFunctions;
    private PanelSystem m_PanelSystem;

    [Header("Chat Panel")]
    public Chat m_Chat;
    public Transform m_ChatPanel;
    

    private void Start()  
    {
        m_UtilityFunctions = GetComponent<UtilityFunctions>();
        m_PanelSystem = GetComponent<PanelSystem>();
        m_PlayerNameInputField.text = "Player" + UnityEngine.Random.Range(1, 10000);
        m_PlayerName = m_PlayerNameInputField.text;
        m_RoomList = new Dictionary<string, RoomInfo>();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void ConnectToGame()
    {
        PhotonNetwork.GameVersion = "0.0.1";

        if (UtilityFunctions.m_InputFieldDataName != null)
        {
            m_PlayerName = UtilityFunctions.m_InputFieldDataName;
        }

        PhotonNetwork.LocalPlayer.NickName = m_PlayerName;
        PhotonNetwork.ConnectUsingSettings();
        UpdateLeaderboardUsername();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master server");
        m_PanelSystem.ActivatePanel(m_PanelSystem.m_Panels[(int)PanelSystem.e_Panels.SelectionPanel].name);
    }

    public void DisconnectFromGame()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from Master server");
        m_PanelSystem.ActivatePanel(m_PanelSystem.m_Panels[(int)PanelSystem.e_Panels.LoginPanel].name);
    }

    public void CreateRoomMenuButton()
    {
        m_PanelSystem.ActivatePanel(m_PanelSystem.m_Panels[(int)PanelSystem.e_Panels.CreateRoomPanel].name);
    }

    public void ExitToSelectionPanel()
    {
        m_PanelSystem.ActivatePanel(m_PanelSystem.m_Panels[(int)PanelSystem.e_Panels.SelectionPanel].name);
    }

    public void CreateRoom()
    {
        GameRoomOptions GRO = new GameRoomOptions();
        PhotonNetwork.CreateRoom(GRO.GetRoomName(), GRO.GetRoomOptions());
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");

        m_Chat.m_Username = PhotonNetwork.LocalPlayer.NickName;
        var authenticationValues = new Photon.Chat.AuthenticationValues(m_Chat.m_Username);
        m_Chat.m_ChatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "0.0.1", authenticationValues);

        m_PanelSystem.ActivatePanel(m_PanelSystem.m_Panels[(int)PanelSystem.e_Panels.InsideRoomPanel].name);
        m_StartGameButton.SetActive(PhotonNetwork.IsMasterClient);

        foreach (var player in PhotonNetwork.PlayerList)
        {
            var newPlayerRoomEntry = Instantiate(m_TextPrefab, m_InsideRoomPanelContent);
            newPlayerRoomEntry.GetComponent<Text>().text = player.NickName;
            newPlayerRoomEntry.name = player.NickName;
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Room left");
        m_Chat.m_ChatClient.Disconnect();
        m_UtilityFunctions.DeleteChildren(m_InsideRoomPanelContent);
        m_PanelSystem.ActivatePanel(m_PanelSystem.m_Panels[(int)PanelSystem.e_Panels.SelectionPanel].name);
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Lobby left");
        m_UtilityFunctions.DeleteChildren(m_ListRoomPanelContent);
        m_RoomList.Clear();
        m_PanelSystem.ActivatePanel(m_PanelSystem.m_Panels[(int)PanelSystem.e_Panels.SelectionPanel].name);
    }

    public void ListRooms()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby joined");
        m_PanelSystem.ActivatePanel(m_PanelSystem.m_Panels[(int)PanelSystem.e_Panels.ListRoomPanel].name);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room update: " + roomList.Count);
        m_UtilityFunctions.DeleteChildren(m_ListRoomPanelContent);
        m_UtilityFunctions.UpdateRoomList(roomList, m_RoomList);

        foreach (var room in m_RoomList)
        {
            var newRoomEntry = Instantiate(m_RoomEntryPrefab, m_ListRoomPanelContent);
            var newRoomEntryScript = newRoomEntry.GetComponent<RoomEntry>();
            newRoomEntryScript.m_RoomName = room.Key;
            newRoomEntryScript.m_RoomText.text = $"[{room.Key}] - ({room.Value.PlayerCount} / {room.Value.MaxPlayers})";
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("Player entered room");
        var newPlayerRoomEntry = Instantiate(m_TextPrefab, m_InsideRoomPanelContent);
        newPlayerRoomEntry.GetComponent<Text>().text = newPlayer.NickName;
        newPlayerRoomEntry.name = newPlayer.NickName;
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("Player left room");
        foreach (Transform child in m_InsideRoomPanelContent)
        {
            if (child.name == otherPlayer.NickName)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join random room failed: " + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Join room failed: " + message);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        Debug.Log("Master client switched");
        m_StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void StartButton()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        PhotonNetwork.LoadLevel("Multiplayer");
    }

    private void UpdateLeaderboardUsername()
    {
        var request = new UpdateUserTitleDisplayNameRequest()
        {
            DisplayName = m_PlayerName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, 
            (result) => { Debug.Log("PlayFab - Get update user title completed!"); },
            (error) => { Debug.Log("PlayFab - Error occured while retrieving the title: " + error.ErrorMessage); });
    }

}
