using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salad : Grabbable
{
    public List<Sprite> possibleSprites;
    public List<Vegetable> ingredients;

    public List<SpriteRenderer> sprites;

    //Checks to see if there is already an ingredient of the specified id in the salad.
    //Returns "true" if it contains that ingredient, and "false" if it doesn't.
    public bool ContainsIngredient(int id)
    {
        foreach (Vegetable vegetable in ingredients)
        {
            if (vegetable.id == id)
            {
                return true;
            }
        }
        return false;
    }

    public void OrganizeSalad()
    {
        ingredients.Sort();

        for (int i = 0; i < ingredients.Count; i++)
        {
            sprites[i].sprite = possibleSprites[ingredients[i].id];
        }
    }

    private void Start()
    {
        foreach (SpriteRenderer sr in sprites)
        {
            //sr.sprite = null;
        }
    }

}
