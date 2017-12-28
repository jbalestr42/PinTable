using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage transiton, allow to add a new transiton to a different texture
/// </summary>
public class TransitionManager : MonoBehaviour {

    readonly int mainTexID = Shader.PropertyToID("_MainTex");
    readonly int nextTexID = Shader.PropertyToID("_NextTex");

    CreateTable _table;
    Texture _lastTexture;
    bool _isInTransition = false;

    public enum ETransitionType {
        Horizontal,
        Circle
    }

	void Start () {
        _table = GetComponentInParent<CreateTable>();
        _table._pintableMat.SetTexture(mainTexID, null);
        _table._pintableMat.SetTexture(nextTexID, null);
    }

    /// <summary>
    /// This method is called when the transition is over in order to set the new texture as current texture in the material
    /// </summary>
    public void EndTransition() {
        _table._pintableMat.SetTexture(mainTexID, _lastTexture);
        _table._pintableMat.SetTexture(nextTexID, null);
        _isInTransition = false;
    }

    /// <summary>
    /// Create a transition that will go from the current texture to the given texture
    /// </summary>
    /// <param name="p_transitionType">The transition type to be instantiated</param>
    /// <param name="p_texture">The new texture to display once the transition is finished</param>
    /// <param name="p_duration">The duration of the transition</param>
    public void CreateTransition(ETransitionType p_transitionType, Texture p_texture, float p_duration) {
        if (!_isInTransition && p_texture != _lastTexture) {
            _isInTransition = true;
            GameObject transition = null;

            switch (p_transitionType) {
                case ETransitionType.Horizontal:
                    transition = Instantiate(Resources.Load("Prefabs/HorizontalTransition")) as GameObject;
                    break;

                case ETransitionType.Circle:
                    transition = Instantiate(Resources.Load("Prefabs/CircleTransition")) as GameObject;
                    break;

                default:
                    break;
            }

            if (transition != null) {
                transition.transform.SetParent(transform, false);
                transition.GetComponent<ATableTransition>().Duration = p_duration;
                _table._pintableMat.SetTexture(nextTexID, p_texture);
                _lastTexture = p_texture;
            }
        }
    }
}
