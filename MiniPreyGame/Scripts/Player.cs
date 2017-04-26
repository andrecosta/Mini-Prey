using System.Collections;
using System.Collections.Generic;
using KokoEngine;

namespace MiniPreyGame
{
    public abstract class Player : Behaviour
    {
        public string Name;
        public Color TeamColor;
        //public Material TeamMaterial;
        public bool IsNeutral;
    }
}
