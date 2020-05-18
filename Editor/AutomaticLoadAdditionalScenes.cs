using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.Build.Reporting;

#if UNITY_EDITOR
using UnityEditor.Build;
#endif

namespace TF.MultiSceneManager.Editor
{
    [InitializeOnLoad]
    class AutomaticLoadAdditionalScenes
#if UNITY_EDITOR
        : IPreprocessBuildWithReport, IPostprocessBuildWithReport
#endif
    {
        static AutomaticLoadAdditionalScenes()
        {
            Debug.LogFormat("<color=yellow>MultiScene</color> # Automatic loading initalized.");

            EditorSceneManager.sceneOpened += EditorSceneManager_sceneOpened;
        }


        private static void EditorSceneManager_sceneOpened(Scene scene, OpenSceneMode mode)
        {
            // prevent scene loading twice
            if (Application.isPlaying || _isBuilding)
                return;

            EditorMultiSceneManager.LoadScene(scene, mode);
        }


        // We need to check if the Editor is building
        // Because without it, EditorMultiSceneManager is called
        // and so it include additionals in build
        public int callbackOrder => 1;
        public static bool _isBuilding = false;

        public void OnPostprocessBuild(BuildReport report)
        {
            Debug.Log("_isBuilding = false");
            _isBuilding = false;
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("_isBuilding = true");
            _isBuilding = true;
        }
    }
}