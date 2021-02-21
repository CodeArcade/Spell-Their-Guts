using Brackeys.Component.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Linq;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

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

            AddComponent(new Label(ContentManager.KenneyMini(18)) { Position = new Vector2((JamGame.ScaleOriginal.Width / 2 - 72), JamGame.ScaleOriginal.Height / 2 - 40), Text = DidWin ? "You made it!" : "Game Over!" }, (int)States.Layers.UI);
            if (!DidWin)
                AddComponent(new Label(ContentManager.KenneyMini(14)) { Position = new Vector2((JamGame.ScaleOriginal.Width / 2 - 130), JamGame.ScaleOriginal.Height / 2 - 20), Text = $"You have reached stage {StageReached}", Name = "stageLabel" }, (int)States.Layers.UI);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentManager.MenuBackground, new Rectangle(0, 0, 1280, 720), Color.White);
            base.Draw(gameTime, spriteBatch);
        }

        protected override void LoadComponents()
        {

            Button startGameButton = new Button(ContentManager.WhiteButton, ContentManager.KenneyMini(20))
            {
                Text = "RESTART GAME",
                Position = new Vector2((JamGame.ScaleOriginal.Width / 2 - 72), JamGame.ScaleOriginal.Height / 2 + 48),
                Size = new Size(144, 48)
            };

            startGameButton.OnClick += (sender, e) =>
            {
                StateManager.ChangeTo<GameState>(GameState.Name);
            };


            Button menuButton = new Button(ContentManager.WhiteButton, ContentManager.KenneyMini(20))
            {
                Text = "BACK TO MENU",
                Position = new Vector2((JamGame.ScaleOriginal.Width / 2) - 72, (JamGame.ScaleOriginal.Height / 2) + (JamGame.HasSeenTutorial ? 120 : 48)),
                Size = new System.Drawing.Size(144, 48)
            };

            menuButton.OnClick += (sender, e) =>
            {
                StateManager.ChangeTo<MenuState>(MenuState.Name);
            };


            AddComponent(menuButton, States.Layers.UI);
            AddComponent(startGameButton, States.Layers.UI);

            Title title = new Title();
            title.Position = new Vector2((JamGame.ScaleOriginal.Width / 2) - (title.Texture.Width / 2), 100 - (title.Texture.Height / 2));
            AddComponent(title, States.Layers.UI);
        }
    }
}
