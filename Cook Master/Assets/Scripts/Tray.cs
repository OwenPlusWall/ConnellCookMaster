using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : Tile, ITile
{
    public Customer customer;

    public new void Interact(Player player)
    {
        if (player.vegetables.Count == 0 && player.salad && customer)
        {
            customer.GetOrder(player);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
