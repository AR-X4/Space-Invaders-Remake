using System;


namespace SpaceInvaders
{
    class DeadShipSoundObserver : CollisionObserver
    {
        public override void Notify()
        {
            Sound pSound = SoundManager.Find(Sound.Name.Explosion);
            pSound.PlaySound();
        }

    }
}
