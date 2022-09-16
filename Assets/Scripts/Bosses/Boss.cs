using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{


    [SerializeField] BossBase boss;
    [SerializeField] int targetSize;
    public BossBase Base => boss;

    SpriteRenderer sprite_renderer;


    

    public void Awake()
    {
        sprite_renderer = GetComponentInParent<SpriteRenderer>();


    }
    public void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex < PlayerStats.Instance.Reached)
            Destroy(gameObject);
        else
        {
            sprite_renderer.sprite = boss.NPCSprite;
            transform.localScale = new Vector3(1, 1, 1);


            var bounds = GetComponent<SpriteRenderer>().sprite.bounds;
            var factor = targetSize / bounds.size.y;
            transform.localScale = new Vector3(factor, factor, factor);


            //sprite_renderer.enabled = true;
            gameObject.GetComponent<BoxCollider2D>().size = sprite_renderer.sprite.bounds.size;
        }
    }
}

