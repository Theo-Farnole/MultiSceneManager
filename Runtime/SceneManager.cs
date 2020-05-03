﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TF.SceneManager
{
    public delegate void OnSceneActivation();

    /// <summary>
    /// SceneManager that load scene w/ logics scenes.
    /// </summary>
    public static class SceneManager
    {
        #region Fields
        public static OnSceneActivation OnSceneActivation;        

        private static SceneManagerData _data;

        private static List<AsyncOperation> _asyncLoad = new List<AsyncOperation>();
        private static List<AsyncOperation> _asyncUnload = new List<AsyncOperation>();
        #endregion

        #region Properties
        public static SceneManagerData Data
        {
            get => SceneManagerData.Instance;
        }
        #endregion

        #region Methods
        #region Public Methods
        /// <summary>
        /// Reload current scene with logic scenes.
        /// </summary>
        public static void ReloadScene()
        {
            LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Reload current scene with logic scenes asynchronously.
        /// </summary>
        public static void ReloadSceneAsync()
        {
            LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }


        /// <summary>
        /// Load scene with logic scenes.
        /// </summary>
        /// <param name="level">Level to load</param>
        public static void LoadScene(string level)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level);

            for (int i = 0; i < Data.LogicScenesNames.Length; i++)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(_data.LogicScenesNames[0], UnityEngine.SceneManagement.LoadSceneMode.Additive);
            }
        }

        /// <summary>
        /// Load scene with logic scenes asynchronously.
        /// </summary>
        /// <param name="level">Level to load</param>
        public static void LoadSceneAsync(string level)
        {
            // load LEVEL
            UnityEngine.SceneManagement.SceneManager.LoadScene(level);

            // async load GAME_LOGIC
            _asyncLoad.Clear();

            for (int i = 0; i < Data.LogicScenesNames.Length; i++)
            {
                var ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_data.LogicScenesNames[0], UnityEngine.SceneManagement.LoadSceneMode.Additive);

                _asyncLoad.Add(ao);
                _asyncLoad[i].allowSceneActivation = false;
            }
        }

        /// <summary>
        /// Allow activation of scenes.
        /// </summary>
        public static void AllowScenesActivation()
        {
            // load scenes
            for (int i = 0; i < _asyncLoad.Count; i++)
            {
                _asyncLoad[i].allowSceneActivation = true;
            }

            // ... then unload old scenes
            for (int i = 0; i < _asyncUnload.Count; i++)
            {
                _asyncUnload[i].allowSceneActivation = true;
            }

            OnSceneActivation?.Invoke();
        }
        #endregion
        #endregion
    }
}
