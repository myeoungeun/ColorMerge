using System.Collections;
using System.Collections.Generic;
using PinkDatatable;
using UnityEngine;

public class Cube : ShapeBase
{
    public override void Init()
    {
        shapeType = ShapeType.Cube;
        ShapePhysicsData cubeData = shapePhysicsData.GetShapePhysicsData(0);
        
        float mass = cubeData.mass;
        float angularDrag = cubeData.angularDrag;
        float bounciness = cubeData.bounciness;
        float friction = cubeData.friction;
        
        rb.mass = mass;
        rb.angularDrag = angularDrag;
        phyMat.bounciness = bounciness;
        phyMat.dynamicFriction = friction;

        base.Init();
    }
}
