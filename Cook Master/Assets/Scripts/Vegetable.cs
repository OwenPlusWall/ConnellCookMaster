using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : Grabbable, System.IComparable<Vegetable>
{
    public List<Sprite> spriteList;
    public int id = 0;
    private SpriteRenderer veggieSprite;

    public int CompareTo(Vegetable vegetable)
    {
        if (vegetable == null)
        {
            return 1;
        }
        else
        {
            return id.CompareTo(vegetable.id);
        }
    }

    public void SetSprite()
    {
        veggieSprite = this.GetComponent<SpriteRenderer>();
        veggieSprite.sprite = spriteList[id];
    }
}
