using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
    [SerializeField] GameObject contador;
    int gameOverCount;


    private void Start()
    {
                gameOverCount = 0;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Fruit"))
        {
            gameOverCount++;
            contador.SetActive(true);
            StartCoroutine(ContadorGameOver());


            Debug.Log($"{gameOverCount} de 3");

            if (gameOverCount >= 3)
            {
                FindObjectOfType<GameManager>().Explode();
                gameOverCount = 0;
            }

        }

    }

    public IEnumerator ContadorGameOver()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        contador.GetComponent<TMP_Text>().text = gameOverCount.ToString();



        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            contador.GetComponent<TMP_Text>().color = Color.Lerp(Color.clear, Color.white, t);

            //Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            contador.GetComponent<TMP_Text>().color = Color.Lerp(Color.white, Color.clear, t);

            //Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        contador.SetActive(false);


    }

}
