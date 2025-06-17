using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public List<Sound> sounds;
    private Dictionary<string, AudioClip> soundDict;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        soundDict = new Dictionary<string, AudioClip>();
        foreach (var sound in sounds)
        {
            soundDict[sound.name] = sound.clip;
        }
    }

    public void Play3D(string name, Vector3 pos)
    {
        if (soundDict.ContainsKey(name))
        {
            AudioSource.PlayClipAtPoint(soundDict[name], pos);
        }
    }

    public void Play2D(string name)
    {
        if (soundDict.ContainsKey(name))
        {
            GameObject temp = new GameObject("2DSound_" + name);
            AudioSource source = temp.AddComponent<AudioSource>();
            source.clip = soundDict[name];
            source.spatialBlend = 0f;
            source.Play();
            Destroy(temp, source.clip.length);
        }
    }

}
