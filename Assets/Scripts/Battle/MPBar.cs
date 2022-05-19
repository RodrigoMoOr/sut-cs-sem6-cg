using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBar : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject mana;

    public void SetMP(float value)
    {

        mana.transform.localScale = new Vector3(value, 1f);

    }
}
