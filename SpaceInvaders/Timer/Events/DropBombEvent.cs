using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class DropBombEvent : Command
    {
       

        public DropBombEvent()
        {
            
        }


        public override void Execute(float deltaTime)
        {

            float NewTime = RandomManager.RandomInt(1, 3);
            float randColumn = RandomManager.RandomInt(1, 12);

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
