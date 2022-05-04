using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : Tile, ITile
{
    public GameObject saladObject;
    public float cutTime;
    public float chopSpeed;
    private float cutTimer;
    public bool interacting = false;

    private Player choppingPlayer;
    private Salad newSalad;


    public new void Interact(Player player)
    {
        //If another player is not already interacting with the cutting board...
        if (!interacting)
        {
            //If the player is carrying any vegetables when they interact with the cutting board...
            if (player.vegetables.Count > 0 && !player.salad)
            {
                //If nothing is on the cutting board, create a new salad.
                if (item == null)
                {
                    GameObject newSaladObject = Instantiate(saladObject, this.transform);
                    item = newSaladObject.GetComponent<IGrabbable>();
                    newSalad = newSaladObject.GetComponent<Salad>();
                    newSalad.ingredients = new List<Vegetable>();
                }
                //If the salad does not already contain the maximum number of ingredients, AND does not already contain the ingredient the player is holding...
                if (newSalad.ingredients.Count < 3 && !newSalad.ContainsIngredient(player.vegetables[0].id))
                {
                    StartCutting(player);
                }
            }
            //If the player DOESN'T have any vegetables when they interact with the cutting board...
            else if (player.vegetables.Count == 0)
            {
                //If the player isn't currently carrying a salad, and there is a salad on this cutting board...
                if (!player.salad && item != null)
                {
                    player.salad = newSalad; //Give the salad to the player.
                    newSalad.gameObject.SetActive(false); //Hide the salad from view.
                    newSalad = null; //The salad is no longer on the cutting board, so remove it.
                    item = null; //Nothing is on the cutting board, so item gets return to null as well.
                    player.playerHUD.UpdateInventory(); //Update the player's HUD to match.
                }
                //If the player is carrying a salad while the cutting board is empty...
                else if (player.salad && item == null)
                {
                    newSalad = player.salad;
                    newSalad.transform.position = this.transform.position;
                    item = newSalad.GetComponent<IGrabbable>();
                    newSalad.gameObject.SetActive(true);
                    player.salad = null;
                    player.playerHUD.UpdateInventory(); //Update the player's HUD to match.
                }
            }
        }

    }

    //Makes the player start cutting the first vegetable they are carrying into a salad on the cutting board.
    void StartCutting(Player player)
    {
        choppingPlayer = player; //Identify the player using the cutting board.
        interacting = true; //Stop the other player from interacting with this cutting board.
        choppingPlayer.cutting = true; //Stop the player from making any other actions when using the cutting board.
        chopSpeed = choppingPlayer.chopSpeed; //Set the speed at which the cutTimer depletes from the player's current status.
        cutTimer = cutTime; //Restart the cutTimer.
    }

    //Adds a new ingredient to the salad on the cutting board.
    void AddIngredient()
    {
        newSalad.ingredients.Add(choppingPlayer.vegetables[0]); //Add the player's first vegetable to the salad on the cutting board.
        //choppingPlayer.vegetables[0].gameObject.SetActive(false);
        choppingPlayer.vegetables.RemoveAt(0); //Remove the first vegetable from the player's inventory.
        newSalad.OrganizeSalad(); //Organize the sprite order on the salad.
        choppingPlayer.cutting = false; //Let the player move again.
        interacting = false; //Let the player(s) interact with the cutting board again.
        choppingPlayer.playerHUD.UpdateInventory(); //Update the player's HUD to match.
    }

    // Update is called once per frame
    void Update()
    {
        //If a player is interacting with this cutting board...
        if (interacting)
        {
            //Decrease the cutTimer, and add the ingredient to the salad on the cutting board when time expires.
            if (cutTimer > 0)
            {
                cutTimer -= chopSpeed * Time.deltaTime;
            }
            else
            {
                AddIngredient();
            }
        }
    }
}
