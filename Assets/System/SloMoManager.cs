using UnityEngine;
using System.Collections;

public class SloMoManager : MonoBehaviour
{
	public FollowTarget followCamera;
	private bool _isSlowed = false;
	public  bool  isSlowed
	{
		get { return _isSlowed; }
		set { if(value) SlowMo(); else NormalMo(); }
	}

	void Start()
	{
		// Setup references
		if (!followCamera)
		{
			followCamera = Camera.main.GetComponent<FollowTarget>();
		}
	}

	public void SlowMo()
	{
		_isSlowed = true;
		followCamera.ZoomIn();
		Time.timeScale = 0.2f;
	}

	public void NormalMo()
	{
		_isSlowed = false;
		followCamera.ZoomReset();
		Time.timeScale = 1f;
	}
}
