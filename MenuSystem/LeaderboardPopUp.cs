using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardPopUp : MonoBehaviour
{
    public GameObject m_ScoreHolder;
    public GameObject m_NoScoreText;
    public GameObject m_LeaderboardItem;
    UtilityFunctions m_UtilityFunctions = new UtilityFunctions();

    private void OnEnable()
    {
        GameManager.instance.GlobalLeaderboard.GetLeaderboard(); /////////////////
    }

    public void UpdateUI(List<PlayerLeaderboardEntry> p_ScoreList)
    {
        if (p_ScoreList.Count >= 0)
        {
            m_UtilityFunctions.DeleteChildren(m_ScoreHolder.transform);
            for (int i = 0; i < p_ScoreList.Count; i++)
            {
                var newLeaderboardItem = Instantiate(m_LeaderboardItem, Vector3.zero, Quaternion.identity, m_ScoreHolder.transform);
                newLeaderboardItem.GetComponent<LeaderBoardItem>().SetValues(i + 1, p_ScoreList[i].DisplayName, p_ScoreList[i].StatValue);
            }

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
