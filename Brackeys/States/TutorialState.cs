using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.States
{
    public partial class TutorialState
    {
        public static string Name = "Tutorial";
        public Queue<string> TutorialTexts { get; set; }


        private void FillTutorialTexts()
        {
            TutorialTexts = new Queue<string>();
            TutorialTexts.Enqueue("Hi and welcome to \n\"Mages Together Strong\"!");
            TutorialTexts.Enqueue("My name is Tien, the tutorial sprite!");
            TutorialTexts.Enqueue("I will guide you through the fun and colorful world of this game!");
            TutorialTexts.Enqueue("First, things first: These are your stats.");
            TutorialTexts.Enqueue("They show how well you're doing.");
            TutorialTexts.Enqueue("This is the shop.");
            TutorialTexts.Enqueue("Here you can pay mages to defend your magic tower!");
            TutorialTexts.Enqueue("Currently, there is only one type of mage recruitable.");
            TutorialTexts.Enqueue("Go ahead, buy him and place him on the marked spot over there");
            TutorialTexts.Enqueue("Clicking on a mage will show additional info. Try it!");
            TutorialTexts.Enqueue("The circle around the mage shows his range!");
            TutorialTexts.Enqueue("If an enemy is inside the range, the mage will start to shoot him");
            TutorialTexts.Enqueue("Down on the right you can see the mages stats.");
            TutorialTexts.Enqueue("This one is rather weak, but he gets the job done");
            TutorialTexts.Enqueue("If a mages services are no longer needed, you can sell them.");
            TutorialTexts.Enqueue("Alright, let's see how the little guy does on the battlefield");
            TutorialTexts.Enqueue("Oh no! A stronger enemy appeared");
            TutorialTexts.Enqueue("I doubt our little guy can deal with that brute on his own");
            TutorialTexts.Enqueue("Let's get some support!");
            TutorialTexts.Enqueue("Go buy another mage and place him on the marked tile");
            TutorialTexts.Enqueue("The first mage is now in the range of the second mage!");
            TutorialTexts.Enqueue("The second mage will support any already shooting mages in his range");
            TutorialTexts.Enqueue("For more details on buffs, check out their stats in the shop.");
            TutorialTexts.Enqueue("The stats of the first mage will have adjusted after the buff.");
            TutorialTexts.Enqueue("Now let's show that enemy what we're made of!");
            TutorialTexts.Enqueue("You did it!");
            TutorialTexts.Enqueue("Now you're ready to go and play!");
            TutorialTexts.Enqueue("If you ever want to replay the tutorial,");
            TutorialTexts.Enqueue("you can do so from the main menu");
            TutorialTexts.Enqueue("Have fun!");
        }
    }
}
