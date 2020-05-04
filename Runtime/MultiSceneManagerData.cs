using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Scripting;

namespace TF.MultiSceneManager
{
    public class MultiSceneManagerData : SingletonScriptableObject<MultiSceneManagerData>, IPreprocessBuildWithReport
    {
        #region Fields
        [SerializeField, FolderBrowser] private string _scenePath = "Assets/Scenes/";
        [Space]
#if UNITY_EDITOR
        [SerializeField] private SceneAsset[] _logicScenesAssets = new SceneAsset[0];
#endif  

        [SerializeField, HideInInspector] private string[] _logicScenesNames = new string[0];
        #endregion

        #region Properties
        public string ScenePath { get => _scenePath; }
        public string[] LogicScenesNames { get => _logicScenesNames; }
        #endregion

        #region Methods
        #region SceneAssets to scenes' name
#if UNITY_EDITOR
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            ScenesAssetsToName();
        }

        void OnValidate()
        {
            ScenesAssetsToName();
        }

        void ScenesAssetsToName()
        {
            _logicScenesNames = _logicScenesAssets.Select(x => x.name).ToArray();
        }
#endif
        #endregion
        #endregion
    }
}

