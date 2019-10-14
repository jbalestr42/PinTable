using UnityEngine;

public class HorizontalTransition : ATableTransition
{
    readonly int textureID = Shader.PropertyToID("_MainTex");
    Material _material;

    public override void StartTransition()
    {
        _material = GetComponent<MeshRenderer>().sharedMaterial;
        _material.SetTextureOffset(textureID, new Vector2(-1.0f, 0f));
    }

    public override void UpdateTransition(float p_percent)
    {
        _material.SetTextureOffset(textureID, new Vector2(Mathf.Lerp(-1.0f, 1.0f, p_percent), 0f));
    }
}
