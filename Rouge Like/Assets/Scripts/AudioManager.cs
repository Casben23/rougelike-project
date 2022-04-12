using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum Sound
    {
        PlayerAttack,
        PlayerMove,
        PlayerHit,
        EnemyHit,
        EnemyAttack,
        EnemyDeath,
        EnemyMove,
        ItemPickup
    }

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    public SoundAudioClip[] mySoundAudioClips;

    [System.Serializable]
    public class SoundAudioClip
    {
        public string name;
        public Sound sound;
        public AudioClip audioClip;
    }

    private AudioClip GetAudioClip(Sound aSound)
    {
        foreach (SoundAudioClip soundClip in mySoundAudioClips)
        {
            if(soundClip.sound == aSound)
            {
                return soundClip.audioClip;
            }
        }
        Debug.Log("Sound " + aSound + " not found");
        return null;
    }

    public void PlaySound(Sound aSound, Vector3 aPos, bool aLooping, bool isSpacial)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.localPosition = aPos;

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.loop = aLooping;

        if (isSpacial)
        {
            audioSource.spatialBlend = 1;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
        }

        audioSource.PlayOneShot(GetAudioClip(aSound));
        Destroy(soundGameObject, 2);
    }
}
