using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishWall : MonoBehaviour {

    private bool VanishFlg;
    Color Alpha = new Color(0, 0, 0, 0.3f);
    [SerializeField]
    private IceSmoke IceEffect;

    Material WallMaterial;
	// Use this for initialization
	void Start () {
        VanishFlg = false;
        WallMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (VanishFlg)
        {
            if (WallMaterial.color.a >= 0)
            {
                IceEffect.PlaysEffect();
                WallMaterial.color -= Alpha * Time.deltaTime;
            }
            if (WallMaterial.color.a <= 0)
            {
                AudioManager.GetInstance.PlaySE0(AUDIO.SE_Vanish, 0.0f);
                IceEffect.EndEffect();
                Destroy(this.gameObject);
            }
        }
        
	}

    public void OnVanish()
    {
        VanishFlg = true;
    }
}
