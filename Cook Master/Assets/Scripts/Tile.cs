using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
    void Initialize();
    void Interact(Player player);
    void SpawnItem(IGrabbable item);
    bool GetSolidity();

    GameObject gameObject { get; }
}


public class Tile : MonoBehaviour, ITile
{

    private BoxCollider2D bcollider; //The collider for the tile.
    public bool solid = false; //Whether a player can walk through this tile.
    public bool storage = false; //Whether a vegetable can be placed ONTO this tile by the player. A tile may receie a vegetable by other means.

    public IGrabbable item; //The item contained on this tile, if any.

    // Start is called before the first frame update
    void Start()
    {
        bcollider = GetComponent<BoxCollider2D>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Interfaces call this within Start().
    public void Initialize()
    {
        
    }
    //If a tile can be interacted with, this is what is called.
    public void Interact(Player player)
    {
        //Nothing happens when a player tries to interact with a tile by default.
        //Other types of tiles can add functionality for this, however.
    }

    public void SpawnItem(IGrabbable item)
    {
        GameObject newItem = (item.gameObject);
        newItem.transform.position = this.transform.position;
        newItem.SetActive(true);
    }

    public bool GetSolidity()
    {
        return solid;
    }
}
