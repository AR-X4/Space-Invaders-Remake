using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class DeadAlienSoundObserver : CollisionObserver
    {
        public override void Notify()
        {
            Sound pSound = SoundManager.Find(Sound.Name.DeadAlien);
            pSound.PlaySound();
        }
    }
}
