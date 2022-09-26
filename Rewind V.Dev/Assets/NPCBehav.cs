using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehav : MonoBehaviour
{
    private GameObject player;

    private float distanceToPlayer;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        if(player.transform.position.x > this.transform.position.x && distanceToPlayer < 5)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (player.transform.position.x < this.transform.position.x && distanceToPlayer < 5)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
