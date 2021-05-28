using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public string Name;
    protected Transform origin;

    public Weapon(Transform origin, string name)
    {
        this.origin = origin;
        this.Name = name;
    }

    public abstract void Shoot();
}

public class CreationTool : Weapon
{
    private GameObject toInstantiate;

    public CreationTool(Transform origin) : base(origin, "Creation Tool")
    {
        toInstantiate = GameObject.CreatePrimitive(PrimitiveType.Cube);
        toInstantiate.SetActive(false);
        toInstantiate.layer = 8;
    }

    public override void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin.position, origin.forward, out hit, 100f))
        {
            //var objectsRigidBody = hit.transform.GetComponent<Rigidbody>();
            //if (objectsRigidBody != null)
            //{
            //    var direction = hit.point - camera.transform.position;
            //    var force = direction.normalized * forcePower;

            //    objectsRigidBody.AddForceAtPosition(force, hit.point, ForceMode.Impulse);
            //}

            var normal = hit.normal;

            var createdObject = GameObject.Instantiate(toInstantiate, hit.point + normal * 0.5f, Quaternion.identity);
            createdObject.SetActive(true);
        }
    }
}

public class ForceGun : Weapon
{
    private float forcePower;
    
    public ForceGun(Transform origin, string name, float forcePower) : base(origin, name)
    {
        this.forcePower = forcePower;
    }

    public override void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin.position, origin.forward, out hit, 100f))
        {
            var objectsRigidBody = hit.transform.GetComponent<Rigidbody>();
            if (objectsRigidBody != null)
            {
                var direction = hit.point - origin.position;
                var force = direction.normalized * forcePower;

                objectsRigidBody.AddForceAtPosition(force, hit.point, ForceMode.Impulse);
            }
        }
    }

    public static ForceGun PushGun(Transform origin)
    {
        return new ForceGun(origin, "Push gun", 3f);
    }

    public static ForceGun PullGun(Transform origin)
    {
        return new ForceGun(origin, "Pull gun", -3f);
    }
}

public class CannonGun : Weapon
{
    private GameObject cannonBall;
    private float force = 20f;

    public CannonGun(Transform origin) : base(origin, "Cannon")
    {
        cannonBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var cannonBallRigidBody = cannonBall.AddComponent<Rigidbody>();
        cannonBallRigidBody.mass = 0.2f;
        cannonBallRigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        cannonBall.transform.localScale *= 0.4f;

        cannonBall.SetActive(false);
    }

    public override void Shoot()
    {
        var projectile = GameObject.Instantiate(cannonBall, origin.position, origin.rotation);
        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * force, ForceMode.Impulse);
    }
}