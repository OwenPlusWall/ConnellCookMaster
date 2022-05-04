using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public List<Player> players; //The players in the game.
    public GameObject customerObject;
    public List<Tray> trayList;
    [HideInInspector] public int customers = 0;
    private List<HUD> playerHUDs; //The list of player HUD controllers in the game. There is one for each player.

    [Header("Results Screen")]
    public GameObject ResultsScreen;
    public Text WinningText;

    
    public void SpawnCustomer()
    {
        //If every tray does not already have a customer...
        if (customers < 5)
        {
            int randomTray = Random.Range(0, 5);
            if (trayList[randomTray].customer != null)
            {
                SpawnCustomer();
            }
            else
            {
                GameObject newCustomer = Instantiate(customerObject, new Vector2(trayList[randomTray].transform.position.x, trayList[randomTray].transform.position.y + 16), this.transform.rotation);
                trayList[randomTray].customer = newCustomer.GetComponent<Customer>();
                trayList[randomTray].customer.gm = this;
                trayList[randomTray].customer.tray = trayList[randomTray];
                customers += 1;
            }
        }

    }

    public void EndGame()
    {
        string EndText = "";
        if (players.Count > 1)
        {
            if (players[0].score > players[1].score)
            {
                EndText = "PLAYER 1 WINS!";
            }
            else if (players[1].score > players[0].score)
            {
                EndText = "PLAYER 2 WINS!";
            }
            else if (players[0].score == players[1].score)
            {
                EndText = "DRAW!";
            }
        }
        WinningText.text = EndText;
        ResultsScreen.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        playerHUDs = new List<HUD>();
        foreach (Player player in players)
        {
            playerHUDs.Add(player.playerHUD);
        }
        SpawnCustomer();
        SpawnCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count > 1)
        {
            if (players[0].time == 0 && players[1].time == 0)
            {
                foreach(Player player in players)
                {
                    player.immobile = true;
                    EndGame();
                }
            }
        }
    }
}
