using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem current;

    public delegate void Events();
    public Events enemysEvent;
    public Events playerEvent;

    private void Awake()
    {
        current = this;
    }
    public void enemyNewLevel()
    {
        if(enemysEvent != null)
            enemysEvent();
    }
    public void playerNewLevel()
    {
        if (playerEvent != null)
        {
            playerEvent();
        }
    }
}
