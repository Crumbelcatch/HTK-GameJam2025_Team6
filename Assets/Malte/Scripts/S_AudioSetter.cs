using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class S_AudioSetter : MonoBehaviour
{
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
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // �������� VCA
        masterVCA = RuntimeManager.GetVCA(masterPath);
        musicVCA = RuntimeManager.GetVCA(musicPath);
        sfxVCA = RuntimeManager.GetVCA(sfxPath);
        amnbienceVCA = RuntimeManager.GetVCA(ambiencePath);

        // ��������� ���������� �������� (���� ����)
        float masterVol = PlayerPrefs.GetFloat("MasterVolume");
        float musicVol = PlayerPrefs.GetFloat("MusicVolume");
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume");
        float atmoVol = PlayerPrefs.GetFloat("AtmoVolume");

        // ��������� �������� � ��������� � VCA

        ApplyVolume(masterVCA, masterVol, "MasterVolume");
        ApplyVolume(musicVCA, musicVol, "MusicVolume");
        ApplyVolume(sfxVCA, sfxVol, "SFXVolume");
        ApplyVolume(amnbienceVCA, atmoVol, "AtmoVolume");
    }

    private void ApplyVolume(VCA vca, float volume, string key)
    {
            vca.setVolume(volume);        
    }
}
