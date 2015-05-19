/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                   MinMaxPropertyDrawer.cs                   </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    18-May-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;
    using UnityEditor;


    [CustomPropertyDrawer(typeof(MinMax))]
    public class MinMaxPropertyDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Prepare sublables and tooltips
            var subLabels = new GUIContent[2];
            subLabels[0] = new GUIContent("≥", "The minimal value of " + label.text);
            subLabels[1] = new GUIContent("≤", "The maximal value of " + label.text);

            // Get property values
            var oldValues = new float[2];
            oldValues[0] = property.FindPropertyRelative("_min").floatValue;
            oldValues[1] = property.FindPropertyRelative("_max").floatValue;

            var newValues = (float[]) oldValues.Clone();

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.MultiFloatField(position, subLabels, newValues);

            // Sanity check values
            if (newValues[0] != oldValues[0])
            {
                if (newValues[0] > newValues[1])
                    newValues[0] = newValues[1];
            }
            else
            {
                if (newValues[1] < newValues[0])
                    newValues[1] = newValues[0];
            }

            // Set property values
            property.FindPropertyRelative("_min").floatValue = newValues[0];
            property.FindPropertyRelative("_max").floatValue = newValues[1];

            EditorGUI.EndProperty();
        }
    }
}
