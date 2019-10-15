using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This code mainly come from the UnityChan asset
public class UpdateUnityChanAnimation : MonoBehaviour
{
	public float _interval = 2f;

	private Animator _animator;
	private AnimatorStateInfo _currentState;
	private AnimatorStateInfo _previousState;

    void Start()
    {
		_animator = GetComponent<Animator> ();
		_currentState = _animator.GetCurrentAnimatorStateInfo(0);
		_previousState = _currentState;
		StartCoroutine(RandomChange());
    }

    void Update()
    {
		if (_animator.GetBool ("Next"))
        {
			_currentState = _animator.GetCurrentAnimatorStateInfo(0);
			if (_previousState.fullPathHash != _currentState.fullPathHash)
            {
				_animator.SetBool ("Next", false);
				_previousState = _currentState;				
			}
		}
    }
    
	IEnumerator RandomChange()
	{
		while (true)
        {
			_animator.SetBool("Next", true);
			yield return new WaitForSeconds (_interval);
		}
	}
}
