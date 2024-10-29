using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MakePlatformsInvisible : MonoBehaviour
{
    public string invisiblePlatformTag = "InvisiblePlatform";
    private GameObject[] Platforms;

    // Start is called before the first frame update
    void Start()
    {
        Platforms = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject platform in Platforms)
        {
            // Set alpha of invisible platforms to 0.
            if(platform.layer == LayerMask.NameToLayer(invisiblePlatformTag))
            {
                Material pMat = platform.GetComponent<SpriteRenderer>().material;
                pMat.color = new Color (pMat.color.r, pMat.color.g, pMat.color.b, 0.0f);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
