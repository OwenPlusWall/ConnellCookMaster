using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Player player;

    public List<Image> frames;
    public List<Container> containers;
    public Sprite plateSprite;
    public Text ScoreText;
    public Text TimeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        ScoreText.text = player.score + "";
    }

    public void UpdateTime()
    {
        TimeText.text = player.time + "";
    }

    void UpdateFrame()
    {
        if (player.vegetables.Count > 0 || player.salad)
        {
            frames[0].color = new Color(frames[0].color.r, frames[0].color.g, frames[0].color.b, 1.0f);
        }
        else
        {
            frames[0].color = new Color(frames[0].color.r, frames[0].color.g, frames[0].color.b, 0.5f);
        }

        if (player.vegetables.Count > 1)
        {
            frames[1].color = new Color(frames[1].color.r, frames[1].color.g, frames[1].color.b, 1.0f);
        }
        else
        {
            frames[1].color = new Color(frames[1].color.r, frames[1].color.g, frames[1].color.b, 0.5f);
        }
    }

    public void UpdateInventory()
    {
        foreach (Container container in containers)
        {
            foreach (Image image in container.images)
            {
                image.enabled = false;
            }
        }

        if (player.salad) //If the player is carrying a salad...
        {
            containers[0].images[0].sprite = plateSprite;
            containers[0].images[0].enabled = true;
            for (int i = 0; i < player.salad.ingredients.Count; i++)
            {
                containers[0].images[i + 1].sprite = player.salad.possibleSprites[player.salad.ingredients[i].id];
                containers[0].images[i + 1].enabled = true;
            }
        }
        else if (player.vegetables.Count > 0)
        {
            for (int i = 0; i < player.vegetables.Count; i++)
            {
                containers[i].images[0].sprite = player.vegetables[i].spriteList[player.vegetables[i].id];
                containers[i].images[0].enabled = true;
            }
        }

        UpdateFrame();
    }
}
