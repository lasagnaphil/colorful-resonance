using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

namespace Monsters
{
    public class InvincibleMonster : Monster
    {
        public Sprite Angry;
        public Sprite Normal;
        
        public GameObject shieldEffectObject;
        GameObject shieldEffect;

        int deltaX;
        int deltaY;
        int CoolTime;

        protected override Sequence OnTurn()
        {
            Sequence sequence = base.OnTurn();
            Player player = GameStateManager.Instance.player;
            Position PlayerPos = player.GetComponent<Position>();

            deltaX = PlayerPos.X - pos.X;
            deltaY = PlayerPos.Y - pos.Y;

            if (moveCancelled)
            {
                moveCancelled = false;
                return sequence;
            }

            if (CoolTime < 6)
                CoolTime++;
            else CoolTime = 1;

            if (CoolTime > 3)
            {
                monstersColor = TileColor.Black;
                GetComponent<SpriteRenderer>().sprite = Angry;
                ShieldInactive();

                if (Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
                {
                    if (deltaX > 0)
                    {
                        if (CheckPosition(pos.X + 1, pos.Y))
                            AnimatedMove(sequence, pos.X + 1, pos.Y);
                    }
                    else if (deltaX < 0)
                    {
                        if (CheckPosition(pos.X - 1, pos.Y))
                            AnimatedMove(sequence, pos.X - 1, pos.Y);
                    }
                }
                else
                {
                    if (deltaY > 0)
                    {
                        if (CheckPosition(pos.X, pos.Y + 1))
                            AnimatedMove(sequence, pos.X, pos.Y + 1);
                    }
                    else if (deltaY < 0)
                    {
                        if (CheckPosition(pos.X, pos.Y - 1))
                            AnimatedMove(sequence, pos.X, pos.Y - 1);
                    }
                }
            }
            else
            {
                monstersColor = TileColor.Yellow;
                GetComponent<SpriteRenderer>().sprite = Normal;
                ShieldActive();

                if (Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
                {
                    if (deltaX > 0)
                    {
                        if(CheckPosition(pos.X - 1, pos.Y))
                            AnimatedMove(sequence, pos.X - 1, pos.Y);
                    }
                    else if (deltaX < 0)
                    {
                        if (CheckPosition(pos.X + 1, pos.Y))
                            AnimatedMove(sequence, pos.X + 1, pos.Y);
                    }
                }
                else
                {
                    if (deltaY > 0)
                    {
                        if(CheckPosition(pos.X, pos.Y - 1))
                            AnimatedMove(sequence, pos.X, pos.Y - 1);
                    }
                    else if (deltaY < 0)
                    {
                        if (CheckPosition(pos.X, pos.Y + 1))
                            AnimatedMove(sequence, pos.X, pos.Y + 1);
                    }
                }
            }

            return sequence;
        }

        void ShieldActive()
        {
            if (shieldEffect == null)
            {
                shieldEffect = Instantiate(shieldEffectObject, transform.position - new Vector3(0, 0.05f, 0), Quaternion.identity) as GameObject;
                shieldEffect.transform.parent = gameObject.transform;
            }
        }
        
        void ShieldInactive()
        {
            if (shieldEffect != null)
                Destroy(shieldEffect);
        }

        bool CheckPosition(int a, int b)
        {
            if (TileManager.Instance.GetTileType(a, b) != TileType.Wall && TileManager.Instance.GetTileType(a, b) != TileType.None)
                return true;
            else return false;
        }
    }
}