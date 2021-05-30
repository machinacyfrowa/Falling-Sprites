using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        //gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGameManager(GameManager gameManager)
    {
        gm = gameManager;
    }
    private void FixedUpdate()
    {
        float posY = transform.position.y; //czytamy pozycje obiektu na osi Y (pion)
        if(posY < -gm.verticalExtent) //obiekt poza ekranem
        {
            Destroy(this.gameObject);
        }
    }
}
