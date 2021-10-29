using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimatorController
{
    public static class Params
    {
        public const string Hit = "Hit";
        public const string Heal = "Heal";
        public const string Health = "Health";
        public const string IsDead = "IsDead";
    }

    public static class States
    {
        public const string Hitted = "GetHit";
        public const string Healed = "Dizzy";
        public const string Dead = "Die";
        public const string Recovered = "DieRecover";
    }
}
