using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    SpriteRenderer sr;

    private bool ready;
    public bool isAwake;
    
    [SerializeField] public float timeToFade = 3f;
    [SerializeField] private bool stopCor;
    Transform pos;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        ready = false;
        isAwake = false;

        

        pos = transform;

        Color c = sr.material.color;
        c.a = 0f;
        sr.material.color = c;

    }

    private void Update()
    {
        if (isAwake && ready && Time.timeScale == 1)
        {
            Debug.Log("durdu");
            if (timeToFade <= 0)
            {
                FindObjectOfType<GameManager>().hideHeart();
                SpawnOnMe();
                Debug.Log("yok oluyorum");
                ready = false;
                isAwake = false;
            }
            else
            {
                timeToFade -= Time.deltaTime;
            }
        }
    }

    private void OnMouseDown()
    {


        if (isAwake && ready && Time.timeScale == 1)
        {
            GameManager.instance.IncreaseScore();
            Sounds.instance.playSound(1);
            
            SpawnOnMe();
            Debug.Log("bast�m");

        }

    }

    public IEnumerator fadeIn()
    {

        for (float f = 0.05f; f <= 1; f += 0.25f)
        {
            if (!sr)
            {
                Debug.Log("sprite im yok");
                break;
            }
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }


        isAwake = true;
        ready = true;
        yield break;

    }
    private void SpawnOnMe()
    {
        if (Time.timeScale == 0) return;
       // SpawnBetweenSpace.i.getObjbectTransform(transform);
        Destroy(gameObject);
        //if (Time.timeScale == 0) return;
        //GameObject BALL = FindObjectOfType<SpawnBetweenSpace>().ball[Random.Range(0, 7)];
        //Instantiate(BALL, pos.position, Quaternion.identity);

        //Destroy(gameObject);
    }




}
