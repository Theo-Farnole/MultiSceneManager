using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnitySceneManager = UnityEngine.SceneManagement;

namespace TF.SceneManager
{
    public delegate void OnSceneActivation();

    /// <summary>
    /// SceneManager that load scene w/ logics scenes.
    /// </summary>
    public static class SceneManager
    {
        #region Fields
        private static bool _allowSceneActivation = false;

        private static List<AsyncOperation> _asyncLoad = new List<AsyncOperation>();
        private static List<AsyncOperation> _asyncUnload = new List<AsyncOperation>();
        #endregion

        #region Properties
        public static SceneManagerData SceneManagerData { get => SceneManagerData.Instance; }

        public static bool AllowSceneActivation
        {
            get => _allowSceneActivation; set
            {
                _allowSceneActivation = value;
                UpdateAsyncOperation_AllowSceneActivation();
            }
        }
        #endregion

        #region Methods
        #region Public Methods
        /// <summary>
        /// Reload current scene with logic scenes.
        /// </summary>
        public static void ReloadScene()
        {
            LoadScene(UnitySceneManager.SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Reload current scene with logic scenes asynchronously.
        /// </summary>
        public static void ReloadSceneAsync()
        {
            LoadSceneAsync(UnitySceneManager.SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Load scene with logic scenes.
        /// </summary>
        /// <param name="level">Level to load</param>
        public static void LoadScene(string level)
        {
            UnitySceneManager.SceneManager.LoadScene(level);

            for (int i = 0; i < SceneManagerData.LogicScenesNames.Length; i++)
            {
                UnitySceneManager.SceneManager.LoadScene(SceneManagerData.LogicScenesNames[0], UnitySceneManager.LoadSceneMode.Additive);
            }
        }

        /// <summary>
        /// Load scene with logic scenes asynchronously.
        /// </summary>
        /// <param name="level">Level to load</param>
        public static void LoadSceneAsync(string level)
        {
            // load 
            UnitySceneManager.SceneManager.LoadScene(level);

            // async load GAME_LOGIC
            _asyncLoad.Clear();

            for (int i = 0; i < SceneManagerData.LogicScenesNames.Length; i++)
            {
                var ao = UnitySceneManager.SceneManager.LoadSceneAsync(SceneManagerData.LogicScenesNames[0], UnitySceneManager.LoadSceneMode.Additive);
                _asyncLoad.Add(ao);
            }

            UpdateAsyncOperation_AllowSceneActivation();
        }

        static void UpdateAsyncOperation_AllowSceneActivation()
        {
            foreach (var ao in _asyncLoad)
            {
                ao.allowSceneActivation = _allowSceneActivation;
            }

            foreach (var ao in _asyncUnload)
            {
                ao.allowSceneActivation = _allowSceneActivation;
            }
        }
        #endregion
        #endregion
    }
}
