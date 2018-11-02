using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilSystem : MonoBehaviour {


    public Material material;

    void Start()
    {
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, material);
    }
}
