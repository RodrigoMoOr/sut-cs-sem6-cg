using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int amountOfPointsToGo = -1;
    [SerializeField] ItemBase requiredItem = null;
    bool isLoading = false;
    bool isNotified = false;
    bool isNotifiedAboutLackOfItem = false;
    Fader fader;
    PlayerController player;
    private void Start()
    {
       fader =  FindObjectOfType<Fader>();
    }
    public void OnTrigger(PlayerController player)
    {
        if(requiredItem != null)
        {
            if (isNotifiedAboutLackOfItem) return;
            if(!Inventory.GetInventory().CheckIfExists(requiredItem))
            {
                isNotifiedAboutLackOfItem = true;
                StartCoroutine(DialogManager.Instance.ShowDialogText("You haven't got your student ID, so you cant enter floors"));
                StartCoroutine(NotificationCooldown(15f));
                return;
            }
        }
        if (isLoading || isNotified || isNotifiedAboutLackOfItem) return;
        this.player = player;
        if (amountOfPointsToGo > PlayerStats.Instance.ECTS)
        {
            isNotified = true;
            Debug.Log("Not enough ECTS");
            StartCoroutine(DialogManager.Instance.ShowDialogText("I am not ready to go there yet"));
            StartCoroutine(NotificationCooldown(5f));

        }
        else
        {
            isLoading = !isLoading;

            StartCoroutine(SwitchScene(player));
        }
    }

    IEnumerator NotificationCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        isNotified = false;
        isNotifiedAboutLackOfItem = false;
    }

    IEnumerator SwitchScene(PlayerController player)
    {
        DontDestroyOnLoad(gameObject);
        yield return fader.FadeIn(0.5f);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        
        var destination  = FindObjectsOfType<Portal>().First(x=>x!=this);
        player.transform.position = destination.SpawnPoint.position;

        yield return fader.FadeOut(0.5f);



        isLoading = !isLoading;
        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;
}
