using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerLevelManager : MonoBehaviourPunCallbacks
{
    [SerializeField] int maxKills = 3;
    [SerializeField] GameObject gamePopUp;
    [SerializeField] Text winnerText;


    private void Start()              
    {
        PhotonNetwork.Instantiate("Multiplayer", new Vector3(0, 0, 0), Quaternion.identity);
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer.GetScore() == maxKills)
        {
            winnerText.text = targetPlayer.NickName;
            gamePopUp.SetActive(true);
            StorePersonalBest();
        }
    }

    private void StorePersonalBest()
    {
        var currentScore = PhotonNetwork.LocalPlayer.GetScore();
        var playerData = GameManager.instance.playerData;
        if (currentScore > playerData.bestScore)
        {
            playerData.username = PhotonNetwork.LocalPlayer.NickName;
            playerData.bestScore = currentScore;
            playerData.date = DateTime.UtcNow;
            playerData.totalPlayersInTheGame = PhotonNetwork.CurrentRoom.PlayerCount;
            playerData.roomName = PhotonNetwork.CurrentRoom.Name;

            GameManager.instance.GlobalLeaderboard.SubmitScore(currentScore); /////////////////////
            GameManager.instance.SavePlayerData();
        }
    }

    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("MultiplayerLobby");
    }
}
