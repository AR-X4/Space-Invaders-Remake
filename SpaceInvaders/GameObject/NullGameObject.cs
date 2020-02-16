using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class NullGameObject : Leaf
    {
        public NullGameObject()
            : base(GameObject.Name.Null_Object)
        {

        }
        ~NullGameObject()
        {

        }
        public override void Update()
        {
            // do nothing - its a null object
        }

    }
}
