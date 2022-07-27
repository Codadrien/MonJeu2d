using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    public Transform playerSpawn;
    public Animator fadeSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
            
        }
    }

    private IEnumerator ReplacePlayer(Collider2D collision)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.2f);
        collision.transform.position = playerSpawn.position;
        
    }
}