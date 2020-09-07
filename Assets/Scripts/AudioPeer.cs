using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource _audioSource;
    public const int _bandNum = 10;
    public static float[] _samples = new float[512];
    public static float[] _freqBand = new float[_bandNum];
    public static float[] _bandBuffer = new float[_bandNum];
    float[] _bufferDecrease = new float[_bandNum];

    // Start is called before the first frame update
    void Start()
    {
        Globals globals = new Globals();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for (int g = 0; g < _bandNum; ++g)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }

            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }
    void MakeFrequencyBands()
    {
        int band = 0;

        for (int i = 0; i < _freqBand.Length; i++)
        {
            float average = 0;
            // As you increment on Frequency bands to set, get number of samples looking to get average of next based on for loop progress percentage
            int sampleCount = (int)Mathf.Lerp(2f, _samples.Length - 1, i / ((float)_freqBand.Length - 1));


            // always start the j index at the current value of band here!  if you always start from 0, band++ will increment out of _samples bounds
            for (int j = band; j < sampleCount; j++)
            {
                average += _samples[band] * (band + 1);
                band++;
            }

            average /= sampleCount;

            _freqBand[i] = average;
        }
    }
}
