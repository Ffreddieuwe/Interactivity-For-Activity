using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioResource m_clickAudio;
    [SerializeField]
    private AudioResource m_block;
    [SerializeField]
    private AudioResource m_heal;
    [SerializeField]
    private AudioResource m_hit;
    [SerializeField]
    private AudioResource m_miss;
    [SerializeField]
    private AudioResource m_levelComplete;
    [SerializeField]
    private AudioResource m_levelLost;
    [SerializeField]
    private AudioResource m_trainingComplete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void PlayClick()
    {
        audioSource.resource = m_clickAudio;
        audioSource.Play();
    }

    public void PlayBlock()
    {
        audioSource.resource = m_block;
        audioSource.Play();
    }

    public void PlayHit()
    {
        audioSource.resource = m_hit;
        audioSource.Play();
    }

    public void PlayMiss()
    {
        audioSource.resource = m_miss;
        audioSource.Play();
    }

    public void PlayHeal()
    {
        audioSource.resource = m_heal;
        audioSource.Play();
    }

    public void PlayLevelComplete()
    {
        audioSource.resource = m_levelComplete;
        audioSource.Play();
    }

    public void PlayLevelLost()
    {
        audioSource.resource = m_levelLost;
        audioSource.Play();
    }

    public void PlayTrainingComplete()
    {
        audioSource.resource = m_trainingComplete;
        audioSource.Play();
    }
}
