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

	public void SlowMo()
	{
        if (!followCamera)
        {
            followCamera = ReferenceManager.GetCamera().GetComponent<FollowTarget>();
        }

		_isSlowed = true;
		//followCamera.ZoomIn();
		Time.timeScale = 0.2f;
	}

	public void NormalMo()
	{
        if (!followCamera)
        {
            followCamera = ReferenceManager.GetCamera().GetComponent<FollowTarget>();
        }

		_isSlowed = false;
		followCamera.ZoomReset();
		Time.timeScale = 1f;
	}
}
