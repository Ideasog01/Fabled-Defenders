using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorrupted : MonoBehaviour
{
    private GameObject player;

    private bool behavActivated;

    private Transform projectilePrefab;



    private void Start()
    {
        player = GameObject.Find("Player");
        projectilePrefab = GameObject.Find("GameManager").GetComponent<GameManager>().playerCorruptedPrefab;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);

        if(distanceToPlayer < 9 && distanceToPlayer > 2 && !behavActivated)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 5 * Time.fixedDeltaTime);
        }

        if(distanceToPlayer < 3 && !behavActivated)
        {
            StartCoroutine(BehaviourForEnemyPlayer());
            behavActivated = true;
        }

        if(player.transform.position.x > this.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }




    IEnumerator BehaviourForEnemyPlayer()
    {
        yield return new WaitForSeconds(5);
        Instantiate(projectilePrefab, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(projectilePrefab, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(projectilePrefab, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(projectilePrefab, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(5);
        behavActivated = false;
    }
}
