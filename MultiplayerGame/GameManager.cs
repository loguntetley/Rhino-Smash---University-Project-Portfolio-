using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerData playerData;
    public string FilePath;
    public GlobalLeaderBoard GlobalLeaderboard;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        LoadPlayerData();
        LoginToPlayFab();
    }

    private void LoginToPlayFab()
    {
        var request = new LoginWithCustomIDRequest()
        {
            CreateAccount = true,
            CustomId = playerData.id,
        };
        PlayFabClientAPI.LoginWithCustomID(request, PlayFabLoginResult, PlayFabLoginError);
    }

    private void PlayFabLoginResult(LoginResult loginResult)
    {
        Debug.Log("PlayFab - Logged in: " + loginResult.ToJson());
    }

    private void PlayFabLoginError(PlayFabError playFabError)
    {
        Debug.Log("PlayFab - Login false: " + playFabError.ErrorMessage);

    }

    public void SavePlayerData()
    {
        var serializedData = JsonConvert.SerializeObject(playerData);
        File.WriteAllBytes(FilePath, AESEcryption.Encrypt(serializedData)); //File.WriteAllText(FilePath, serializedData);
    }

    public void LoadPlayerData()
    {
        if (!File.Exists(FilePath))
        {
            playerData = new PlayerData();
            SavePlayerData();
        }
        var fileContents = File.ReadAllText(FilePath);
        playerData = JsonConvert.DeserializeObject<PlayerData>(AESEcryption.Decrypt(File.ReadAllBytes(FilePath))); //JsonConvert.DeserializeObject<PlayerData>(fileContents);
    }
}