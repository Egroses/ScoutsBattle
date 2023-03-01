using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;

    bool gameGoing = false;
    bool GameStartWaiting = false;
    private void Awake()
    {
        Instance = this;
    }

    public void gameGoingTrue()
    {
        gameGoing = true;
    }
    public void gameGoingFalse()
    {
        gameGoing = false;
    }
    public bool gameSituation()
    {
        return gameGoing;
    }

    public void gameWaitingTrue()
    {
        GameStartWaiting = true;
    }
    public void gameWaitingFalse()
    {
        GameStartWaiting = false;
    }
    public bool gameWaitSituation()
    {
        return GameStartWaiting;
    }
}
