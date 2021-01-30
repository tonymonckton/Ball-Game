using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsSystem : MonoBehaviour
{
    [Header("Simple Physics System")]
    public Vector3 location;
    public Vector3 velocity     = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public float   gravity      = 9.8f; 
    public float   mass         = 1.0f;
    [Range (0.0f, 10.0f)] public float   friction = 0.001f;
    public Vector3 frictionVector = Vector3.zero;
    public float maxSpeed       = 0.5f;
    public float maxAcceleration = 0.2f;

    public float earthGravity   = 9.8f;
    public float marsGravity    = 3.7f;
    public float moonGravity    = 1.6f;

    Vector3 RayPosition     = Vector3.zero;
    Vector3 RayDirection    = new Vector3(0.0f, -1.0f, 0.0f);
    Vector3 hitPoint        = Vector3.zero;

    public void Init(float _gravity, float _mass)
    {   
        gravity      = _gravity;
        mass         = _mass;
        velocity     = Vector3.zero;
        acceleration = Vector3.zero;
    }


    public void UpdatePhysics(float deltaTime, GameObject floor, GameObject player)
    {
        velocity += acceleration;

        frictionVector = -velocity * friction;
        if (frictionVector.magnitude >= velocity.magnitude)
            frictionVector = frictionVector.normalized * velocity.magnitude;

        velocity += frictionVector;
        location += velocity;

        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;

        if (floor != null)
        {
            RayPosition = player.transform.position;
            RayPosition.y += 5.0f;
            hitPoint = Intersect(floor.transform.position, floor.transform.up, RayPosition, RayDirection);

        }

        acceleration = Vector3.zero;
    }

    // Newton's 2nd Law of Physics
    //  force   = mass * acceleration
    //  acceleration = force/mass
    //
    //  weight  = mass * gravity
    //  gravity: earth 9.7, Mars 3.7, Moon 1.6

    public void AddForce(Vector3 force)
    {
        // not using gravity at the moment, gravitational force = mass*gravity 
        // acceleration += (force/mass) * deltaTime;

        acceleration += force/mass;
        if (acceleration.magnitude > maxAcceleration)
            acceleration = acceleration.normalized * maxAcceleration;

    }


    public Vector3 GetPosition()
    {
        return location;
    }

    public Vector3 Intersect(Vector3 planeP, Vector3 planeN, Vector3 rayP, Vector3 rayD)
    {
        var d = Vector3.Dot(planeP, -planeN);
        var t = -(d + rayP.z * planeN.z + rayP.y * planeN.y + rayP.x * planeN.x) / (rayD.z * planeN.z + rayD.y * planeN.y + rayD.x * planeN.x);
        return rayP + t * rayD;
    }
}
