using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class VolumeManager : MonoBehaviour
{
    [Header("UI Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("VCA Paths")]
    private string masterPath = "vca:/Master";
    private string musicPath = "vca:/Music";
    private string sfxPath = "vca:/SFX";

    private VCA masterVCA;
    private VCA musicVCA;
    private VCA sfxVCA;

    private void Awake()
    {
        // Сохраняем объект между сценами
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Получаем VCA
        masterVCA = RuntimeManager.GetVCA(masterPath);
        musicVCA = RuntimeManager.GetVCA(musicPath);
        sfxVCA = RuntimeManager.GetVCA(sfxPath);


        // Загружаем сохранённые значения (если есть)
        float masterVol = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);


        // Применяем значения к слайдерам и VCA
        ApplyVolume(masterSlider, masterVCA, masterVol, "MasterVolume");
        ApplyVolume(musicSlider, musicVCA, musicVol, "MusicVolume");
        ApplyVolume(sfxSlider, sfxVCA, sfxVol, "SFXVolume");

    }

    private void ApplyVolume(Slider slider, VCA vca, float volume, string key)
    {
        if (slider != null)
        {
            slider.value = volume;
            vca.setVolume(volume);

            slider.onValueChanged.AddListener((value) =>
            {
                vca.setVolume(value);
                PlayerPrefs.SetFloat(key, value);
                PlayerPrefs.Save();
            });
        }
    }
}