using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    private PhotoServerSystem m_PhotonServerSystem;
    [SerializeField] private GameObject m_PersonalBestPanel, m_LeaderBoard, m_MainMenu, m_Chat, m_InsideRoom;

    private void Start()
    {
        m_PhotonServerSystem = GetComponent<PhotoServerSystem>();
    }

    public void LoginButtonClicked()
    {
        m_PhotonServerSystem.ConnectToGame();
    }

    public void DisconnectButtonClicked()
    {
        m_PhotonServerSystem.DisconnectFromGame();
    }

    public void CreateRoomMenuButtonClicked()
    {
        m_PhotonServerSystem.CreateRoomMenuButton();
    }

    public void CreateRoomButtonClicked()
    {
        m_PhotonServerSystem.CreateRoom();
    }

    public void LeaveRoomClicked()
    {
        m_PhotonServerSystem.LeaveRoom();
    }

    public void LeaveLobbyClicked()
    {
        m_PhotonServerSystem.LeaveLobby();
    }

    public void ListRoomsClicked()
    {
        m_PhotonServerSystem.ListRooms();
    }

    public void JoinRandomRoomClicked()
    {
        m_PhotonServerSystem.JoinRandomRoom();
    }

    public void StartButtonClicked()
    {
        m_PhotonServerSystem.StartButton();
    }

    public void ExitButtonClicked()
    {
        m_PhotonServerSystem.ExitToSelectionPanel();
    }

    public void PersonalBestButtonClicked()
    {
        m_PersonalBestPanel.SetActive(true);
        m_MainMenu.SetActive(false);
    }

    public void PersonalBestExitButtonClicked()
    {
        m_PersonalBestPanel.SetActive(false);
        m_MainMenu.SetActive(true);
    }

    public void LeaderBoardButtonClicked()
    {
        m_LeaderBoard.SetActive(true);
        //m_MainMenu.SetActive(false);
    }

    public void LeaderBoardExitButtonClicked()
    {
        m_LeaderBoard.SetActive(false);
        //m_MainMenu.SetActive(true);
    }

    public void ChatExitButtonClicked()
    {
        m_Chat.SetActive(false);
        m_InsideRoom.SetActive(true);
    }

    public void ChattButtonClicked()
    {
        m_Chat.SetActive(true);
        m_InsideRoom.SetActive(false);
    }

}
