
using Brackeys.Models;

namespace Brackeys.States
{
    public partial class GameState : State
    {
        public static string Name = "Game";
        public Player Player { get; set; }


        public GameState()
        {
            //Player = new Player()
            //{
            //    CurrentlyHolding
            //}
        }

  
    }
}