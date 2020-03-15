using System;

namespace SpaceInvaders
{
    public class SpawnUFOEvent : Command
    {
        private Random pRandom;
        private UFOFlyingSoundEvent pSoundEvent;

        public SpawnUFOEvent(Random pRandom)
        {
            this.pRandom = pRandom;
            this.pSoundEvent = new UFOFlyingSoundEvent();
        }

        public override void Execute(float deltaTime)
        {

            float NewTime = pRandom.Next(7, 15);
            
            UFORoot pUFORoot = (UFORoot)GameObjectManager.Find(GameObject.Name.UFORoot);
            OrangeSaucer pUFO = (OrangeSaucer)pUFORoot.GetFirstChild();

            pUFO.RandomizeDirection();

            // Add itself back to timer
            TimerManager.Add(TimeEvent.Name.UFOSpawn, this, NewTime);

            TimerManager.Add(TimeEvent.Name.PlayUFOSound, this.pSoundEvent, 0.17f);
        }

    }
}
