using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public AudioClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(clip, transform.position);
            Inventory.instance.AddCoins(1);
            CurrentSceneManager.instance.coinsPickedUpInThisSceneCount++;
            Destroy(gameObject);
        }
    }
}