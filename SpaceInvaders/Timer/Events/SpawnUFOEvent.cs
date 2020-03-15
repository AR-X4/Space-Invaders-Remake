using System;

namespace SpaceInvaders
{
    public class SpawnUFOEvent : Command
    {
        
        private UFOFlyingSoundEvent pSoundEvent;

        public SpawnUFOEvent()
        {
            
            this.pSoundEvent = new UFOFlyingSoundEvent();
        }

        public override void Execute(float deltaTime)
        {
            float NewTime = RandomManager.RandomInt(7, 15);

            UFORoot pUFORoot = (UFORoot)GameObjectManager.Find(GameObject.Name.UFORoot);
            OrangeSaucer pUFO = (OrangeSaucer)pUFORoot.GetFirstChild();

            pUFO.RandomizeDirection();

            // Add itself back to timer
            TimerManager.Add(TimeEvent.Name.UFOSpawn, this, NewTime);

            TimerManager.Add(TimeEvent.Name.PlayUFOSound, this.pSoundEvent, 0.17f);
        }

    }
}
