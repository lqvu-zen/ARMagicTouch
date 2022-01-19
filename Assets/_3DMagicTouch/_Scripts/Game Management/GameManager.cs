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
    public IntVariable score;
    public bool isDead;

    void Awake()
    {
        isDead = false;
        instance = this;
        SceneManager.LoadSceneAsync((int)SceneIndexes.START_SCREEN, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame(){
        isDead = false;
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.START_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.AR, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MENU, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.DETECT, LoadSceneMode.Additive));
        healthPlayer.gameObject.SetActive(true);
        StartCoroutine(GetSceneLoadingProgress());
    }

    public void Reload()
    {
        isDead = false;
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.AR));
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.DETECT));
        StartCoroutine(GetSceneLoadingProgress());
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.AR, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.DETECT, LoadSceneMode.Additive));
        healthPlayer.Reset();
        score.value = 0;
        healthPlayer.gameObject.SetActive(true);
        StartCoroutine(GetSceneLoadingProgress());
    }

    public void LoadTutorial()
    {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.START_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MENU, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.DETECT, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.TUTORIAL, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadingProgress());
    }

    public void LoadStartScreen()
    {
        loadingScreen.SetActive(true);
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            if (SceneManager.GetSceneAt(i).buildIndex != (int)SceneIndexes.GAME)
            {
                scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i)));
            }
        }
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.START_SCREEN, LoadSceneMode.Additive));
        healthPlayer.gameObject.SetActive(false);
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
