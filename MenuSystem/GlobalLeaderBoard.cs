using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLeaderBoard : MonoBehaviour
{
    public int MaxResult = 5;
    public LeaderboardPopUp m_LeaderboardPopUp;

    public void SubmitScore(int p_PlayerScore)
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate()
                {
                    StatisticName = "Most Kills",
                    Value = p_PlayerScore,
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, 
            (result) => { Debug.Log("PlayFab - Score submitted!"); }, 
            (error) => { Debug.Log("PlayFab - Error occured while submitting the score: " + error.ErrorMessage); });
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest()
        {
            MaxResultsCount = MaxResult,
            StatisticName = "Most Kills"
        };
        PlayFabClientAPI.GetLeaderboard(request, 
            (result) => { Debug.Log("PlayFab - Get leaderboard completed!"); m_LeaderboardPopUp.UpdateUI(result.Leaderboard); }, 
            (error) => { Debug.Log("PlayFab - Error occured while retrieving the score: " + error.ErrorMessage); });
    }

}
