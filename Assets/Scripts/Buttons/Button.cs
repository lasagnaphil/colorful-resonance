using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Position))]
public class Button : MonoBehaviour {

	public Sprite onButton;
	public Sprite offButton;

    [NonSerialized]
    public Position pos;
	protected SpriteRenderer renderer;

    [SerializeField]
    private bool isActive;
    public bool IsActive
    {
        get { return isActive; }
        set
        {
            isActive = value;
            renderer.sprite = isActive ? onButton : offButton;
            if (isActive) Debug.Log("Button activated!");
        }
    }

    protected virtual void Awake()
    {
        pos = GetComponent<Position>();
		renderer = GetComponent<SpriteRenderer>();
    }

	bool IsPushed()
	{
		bool isPushed = false;
		
		Player player = GameStateManager.Instance.player;
		if ((player.pos.X == pos.X) && (player.pos.Y == pos.Y))
		{
			isPushed = true;
		}

	    if (GameStateManager.Instance.CheckMonsterPosition(pos.X, pos.Y) != null) isPushed = true;
		
		return isPushed;
	}

	// Use this for initialization
	void Start ()
	{
	    IsActive = false;
        GameStateManager.Instance.buttons.Add(this);
	    GameStateManager.Instance.ButtonTurns += OnTurn;
	}
	
	// Update is called once per frame
	protected virtual void OnTurn ()
	{
	    IsActive = IsPushed();
	}
}
