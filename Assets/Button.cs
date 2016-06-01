using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour {

	public Sprite onButton;
	public Sprite offButton;
	
	Vector2 position;
	new SpriteRenderer renderer;
	bool isActive;

	public void SetActive(bool trueOrFalse)
	{
		if (trueOrFalse == true)
		{
			isActive = true;
			renderer.sprite = onButton;			
		}
		else
		{
			isActive = false;
			renderer.sprite = offButton;
		}
	}
	
	bool IsPushed()
	{
		bool isPushed = false;
		
		Player player = GameStateManager.Instance.player;
		if ((player.pos.X == (int)transform.position.x) && (player.pos.Y == (int)transform.position.y))
		{
			isPushed = true;
		}
		
		List<Monster> monsters = GameStateManager.Instance.monsters;
		foreach (var monster in monsters)
		{
			if ((monster.pos.X == (int)transform.position.x) && (monster.pos.Y == (int)transform.position.y))
			{
				isPushed = true;
			}	
		}
		
		return isPushed;
	}

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer>();
		position = new Vector2((int)transform.position.x, (int)transform.position.y);
		SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (IsPushed())
			SetActive(true);
		else
			SetActive(false);
	}
}
