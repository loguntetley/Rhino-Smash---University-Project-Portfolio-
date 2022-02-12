using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UtilityFunctions : MonoBehaviourPunCallbacks
{
    [HideInInspector] public static string m_InputFieldDataName;

    public void DeleteChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateRoomList(List<RoomInfo> p_RoomList, Dictionary<string, RoomInfo> p_RoomListStorage)
    {
        foreach (var room in p_RoomList)
        {
            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
                p_RoomListStorage.Remove(room.Name);
            else
                p_RoomListStorage[room.Name] = room;
        }
    }

    public static void ReadInputFieldDataName(string p_InputFieldData)
    {
        m_InputFieldDataName = p_InputFieldData;
    }

    public static bool CheckStringisInt(string p_Input)
    {
        bool isNumber = int.TryParse(p_Input, out int n);
        return isNumber;
    }

    public static byte InputToByte(int p_Int)
    {
        return (byte)p_Int;
    }

}





