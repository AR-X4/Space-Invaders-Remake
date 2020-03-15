using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class DeadUFOSoundObserver : CollisionObserver
    {
        public override void Notify()
        {
            Sound pSound = SoundManager.Find(Sound.Name.UFOExplosion);
            pSound.PlaySound();
        }

    }
}
