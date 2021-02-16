using Brackeys.Component.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Brackeys.States
{
    public partial class MenuState : State
    {
        protected override void LoadComponents()
        {
            AddUi();
        }

        private void AddUi()
        {
            Button startGameButton = new Button(ContentManager.RangeTexture, ContentManager.TestFont)
            {
                Text = "Start Game",
                Position = new Vector2(JamGame.ScaleOriginal.Width / 2 - 50, JamGame.ScaleOriginal.Height / 2 - 20),
                Size = new System.Drawing.Size(100, 40)
            };

            startGameButton.OnClick += (sender, e) =>
            {
                StateManager.ChangeTo<GameState>(GameState.Name);
            };

            AddComponent(startGameButton, States.Layers.UI);

            Label title = new Label(ContentManager.KenneyMini(20))
            {
                Text = "OwO strong mage OwO",
                FontColor = Color.White,
                DropShadow = Color.Black * 0.7f,
                Position = new Vector2(300, 300),
                FontScale = 1.5f,
                DropShadowOffset = 2
            };
            AddComponent(title, States.Layers.UI);
        }
    }
}
