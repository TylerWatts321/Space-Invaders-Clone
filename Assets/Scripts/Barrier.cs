using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int integrityLevel;
    private SpriteRenderer rend;

    public Sprite integrityLevel_19;
    public Sprite integrityLevel_18;
    public Sprite integrityLevel_17;
    public Sprite integrityLevel_16;
    public Sprite integrityLevel_15;
    public Sprite integrityLevel_14;
    public Sprite integrityLevel_13;
    public Sprite integrityLevel_12;
    public Sprite integrityLevel_11;
    public Sprite integrityLevel_10;
    public Sprite integrityLevel_09;
    public Sprite integrityLevel_08;
    public Sprite integrityLevel_07;
    public Sprite integrityLevel_06;
    public Sprite integrityLevel_05;
    public Sprite integrityLevel_04;
    public Sprite integrityLevel_03;
    public Sprite integrityLevel_02;
    public Sprite integrityLevel_01;


    void Start()
    {

        rend = gameObject.GetComponent<SpriteRenderer>();
        integrityLevel = 20;
    }

    public void HitBarrier()
    {
        integrityLevel--;
        ChangeSprite(integrityLevel);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }
    public void DestroyBarrier()
    {
        Destroy(gameObject);
    }

    private void ChangeSprite(int integrityLevel)
    {
        switch (integrityLevel)
        {
            case 19:
                rend.sprite = integrityLevel_19;
                break;
            case 18:
                rend.sprite = integrityLevel_18;
                break;
            case 17:
                rend.sprite = integrityLevel_17;
                break;
            case 16:
                rend.sprite = integrityLevel_16;
                break;
            case 15:
                rend.sprite = integrityLevel_15;
                break;
            case 14:
                rend.sprite = integrityLevel_14;
                break;
            case 13:
                rend.sprite = integrityLevel_13;
                break;
            case 12:
                rend.sprite = integrityLevel_12;
                break;
            case 11:
                rend.sprite = integrityLevel_11;
                break;
            case 10:
                rend.sprite = integrityLevel_10;
                break;
            case 9:
                rend.sprite = integrityLevel_09;
                break;
            case 8:
                rend.sprite = integrityLevel_08;
                break;
            case 7:
                rend.sprite = integrityLevel_07;
                break;
            case 6:
                rend.sprite = integrityLevel_06;
                break;
            case 5:
                rend.sprite = integrityLevel_05;
                break;
            case 4:
                rend.sprite = integrityLevel_04;
                break;
            case 3:
                rend.sprite = integrityLevel_03;
                break;
            case 2:
                rend.sprite = integrityLevel_02;
                break;
            case 1:
                rend.sprite = integrityLevel_01;
                break;
            case 0:
                DestroyBarrier();
                break;
        }
    }
}
