using System.Collections;
using System.Collections.Generic;
using TF.MultiSceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSceneActiveOnAwake : MonoBehaviour
{
    void Start()
    {        
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);

            bool isAdditional = MultiSceneManagerData.Instance.IsSceneAdditional(scene.name);
            if (!isAdditional)
            {
                SceneManager.SetActiveScene(scene);
                Debug.LogFormat("Set {0} as active scene.", scene.name);
            }
        }
    }
}
