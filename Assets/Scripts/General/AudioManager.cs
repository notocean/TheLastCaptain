using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType {
    ClickBtn, ClickMap, Shoot, Explosion
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] PlayerDataScriptableObject _playerDataSO;

    [SerializeField] AudioSource backgroundAudioSource;
    [SerializeField] AudioSource ontShotAudioSource;
    [SerializeField] AudioClip clickButtonAudio;
    [SerializeField] AudioClip clickMapAudio;
    [SerializeField] AudioClip shootAudio;
    [SerializeField] AudioClip explosionAudio;

    [HideInInspector] public bool isAudio;
    [HideInInspector] public bool isMusic;

    private void Awake() {
        if (Instance == null) {
            Instance = FindObjectOfType<AudioManager>();
            if (Instance == null) {
                GameObject audioManager = new GameObject("Audio Manager");
                Instance = audioManager.AddComponent<AudioManager>();
            }
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        LoadAudioSetting();
    }

    private void LoadAudioSetting() {
        isAudio = _playerDataSO.isAudio;
        isMusic = _playerDataSO.isMusic;
        if (isMusic) {
            backgroundAudioSource.Play();
        }
    }

    public void AudioOnOff(CustomButton customButton) {
        isAudio = ((ToggleButton)customButton).isOn;
        _playerDataSO.isAudio = isAudio;
        GameManager.Instance.SaveData();
    }

    public void MusicOnOff(CustomButton customButton) {
        isMusic = ((ToggleButton)customButton).isOn;
        if (isMusic) {
            backgroundAudioSource.Play();
        }
        else {
            backgroundAudioSource.Stop();
        }
        _playerDataSO.isMusic = isMusic;
        GameManager.Instance.SaveData();
    }

    public void PlayAudio(AudioType audioType) {
        if (isAudio) {
            AudioClip audioClip = null;
            switch(audioType) {
                case AudioType.ClickBtn:
                    audioClip = clickButtonAudio;
                    break;
                case AudioType.ClickMap:
                    audioClip = clickMapAudio;
                    break;
                case AudioType.Shoot:
                    audioClip = shootAudio;
                    break;
                case AudioType.Explosion:
                    audioClip = explosionAudio;
                    break;
                default: break;
            }
            ontShotAudioSource.PlayOneShot(audioClip);
        }
    }
}
