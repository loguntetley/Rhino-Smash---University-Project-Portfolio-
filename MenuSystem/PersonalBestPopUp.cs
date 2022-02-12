using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalBestPopUp : MonoBehaviour
{

    public GameObject m_ScoreHolder, m_NoScoreText;
    public Text m_UserName, m_BestScore, m_Date, m_TotalPlayers, m_RoomName;

    private void OnEnable()
    {
        GameManager.instance.GlobalLeaderboard.GetLeaderboard();
        UpdatePersonalBestUI();
    }

    public void UpdatePersonalBestUI ()
    {
        var playerData = GameManager.instance.playerData;
        if (playerData.username != null)
        {
            m_UserName.text = playerData.username;
            m_BestScore.text = playerData.bestScore.ToString();
            m_Date.text = playerData.date.ToString();
            m_TotalPlayers.text = playerData.totalPlayersInTheGame.ToString();
            m_RoomName.text = playerData.roomName;

            m_ScoreHolder.SetActive(true);
            m_NoScoreText.SetActive(false);
        }
        else
        {
            m_ScoreHolder.SetActive(false);
            m_NoScoreText.SetActive(true);
        }
    }
}
