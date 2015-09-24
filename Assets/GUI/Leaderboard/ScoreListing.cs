/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                       ScoreListing.cs                       </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    14-May-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;


    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> A list GUI element that holds score fields </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public class ScoreListing : MonoBehaviour
    {
        //..............................................................
        #region            //  INSPECTOR SETTINGS  //
        //--------------------------------------------------------------
        [SerializeField, Tooltip
        ( "A reference to a ScoreLine template prefab for the header.")]
        //--------------------------------------
        ScoreLine _headerLine = null;



        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (  "A reference to a ScoreLine template prefab for the lines.")]
        //--------------------------------------
        ScoreLine _scoreLine = null;



        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (  "Number of records per page."                              )]
        //--------------------------------------
        int _numberOfRecords = 0;
        #endregion
        //......................................



        //..............................................................
        #region           //  METHODS AND PROPERTIES //
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
        void Start()
        {
            // Space for all entries + the header
            scoreLines = new ScoreLine[_numberOfRecords + 1];
            scoreLines[0] = Instantiate<ScoreLine>(_headerLine);

            for (int i = 1; i <= _numberOfRecords; i++)
            {
                scoreLines[i] = Instantiate<ScoreLine>(_scoreLine);
            }

            // Parent everything
            for (int i = 0; i <= _numberOfRecords; i++)
            {
                scoreLines[i].transform.SetParent(transform);
            }
        }
        #endregion
        //......................................
    }
}
