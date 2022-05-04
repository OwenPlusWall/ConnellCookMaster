using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("Inspector Variables")]
    public Sprite angrySprite;
    public List<Sprite> ingredientSprites; //The list of potential sprites that can be used to represent the customer's order (salad ingredient sprites).
    public List<SpriteRenderer> saladLayers; //The sprites that will be used to display the customer's order on-screen.
    public GameObject patienceGauge; //The patience gauge, which decreases over time. Once it's empty, the customer leaves.


    [Header("Runtime Variables")]
    private SpriteRenderer sr;
    [HideInInspector] public GameManager gm;
    [HideInInspector] public Tray tray;
    [HideInInspector] public List<Vegetable> order;
    [HideInInspector] public List<Player> targets; //If the customer becomes angry, they will only deduct points from the players that angered them when they leave.
    public bool angry = false;
    private float patience; //How long the customer will wait before leaving.
    private float waitTimer;
    private int waitMultiplier = 1; //This value doubles if the customer gets angry.
    private int pointValue = 0; //Determines how many points a player gets/loses from the customer when they do/don't feed them.



    // Start is called before the first frame update
    void Start()
    {
        targets = new List<Player>();
        sr = this.GetComponent<SpriteRenderer>();

        GenerateOrder();
    }

    //What the customer wants is generated when it first spawns.
    void GenerateOrder()
    {
        patience = 15f;
        order = new List<Vegetable>();
        int orderLength = Random.Range(1, 4); //The order will have between 1 and 3 ingredients.
        for (int i = 0; i < orderLength; i++)
        {
            Vegetable veggie = this.gameObject.AddComponent<Vegetable>();
            veggie.id = Random.Range(0, 6);
            order.Add(veggie);
            pointValue += 500; //Each ingredient will add/take 500 points, based on the customer's outcome.
            patience += 10f;
        }
        order.Sort();
        for (int i = 0; i < order.Count; i++)
        {
            saladLayers[i].sprite = ingredientSprites[order[i].id];
        }
        waitTimer = patience;
    }

    //Called if a player gives the customer an incorrect order.
    void Anger(Player player)
    {
        if (angry == false)
        {
            sr.sprite = angrySprite;
            waitMultiplier = 2;
            angry = true;
        }
        if (!targets.Contains(player))
        {
            targets.Add(player);
        }
    }

    //Called when a customer's patience expires.
    void LeaveUnhappy()
    {
        //If the customer wasn't angry at anyone, deduct points from both players.
        if (targets.Count == 0)
        {
            foreach(Player player in gm.players)
            {
                targets.Add(player);
            }
        }

        //If the customer was angry, triple the amount of points deducted from the player(s).
        int scoreMultiplier = 1;
        if (angry)
        {
            scoreMultiplier = 3;
        }

        //Deduct the points from the player(s).
        foreach (Player player in targets)
        {
            player.UpdateScore(-pointValue * scoreMultiplier);
        }

        //Spawn a new customer at a different tray, and get rid of this customer.
        gm.SpawnCustomer();
        tray.customer = null;
        gm.customers -= 1;
        Destroy(this.gameObject);
    }

    void LeaveHappy(Player player)
    {
        //Remove the player's salad and add to their score.
        player.UpdateScore(pointValue);
        Destroy(player.salad.gameObject);
        player.salad = null;
        player.playerHUD.UpdateInventory();

        //Afterwards, get rid of the customer, and spawn a new one at a different tray.
        gm.SpawnCustomer();
        tray.customer = null;
        gm.customers -= 1;
        Destroy(this.gameObject);
    }

    //Called when the player interacts with the customer's tray with a salad.
    //If the salad has all the correct ingredients, the customer leaves happily and the player gets points.
    //If the salad doesn't have the correct ingredients, the customer gets angry.
    public void GetOrder(Player player)
    {
        if (player.salad.ingredients.Count == order.Count)
        {
            bool allCorrect = true;
            for (int i = 0; i < order.Count; i++)
            {
                if (player.salad.ingredients[i].id != order[i].id)
                {
                    allCorrect = false;
                }
            }
            if (allCorrect)
            {
                LeaveHappy(player);
            }
            else
            {
                Anger(player);
            }

        }
        else
        {
            Anger(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        patienceGauge.transform.localScale = new Vector2(waitTimer/patience, patienceGauge.transform.localScale.y);

        waitTimer -= Time.deltaTime * waitMultiplier;
        if (waitTimer <= 0)
        {
            LeaveUnhappy();
        }
    }
}
