using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TF.SceneManager.Editor
{
    [CustomPropertyDrawer(typeof(FolderBrowserAttribute))]
    public class FolderBrowserDrawer : PropertyDrawer
    {
        private const int BROWSE_BUTTON_SIZE = 150;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                GUI.Label(position, "FolderBrowser can only be the attribute of a string.");
                return;
            }

            DrawProperty(position, property);
        }

        void DrawProperty(Rect position, SerializedProperty property)
        {
            Rect rectButton = new Rect(position.x, position.y, BROWSE_BUTTON_SIZE, EditorGUIUtility.singleLineHeight);
            Rect rectPath = new Rect(position.x + BROWSE_BUTTON_SIZE + 10, position.y, position.width - BROWSE_BUTTON_SIZE - 10, EditorGUIUtility.singleLineHeight);

            // draw
            var buttonClicked = GUI.Button(rectButton, "Browse");
            property.stringValue = GUI.TextField(rectPath, property.stringValue);

            // reaction
            if (buttonClicked)
            {
                string absolutePath = property.stringValue;
                string relativePath = AbsolutePathToRelative(absolutePath);

                absolutePath = EditorUtility.SaveFolderPanel("Browsing", property.stringValue, Application.dataPath);

                property.stringValue = AbsolutePathToRelative(absolutePath);
            }
        }

        public string RelativePathToAbsolute(string relativePath)
        {
            return relativePath.Insert(0, Application.dataPath + @"/");
        }

        public string AbsolutePathToRelative(string absolutePath)
        {
            return absolutePath.Replace(Application.dataPath + @"/", "").Insert(0, "Assets/");
        }
    }
}
