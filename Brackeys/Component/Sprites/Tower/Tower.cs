using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class Tower : Sprite
    {

        public string Name { get; protected set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public float FireSpeed { get; set; }
        public static int Cost { get; set; }


        public void Shoot()
        {

        }

    }
}
