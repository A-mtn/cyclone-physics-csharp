using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class representing a simple particle affected by gravity.
/// </summary>
public class Ballistic : MonoBehaviour
{
    #region Unity Editor

    public float mass;
    /// <summary>
    ///  The velocity of the particle.
    /// </summary>
    public Vector3 velocity;

    /// <summary>
    /// The acceleration of the particle.
    /// </summary>
    public Vector3 acceleration;

    /// <summary>
    /// The damping of the particle.
    /// </summary>
    public float damping;

    #endregion

    /// <summary>
    /// Create a particle instance.
    /// </summary>
    private Cyclone.Particle particle = new Cyclone.Particle();

    private float initialX = 0f;
    private float initialY = 0f;
    private float initialZ= 0f;
    /// <summary>
    /// Set the default properties of the particle.
    /// </summary>
    private void Start()
    {
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;
        /*
        particle.Mass = mass;
        particle.SetPosition(transform.position.x, transform.position.y, transform.position.z);
        particle.SetVelocity(velocity.x, velocity.y, velocity.z);
        particle.SetAcceleration(acceleration.x, acceleration.y, acceleration.z);
        particle.Damping = damping;

        SetObjectPosition(particle.Position);*/
    }

    /// <summary>
    /// Update the particle position.
    /// </summary>
    private void Update()
    {/*
        particle.Integrate(Time.deltaTime);
        SetObjectPosition(particle.Position);*/
    }

    IEnumerator UpdateProjectile()
    {
        particle.ClearAccumulator();
        for (int i = 0; i < 3000; i++)
        {
            particle.Integrate(Time.deltaTime);
            SetObjectPosition(particle.Position);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("integration ended!");
    }

    public void Fire(string currentShotType)
    {
        Debug.Log(currentShotType);
        switch(currentShotType)
        {
            case "PISTOL":
                particle.Mass = 2.0f;
                particle.SetPosition(initialX, initialY, initialZ);
                particle.SetVelocity(0.0f, 0.0f, 35.0f);
                particle.SetAcceleration(0.0f, -1.0f, 0.0f);
                particle.Damping = 0.99f;
                SetObjectPosition(particle.Position);
                StartCoroutine(UpdateProjectile());
                break;

            case "ARTILLERY":
                particle.Mass = 200.0f;
                particle.SetPosition(initialX, initialY, initialZ);
                particle.SetVelocity(0.0f, 30.0f, 40.0f);
                particle.SetAcceleration(0.0f, -20.0f, 0.0f);
                particle.Damping = 0.99f;
                SetObjectPosition(particle.Position);
                StartCoroutine(UpdateProjectile());
                break;

            case "FIREBALL":
                particle.Mass = 1.0f;
                particle.SetPosition(initialX, initialY, initialZ);
                particle.SetVelocity(0.0f, 0.0f, 10.0f);
                particle.SetAcceleration(0.0f, 0.60f, 0.0f);
                particle.Damping = 0.9f;
                SetObjectPosition(particle.Position);
                StartCoroutine(UpdateProjectile());
                break;

            case "LASER":
                // Note that this is the kind of laser bolt seen in films,
                // not a realistic laser beam!
                particle.Mass = 0.10f;
                particle.SetPosition(initialX, initialY, initialZ);
                particle.SetVelocity(0.0f, 0.0f, 100.0f);
                particle.SetAcceleration(0.0f, 0.0f, 0.0f);
                particle.Damping = 0.99f;
                SetObjectPosition(particle.Position);
                StartCoroutine(UpdateProjectile());
                break;
        }
    }

    /// <summary>
    /// Helper method to convert a Cyclone.Math.Vector3 to a UnityEngine.Vector3 position.
    /// </summary>
    /// <param name="position">The position.</param>
    private void SetObjectPosition(Cyclone.Math.Vector3 position)
    {
        transform.position = new Vector3((float)position.x, (float)position.y, (float)position.z);
    }
}
