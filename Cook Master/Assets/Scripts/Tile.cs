using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private BoxCollider2D collider; //The collider for the tile.
    public bool solid = false; //Whether a player can walk through this tile.
    public bool storage = false; //Whether a vegetable can be placed ONTO this tile by the player. A tile may receie a vegetable by other means.

    public Vegetable veggie; //The vegetable contained on this tile, if any.

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
