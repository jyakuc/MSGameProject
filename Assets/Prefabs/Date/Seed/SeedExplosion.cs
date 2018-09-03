using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedExplosion : MonoBehaviour {
    ParticleSystem ParticleData;
    Color StartColor;
    Color IntermediateColor;
    Color EndColor;

    // Use this for initialization
    void Start () {
        ParticleData = GetComponent<ParticleSystem>();
        ColorSettings();
    }
	
	// Update is called once per frame
	void Update () {
		if(ParticleData.isStopped)
        {
            ColorSettings();
        }
        ColorSettings();
    }
    void ColorSettings()
    {
        var col = ParticleData.colorOverLifetime;
        col.enabled = true;

        StartColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        IntermediateColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        EndColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(StartColor, 0.0f),
                    new GradientColorKey(IntermediateColor, 0.5f),
                    new GradientColorKey(EndColor, 1.0f) },
                    new GradientAlphaKey[] {
                        new GradientAlphaKey(1.0f, 0.0f)
                    , new GradientAlphaKey(0.0f, 1.0f) });

        col.color = grad;
    }
}
