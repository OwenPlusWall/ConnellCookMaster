using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : Tile, ITile
{
    public GameObject saladObject;
    private Salad newSalad;
    public float cutTime;
    public float chopSpeed;
    private float cutTimer;
    public bool interacting = false;
    private Player choppingPlayer;

    //If a tile can be interacted with, this is what is called.
    public new void Interact(Player player)
    {
        if (!interacting)
        {
            if (item == null && player.vegetables.Count > 0)
            {
                choppingPlayer = player;
                saladObject = Instantiate(saladObject, this.transform);
                choppingPlayer.cutting = true;
                cutTimer = cutTime;
                chopSpeed = player.chopSpeed;
                interacting = true;
            }
            else if (item.gameObject.GetComponent<Salad>())
            {
                if (item.gameObject.GetComponent<Salad>() == newSalad)
                {
                    //This section is incomplete, and will be finished by the next commit.
                    if (player.vegetables.Count > 0 && newSalad.ingredients.Count < 3)
                    {
                        choppingPlayer = player;
                        interacting = true;
                        choppingPlayer.cutting = true;
                        cutTimer = cutTime;
                        chopSpeed = player.chopSpeed;
                    }
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interacting)
        {
            if (cutTimer > 0)
            {
                cutTimer -= chopSpeed * Time.deltaTime;
            }
            else
            {
                if (!newSalad)
                {
                    newSalad = saladObject.GetComponent<Salad>();
                }
                item = saladObject.GetComponent<IGrabbable>();
                newSalad.ingredients.Add(choppingPlayer.vegetables[0]);
                Destroy(choppingPlayer.vegetables[0].gameObject);
                choppingPlayer.vegetables.RemoveAt(0);
                newSalad.OrganizeSalad();
                choppingPlayer.cutting = false;
                interacting = false;

            }
        }
    }
}
