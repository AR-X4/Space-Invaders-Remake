using System;
using System.Diagnostics;


namespace SpaceInvaders
{

    abstract public class CollisionVisitor : DLink
    {

        public virtual void VisitGroup(AlienGrid b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by AlienGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitColumn(AlienColumn b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by AlienColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitPurpleOctopus(PurpleOctopus b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by PurplePctopus not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBlueCrab(BlueCrab b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by BlueCrab not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitGreenSquid(GreenSquid b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by GreenSquid not implemented");
            Debug.Assert(false);
        }


        public virtual void VisitOrangeSaucer(OrangeSaucer b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by OrangeSaucer not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissile(Missile m)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Missile not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissileGroup(MissileGroup m)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by MissileGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallGroup(WallGroup w)
        {
            Debug.WriteLine("Visit by WallGroup not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitWallRight(WallRight w)
        {
            Debug.WriteLine("Visit by WallRight not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitWallLeft(WallLeft w)
        {
            Debug.WriteLine("Visit by WallLeft not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitWallTop(WallTop w)
        {
            Debug.WriteLine("Visit by WallTop not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShip(Ship s)
        {
            Debug.WriteLine("Visit by Ship not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShipRoot(ShipRoot s)
        {
            Debug.WriteLine("Visit by ShipRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitNullGameObject(NullGameObject n)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by NullGameObject not implemented");
            Debug.Assert(false);
        }

        abstract public void Accept(CollisionVisitor other);
    }

}