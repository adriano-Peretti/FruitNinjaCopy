using Adriano.Scoreboards;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text finalScoreText;
    public Image fadeImage;
    public GameObject perdeuPopUp;
    public GameObject menuConfig;
    public GameObject menuIniciar;
    public TMP_InputField _inputField;
    public GameObject ranking;
    public Button saveRanking;

    private Blade blade;
    private Spawner spawner;

    public Scoreboard scoreboard;
    private ScoreboardEntryData scoreboardEntry;

    private int score;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
        scoreText.enabled = false;
    }

    private void Start()
    {
        MenuIniciar();
    }

    private void MenuIniciar()
    {
        spawner.enabled = false;

        blade.enabled = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        scoreText.enabled = true;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        ClearScene();

        saveRanking.interactable = true;
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }

        PowerUp[] powerUps = FindObjectsOfType<PowerUp>();

        foreach (PowerUp powerUp in powerUps)
        {
            Destroy(powerUp.gameObject);
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        AudioManager.audioManagerInstance.BombSoundEffect();

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        ClearScene();

        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        scoreText.enabled = false;
        finalScoreText.text = score.ToString();
        perdeuPopUp.SetActive(true);
        _inputField.interactable = true;
    }

    public void MenuConfigPause()
    {
        blade.enabled = false;
        menuConfig.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MenuConfigResume()
    {
        blade.enabled = true;
        menuConfig.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AddRanking()
    {
        if (_inputField.text.Length > 0)
        {
            scoreboardEntry.entryName = _inputField.text;
            scoreboardEntry.entryScore = score;
            scoreboard.AddEntry(scoreboardEntry);

            _inputField.interactable = false;

            saveRanking.interactable = false;

            Debug.Log($"Adicionado {scoreboardEntry.entryName} com {scoreboardEntry.entryScore} no ranking");
        }
        else if (_inputField.text.Length == 0)
        {
            return;
        }
    }

    public void OpenRanking()
    {
        ranking.SetActive(true);
    }

    public void CloseRanking()
    {
        if (perdeuPopUp.activeSelf == true)
        {
            ranking.SetActive(false);
        }
        else
        {
            ranking.SetActive(false);
            menuIniciar.SetActive(true);
        }

    }

}
