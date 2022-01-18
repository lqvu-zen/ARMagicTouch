using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loadingScreen;
    public Slider progressBar;
    float totalProgress;
    public FloatVariable bgmVolume;
    public AudioSource bgm;
    public DeadScreen deadScreen;
    public HealthPlayer healthPlayer;
    public bool isDead;

    void Awake()
    {
        isDead = false;
        instance = this;
        SceneManager.LoadSceneAsync((int)SceneIndexes.START_SCREEN, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame(){
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.START_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.AR, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MENU, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.DETECT, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadingProgress());
    }

    public void Reload()
    {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.AR));
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.DETECT));
        StartCoroutine(GetSceneLoadingProgress());
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.AR, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.DETECT, LoadSceneMode.Additive));
        healthPlayer.Reset();
        StartCoroutine(GetSceneLoadingProgress());
    }


    IEnumerator GetSceneLoadingProgress()
    {
        for (int i = 0; i < scenesLoading.Count; ++i)
        {
            while (!scenesLoading[i].isDone)
            {
                int cnt = 0;
                foreach (AsyncOperation asyncOperation in scenesLoading)
                {
                    cnt +=  asyncOperation.isDone?1:0;
                }
                totalProgress = 1f*cnt/scenesLoading.Count;
                progressBar.value = totalProgress;
                yield return null;
            }
        }
        loadingScreen.SetActive(false);
        healthPlayer.gameObject.SetActive(true);
    }

    void Update()
    {
        bgm.volume = bgmVolume.value;
    }

    [ContextMenu("dead")]
    public void OnDead()
    {
        deadScreen.TurnOn();
        isDead = true;
    }
}
