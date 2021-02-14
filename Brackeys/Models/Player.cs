using System;
using System.Collections.Generic;
using System.Text;
using Brackeys.Component.Sprites.Tower;

namespace Brackeys.Models
{
    public class Player
    {
        public Tower CurrentlyHolding { get; set; }
        public int Money { get; set; }
        public int Health { get; set; }
    }
}
