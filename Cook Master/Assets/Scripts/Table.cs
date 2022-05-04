using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Tile, ITile
{
    //Place an item onto the table, or take an item off the table.
    public new void Interact(Player player)
    {
        //If this table has no item on it currently...
        if (item == null && storage)
        {
            if (player.salad && player.vegetables.Count == 0)
            {
                item = player.salad;
                player.salad = null;
                item.gameObject.transform.position = this.transform.position;
                item.gameObject.SetActive(true);
                player.playerHUD.UpdateInventory();
            }
            else if (player.vegetables.Count > 0)
            {
                item = player.vegetables[0];
                player.vegetables.RemoveAt(0);
                item.gameObject.transform.position = this.transform.position;
                item.gameObject.SetActive(true);
                player.playerHUD.UpdateInventory();
            }
        }
        else if (item != null)
        {
            Salad pickUpSalad = item.gameObject.GetComponent<Salad>();

            if (player.salad == null && pickUpSalad && player.vegetables.Count == 0)
            {
                player.salad = pickUpSalad;
                item.gameObject.SetActive(false);
                item = null;
                player.playerHUD.UpdateInventory();
            }
            else if (player.vegetables.Count < 2 && player.salad == null)
            {
                player.vegetables.Add(item.gameObject.GetComponent<Vegetable>());
                item.gameObject.SetActive(false);
                item = null;
                player.playerHUD.UpdateInventory();
            }
        }

    }
}
