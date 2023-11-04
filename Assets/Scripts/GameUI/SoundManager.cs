using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] AudioClips;

    [SerializeField] AudioSource BGMPlayer; // ��� ����
    [SerializeField] AudioSource SFXPlayer; // ȿ����
    [SerializeField] Slider SoundSlider;

    private static object _lock = new object();
    private static SoundManager _instance = null;
    public static SoundManager instance
    {
        get
        {
            if (applicationQuitting)
            {
                return null;
            }
            lock (_lock)
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject("SoundManager ");
                    obj.AddComponent<SoundManager>();
                    _instance = obj.GetComponent<SoundManager>();
                }
                return _instance;
            }
        }
        set
        {
            _instance = value;
        }
    }
    private static bool applicationQuitting = false;


    void Awake()
    {
        _instance = this;
        // �̱��� �ν��Ͻ�
        SoundSlider.onValueChanged.AddListener(ChangeSoundVolume);
    }
    public void PlaySound(string type)
    {
        int index = 0;

        switch (type)
        {
            case "ButtonClick": index = 0; break; // ��ư Ŭ�� ȿ����
            case "Enhance": index = 1; break; // ��ȭ ȿ����
        }

        SFXPlayer.clip = AudioClips[index];
        SFXPlayer.Play();
    }

    void ChangeSoundVolume(float value)
    {
        BGMPlayer.volume = value;
        SFXPlayer.volume = value;
    }
}
