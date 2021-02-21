using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.States
{
    public partial class MenuState : State
    {
        public static string Name = "Menu";

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentManager.MenuBackground, new Rectangle(0, 0, 1280, 720), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
