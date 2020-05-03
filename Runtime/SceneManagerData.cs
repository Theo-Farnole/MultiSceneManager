using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Scripting;

namespace TF.SceneManager
{
    public class SceneManagerData : ScriptableObject, IPreprocessBuildWithReport
    {
        #region Fields
        public static readonly string filename = "SceneManager Data";

        [SerializeField, FolderBrowser] private string _scenePath = "Assets/Scenes/";
        [Space]
#if UNITY_EDITOR
        [SerializeField] private SceneAsset[] _logicScenesAssets = new SceneAsset[0];
#endif  

        [SerializeField, HideInInspector] private string[] _logicScenesNames = new string[0];

        // cache variable
        private static SceneManagerData _cachedSceneManager = null;
        #endregion

        #region Properties
        public string ScenePath { get => _scenePath; }
        public string[] LogicScenesNames { get => _logicScenesNames; }
        #endregion

        #region Methods
        #region Get or Create SceneManagerData 
        public static SceneManagerData GetOrCreateSceneManagerData()
        {
            // return cached scene manager
            if (_cachedSceneManager != null)
                return _cachedSceneManager;

            // load SceneManager
            SceneManagerData sceneManagerData = Resources.Load<SceneManagerData>(filename);

            // create one, or throw error
            if (sceneManagerData == null)
#if UNITY_EDITOR
                CreateSceneManagerData(out sceneManagerData, filename);
#else
                Debug.LogErrorFormat("No SceneManagerData file founded. Can't create one in Resources.");
#endif

            _cachedSceneManager = sceneManagerData;
            return sceneManagerData;
        }

#if UNITY_EDITOR
        private static void CreateSceneManagerData(out SceneManagerData sceneManagerData, string filename)
        {
            sceneManagerData = ScriptableObject.CreateInstance<SceneManagerData>();

            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");

            AssetDatabase.CreateAsset(sceneManagerData, filename + ".asset");
            AssetDatabase.SaveAssets();
        }
#endif
        #endregion

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

