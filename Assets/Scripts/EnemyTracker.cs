using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTracker : MonoBehaviour
{
    public static Enemy[,] enemyGrid;
    public static int numEnemies;
    //public static bool stopSearching;
    //public bool isDone;
    public static Vector3 FurthestToLeft;
    public static Vector3 FurthestToRight;
    public static Vector3 ClosestToPlayer;
    public static GameObject enemyHolder;

    void Awake()
    {
        gameObject.SetActive(true);
    }
    void Start()
    {
        numEnemies = Spawner.numCols * Spawner.numRows;
        Debug.Log(numEnemies);
        Initialize();
    }

    private void Initialize()
    {
        enemyHolder = new GameObject("Enemy Holder");
    }

    void Update()
    {
        if(GameManager.instance.gameOver)
            Destroy(gameObject);

        if (numEnemies == 1)
        {
            Enemy tempEnemy = SearchForLastEnemy();
            if (tempEnemy.speed < 0) 
            { 
                tempEnemy.speed = -1.3f;
            }
            else
            {
                tempEnemy.speed = 1.3f;
            }
        }

        if (!GameManager.instance.gameOver && (FetchClosestToPlayer().transform.position.y < -4f))
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);
            return;
        }

        if(GameManager.instance.isReloadingScene)
            Destroy(gameObject);
    }
    public static void InitArray(int rows, int cols)
    {
        enemyGrid = new Enemy[rows, cols];
    }
    public static void AddToArray(int row, int col, Enemy enemy)
    {
        enemyGrid[row, col] = enemy;
        enemyGrid[row, col].isFront = false;
        if (row == Spawner.numRows - 1)
            enemyGrid[row, col].isFront = true;
    }

    public static void UpdateEnemyFrontStatus(Enemy enemy)
    {
        if (numEnemies <= 1)
            return;
        for (int i = Spawner.numRows - 1; i >= 0; i--)
        {
            for (int j = 0; j < Spawner.numCols; j++)
            {
                if (enemyGrid[i, j] == enemy)
                {
                    if (i > 0)
                    {
                        if(enemyGrid[i - 1, j] )
                            enemyGrid[i - 1, j].isFront = true;
                    }
                    enemyGrid[i, j] = null;
                    Debug.Log("Enemy at row " + (i + 1) + ", column " + (j + 1) + " just died!");
                }
            }
        }
    }

    public static bool FindEnemyInFront(int row, int col)
    {
        for (int i = Spawner.numRows - 1; i > row; i--)
        {
            if (enemyGrid[i, col])
            {
                return true;
            }
        }

        return false;
    }


    public static bool FrontRowIsEmpty(int row)
    {
        for (int j = 0; j < Spawner.numCols; j++)
        {
            if (enemyGrid[row, j])
                return false;
        }

        return true;
    }

    public static bool ColIsEmpty(int col)
    {
        for (int i = 0; i < Spawner.numRows; i++)
        {
            if (enemyGrid[i, col])
                return false;
        }

        return true;
    }

    public static Enemy FetchFurthestRight()
    {
        if (numEnemies == 1)
            return SearchForLastEnemy();
        for (int j = Spawner.numCols - 1; j > 0; j--)
        {
            if (!ColIsEmpty(j))
            {
                for (int i = 0; i < Spawner.numRows; i++)
                {
                    if (enemyGrid[i, j] != null)
                        return enemyGrid[i, j];
                }
            }
        }
        return null;
    }

    public static Enemy FetchFurthestLeft()
    {
        if (numEnemies == 1)
            return SearchForLastEnemy();
        for (int j = 0; j < Spawner.numCols; j++)
        {
            if (!ColIsEmpty(j))
            {
                for (int i = 0; i < Spawner.numRows; i++)
                {
                    if (enemyGrid[i, j] != null)
                        return enemyGrid[i, j];
                }
            }
        }
        return null;
    }

    public static Enemy SearchForLastEnemy()
    {
        for (int j = 0; j < Spawner.numCols; j++)
        {
            for (int i = 0; i < Spawner.numRows; i++)
            {
                if (enemyGrid[i, j])
                    return enemyGrid[i, j];
            }
        }

        return null;
    }

    public static Enemy FetchRandFront()
    {
        if (numEnemies == 1)
            return SearchForLastEnemy();
        int row;
        int col;
        do
        {
            row = Random.Range(0, Spawner.numRows);
            col = Random.Range(0, Spawner.numCols);
            
        } while ((!enemyGrid[row, col] || !enemyGrid[row, col].isFront));

        return enemyGrid[row, col];
    }

    public static Enemy FetchClosestToPlayer()
    {
        if (numEnemies == 1)
            return SearchForLastEnemy();
        for (int i = Spawner.numRows - 1; i >= 0; i--)
        {
            for (int j = 0; j < Spawner.numCols; j++)
            {
                if (enemyGrid[i, j])
                    return enemyGrid[i, j];
            }
        }

        return null;
    }

    public static void UpdateNumEnemies()
    {
        numEnemies--;
        Debug.Log("There are " + numEnemies + " left!");
    }
}
