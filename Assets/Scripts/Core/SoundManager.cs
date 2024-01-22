using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource, отвечающий за звуки
        musicSource = transform.GetChild(0).GetComponent<AudioSource>(); // Получаем первый дочерний компонент AudioSource, отвечающий за музыку

        // Сохраняем этот объект при переходе на новую сцену
        if (instance == null)
        {
            instance = this;
        }
        // Уничтожаем дублирующиеся объекты
        else if (instance != null && instance != this)
            Destroy(gameObject);

        // Назначаем начальные значения громкости
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound); // Воспроизводим звук один раз
    }

    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource); // Изменяем громкость звуков
    }

    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource); // Изменяем громкость музыки
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        // Получаем текущее значение громкости и изменяем его
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        // Проверяем, достигли ли максимального или минимального значения
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        // Назначаем конечное значение
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        // Сохраняем конечное значение в PlayerPrefs
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}