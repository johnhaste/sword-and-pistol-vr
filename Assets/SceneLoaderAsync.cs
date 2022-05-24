using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoaderAsync : MonoBehaviour
{
        public static SceneLoaderAsync instance;
        private bool _loading = false;

        //Singleton
        private void Awake(){
            if(instance != null && instance != this){
                Destroy(this.gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Load(string sceneName)
        {
            if (_loading) return;
            _loading = true;
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private  IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
}
