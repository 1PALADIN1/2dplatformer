using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private int _sourcesNumber = 20;            //количество источников звука
    
    private Queue<AudioClip> _audios;           //очередь воспроизводимых звуков
    private List<AudioSource> _audioSources;    //список источников звуков


    private void Start()
    {
        _audios = new Queue<AudioClip>();
        _audioSources = new List<AudioSource>();

        //наполняем список источниками звука
        for (int i = 0; i < _sourcesNumber; i++)
        {
            _audioSources.Add(gameObject.AddComponent<AudioSource>());
        }
    }
	
	private void Update ()
    {
        //если в очереди есть звуки, то воспроизводим их
        //а потом удаляем из неё
		if (_audios.Count != 0)
        {
            AudioSource src = FindEmptySource();
            if (src != null)
            {
                src.clip = _audios.Dequeue();
                src.Play();
            }
        }
	}

    /// <summary>
    /// Добавляем в очередь звук на воспроизведение
    /// </summary>
    /// <param name="audioClip">Фрагмент звука</param>
    public void AddSound(AudioClip audioClip)
    {
        _audios.Enqueue(audioClip);
    }

    /// <summary>
    /// Ищет свободный источник звука (для распараллеливания воспроизведения звуков)
    /// </summary>
    /// <returns>Возвращает свободный источник звука</returns>
    private AudioSource FindEmptySource()
    {
        foreach (var src in _audioSources)
        {
            if (!src.isPlaying)
                return src;
        }
        return null;
    }
}
