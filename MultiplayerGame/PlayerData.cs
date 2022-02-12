using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string username, roomName, id;
    public int bestScore, totalPlayersInTheGame;
    public DateTime date;

    public PlayerData()
    {
        id = Guid.NewGuid().ToString();
    }
}
