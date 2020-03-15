using System;


namespace SpaceInvaders
{
    public class UFOFlyingSoundEvent : Command
    {
        OrangeSaucer pUFO;
        Sound pSound;

        public UFOFlyingSoundEvent() {
            this.pSound = SoundManager.Find(Sound.Name.UFO);

            UFORoot pRoot = (UFORoot)GameObjectManager.Find(GameObject.Name.UFORoot);
            this.pUFO = (OrangeSaucer)pRoot.GetFirstChild();
        }

        public override void Execute(float deltaTime)
        {
            this.pSound.PlaySound();

            if (this.pUFO.pProxySprite.pSprite.name != GameSprite.Name.NullObject)
            {
                TimerManager.Add(TimeEvent.Name.PlayUFOSound, this, deltaTime);
            }
        }
    }
}
