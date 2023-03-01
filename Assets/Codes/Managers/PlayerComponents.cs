using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public static PlayerComponents Instance;

    [SerializeField] List<GameObject> playersList=new List<GameObject>();
    
    List<CaptainOrder> captains = new List<CaptainOrder>();
    List<Color> colors = new List<Color>();
    void Awake()
    {
        Instance = this;
    }

    public void joinPlayer(GameObject go)
    {
        playersList.Add(go);
        captains.Add(go.GetComponent<CaptainOrder>());
        colors.Add(go.transform.Find("mesh").GetComponent<SkinnedMeshRenderer>().material.color);
    }
    public void removePlayer(string nameOfPlayer)
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            if (playersList[i].name == nameOfPlayer)
            {
                playersList.RemoveAt(i);
                captains.RemoveAt(i);
                colors.RemoveAt(i);
            }
        }
    }

    public CaptainOrder getCaptain(string nameOfPlayer)
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            if (playersList[i].transform.parent.name == nameOfPlayer)
            {
                return captains[i];
            }
        }
        return null;
    }
    public Color getColor(string nameOfPlayer)
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            if (playersList[i].transform.parent.name == nameOfPlayer)
            {
                return colors[i];
            }
        }
        return Color.black;
    }
}
