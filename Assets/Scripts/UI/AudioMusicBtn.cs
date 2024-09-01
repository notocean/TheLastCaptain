using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

enum ToggleType {
    Audio, Music
}

public class AudioMusicBtn : ToggleButton
{
    [SerializeField] ToggleType type;

    private void Start() {
        if (type == ToggleType.Audio) {
            SetOnOff(AudioManager.Instance.isAudio);
            clickedEvent.AddListener(AudioManager.Instance.AudioOnOff);
        }
        else if (type == ToggleType.Music) {
            SetOnOff(AudioManager.Instance.isMusic);
            clickedEvent.AddListener(AudioManager.Instance.MusicOnOff);
        }
    }
}
