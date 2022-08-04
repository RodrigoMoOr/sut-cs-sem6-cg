using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IPlayerTriggerable
{

    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int amountOfPointsToGo = -1;
    public void OnTrigger(PlayerController player)
    {
        if(amountOfPointsToGo <= PlayerStats.Instance.ECTS)
            StartCoroutine(SwitchScene(player));
        else Debug.Log("Not enough ECTS");
    }


    IEnumerator SwitchScene(PlayerController player)
    {
        DontDestroyOnLoad(gameObject);
        var destination  = FindObjectsOfType<Portal>().First(x=>x!=this);
        player.transform.position = destination.spawnPoint.position;

        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        Destroy(gameObject);
    }
}
