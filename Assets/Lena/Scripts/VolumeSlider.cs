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
    public Slider amnbienceSlider;

    [Header("VCA Paths")]
    private string masterPath = "vca:/Master";
    private string musicPath = "vca:/Music";
    private string sfxPath = "vca:/SFX";
    private string ambiencePath = "vca:/Ambience";

    private VCA masterVCA;
    private VCA musicVCA;
    private VCA sfxVCA;
    private VCA amnbienceVCA;

    private void Awake()
    {
        // ��������� ������ ����� �������
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // �������� VCA
        masterVCA = RuntimeManager.GetVCA(masterPath);
        musicVCA = RuntimeManager.GetVCA(musicPath);
        sfxVCA = RuntimeManager.GetVCA(sfxPath);
        amnbienceVCA = RuntimeManager.GetVCA(ambiencePath);

        // ��������� ����������� �������� (���� ����)
        float masterVol = PlayerPrefs.GetFloat("MasterVolume");
        float musicVol = PlayerPrefs.GetFloat("MusicVolume");
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume");
        float atmoVol = PlayerPrefs.GetFloat("AtmoVolume");

        // ��������� �������� � ��������� � VCA

        ApplyVolume(masterSlider, masterVCA, masterVol, "MasterVolume");
        ApplyVolume(musicSlider, musicVCA, musicVol, "MusicVolume");
        ApplyVolume(sfxSlider, sfxVCA, sfxVol, "SFXVolume");
        ApplyVolume(amnbienceSlider, amnbienceVCA, atmoVol, "AtmoVolume");
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