using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manage transiton, allow to add a new transiton to a different texture
/// </summary>
public class PinTableTransitionManager : MonoBehaviour
{
    readonly int NextTexID = Shader.PropertyToID("_NextTex");
    readonly int CurrentTexID = Shader.PropertyToID("_CurrentTex");
    
    public UnityEvent OnTransitionStartEvent;
    public UnityEvent OnTransitionEndEvent;

    private CreateTable _table = null;
    private Texture _lastTexture = null;
    private bool _isInTransition = false;

	void Start()
    {
        _table = GetComponentInParent<CreateTable>();
        _table.PintableMat.SetTexture(NextTexID, null);
        _table.PintableMat.SetTexture(CurrentTexID, null);
        OnTransitionStartEvent = new UnityEvent();
        OnTransitionEndEvent = new UnityEvent();
    }

    /// <summary>
    /// This method is called when the transition is over in order to set the new texture as current texture in the material
    /// </summary>
    public void EndTransition()
    {
        _table.PintableMat.SetTexture(NextTexID, _lastTexture);
        _table.PintableMat.SetTexture(CurrentTexID, null);
        _isInTransition = false;
        OnTransitionEndEvent.Invoke();
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
                _table.PintableMat.SetTexture(CurrentTexID, p_texture);
                _lastTexture = p_texture;
                OnTransitionStartEvent.Invoke();
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
    
    /// <summary>
    /// Return The current texture
    /// </summary>
    public Texture GetCurrentTexture()
    {
        return _table.PintableMat.GetTexture(CurrentTexID);
    }
    
    /// <summary>
    /// Return the texture we are trqnsitioning to
    /// </summary>
    public Texture GetNextTexture()
    {
        return _table.PintableMat.GetTexture(NextTexID);
    }
}
