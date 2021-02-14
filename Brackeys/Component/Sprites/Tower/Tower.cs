using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Brackeys.Component.Sprites.Tower
{
    public class Tower : Sprite
    {

        public string Name { get; protected set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public float FireSpeed { get; set; }
        public static int GlobalCost { get; set; }
        public int Cost => GlobalCost;

        public bool IsRoot { get; protected set; }

        public void Shoot()
        {

        }

        public virtual void OnPlace(Cell cell, Cell[,] cells)
        {
            Color = Color.White;
        }

    }
}
