using UnityEngine;

namespace Buttons
{
    [RequireComponent(typeof(Position))]
    public class Lever : Button
    {
        [SerializeField] private bool wasPushedBefore = false;
        protected override void OnTurn()
        {
            if (!wasPushedBefore && IsPushed())
            {
                IsActive = !IsActive;
                wasPushedBefore = true;
            }
            else
            {
                wasPushedBefore = false;
            }
        }
    }
}