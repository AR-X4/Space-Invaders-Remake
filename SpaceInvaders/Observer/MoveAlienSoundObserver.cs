using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveAlienSoundObserver : AlienObserver
    {
        private int soundIndex;
        public MoveAlienSoundObserver()
        {
            this.soundIndex = 1;
        }

        public override void Notify()
        {
            Sound pSound;
            switch (this.soundIndex)
            {
                case 1:
                    pSound = SoundManager.Find(Sound.Name.Invader1);
                    break;
                case 2:
                    pSound = SoundManager.Find(Sound.Name.Invader2);
                    break;
                case 3:
                    pSound = SoundManager.Find(Sound.Name.Invader3);
                    break;
                case 4:
                    pSound = SoundManager.Find(Sound.Name.Invader4);
                    break;
                default:
                    Debug.Assert(false);
                    pSound = SoundManager.Find(Sound.Name.Invader1);
                    break;
            }
            this.soundIndex++;
            if (this.soundIndex > 4)
            {
                this.soundIndex = 1;
            }
            pSound.PlaySound();
        }
    }
}
