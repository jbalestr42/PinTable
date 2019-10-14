using UnityEngine;

/// <summary>
/// Manage transiton, allow to add a new transiton to a different texture
/// </summary>
public class PinTableTransitionManager : MonoBehaviour
{
    readonly int MainTexID = Shader.PropertyToID("_MainTex");
    readonly int NextTexID = Shader.PropertyToID("_NextTex");

    CreateTable _table = null;
    Texture _lastTexture = null;
    bool _isInTransition = false;

	void Start()
    {
        _table = GetComponentInParent<CreateTable>();
        _table.PintableMat.SetTexture(MainTexID, null);
        _table.PintableMat.SetTexture(NextTexID, null);
    }

    /// <summary>
    /// This method is called when the transition is over in order to set the new texture as current texture in the material
    /// </summary>
    public void EndTransition()
    {
        _table.PintableMat.SetTexture(MainTexID, _lastTexture);
        _table.PintableMat.SetTexture(NextTexID, null);
        _isInTransition = false;
    }

    /// <summary>
    /// Create a transition that will go from the current texture to the given texture
    /// </summary>
    /// <param name="p_transitionType">The transition type to be instantiated</param>
    /// <param name="p_texture">The new texture to display once the transition is finished</param>
    /// <param name="p_duration">The duration of the transition</param>
    public void CreateTransition(GameObject p_transitionPrefab, Texture p_texture, float p_duration)
    {
        if (!_isInTransition && p_texture != _lastTexture)
        {
            _isInTransition = true;
            GameObject transition = Instantiate(p_transitionPrefab);

            if (transition != null && transition.GetComponent<APinTableTransition>() != null)
            {
                transition.transform.SetParent(transform, false);
                transition.GetComponent<APinTableTransition>().Duration = p_duration;
                _table.PintableMat.SetTexture(NextTexID, p_texture);
                _lastTexture = p_texture;
            }
        }
    }
    
    /// <summary>
    /// Return whether a transition is in progress or not.
    /// </summary>
    public bool IsTransitionInProgress()
    {
        return _isInTransition;
    }
}
