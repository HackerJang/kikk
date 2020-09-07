using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public Joystick joystick;
    public RectTransform hitCircle;
    public RectTransform hitEffect;
    void Start()
    {
        hitEffect.position = hitCircle.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.SqrMagnitude(new Vector2(joystick.Horizontal, joystick.Vertical)) < .1f)
        {
            hitEffect.gameObject.SetActive(false);
        }
        else
        {
            hitEffect.gameObject.SetActive(true);
            Vector3 hitEffectRotation = hitEffect.transform.eulerAngles;
            hitEffectRotation.z = Mathf.Rad2Deg * Mathf.Atan2(joystick.Horizontal, joystick.Vertical);
            hitEffect.transform.eulerAngles = -hitEffectRotation;
            Debug.Log(hitEffectRotation);
        }
    }
}
