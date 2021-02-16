using Brackeys.Component.Controls;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace Brackeys.States
{
    public class GameOverState : State
    {
        public static string Name = "GameOver";

        public bool DidWin { get; set; }
        public int StageReached { get; set; }

        protected override void OnLoad(params object[] parameter)
        {
            DidWin = (bool)parameter[0];
            StageReached = (int)parameter[1];
        }

        protected override void LoadComponents()
        {
            AddComponent(new Label(ContentManager.TestFont) { Position = new Vector2(50, 50), Text = DidWin ? "You made it!" : "Game Over!" }, (int)States.Layers.UI);
            AddComponent(new Label(ContentManager.TestFont) { Position = new Vector2(50, 80), Text = $"You have reaced {StageReached}" }, (int)States.Layers.UI);

            Button button1 = new Button(ContentManager.EnemyTexture, ContentManager.TestFont)
            {
                Position = new Vector2(25, 100),
                Size = new Size(50, 25),
                Text = "Restart"
            };

            Button button2 = new Button(ContentManager.EnemyTexture, ContentManager.TestFont)
            {
                Position = new Vector2(100, 100),
                Size = new Size(50, 25),
                Text = "Menu"
            };

            button1.OnClick += (sender, e) =>
            {
                StateManager.ChangeTo<GameState>(GameState.Name);
            };

            button2.OnClick += (sender, e) =>
            {
                StateManager.ChangeTo<GameState>(GameState.Name);
            };

            AddComponent(button1, (int)States.Layers.UI);
            AddComponent(button2, (int)States.Layers.UI);
        }

    }
}
