﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private TileManager tileManager;

    private Animator animator;

    public SoundManager soundmanager;
	public GameObject MobileTouchManager;

    public new Camera camera;

    public Position pos;
    public Vector2i prevPos;
    public Vector2i tempPos;

    public TileColor playerTileColor;
    public int MaxHealth;
    public int Health;

    public ParticleSystem auraParticle;
    public ParticleSystem damagedParticle;

    public GameObject[] arrowObjects;
    public Dictionary<Vector2i, GameObject> arrowObjectDict;

    protected void Awake()
    {
        pos = GetComponent<Position>();
        animator = GetComponent<Animator>();
        soundmanager = GameStateManager.Instance.soundManager;
        arrowObjectDict = new Dictionary<Vector2i, GameObject>()
        {
            {new Vector2i(-3, 0), arrowObjects[0]},
            {new Vector2i(3, 0), arrowObjects[1]},
            {new Vector2i(0, 3), arrowObjects[2]},
            {new Vector2i(0, -3), arrowObjects[3]}
        };
    }

    protected void Start()
    {
        tileManager = TileManager.Instance;
        if (playerTileColor != TileColor.None)
            tileManager.SetTileColor(pos.X, pos.Y, playerTileColor);
        UpdateAuraColor();
        damagedParticle.gameObject.SetActive(false);
        GameStateManager.Instance.PlayerTurn += () => OnTurn(tempPos.x, tempPos.y);

        ArrowInactive();
    }

    public void Restart()
    {
        Health = MaxHealth;
        animator.SetBool("IsDead", false);
        playerTileColor = TileColor.None;
        UpdateAuraColor();
        damagedParticle.gameObject.SetActive(false);

        ArrowInactive();
    }

    public void ArrowActive()
    {
        foreach (var entry in arrowObjectDict)
        {
            if (tileManager.GetTileType(pos.X + entry.Key.x, pos.Y + entry.Key.y) == TileType.Normal)
                entry.Value.SetActive(true);
        }
    }

    public void ArrowInactive()
    {
        foreach (var entry in arrowObjectDict)
        {
            entry.Value.SetActive(false);
        }
    }

    public void CameraUpdate()
    {
        // Move camera position to player
        var camPos = camera.transform.position;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
    }

	public void MobileManagerUpdate()
	{
		MobileTouchManager.transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
	}

    public void GameUpdate()
    {
    }

    public Sequence OnTurn(int x, int y)
    {
        Sequence sequence = DOTween.Sequence();

        // Store the previous location
        prevPos = pos.GetVector2i();

        if (tileManager.GetTileData(x, y).color == TileColor.None ||
            tileManager.GetTileData(x, y).type == TileType.Wall)
            return sequence;


        Monster foundMonster = GameStateManager.Instance.CheckMonsterPosition(x, y);
        if (foundMonster != null)
        {
            ApplyDamage(foundMonster.DamageToPlayer);
            foundMonster.moveCancelled = true;
            sequence.Append(pos.AnimatedMove(x, y, 0.2f).OnPlay(() =>
            {
                animator.SetTrigger("Hit");
                StartCoroutine(PlayDamageEffect());
            }));
            sequence.Append(pos.AnimatedMove(prevPos.x, prevPos.y, 0.2f));
            return sequence;
        }

        sequence.Append(pos.AnimatedMove(x, y, 0.2f).OnPlay(() =>
        {
            animator.SetTrigger("Move");
        }));

        // Consume the orb after the player paints the current color
        Orb foundOrb = GameStateManager.Instance.CheckOrbPosition(x, y);
        if (foundOrb != null)
        {
            if (playerTileColor != foundOrb.Color)
            {
                if (foundOrb.Color == TileColor.Red)
                {
                    soundmanager.PlayBackground(SoundManager.Sounds.Red);
                }
                else if (foundOrb.Color == TileColor.Blue)
                {
                    soundmanager.PlayBackground(SoundManager.Sounds.Blue);
                }
                else if (foundOrb.Color == TileColor.Yellow)
                {
                    soundmanager.PlayBackground(SoundManager.Sounds.Yellow);
                }
            }
            playerTileColor = foundOrb.Color;
            UpdateAuraColor();
        }

        if ((playerTileColor != TileColor.None) && (foundOrb == null))
        {
            bool fillingExecuted = tileManager.SetTileColorAndFill(x, y, playerTileColor);
            if (fillingExecuted) soundmanager.Play(SoundManager.Sounds.TileActivate);

            tileManager.GetTile(x, y).PlaySubEffect();
        }

        return sequence;
    } // end of OnTurn

    public void ApplyDamage(int damage)
    {
        if (Health > 0) Health -= damage;
        soundmanager.Play(SoundManager.Sounds.Hit);
    }

    public void RevertTurn()
    {
        pos.X = prevPos.x;
        pos.Y = prevPos.y;
    }

    public bool PositionCheck()
    {
        if (TileManager.Instance.GetTileType(tempPos.x, tempPos.y) == TileType.Wall || TileManager.Instance.GetTileType(tempPos.x, tempPos.y) == TileType.None)
            return false;
        else return true;
    }

    public void UpdateAuraColor()
    {
        if (playerTileColor == TileColor.None || Health <= 0)
            auraParticle.startColor = new Color(0, 0, 0, 0);
        else if (playerTileColor == TileColor.Black)
            auraParticle.startColor = new Color(0, 0, 0, 1);
        else if (playerTileColor == TileColor.White)
            auraParticle.startColor = new Color(1, 1, 1, 1);
        else if (playerTileColor == TileColor.Red)
            auraParticle.startColor = new Color(249f/255f, 123f/255f, 188f/255f, 1);
        else if (playerTileColor == TileColor.Blue)
            auraParticle.startColor = new Color(84f/255f, 202f/255f, 249f/255f, 1);
        else if (playerTileColor == TileColor.Yellow)
            auraParticle.startColor = new Color(253f/255f, 249f/255f, 87f/255f, 1);
    }

    public void TurnLeft()
    {
        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
    }

    public void TurnRight()
    {
        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator PlayDamageEffect()
    {
        damagedParticle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        damagedParticle.gameObject.SetActive(false);
    }
}
