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
#if UNITY_EDITOR
        [SerializeField] private SceneAsset[] _assetsAddionalScenes = new SceneAsset[0];
#endif

        [SerializeField, HideInInspector] private string[] _additionalScenes = new string[0];
        public string[] AdditionalScenes { get => _additionalScenes; }
        #endregion

        #region Methods
        /// <summary>
        /// Returns every scenes' name the sceneToLoad needs.
        /// </summary>
        public string[] GetSceneNeeds(string sceneToLoad)
        {
            // Later, we will use this method to add differents requirement for unique levels.
            return _additionalScenes;
        }        

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
            _additionalScenes = _assetsAddionalScenes.Select(x => x.name).ToArray();
        }
#endif
        #endregion
        #endregion
    }
}

