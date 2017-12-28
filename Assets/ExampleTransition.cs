using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTransition : MonoBehaviour {

    public Texture _playerTexture;
    public Texture _noiseTexture;

    TransitionManager _transitionManager;

    void Start () {
        _transitionManager = GameObject.FindObjectOfType<TransitionManager>();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            _transitionManager.CreateTransition(TransitionManager.ETransitionType.Horizontal, _noiseTexture, 2.0f);
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            _transitionManager.CreateTransition(TransitionManager.ETransitionType.Circle, _playerTexture, 1.0f);
        }
    }
}
