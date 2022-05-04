using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : Tile, ITile
{
    public new void Interact(Player player)
    {
        //IF the player is carrying any vegetables, get rid of the first one.
        if (player.vegetables.Count > 0)
        {
            Destroy(player.vegetables[0].gameObject);
            player.vegetables.RemoveAt(0);
            player.playerHUD.UpdateInventory();
            player.UpdateScore(-200);
        }
        //If the player is carrying a salad, get rid of it.
        else if (player.salad != null)
        {
            Destroy(player.salad.gameObject);
            player.salad = null;
            player.playerHUD.UpdateInventory();
            player.UpdateScore(-200);
        }
    }
}
