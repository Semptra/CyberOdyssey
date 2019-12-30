using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] SpawnSpacesheeps;

    public GameObject[] SpawnPoints;

    public float SpawnTime;

    public AudioClip[] Music;

    public AudioClip GameOverSound;

    private AudioSource _audioSource;

    private int _currentScore = 0;

    public GameManager() : base()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        StartCoroutine(SpawnRandomSpacesheep());
        StartCoroutine(DestroyUnusedGameObjectsRoutine());
        StartCoroutine(PlayMusicRoutine());
    }

    public void AddScore(int score)
    {
        _currentScore += score;
        Debug.Log($"Score: {_currentScore} (+{score})");
    }

    public void UpdateHealth(int health)
    {
        Debug.Log($"Health: {health}");

        if (health <= 0)
        {
            StopAllCoroutines();

            foreach (var @object in FindObjectsOfType<GameObject>())
            {
                if (@object.GetComponent<GameManager>() == null && @object.GetComponent<Camera>() == null)
                {
                    Destroy(@object);
                }
            }

            PlaySound(GameOverSound);
        }
    }

    public void PlaySound(AudioClip sound, float volume = 1f)
    {
        StartCoroutine(PlaySoundRoutine(sound, volume));
    }

    IEnumerator SpawnRandomSpacesheep()
    {
        while (true)
        {
            var spacesheepToCreate = SpawnSpacesheeps[Random.Range(0, SpawnSpacesheeps.Length)];
            var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            Instantiate(spacesheepToCreate, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnTime + Random.Range(-2, 2));
        }
    }

    IEnumerator DestroyUnusedGameObjectsRoutine()
    {
        while (true)
        {
            foreach (var @object in FindObjectsOfType<GameObject>())
            {
                if (@object.transform.position.magnitude > 40f)
                {
                    Destroy(@object);
                }
            }

            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator PlayMusicRoutine()
    {
        while (true)
        {
            if (_audioSource.isPlaying)
            {
                yield return new WaitForSeconds(3);
                continue;
            }

            _audioSource.clip = Music[Random.Range(0, Music.Length)];
            _audioSource.Play();
        }
    }

    IEnumerator PlaySoundRoutine(AudioClip sound, float volume)
    {
        var soundAudioSource = gameObject.AddComponent<AudioSource>();
        soundAudioSource.volume = volume;
        soundAudioSource.Play(sound);

        while (true)
        {
            if (soundAudioSource.isPlaying)
            {
                yield return new WaitForSeconds(1);
                continue;
            }

            break;
        }

        Destroy(soundAudioSource);
    }
}
