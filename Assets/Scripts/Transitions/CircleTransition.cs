using UnityEngine;

public class CircleTransition : ATableTransition
{
    readonly int scaleID = Shader.PropertyToID("_Scale");
    Material _material;

    public override void StartTransition()
    {
        _material = GetComponent<MeshRenderer>().sharedMaterial;
        _material.SetFloat(scaleID, 10f);
    }

    public override void UpdateTransition(float p_percent)
    {
        _material.SetFloat(scaleID, Mathf.Lerp(8.0f, 0.5f, p_percent));
    }
}
