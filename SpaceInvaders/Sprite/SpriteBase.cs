using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SpriteBase : DLink
    {
        // Create a single sprite and all dynamic objects ONCE and ONLY ONCE (OOO- tm)
        public SpriteBase()
            : base()
        {
        }

        abstract public void Update();
        abstract public void Render();
    }
}

