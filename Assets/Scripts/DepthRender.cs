using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthRender : MonoBehaviour
{
    public Material _material;

    void Start()
    {
       GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _material);
    }
}
