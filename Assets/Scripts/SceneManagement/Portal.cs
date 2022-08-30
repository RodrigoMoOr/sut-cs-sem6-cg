using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int amountOfPointsToGo = -1;
    bool isLoading = false;
    Fader fader;
    PlayerController player;
    private void Start()
    {
       fader =  FindObjectOfType<Fader>();
    }
    public void OnTrigger(PlayerController player)
    {
        this.player = player;
        if ((amountOfPointsToGo <= PlayerStats.Instance.ECTS) && !isLoading)
        { 
            isLoading = !isLoading;
            StartCoroutine(SwitchScene(player));
        }
        else Debug.Log("Not enough ECTS");
    }


    IEnumerator SwitchScene(PlayerController player)
    {
        DontDestroyOnLoad(gameObject);
        yield return fader.FadeIn(0.5f);
        Debug.Log("dupaaaa");
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        
        var destination  = FindObjectsOfType<Portal>().First(x=>x!=this);
        player.transform.position = destination.SpawnPoint.position;

        yield return fader.FadeOut(0.5f);



        isLoading = !isLoading;
        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;
}
