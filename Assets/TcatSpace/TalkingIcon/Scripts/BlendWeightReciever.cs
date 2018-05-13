using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlendWeightReciever : MonoBehaviour {

    public Sprite neutral;
    public Sprite openMouth;

    Image image;
    float weight;

    public void Reset(int index) {
        weight = 0.0f;
    }

    public void SetBlendShapeWeight(int index, float weight) {
        this.weight = weight;
    }

	void Start () {
        image = GetComponentInChildren<Image>();
	}
	
	void Update () {
        if(weight > 0.0f) {
            image.sprite = openMouth;
        } else {
            image.sprite = neutral;
        }
	}
}
