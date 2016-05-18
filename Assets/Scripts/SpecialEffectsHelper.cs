using UnityEngine;
using System.Collections;

public class SpecialEffectsHelper : MonoBehaviour {

    public static SpecialEffectsHelper Instance;

    public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Multiple instance of SpecialEffectHelper");
        }

        Instance = this;
    }

    public void Explosion(Vector3 position)
    {
        instantiate(smokeEffect, position);

        instantiate(fireEffect, position);
    }

    private ParticleSystem instantiate(ParticleSystem prefabs, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(prefabs, position, Quaternion.identity) as ParticleSystem;

        Destroy(newParticleSystem.gameObject, newParticleSystem.startLifetime);

        return newParticleSystem;
    }
}
