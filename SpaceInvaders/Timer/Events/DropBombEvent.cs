using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class DropBombEvent : Command
    {
        private Random pRandom;

        public DropBombEvent(Random pRandom)
        {
            this.pRandom = pRandom;
        }


        public override void Execute(float deltaTime)
        {

            float NewTime = pRandom.Next(1, 2);
            float randColumn = pRandom.Next(1, 11);

            AlienGrid pGrid = (AlienGrid)GameObjectManager.Find(GameObject.Name.AlienGrid);
            AlienColumn pColumn = (AlienColumn)pGrid.GetFirstChild();

            AlienColumn temp = pColumn;
            int col = 0;
            while (temp != null) {
                
                col++;
                if (col == randColumn)
                {
                    temp.DropBomb();
                }
                temp = (AlienColumn)Iterator.GetSibling(temp);
            }

            // Add itself back to timer
            TimerManager.Add(TimeEvent.Name.DropBomb, this, NewTime);
        }
    }
}
