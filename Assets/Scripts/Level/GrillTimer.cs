using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GrillTimer : MonoBehaviour
{
    [SerializeField]
    private Material offMaterial;
    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private ParticleSystem grillParticles;
    private EmissionModule emission;

    private MeshRenderer mr;
    public float onTime = 1f;
    public float offTime = 1f;
    public float telegraphTime = 1f;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();

        grillParticles.Stop();
        var main = grillParticles.main;
        emission = grillParticles.emission;
        main.duration = telegraphTime + onTime;

        StartCoroutine(ToggleGrill());
    }

    IEnumerator ToggleGrill()
    {
        while (true)
        {
            yield return new WaitForSeconds(offTime - telegraphTime);

            grillParticles.Play();

            yield return new WaitForSeconds(telegraphTime);

            gameObject.tag = "Killer";
            mr.material = onMaterial;
            emission.rateOverTime = 50;

            yield return new WaitForSeconds(onTime);

            gameObject.tag = "Untagged";
            mr.material = offMaterial;
            emission.rateOverTime = 0;
        }
    }
}
