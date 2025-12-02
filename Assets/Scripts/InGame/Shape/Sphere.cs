using System.Collections;
using System.Collections.Generic;
using PinkDatatable;
using UnityEngine;

public class Sphere : ShapeBase
{
    public override void Init()
    {
        shapeType = ShapeType.Sphere;
        ShapePhysicsData sphereData = shapePhysicsData.GetShapePhysicsData(1);
        
        float mass = sphereData.mass;
        float angularDrag = sphereData.angularDrag;
        float bounciness = sphereData.bounciness;
        float friction = sphereData.friction;
        
        rb.mass = mass;
        rb.angularDrag = angularDrag;
        phyMat.bounciness = bounciness;
        phyMat.dynamicFriction = friction;
    }
}
