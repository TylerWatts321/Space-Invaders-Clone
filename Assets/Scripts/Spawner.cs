using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spawner : MonoBehaviour
{
    public Camera MainCamera;
    public float xGap;
    public float yGap;
    private Enemy spawnedEnemy;
    public Enemy firstEnemy;
    public Enemy secondEnemy;
    public Enemy lastEnemy;
    public static int numRows = 5;
    public static int numCols = 11;
    public static Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    
 
    private void Start()
    {
        Vector3 startPos = transform.position;
        EnemyTracker.InitArray(numRows, numCols);
        GetDimensions();

        for (int i = 0; i < numRows; i++)
        {
            switch (i)
            {
                case 0:
                    spawnedEnemy = firstEnemy;
                    break;
                case 1:
                    spawnedEnemy = secondEnemy;
                    break;
                case 3:
                    spawnedEnemy = lastEnemy;
                    break;
                default:
                    break;
            }

            for (int j = 0; j < numCols; j++)
            {
                if (transform.position.x + objectWidth < screenBounds.x)
                {
                    GameObject newEnemy = Instantiate(spawnedEnemy.gameObject, transform.position, transform.rotation).gameObject;
                    newEnemy.transform.SetParent(EnemyTracker.enemyHolder.transform);
                    EnemyTracker.AddToArray(i, j, newEnemy.GetComponent<Enemy>());
                    transform.position += new Vector3(xGap + (objectWidth*2), 0, 0);
                }
            }

            transform.position = new Vector3(startPos.x, startPos.y - yGap - (objectHeight*2), 0);
            startPos = transform.position;
        }
    }

    public void GetDimensions()
    {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = lastEnemy.transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = lastEnemy.transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2}
    }
}
