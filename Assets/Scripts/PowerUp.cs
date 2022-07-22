using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();

            if (FindObjectOfType<Blade>().click == true)
            {
                AudioManager.audioManagerInstance.PowerUpSoundEffect();
                FindObjectOfType<GameManager>().IncreaseScore();
                FindObjectOfType<GameManager>().IncreaseScore();
                FindObjectOfType<GameManager>().IncreaseScore();
                FindObjectOfType<GameManager>().IncreaseScore();
                Destroy(this.gameObject);
            }
        }
    }
}

