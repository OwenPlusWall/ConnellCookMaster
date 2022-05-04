using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Player class is what holds all the essential information for each player in the game, including how they are controlled, as well as their time and score.

public class Player : MonoBehaviour
{
    public PlayerControls controls; //The keys that the player uses to move/interact with the game. These differ by player.
    public HUD playerHUD; //The  object that controls the HUD's appearance for this player.

    public int time = 180; //The amount of time the player has remaining.
    public int score; //The player's current score.
    public List<Vegetable> vegetables; //The player's vegetables on-hand.
    public Salad salad; //The salad the player is carrying (if any).
    public bool immobile = false; //Whether the player can move.
    public float moveSpeed; //How fast the player can move. This can be modified by powerups.
    public float chopSpeed; //How fast the player can chop vegetables. This can be modified by powerups.
    public bool cutting = false; //Whether the player is in the middle of cutting a vegetable.

    public List<Powerup> powerups; //What powerups are currently active on the player. These are added when the player collects a powerup, and are removed whenever a powerup has expired.

    private bool moving = false; //Used to check whether the player is moving.
    private int direction = 0; //Used to check the direction the player is currently facing. 0 = down, 1 = right, 2 = up, 3 = left.
    private Animator playerAnimator; //The animator attached to the player GameObject.
    private BoxCollider2D interactTrigger; //A trigger that determines whether the player is close enough to an object/tile to interact with it, as well as determining collision.

    private ITile selectedTile; //The tile that the player is currently interacting with, both in terms of interacting with vegetables, and collision.
    private float timer = 1.0f;


    void ChangeDirection(string dir)
    {
        selectedTile = null; //Reset selectedTile to give freedom to move in a different direction.

        //Set information such as the player's animation and the relative position of the interactTrigger based on the direciton specified.
        //For the Animator: 0 = down, 1 = right, 2 = up, 3= left (counter-clockwise, starting from down, the default direction).
        switch (dir)
        {
            case "down":
                interactTrigger.offset = new Vector2(0, -16f); //Change the position of the interactTrigger relative to the player.
                direction = 0;
                break;
            case "right":
                interactTrigger.offset = new Vector2(16f, 0);
                direction = 1;
                break;
            case "up":
                interactTrigger.offset = new Vector2(0, 16f);
                direction = 2;
                break;
            case "left":
                interactTrigger.offset = new Vector2(-16f, 0);
                direction = 3;
                break;
        }
        moving = true;
        playerAnimator.SetBool("isMoving", moving);
        playerAnimator.SetInteger("direction", direction);
    }

    void PlayerMovement()
    {
        //When a movement key is pressed, change the direction the player is moving in.
        //Precedence is given to the last key that was pressed. (If holding down a key and then another is pressed, the direction will change).
        if (Input.GetKeyDown(controls.moveDown))
        {
            ChangeDirection("down");
        }
        else if (Input.GetKeyDown(controls.moveUp))
        {
            ChangeDirection("up");
        }
        else if (Input.GetKeyDown(controls.moveLeft))
        {
            ChangeDirection("left");
        }
        else if (Input.GetKeyDown(controls.moveRight))
        {
            ChangeDirection("right");
        }

        //If more than one key is being held, but one is then released, change the direction of the player to one for a key that is STILL being held.
        if (Input.GetKeyUp(controls.moveUp) || Input.GetKeyUp(controls.moveDown) || Input.GetKeyUp(controls.moveLeft) || Input.GetKeyUp(controls.moveRight))
        {
            if (Input.GetKey(controls.moveDown))
            {
                ChangeDirection("down");
            }
            else if (Input.GetKey(controls.moveUp))
            {
                ChangeDirection("up");
            }
            else if (Input.GetKey(controls.moveLeft))
            {
                ChangeDirection("left");
            }
            else if (Input.GetKey(controls.moveRight))
            {
                ChangeDirection("right");
            }
            else //If no keys are being pressed, the player is not moving anymore.
            {
                moving = false; //Set the movement to false.
                playerAnimator.SetBool("isMoving", moving); //Tell the animator to stop animating the player moving.
            }
        }

        //If the player is moving...
        if (moving)
        {
            switch (direction) //Move the player in the direction they are currently facing.
            {
                case 0:
                    this.transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
                    break;
                case 1:
                    this.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                    break;
                case 2:
                    this.transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
                    break;
                case 3:
                    this.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                    break;
            }
        }
    }

    //Check to see if the player is in an area where they shouldn't be, and move them back into a proper position if they are.
    void CheckCollision()
    {
        switch (direction) //For each direction...
        {
            case 0:
                if (selectedTile != null && selectedTile.GetSolidity() && this.transform.position.y < selectedTile.gameObject.transform.position.y + 16) //If the player has moved beyond a point where they should be...
                {
                    this.transform.position = new Vector2(this.transform.position.x, selectedTile.gameObject.transform.position.y + 16); //Move them back to the proper position relative to the tile (16 units in the direction opposite to the player).
                }
                break;
            case 1:
                if (selectedTile != null && selectedTile.GetSolidity() && this.transform.position.x > selectedTile.gameObject.transform.position.x - 16)
                {
                    this.transform.position = new Vector2(selectedTile.gameObject.transform.position.x - 16, this.transform.position.y);
                }
                break;
            case 2:
                if (selectedTile != null && selectedTile.GetSolidity() && this.transform.position.y > selectedTile.gameObject.transform.position.y - 16)
                {
                    this.transform.position = new Vector2(this.transform.position.x, selectedTile.gameObject.transform.position.y - 16);
                }
                break;
            case 3:
                if (selectedTile != null && selectedTile.GetSolidity() && this.transform.position.x < selectedTile.gameObject.transform.position.x + 16)
                {
                    this.transform.position = new Vector2(selectedTile.gameObject.transform.position.x + 16, this.transform.position.y);
                }
                break;

        }
    }

    void TileInteraction()
    {
        if (Input.GetKeyDown(controls.use))
        {
            if (selectedTile != null)
            {
                selectedTile.Interact(this);
            }
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        if (score < 0)
        {
            score = 0;
        }
        playerHUD.UpdateScore();
    }


    //If the interactTrigger finds a Tile...
    private void OnTriggerStay2D(Collider2D collision)
    {
        ITile newTile = collision.GetComponent<ITile>();
        if (newTile != null)
        {
            selectedTile = newTile; //Set the Tile found to the current selectedTile.
        }
    }

    //If the interactTrigger moves out of the range of a tile...
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (selectedTile == collision.GetComponent<ITile>())
        {
            selectedTile = null; //Reset the current selectedTile.
        }
    }


    void Start()
    {
        playerAnimator = this.GetComponent<Animator>();
        interactTrigger = this.GetComponent<BoxCollider2D>();
        playerHUD.UpdateInventory();
        playerHUD.UpdateTime();
    }

    void Update()
    {
        if (!cutting)
        {
            PlayerMovement();
            TileInteraction();
        }
        CheckCollision();

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (time > 0)
            {
                time -= 1;
            }
            timer = 1.0f;
            playerHUD.UpdateTime();
        }
    }


}

