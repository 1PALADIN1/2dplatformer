using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private Queue<AudioClip> audios;            //очередь воспроизводимых звуков

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        audios = new Queue<AudioClip>();
    }
	
	private void Update ()
    {
        //если в очереди есть звуки, то воспроизводим их
        //а потом удаляем из неё
		if (audios.Count != 0)
        {
            _audioSource.clip = audios.Dequeue();
            _audioSource.Play();
        }
	}

    /// <summary>
    /// Добавляем в очередь звук на воспроизведение
    /// </summary>
    /// <param name="audioClip">Фрагмент звука</param>
    public void AddSound(AudioClip audioClip)
    {
        audios.Enqueue(audioClip);
    }
}
