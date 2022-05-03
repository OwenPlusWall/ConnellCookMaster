using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salad : Grabbable
{
    public List<Sprite> possibleSprites;
    public List<Vegetable> ingredients;

    public List<SpriteRenderer> sprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CompareTo(Vegetable vegetable)
    {
        if (vegetable == null)
        {
            return 1;
        }
        else
        {
            return vegetable.id.CompareTo(vegetable.id);
        }
    }

    public void OrganizeSalad()
    {
        ingredients.Sort();
        for (int i = 0; i < ingredients.Count; i++)
        {
            sprites[i].sprite = possibleSprites[ingredients[i].id];
        }
    }

}
