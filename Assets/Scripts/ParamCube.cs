using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public GameObject _sampleCubePrefab;
    public float _startScale, _scaleMultiplier;
    public bool _useBuffer;
    public GameObject[] _sampleCube = new GameObject[AudioPeer._bandNum];

    void Start()
    {
        for (int i = 0; i < AudioPeer._bandNum; i++)
        {
            GameObject _instanceSampleCube = (GameObject)Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = this.transform.position;
            _instanceSampleCube.transform.parent = this.transform;
            _instanceSampleCube.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            _instanceSampleCube.transform.position = new Vector3(0.1f * (i + 1), 0, 0);
            _sampleCube[i] = _instanceSampleCube;
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < AudioPeer._bandNum; i++)
        {
            if (_useBuffer)
            {
                _sampleCube[i].transform.localScale = new Vector3(0.1f*transform.localScale.x, (AudioPeer._bandBuffer[i] * _scaleMultiplier) + _startScale, 0.1f*transform.localScale.z);
            }
            if (!_useBuffer)
            {
                _sampleCube[i].transform.localScale = new Vector3(0.1f*transform.localScale.x, (AudioPeer._freqBand[i] * _scaleMultiplier) + _startScale, 0.1f*transform.localScale.z);
            }
        }
    }
}
