using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Transform Sprite;

    public float speed;

    public float minsize, maxsize;

    private float activesize;
    // Start is called before the first frame update
    void Start()
    {
        activesize = maxsize;
        speed = speed * Random.Range(0.8f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        Sprite.localScale = Vector3.MoveTowards(Sprite.localScale, Vector3.one * activesize, speed*Time.deltaTime);

        if (Sprite.localScale.x==activesize)
        {
            if (activesize==maxsize)
            {
                activesize = minsize;
            }
            else
            {
                activesize = maxsize;
            }
        }
        
        
    }
}
