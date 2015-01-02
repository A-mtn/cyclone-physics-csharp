﻿using UnityEngine;

public class BVHNodeTest : MonoBehaviour
{
    private void Start()
    {
        TestLog.Instance.LogResult("BVHNodeTest.Test1", Test1());
    }

    /// <summary>
    /// Tests insertion of elements into the BVH tree.
    /// </summary>
    /// <returns><c>true</c> if test succeeded; otherwise, <c>false</c>.</returns>
    private bool Test1()
    {
        Cyclone.BVHNode root = new Cyclone.BVHNode(null, null, null);

        Cyclone.BoundingSphere volume = new Cyclone.BoundingSphere(new Cyclone.Math.Vector3(0, 0, 0), 1);
        Cyclone.RigidBody body = new Cyclone.RigidBody();

        //root.Insert(body, volume);

        return true;
    }
}