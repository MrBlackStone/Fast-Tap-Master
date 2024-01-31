using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBetweenSpace : MonoBehaviour
{
    public Transform container;
    public GameObject[] ball;
    public Vector3 startPos;
    [SerializeField] GameObject[] Obstacles;
    public int numberOfRows;
    public int objectsPerRow;
    public float spacing;
    public GameObject obj;
    [SerializeField] private int ballCount;

    [SerializeField] private Queue<Transform> transformsPool = new Queue<Transform>();
    public static SpawnBetweenSpace i { get; private set; }

    private void Awake()
    {
        i = this;
        for (int row = 0; row < numberOfRows; row++)
        {
            for (int col = 0; col < objectsPerRow; col++)
            {
                Vector3 startingpos = new Vector3(startPos.x + col * spacing, startPos.y - row * spacing, 1);
                 GameObject dba = Instantiate(obj, startingpos, Quaternion.identity);
                dba.AddComponent<ChildSpawn>();
                GameObject _ball = Instantiate(ball[Random.Range(0, ballCount)], startingpos, Quaternion.identity);
                _ball.transform.SetParent(dba.transform);
                
            }
        }

        
    }

    
    private void Update()
    {
        Obstacles = GameObject.FindGameObjectsWithTag("obs");
    }

    public void getObjbectTransform(Transform transform)
    {
        transformsPool.Enqueue(transform);
        spawnNextObject();
    }
    public void spawnNextObject()
    {
        var gm = GameManager.instance.obs;
        if (gm.Length > 20) return;
        Transform t = transformsPool.Dequeue();
        GameObject BALL = ball[Random.Range(0, ballCount)];
        Instantiate(BALL, t.position, Quaternion.identity);
        
    }

    public void respawnObject(Transform transform)
    {
        GameObject _ball = Instantiate(ball[Random.Range(0, ballCount)], transform.position, Quaternion.identity);
        _ball.transform.SetParent(transform);
        _ball.SetActive(false);
    }


   

    



}
