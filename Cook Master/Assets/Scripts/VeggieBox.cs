using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggieBox : Table, ITile
{
    public GameObject Veggie;
    public int id;

    //Interfaces call this within Start().
    public new void Initialize()
    {
        SpawnItem();
    }

    public void Update()
    {
        if (item == null)
        {
            SpawnItem();
        }
    }

    public void SpawnItem()
    {
        GameObject newVeggie = GameObject.Instantiate(Veggie, this.transform);
        IGrabbable newItem = newVeggie.GetComponent<IGrabbable>();
        Vegetable vegetable = newVeggie.GetComponent<Vegetable>();
        vegetable.id = id;
        vegetable.SetSprite();
        item = newItem;
    }
}
