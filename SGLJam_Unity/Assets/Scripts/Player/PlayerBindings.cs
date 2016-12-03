using UnityEngine;
using System.Collections;
using InControl;

public class PlayerBindings : PlayerActionSet 
{
	public PlayerAction moveLeft;
	public PlayerAction moveRight;
	public PlayerAction moveBack;
	public PlayerAction moveForward;
	public PlayerTwoAxisAction move;

	public PlayerAction lookLeft;
	public PlayerAction lookRight;
	public PlayerAction lookDown;
	public PlayerAction lookUp;
	public PlayerTwoAxisAction look;

	public PlayerAction interact;
	public PlayerAction fire;
	public PlayerAction reload;
	public PlayerAction blink;
	public PlayerAction cancel;

	public PlayerAction sprint;
	public PlayerAction jump;
	public PlayerAction inventory;
	public PlayerAction crouch;
	public PlayerAction pauseGame;

	public PlayerAction hotbar1;
	public PlayerAction hotbar2;
	public PlayerAction hotbar3;
	public PlayerAction hotbar4;
	public PlayerAction hotbar5;
	public PlayerBindings()
	{
		moveLeft = CreatePlayerAction("Move Left");
		moveRight = CreatePlayerAction("Move Right");
		moveForward = CreatePlayerAction("Move Forward");
		moveBack = CreatePlayerAction("Move Back");
		move = CreateTwoAxisPlayerAction(moveLeft, moveRight, moveBack, moveForward);

		lookLeft = CreatePlayerAction("look Left");
		lookRight = CreatePlayerAction("look Right");
		lookDown = CreatePlayerAction("look Down");
		lookUp = CreatePlayerAction("look Up");
		look = CreateTwoAxisPlayerAction(lookLeft, lookRight, lookDown, lookUp);

		interact = CreatePlayerAction("Interact");
		fire = CreatePlayerAction("Fire");
		blink = CreatePlayerAction("Blink");
		cancel = CreatePlayerAction("Cancel");
		reload = CreatePlayerAction("Reload");

		sprint = CreatePlayerAction("Sprint");
		jump = CreatePlayerAction("Jump");
		inventory = CreatePlayerAction("Inventory");
		crouch = CreatePlayerAction("Crouch");
		pauseGame = CreatePlayerAction("Pause Game");

		hotbar1 = CreatePlayerAction("Hotbar One");
		hotbar2 = CreatePlayerAction("Hotbar Two");
		hotbar3 = CreatePlayerAction("Hotbar Three");
		hotbar4 = CreatePlayerAction("Hotbar Four");
		hotbar5 = CreatePlayerAction("Hotbar Five");

	}
}