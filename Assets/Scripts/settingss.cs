using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class settingss : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    void Start()
    {
        // Setează valoarea inițială a sliderului de volum
        volumeSlider.value = GetMasterVolume();
    }

    public void SetMasterVolume(float volume)
    {
        // Actualizează volumul maestrului în mixerul audio
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
    }

    public float GetMasterVolume()
    {
        // Obține volumul maestrului din mixerul audio
        float volume;
        audioMixer.GetFloat("MasterVolume", out volume);
        return Mathf.Pow(10f, volume / 20f);
    }
}
