using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRoomOptions : MonoBehaviourPunCallbacks
{
    private static string m_InputFieldDataRoomName, m_InputFieldDataPlayerLimit, m_InputFieldDataAFKTimer;

    public static void ReadInputFieldDataRoomName(string p_InputFieldData)
    {
        m_InputFieldDataRoomName = p_InputFieldData;
    }

    public static void ReadInputFieldDataPlayerLimit(string p_InputFieldData)
    {
        m_InputFieldDataPlayerLimit = p_InputFieldData;
    }

    public static void ReadInputFieldDataAFKTimer(string p_InputFieldData)
    {
        m_InputFieldDataAFKTimer = p_InputFieldData;
    }

    private int InputResult(int p_Default, string p_Input)
    {
        if (UtilityFunctions.CheckStringisInt(p_Input) == true)
        {
            int resultInt = int.Parse(p_Input);
            return resultInt;
        }
        else
        {
            return p_Default;
        }
    }

    public RoomOptions GetRoomOptions()
    {
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = UtilityFunctions.InputToByte(InputResult(4, m_InputFieldDataPlayerLimit));
        roomOptions.PlayerTtl = UtilityFunctions.InputToByte(InputResult(10000, m_InputFieldDataAFKTimer));
        return roomOptions;
    }

    public string GetRoomName()
    {
        return m_InputFieldDataRoomName;
    }

}