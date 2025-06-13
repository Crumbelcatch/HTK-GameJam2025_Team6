using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_SoundSlider : MonoBehaviour
{




    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _ambienceSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;


    private void Start()
    {
        SetSliders(_masterSlider, "bus:/Master");
        SetSliders(_ambienceSlider, "bus:/Master/Ambience");
        SetSliders(_musicSlider, "bus:/Master/Music");
        SetSliders(_sfxSlider, "bus:/Master/SFX");

        _masterSlider.value = 0.5f;
        _ambienceSlider.value = 0.4f;
        _musicSlider.value = 0.5f;
        _sfxSlider.value = 0.4f;
    }


    private void SetSliders(Slider slider, string fmodBusPath)
    {
        RuntimeManager.GetBus(fmodBusPath).getVolume(out float vol);
        slider.value = vol;
    }

    public void SetMasterVolume()
    {
        RuntimeManager.GetBus("bus:/Master").setVolume(_masterSlider.value);
    }
    public void SetAmbieceVolume()
    {
        RuntimeManager.GetBus("bus:/Master/Ambience").setVolume(_ambienceSlider.value);
    }
    public void SetMusicVolume()
    {
        RuntimeManager.GetBus("bus:/Master/Music").setVolume(_musicSlider.value);
    }
    public void SetSfxVolume()
    {
        RuntimeManager.GetBus("bus:/Master/SFX").setVolume(_sfxSlider.value);
    }

}
