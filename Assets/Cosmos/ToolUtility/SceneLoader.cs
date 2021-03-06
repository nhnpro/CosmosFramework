﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Cosmos {
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            Facade.Instance.LoadScene(sceneName);
        }
        public void LoadScene(int sceneIndex)
        {
            Facade.Instance.LoadScene(sceneIndex);
        }
    }
}