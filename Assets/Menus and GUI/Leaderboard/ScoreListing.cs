using UnityEngine;


//--------------------------------------------------------------
/// <summary> A list GUI element that holds score fields </summary>
//--------------------------------------
public class ScoreListing : MonoBehaviour
{
    //--------------------------------------------------------------
    #region Inspector properties
    //--------------------------------------
    [SerializeField] ScoreLine _headerLine  = null;
	[SerializeField] ScoreLine _scoreLine   = null;
	[SerializeField] int _numberOfRecords   = 0;
    #endregion



    //--------------------------------------------------------------
    /// <summary> Gets the number of records in this list </summary>
    //--------------------------------------
    public int numberOfRecords
	{
		get
		{
			return _numberOfRecords;
		}
	}



    //--------------------------------------------------------------
    /// <summary> Gets individual scoreLines 
    ///           by indexing ScoreListing </summary>
    //--------------------------------------
    ScoreLine[] scoreLines;
	public ScoreLine this[int i]
	{
		get { return scoreLines[i]; }
	}



    //--------------------------------------------------------------
    /// <summary> Initialises the ScoreListing </summary>
    //--------------------------------------
    void Start ()
	{
		// Space for all entries + the header
		scoreLines = new ScoreLine[_numberOfRecords+1];
		scoreLines[0] = Instantiate<ScoreLine>(_headerLine);
		
		for( int i=1; i<=_numberOfRecords; i++)
		{
			scoreLines[i] = Instantiate<ScoreLine>(_scoreLine);
		}

		// Parent everything
		for( int i=0; i<=_numberOfRecords; i++)
		{
			scoreLines[i].transform.SetParent( transform );
		}
	}
}
