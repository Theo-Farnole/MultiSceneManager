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
            EditorSceneManager.sceneOpened += LoadGameplay;
            Debug.LogFormat("<color=yellow>Auto Load</color> # Plugin initalized...");
        }

        private static void LoadGameplay(Scene loadedScene, OpenSceneMode mode)
        {
            bool isLoadedSceneIsGameplay = MultiSceneManager.SceneManagerData.LogicScenesNames.Contains(loadedScene.name);

            if (isLoadedSceneIsGameplay)
                return;

            string[] logicScenesNames = MultiSceneManager.SceneManagerData.LogicScenesNames;
            string path = MultiSceneManager.SceneManagerData.ScenePath;

            for (int i = 0; i < logicScenesNames.Length; i++)
            {
                var logicScene = EditorSceneManager.OpenScene(path + "/" + logicScenesNames[i] + ".unity", OpenSceneMode.Additive);
                EditorSceneManager.MoveSceneBefore(logicScene, loadedScene);
            }
        }
    }
}