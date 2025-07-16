using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    [Header("Player Prefabs")]
    public GameObject Tida;
    public GameObject LithaAvatar;



    //public GameObject SpawnedPlayer { get; private set; }

    //public delegate void PlayerSpawnedHandler();
    //public event PlayerSpawnedHandler OnPlayerSpawned;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Loads a scene by name. You can expand this to include saving/loading player data.
    /// </summary>
    /// <param name="sceneName">Target scene's name</param>
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
            yield return null;

        SwitchControls(sceneName);

        //SpawnPlayer(sceneName);
    }


    private void SwitchControls(string sceneName) {
        Vector3 spawnPosition = Vector3.zero;

        // Choose the right prefab based on scene name or type
        bool isUniverseMap = sceneName.Contains("UniverseMap");  // Or use a lookup from GameManager
        GameObject playerPrefab = isUniverseMap ? LithaAvatar : Tida;

        if (isUniverseMap) 
        {
            Transform Tida = transform.Find("Tida");
            Transform LithaAvatar = transform.Find("Tida");

            Tida.gameObject.SetActive(false);
            LithaAvatar.gameObject.SetActive(true);               
        }
        else
        {
            Tida.SetActive(true);
            LithaAvatar.SetActive(false);
        }

    }
    //private void SpawnPlayer(string sceneName)
    //{
    //    Vector3 spawnPosition = Vector3.zero;

    //    // Choose the right prefab based on scene name or type
    //    bool isUniverseMap = sceneName.Contains("UniverseMap");  // Or use a lookup from GameManager
    //    GameObject playerPrefab = isUniverseMap ? LithaAvatar : Tida;

    //    // Actually spawn the player
    //    if (GameObject.FindWithTag("Player") == null)
    //    {
    //        Instantiate(playerPrefab, spawnPosition, Quaternion.identity, transform);
    //    }
    //    OnPlayerSpawned?.Invoke();
    //}
}

