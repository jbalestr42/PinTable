﻿using UnityEngine;

public class CircleTransition : APinTableTransition
{
    readonly int ScaleID = Shader.PropertyToID("_Scale");
    Material _material;

    public override void StartTransition()
    {
        _material = GetComponent<MeshRenderer>().sharedMaterial;
        _material.SetFloat(ScaleID, 10f);
    }

    public override void UpdateTransition(float p_percent)
    {
        _material.SetFloat(ScaleID, Mathf.Lerp(8.0f, 0.5f, p_percent));
    }
}
