using UnityEngine;
using System.Collections;

public class ParticleZModifier : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private int particleCount;

    public float d = 0.01f;

	void Awake()
	{
	    particles = new ParticleSystem.Particle[1024*1024];
	    particleSystem = GetComponent<ParticleSystem>();
	}
	
	void LateUpdate ()
	{
	    particleCount = particleSystem.GetParticles(particles);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.parent.position.z + d);
	    for(int i = particleCount - 1; i >= 0; i--)
	    {
	       // particles[i].position = new Vector3(particles[i].position.x, particles[i].position.y, transform.parent.position.z + d);
            //particles[i].velocity = new Vector3(particles[i].velocity.x, particles[i].velocity.y, 0);
	    }
        particleSystem.SetParticles(particles, particleCount);
	}
}
