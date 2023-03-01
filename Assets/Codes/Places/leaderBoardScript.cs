using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class leaderBoardScript : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] GameObject playersImage;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;
    [SerializeField] List<List<TextMeshProUGUI>> listOfPlayers = new List<List<TextMeshProUGUI>>();

    List<RectTransform> ImagePos=new List<RectTransform>();

    public static leaderBoardScript Instance;
    bool warWasHappened;
    private void Awake()
    {
        Instance = this;
        warWasHappened = false;
    }

    public void boardUpdate(string name, int soldiers)
    {
        bool foundPlayer = false;
        if (listOfPlayers.Count > 0)
        {
            for (int i = 0; i < listOfPlayers.Count; i++)
            {
                if (listOfPlayers[i][0].text.Equals(name))
                {
                    if (soldiers >= 0)
                    {
                        listOfPlayers[i][1].text = soldiers + "";
                    }
                    else
                    {
                        if (playerName == name)
                        {
                            GameManagerScript.Instance.gameGoingFalse();
                            loseCanvas.SetActive(true);
                            listOfPlayers.Clear();
                            ImagePos.Clear();
                        }
                        else
                        {
                            listOfPlayers.RemoveAt(i);
                            Destroy(ImagePos[i].gameObject);
                            ImagePos.RemoveAt(i);
                        }
                    }
                    foundPlayer = true;
                }
            }
            if (!foundPlayer)
            {
                notFoundPlayer(name,soldiers);
            }
        }
        else
        {
            notFoundPlayer(name,soldiers);
        }
        boardLeaderUpdate();
    }

    void notFoundPlayer(string name, int soldiers)
    {
        List<TextMeshProUGUI> player = new List<TextMeshProUGUI>();
        GameObject obj = Instantiate(playersImage);
        obj.transform.parent = transform;
        ImagePos.Add(obj.GetComponent<RectTransform>());

        ImagePos[ImagePos.Count-1].anchoredPosition = new Vector3(380, 1200 - listOfPlayers.Count * 70, 0);
       
        TextMeshProUGUI nameOfPlayer = obj.transform.Find("playerName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI soldierCount = obj.transform.Find("armyCount").GetComponent<TextMeshProUGUI>();

        player.Add(nameOfPlayer);
        player.Add(soldierCount);

        listOfPlayers.Add(player);

        listOfPlayers[listOfPlayers.Count - 1][0].text = name;
        listOfPlayers[listOfPlayers.Count - 1][1].text = soldiers + "";
    }

    void boardLeaderUpdate()
    {
        if (listOfPlayers.Count > 0)
        {
            for (int i = 0; i < listOfPlayers.Count-1; i++)
            {
                int max = i;
                for (int j = i + 1; j < listOfPlayers.Count;j++)
                {
                    if (int.Parse(listOfPlayers[max][1].text) < int.Parse(listOfPlayers[j][1].text))
                    {
                        max = j;
                    }
                }

                var temporary = listOfPlayers[max];
                listOfPlayers[max] = listOfPlayers[i];
                listOfPlayers[i] = temporary;

                var temporaryPos = ImagePos[max];
                ImagePos[max] = ImagePos[i];
                ImagePos[i] = temporaryPos;

            }

            for (int i = 0; i < listOfPlayers.Count; i++)
            {
                ImagePos[i].anchoredPosition = new Vector3(380, 1200 - 70 * i, 0);
            }
        }
        
        if (listOfPlayers.Count > 1)
        {
            warWasHappened = true;
        }
        if (listOfPlayers.Count == 1 && listOfPlayers[0][0].text.Equals(playerName) && warWasHappened)
        {
            GameManagerScript.Instance.gameGoingFalse();
            winCanvas.SetActive(true);
            warWasHappened = false;
            listOfPlayers.Clear();
            ImagePos.Clear();
        }
    }
}
