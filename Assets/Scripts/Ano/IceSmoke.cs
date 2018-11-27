using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSmoke : MonoBehaviour {
    public GameObject GlitterPrefab;
    private GameObject CreateGlitter=null;
    private ParticleSystem ParticleData;
    private float LifetimeCount = 0;
    // Use this for initialization
    void Start () {
        ParticleData = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        LifetimeCount += Time.deltaTime;
        Debug.Log(LifetimeCount);
        if(3.5f<=LifetimeCount)
        {
            if(CreateGlitter==null)
            {
                CreateGlitter=(GameObject)Instantiate(GlitterPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 10.0f, this.transform.position.z), Quaternion.identity);
                CreateGlitter.transform.localScale = this.transform.localScale;
            }
        }
        if (!ParticleData.isPlaying)
        {
            Destroy(this.gameObject);
        }

    }
}
