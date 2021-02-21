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
            Button startGameButton = new Button(ContentManager.WhiteButton, ContentManager.KenneyMini(20))
            {
                Text = "START GAME",
                Position = new Vector2((JamGame.ScaleOriginal.Width / 2) - 72, (JamGame.ScaleOriginal.Height / 2) + 48),
                Size = new System.Drawing.Size(144, 48)
            };

            startGameButton.OnClick += (sender, e) =>
            {
                StateManager.ChangeTo<GameState>(GameState.Name);
            };

            Button tutorialButton = new Button(ContentManager.WhiteButton, ContentManager.KenneyMini(20))
            {
                Text = "TUTORIAL",
                Position = new Vector2((JamGame.ScaleOriginal.Width / 2 - 72), JamGame.ScaleOriginal.Height / 2 + 48),
                Size = new System.Drawing.Size(144, 48)
            };

            tutorialButton.OnClick += (sender, e) =>
            {
                StateManager.ChangeTo<TutorialState>(TutorialState.Name);
            };

            Button quitButton = new Button(ContentManager.WhiteButton, ContentManager.KenneyMini(20))
            {
                Text = "QUIT",
                Position = new Vector2((JamGame.ScaleOriginal.Width / 2) - 72, (JamGame.ScaleOriginal.Height / 2) + (JamGame.HasSeenTutorial ? 120 : + 120)),
                Size = new System.Drawing.Size(144, 48)
            };

            quitButton.OnClick += (sender, e) =>
            {
                JamGame.Exit();
            };

            if (JamGame.HasSeenTutorial)
            {
                AddComponent(tutorialButton, States.Layers.UI);
            }

            AddComponent(quitButton, States.Layers.UI);
            AddComponent(startGameButton, States.Layers.UI);

            Title title = new Title();
            title.Position = new Vector2((JamGame.ScaleOriginal.Width / 2) - (title.Texture.Width / 2), 100 - (title.Texture.Height / 2));
            AddComponent(title, States.Layers.UI);
        }
    }
}
