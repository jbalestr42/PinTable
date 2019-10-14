using System.Collections.Generic;
using UnityEngine;

public class ExampleTransition : MonoBehaviour
{
    [System.Serializable]
    public struct TransitionData
    {
        public RenderTexture Texture;
        public TransitionManager.ETransitionType TransitionType;
        public float TransitionDuration;

        public TransitionData(RenderTexture p_texture, TransitionManager.ETransitionType p_type, float p_duration)
        {
            Texture = p_texture;
            TransitionType = p_type;
            TransitionDuration = p_duration;
        }
    }

    public List<TransitionData> _transitions;

    TransitionManager _transitionManager;
    int _currentIndex = 0;
    bool _isInitialized = false;

    void Start()
    {
        _transitionManager = GameObject.FindObjectOfType<TransitionManager>();
        if (_transitions.Count == 0)
        {
            Debug.LogError("There is no transition.");
        }
        else
        {
            _isInitialized = true;
        }
	}
	
	void Update()
    {
        if (_isInitialized && !_transitionManager.IsTransitionInProgress())
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _currentIndex--;
                if (_currentIndex < 0)
                {
                    _currentIndex = _transitions.Count - 1;
                }
                CreateTransition(_transitions[_currentIndex]);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _currentIndex++;
                if (_currentIndex >= _transitions.Count)
                {
                    _currentIndex = 0;
                }
                CreateTransition(_transitions[_currentIndex]);
            }
        }
    }

    void CreateTransition(TransitionData p_data)
    {
        _transitionManager.CreateTransition(p_data.TransitionType, p_data.Texture, p_data.TransitionDuration);
    }
}
