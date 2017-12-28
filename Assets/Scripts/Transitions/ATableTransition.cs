using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class to implement a custom transition
/// The duration is set by the TransitonManager when we add a new transition
/// </summary>
public abstract class ATableTransition : MonoBehaviour {

    float _transitionTimer;
    float _duration = 3.0f;

    void Start() {
        _transitionTimer = 0.0f;
        StartTransition();
        StartCoroutine(UpdateTransitionTimer());
    }

    IEnumerator UpdateTransitionTimer() {
        while (_transitionTimer < _duration) {
            _transitionTimer += Time.deltaTime;
            UpdateTransition(Mathf.Clamp(_transitionTimer / _duration, 0.0f, 1.0f));
            yield return new WaitForEndOfFrame();
        }
        GetComponentInParent<TransitionManager>().EndTransition();
        Destroy(gameObject);
    }

    public abstract void StartTransition();
    public abstract void UpdateTransition(float p_percent);

    public float Duration {
        get { return _duration; }
        set { _duration = value; }
    }
}
