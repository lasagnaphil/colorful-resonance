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

        int CoolTime;

        protected override void OnTurn(Sequence sequence)
        {
            Vector2i delta = DiffFromPlayer();

            if (CoolTime < 6)
                CoolTime++;
            else CoolTime = 1;

            if (CoolTime > 3)
            {
                monstersColor = TileColor.Black;
                GetComponent<SpriteRenderer>().sprite = Angry;
                ShieldActive();

                if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
                {
                    if (delta.x > 0)
                    {
                        if (CheckTileIsNormal(pos.X + 1, pos.Y))
                            AnimatedMove(sequence, pos.X + 1, pos.Y);
                    }
                    else if (delta.x < 0)
                    {
                        if (CheckTileIsNormal(pos.X - 1, pos.Y))
                            AnimatedMove(sequence, pos.X - 1, pos.Y);
                    }
                }
                else
                {
                    if (delta.y > 0)
                    {
                        if (CheckTileIsNormal(pos.X, pos.Y + 1))
                            AnimatedMove(sequence, pos.X, pos.Y + 1);
                    }
                    else if (delta.y < 0)
                    {
                        if (CheckTileIsNormal(pos.X, pos.Y - 1))
                            AnimatedMove(sequence, pos.X, pos.Y - 1);
                    }
                }
            }
            else
            {
                monstersColor = TileColor.Yellow;
                GetComponent<SpriteRenderer>().sprite = Normal;
                ShieldInactive();

                if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
                {
                    if (delta.x > 0)
                    {
                        if(CheckTileIsNormal(pos.X - 1, pos.Y))
                            AnimatedMove(sequence, pos.X - 1, pos.Y);
                    }
                    else if (delta.x < 0)
                    {
                        if (CheckTileIsNormal(pos.X + 1, pos.Y))
                            AnimatedMove(sequence, pos.X + 1, pos.Y);
                    }
                }
                else
                {
                    if (delta.y > 0)
                    {
                        if(CheckTileIsNormal(pos.X, pos.Y - 1))
                            AnimatedMove(sequence, pos.X, pos.Y - 1);
                    }
                    else if (delta.y < 0)
                    {
                        if (CheckTileIsNormal(pos.X, pos.Y + 1))
                            AnimatedMove(sequence, pos.X, pos.Y + 1);
                    }
                }
            }
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

        protected override void WhenDestroyed()
        {
            base.WhenDestroyed();
        }
    }
}