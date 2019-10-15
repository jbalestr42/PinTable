using System.Collections.Generic;
using UnityEngine;

public class ExampleTransition : MonoBehaviour
{
    [System.Serializable]
    public struct TransitionData
    {
        public RenderTexture Texture;
        public GameObject Prefab;
        public float Duration;

        public TransitionData(RenderTexture p_texture, GameObject p_prefab, float p_duration)
        {
            Texture = p_texture;
            Prefab = p_prefab;
            Duration = p_duration;
        }
    }

    public List<TransitionData> _transitions;

    [SerializeField]
    public UnityEngine.UI.RawImage _debugCurrentImage;

    [SerializeField]
    public UnityEngine.UI.RawImage _debugNextImage;

    [SerializeField]
    public GameObject _debugUI;
    
    [SerializeField]
    public GameObject _controlUI;

    PinTableTransitionManager _transitionManager;
    int _currentIndex = 0;

    void Start()
    {
        _transitionManager = GameObject.FindObjectOfType<PinTableTransitionManager>();

        _transitionManager.OnTransitionStartEvent.AddListener(OnTransitionStart);
        _transitionManager.OnTransitionEndEvent.AddListener(OnTransitionEnd);
	}
	
	void Update()
    {
        if (!_transitionManager.IsTransitionInProgress())
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

        if (Input.GetKeyDown(KeyCode.G))
        {
            _debugUI.SetActive(!_debugUI.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            _controlUI.SetActive(!_controlUI.activeInHierarchy);
        }
    }

    void CreateTransition(TransitionData p_data)
    {
        _transitionManager.CreateTransition(p_data.Prefab, p_data.Texture, p_data.Duration);
    }
    
    void OnTransitionStart()
    {
        _debugNextImage.texture = _transitionManager.GetNextTexture();
        _debugCurrentImage.texture = _transitionManager.GetCurrentTexture();
    }

    void OnTransitionEnd()
    {
        _debugNextImage.texture = _transitionManager.GetNextTexture();
        _debugCurrentImage.texture = _transitionManager.GetCurrentTexture();
    }
}
