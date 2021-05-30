using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject diamondDropPrefab;
    public GameObject circleDropPrefab;
    public GameObject squareDropPrefab;
    public float verticalExtent; //wysokosc ekranu w jednostack unity
    public float horizontalExtent; //szerokoœæ ekranu w jednostkach unity
    public GameObject wallLeft;
    public GameObject wallRight;
    public Text pointCounter;
    public int points;
    float timeSinceLastDrop;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastDrop = 0;
        CalculateExtent();
    }
    void CalculateExtent()
    {
        verticalExtent = Camera.main.orthographicSize;
        float screenRatio = (float) Screen.width / (float) Screen.height;
        horizontalExtent = verticalExtent * screenRatio;
        wallLeft.transform.position = new Vector2(-horizontalExtent, 0); //ustaw lewa sciane na brzegu ekranu
        wallRight.transform.position = new Vector2(horizontalExtent, 0); //ustaw praw¹ œcianê na brzegu ekranu
        Vector2[] wallPoints = { new Vector2(0, verticalExtent), new Vector2(0, -verticalExtent) }; //stworz 2 punkty na wysokosci gory i dolu ekranu
        wallLeft.GetComponent<EdgeCollider2D>().points = wallPoints; //ustaw wysokosæ sciany na wysokosc ekranu
        wallRight.GetComponent<EdgeCollider2D>().points = wallPoints;
    }
    // Update is called once per frame
    void Update()
    {
        timeSinceLastDrop += Time.deltaTime;
        if(timeSinceLastDrop > 1) //czas przekroczy³ sekundê
        {
            int randomDrop = Random.Range(1, 4);
            switch(randomDrop)
            {
                case 1:
                    createDrop(diamondDropPrefab);
                    break;
                case 2:
                    createDrop(circleDropPrefab);
                    break;
                case 3:
                    createDrop(squareDropPrefab);
                    break;
            }
            timeSinceLastDrop = 0;
        }
        pointCounter.text = "Punkty: " + points.ToString();
    }

    void createDrop(GameObject dropPrefab)
    {
        float x = Random.Range(-horizontalExtent, horizontalExtent);
        GameObject drop = Instantiate(dropPrefab, new Vector2(x, verticalExtent + 1), Quaternion.identity);
        //var euler = drop.transform.eulerAngles;
        //euler.z = Random.Range(0, 360);
        //drop.transform.eulerAngles = euler;
        drop.GetComponent<Rigidbody2D>().AddTorque(Random.Range(0, 20));
        drop.GetComponent<DropScript>().SetGameManager(this);
    }
    public void AddPoints(int pointAmmount = 10)
    {
        points += pointAmmount;
    }
}
