using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZieluController : MonoBehaviour, Interactable
{

    [SerializeField] List<string> possibleJoke;

    [SerializeField] Sprite NPCDialogSprite;

    public IEnumerator Interact(Transform initiator)
    {
       
            yield return DialogManager.Instance.ShowDialogText(possibleJoke.PickRandom(), NPCDialogSprite);
    }
}
