using Brackeys.Models;

namespace Brackeys.Component.Sprites.Tutorial
{
    class Fairy : Sprite
    {
        public Fairy()
        {
            Texture = ContentManager.TutorialFairy;
            AnimationManager.Parent = this;
            AnimationManager.Scale = 3;
            AnimationManager.Play(new Animation(Texture, 4) { FrameSpeed = 0.1f });
        }
    }
}
