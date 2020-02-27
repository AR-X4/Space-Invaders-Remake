using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class WallCategory : Leaf
    {
        public enum Type
        {
            WallGroup,
            Right,
            Left,
            Bottom,
            Top,
            Unitialized
        }
        //data
        protected WallCategory.Type type;

        protected WallCategory(GameObject.Name name, GameSprite.Name spriteName, WallCategory.Type type)
            : base(name, spriteName)
        {
            this.type = type;
        }

        ~WallCategory()
        {
        }

        public WallCategory.Type GetCategoryType()
        {
            return this.type;
        }
    }
}
