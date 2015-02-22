﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GUIWindow : MonoBehaviour
{
	public Animator windowAnimator;
	public GameObject selectedObject;
	private bool _isVisible;
	public  bool  isVisible
	{
		set{ if( value ) Show(); else Hide(); }
		get{ return _isVisible; }
	}

	// Use this for initialization
	void Start ()
	{
		if( !windowAnimator )
		{
			windowAnimator = GetComponent<Animator>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show()
	{
		if( !_isVisible )
		{
			_isVisible = true;

			if( windowAnimator )
				windowAnimator.SetBool("visible", true );
		}
	}

	public void Hide()
	{
		if( _isVisible )
		{
			_isVisible = false;

			if( windowAnimator )
				windowAnimator.SetBool("visible", false );
		}
	}

	public virtual void OnShow()
	{
		GainInputFocus();
	}

	private void GainInputFocus()
	{
		if( selectedObject )
			EventSystem.current.SetSelectedGameObject( selectedObject );
	}
}
