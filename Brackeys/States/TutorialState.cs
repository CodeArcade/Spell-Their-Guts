using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.States
{
    public partial class TutorialState
    {
        public static new string Name = "Tutorial";
        public Queue<string> TutorialTexts { get; set; }
        MouseState previousMouseState;
        KeyboardState previousKeyboardState;

        public TutorialState()
        {
            previousMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();
        }

        private void FillTutorialTexts()
        {
            TutorialTexts = new Queue<string>();
            TutorialTexts.Enqueue("Hi and welcome to \n\"Spell Their Guts\"!");
            TutorialTexts.Enqueue("My name is Tien, \nthe tutorial sprite!");
            TutorialTexts.Enqueue("I will guide you through \nthe fun and colorful \nworld of this game!");
            TutorialTexts.Enqueue("First, things first: \nThese are your stats.");
            TutorialTexts.Enqueue("They show how well \nyou're doing.");
            TutorialTexts.Enqueue("This is the shop.");
            TutorialTexts.Enqueue("Here you can pay mages \nto defend your magic tower!");
            TutorialTexts.Enqueue("Currently, there is only\n one type of mage \nrecruitable.");
            TutorialTexts.Enqueue("Go ahead, buy him and \nplace him on the marked \nspot over there.");
            TutorialTexts.Enqueue("Clicking on a mage \nwill show additional info. \nTry it!");
            TutorialTexts.Enqueue("The circle around \nthe mage shows \nhis range!");
            TutorialTexts.Enqueue("If an enemy is inside \nthe range, the mage will \nstart to shoot him.");
            TutorialTexts.Enqueue("Down on the right you \ncan see the mages stats.");
            TutorialTexts.Enqueue("This one is rather weak, \nbut he gets the job done.");
            TutorialTexts.Enqueue("If a mages services are \nno longer needed, you \ncan sell them.");
            TutorialTexts.Enqueue("Alright, let's see how \nthe little guy does on \nthe battlefield.");
            TutorialTexts.Enqueue("Oh no! \nA stronger enemy appeared!");
            TutorialTexts.Enqueue("I doubt our little guy \ncan deal with that brute \non his own.");
            TutorialTexts.Enqueue("Let's get some support!");
            TutorialTexts.Enqueue("Go buy another mage and \nplace him on the marked tile");
            TutorialTexts.Enqueue("The first mage is now \nin the range of the \nsecond mage!");
            TutorialTexts.Enqueue("The second mage will \nsupport any already shooting \nmages in his range");
            TutorialTexts.Enqueue("For more details on buffs, \ncheck out their stats \nin the shop.");
            TutorialTexts.Enqueue("The stats of the first mage will \nhave adjusted after the buff.");
            TutorialTexts.Enqueue("Now let's show that enemy \nwhat we're made of!");
            TutorialTexts.Enqueue("You did it!");
            TutorialTexts.Enqueue("Now you're ready to \ngo and play!");
            TutorialTexts.Enqueue("If you ever want to \nreplay the tutorial,");
            TutorialTexts.Enqueue("You can do so from \nthe Main Menu");
            TutorialTexts.Enqueue("Have fun!");
        }

        public bool IsKeyPressed()
        {
            if ((previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed) 
                || (previousMouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed))
            {
                return true;
            }

            if (previousKeyboardState.GetPressedKeys().Length == 0 && Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                return true;
            }

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsKeyPressed())
            {
                ShowNextText(TextBoxPosition);
            }

            previousKeyboardState = Keyboard.GetState();
            previousMouseState = Mouse.GetState();
        }
    }
}
