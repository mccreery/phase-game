﻿using UnityEngine;

[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(AudioSource))]
public class HitSound : MonoBehaviour
{
    [Header("Progressively heavier hit sounds")]
    [SerializeField]
    private AudioClip[] clips = default;

    private void Awake()
    {
        AudioSource source = GetComponent<AudioSource>();
        HealthManager healthManager = GetComponent<HealthManager>();

        healthManager.Damaged.AddListener(() =>
        {
            if (healthManager.Health < healthManager.MaxHealth)
            {
                int i = (healthManager.MaxHealth - healthManager.Health)
                    * clips.Length / healthManager.MaxHealth - 1;

                source.PlayOneShot(clips[i]);
            }
        });
    }
}
