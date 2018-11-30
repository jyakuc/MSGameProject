using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishFloor : MonoBehaviour {

    private bool VanishFloorFlg;
    Color Alpha = new Color(0, 0.1f, 0.2f, 0);
    [SerializeField]
    private IceSmoke IceEffect;

    Material WallMaterial;
    // Use this for initialization
    void Start()
    {
        VanishFloorFlg = false;
        WallMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (VanishFloorFlg)
        {
            Debug.Log(WallMaterial.color);
            if (WallMaterial.color.b <= 5)
            {
                IceEffect.PlaysEffect();
                WallMaterial.color += Alpha * Time.deltaTime;
            }
            if (WallMaterial.color.b >= 5)
            {
                AudioManager.GetInstance.PlaySE0(AUDIO.SE_Vanish, 0.0f);
                IceEffect.EndEffect();
                Destroy(this.gameObject);
            }
        }

    }

    public void OnVanishFloor()
    {
        VanishFloorFlg = true;
    }
}
