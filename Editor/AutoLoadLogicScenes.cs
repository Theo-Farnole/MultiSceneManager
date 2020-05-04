using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TF.MultiSceneManager.Editor
{
    [InitializeOnLoad]
    class AutoLoadLogicScenes
    {
        static AutoLoadLogicScenes()
        {
            Debug.LogFormat("<color=yellow>MultiScene</color> # Automatic loading initalized.");

            EditorSceneManager.sceneOpened += EditorMultiSceneManager.LoadScene;
        }
    }
}