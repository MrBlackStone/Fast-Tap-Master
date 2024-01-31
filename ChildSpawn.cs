using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawn : MonoBehaviour
{
    // Update is called once per frame
    private bool isReady = false;
    void Update()
    {
        if (Time.timeScale < 1) return;

        if (transform.childCount > 0) return;


        SpawnBetweenSpace.i.respawnObject(transform);
        if (transform.childCount > 0 && !isReady)
        {
            StartCoroutine(nameof(startObject));
            isReady = true;
        }
    }

    IEnumerator startObject()
    {
        yield return new WaitForSeconds(.25f);
        if (transform.GetChild(0) == null)
        {
            Debug.Log("false");
        }
        else 
        transform.GetChild(0).gameObject.SetActive(true);
        isReady = false;
    }

}
