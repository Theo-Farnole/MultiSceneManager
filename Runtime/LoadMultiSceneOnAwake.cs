using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif

namespace TF.MultiSceneManager
{
    public class LoadMultiSceneOnAwake : MonoBehaviour
#if UNITY_EDITOR
        , IPreprocessBuildWithReport
#endif
    {
#if UNITY_EDITOR
        [SerializeField] private UnityEditor.SceneAsset _sceneToLoad;
#endif

        [SerializeField, HideInInspector] private string _sceneToLoadName;


        void Awake()
        {
            MultiSceneManager.LoadScene(_sceneToLoadName);
        }

#if UNITY_EDITOR
        void OnValidate() => SceneAssetToName();

        int IOrderedCallback.callbackOrder => 1;
        void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report) => SceneAssetToName();

        void SceneAssetToName()
        {
            if (_sceneToLoad != null)
            {
                _sceneToLoadName = _sceneToLoad.name;
            }
        }
#endif
    }
}
