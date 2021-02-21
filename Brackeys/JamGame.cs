using Brackeys.Manager;
using Brackeys.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
using Unity;

namespace Brackeys
{
    public class JamGame : Game
    {
        private readonly GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;

        [Dependency]
        public StateManager StateManager { get; set; }

        public static Size ScaleOriginal => new Size(1280, 720);

        public static float Scale { get; private set; }
        public static bool HasSeenTutorial { get; set; } = false;

        public JamGame()
        {
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = "Spell Their Guts";
            IsMouseVisible = true;

            Graphics.IsFullScreen = false;
            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 720;
            Graphics.ApplyChanges();

            Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("Sprites/GUI/Cursor"), 0, 0));

            Scale = (float)Graphics.PreferredBackBufferWidth / (float)ScaleOriginal.Width;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            StateManager.ChangeTo<MenuState>(MenuState.Name);

            AudioManager.GlobalVolume = 0.25f;
        }

        protected override void Update(GameTime gameTime)
        {
            StateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Blue);

            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            StateManager.Draw(gameTime, SpriteBatch);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
