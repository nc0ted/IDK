using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Slider timerSlider;

    internal void StartTimer(float targetValue)
    {
        timerText.text = targetValue.ToString();
        timerSlider.maxValue = targetValue;
        timerSlider.value = targetValue;
        Visualize();
    }

    private async Task Visualize()
    {
        while (true)
        {
            if (timerSlider.value <= 0) break;
            await Task.Delay(999);
            timerSlider.value -= 1;
            timerText.text = timerSlider.value.ToString();
        }
    }
}