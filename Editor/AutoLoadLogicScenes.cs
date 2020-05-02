using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TF.SceneManager.Editor
{
    [InitializeOnLoad]
    class AutoLoadLogicScenes
    {
        static AutoLoadLogicScenes()
        {
            EditorSceneManager.sceneOpened += LoadGameplay;
            Debug.LogFormat("<color=yellow>Auto Load</color> # Plugin initalized...");
        }

        private static void LoadGameplay(Scene loadedScene, OpenSceneMode mode)
        {
            bool isLoadedSceneIsGameplay = SceneManager.Data.LogicScenesNames.Contains(loadedScene.name);

            if (isLoadedSceneIsGameplay)
                return;

            string[] logicScenesNames = SceneManager.Data.LogicScenesNames;
            string path = SceneManager.Data.ScenePath;

            for (int i = 0; i < logicScenesNames.Length; i++)
            {
                var logicScene = EditorSceneManager.OpenScene(path + "/" + logicScenesNames[i] + ".unity", OpenSceneMode.Additive);
                EditorSceneManager.MoveSceneBefore(logicScene, loadedScene);
            }
        }
    }
}