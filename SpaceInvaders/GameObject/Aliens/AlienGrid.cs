using System.Diagnostics;


namespace SpaceInvaders
{
    //Specific type of composite
    public class AlienGrid : Composite
    {
        public AlienGrid(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
        : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 0, 0);
        }
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitGroup(this);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);

            base.Update();
        }
    }
}
