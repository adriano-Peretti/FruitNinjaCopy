using UnityEngine;
using TMPro;

public class off : MonoBehaviour
{
    public float frequency = 5f;
    public float amplitude = 10f;
    string sourceString;
    TextMeshProUGUI meshText;

    void Start()
    {
        meshText = GetComponent<TextMeshProUGUI>();
        sourceString = meshText.text;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        string newString = sourceString.Replace("<voffset=0px>", "<voffset=" + offset.ToString() + "px>");
        meshText.text = newString;
    }
}