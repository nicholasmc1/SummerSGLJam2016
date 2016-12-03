using UnityEngine;
using System.Collections;
using InControl;

public class InputManager : MonoBehaviour 
{
	public PlayerBindings bindings;
	public static InputManager _instance;

	public static InputManager instance
	{
		get
		{
			return _instance;
		}
	}

	void Awake()
	{
		_instance = this;
		SetupInputs();
	}


	void SetupInputs()//move this to a manager at some point
	{
		bindings = new PlayerBindings();

		bindings.lookLeft.AddDefaultBinding(Mouse.NegativeX);
		bindings.lookRight.AddDefaultBinding(Mouse.PositiveX);
		bindings.lookDown.AddDefaultBinding(Mouse.NegativeY);
		bindings.lookUp.AddDefaultBinding(Mouse.PositiveY);

		bindings.moveLeft.AddDefaultBinding(Key.A);
		bindings.moveRight.AddDefaultBinding(Key.D);
		bindings.moveBack.AddDefaultBinding(Key.S);
		bindings.moveForward.AddDefaultBinding(Key.W);

		bindings.interact.AddDefaultBinding(Key.F);
		bindings.fire.AddDefaultBinding(Mouse.LeftButton);
		bindings.blink.AddDefaultBinding(Mouse.RightButton);
		bindings.reload.AddDefaultBinding(Key.R);
		bindings.cancel.AddDefaultBinding(Key.Q);

		bindings.ragdoll.AddDefaultBinding(Key.Space);
		bindings.pauseGame.AddDefaultBinding(Key.Escape);

		bindings.hotbar1.AddDefaultBinding(Key.Key1);
		bindings.hotbar2.AddDefaultBinding(Key.Key2);
		bindings.hotbar3.AddDefaultBinding(Key.Key3);
		bindings.hotbar4.AddDefaultBinding(Key.Key4);
		bindings.hotbar5.AddDefaultBinding(Key.Key5);
	}
}