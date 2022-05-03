using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : Grabbable
{
    public List<Sprite> spriteList;
    public int id = 0;
    private SpriteRenderer veggieSprite;


    public void SetSprite()
    {
        veggieSprite = this.GetComponent<SpriteRenderer>();
        veggieSprite.sprite = spriteList[id];
    }
}
