using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneLoaderAsync : MonoBehaviour
{
        public GameObject overlayBackground; 
        public GameObject overlayLoadingText;

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
            //Enable loading screen
            overlayBackground.SetActive(true);
            overlayLoadingText.SetActive(true);

            GameObject centerEyeAnchor = GameObject.Find("CenterEyeAnchor");
            overlayLoadingText.gameObject.transform.position = centerEyeAnchor.gameObject.transform.position + new Vector3(0f,0f,3f);

            //Debug Wait
            //yield return new WaitForSeconds(5f);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            //Disabling again
            overlayBackground.SetActive(false);
            overlayLoadingText.SetActive(false);
        }
}
