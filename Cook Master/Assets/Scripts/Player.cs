using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Player class is what holds all the essential information for each player in the game, including how they are controlled, as well as their time and score.

public class Player : MonoBehaviour
{
    public PlayerControls controls; //The keys that the player uses to move/interact with the game. These differ by player.

    public float time; //The amount of time the player has remaining.
    public int score; //The player's current score.
    public List<Vegetable> vegetables; //The player's vegetables on-hand.
    public bool immobile = false; //Whether the player can move.
    public float moveSpeed; //How fast the player can move. This can be modified by powerups.
    public float chopSpeed; //How fast the player can chop vegetables. This can be modified by powerups.

    public List<Powerup> powerups; //What powerups are currently active on the player. These are added when the player collects a powerup, and are removed whenever a powerup has expired.

    private bool moving = false; //Used to check whether the player is moving.
    private int direction = 0; //Used to check the direction the player is currently facing. 0 = down, 1 = right, 2 = up, 3 = left.
    private Animator playerAnimator; //The animator attached to the player GameObject.
    private BoxCollider2D interactTrigger; //A trigger that determines whether the player is close enough to an object/tile to interact with it.


    void ChangeDirection(string dir)
    {
        //TO ADD: Check to make sure that objects/tiles are not in the way before moving.
        //        This will be done once objects/tiles are implemented.

        //Set information such as the player's animation and the relative position of the interactTrigger based on the direciton specified.
        //For the Animator: 0 = down, 1 = right, 2 = up, 3= left (counter-clockwise, starting from down, the default direction).
        switch (dir)
        {
            case "down":
                interactTrigger.offset = new Vector2(0, -10);
                direction = 0;
                break;
            case "right":
                interactTrigger.offset = new Vector2(10, 0);
                direction = 1;
                break;
            case "up":
                interactTrigger.offset = new Vector2(0, 10);
                direction = 2;
                break;
            case "left":
                interactTrigger.offset = new Vector2(-10, 0);
                direction = 3;
                break;
        }
        moving = true;
        playerAnimator.SetBool("isMoving", moving);
        playerAnimator.SetInteger("direction", direction);
    }


    void Start()
    {
        playerAnimator = this.GetComponent<Animator>();
        interactTrigger = this.GetComponent<BoxCollider2D>();
    }

    void Update()
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

}

