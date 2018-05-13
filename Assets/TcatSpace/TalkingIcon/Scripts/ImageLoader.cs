using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using B83.Win32;


public class ImageLoader : MonoBehaviour
{
    int cyclicTargetTextureIndex = 0;
    BlendWeightReciever blendWeightReciever;

    // important to keep the instance alive while the hook is active.
    UnityDragAndDropHook hook;
    void OnEnable ()
    {
        // must be created on the main thread to get the right thread id.
        hook = new UnityDragAndDropHook();
        hook.InstallHook();
        hook.OnDroppedFiles += OnFiles;
    }
    void OnDisable()
    {
        hook.UninstallHook();
    }

    void Start() {
        blendWeightReciever = GetComponent<BlendWeightReciever>();
    }

    void OnFiles(List<string> aFiles, POINT aPos)
    {
        const int size = 1024;
        var bytes = System.IO.File.ReadAllBytes(aFiles[0]);
        var texture2D = new Texture2D(size, size);
        texture2D.LoadImage(bytes);

        var sprite = Sprite.Create(
            texture2D,
            new Rect(0, 0, texture2D.width, texture2D.height),
            new Vector2(0.5f, 0.5f));

        ChangeTexture(sprite);

        UpdateTargetTextureIndex();
    }

    void ChangeTexture(Sprite sprite) {
        switch(cyclicTargetTextureIndex) {
            case 0:
                blendWeightReciever.neutral = sprite;
                break;
            case 1:
                blendWeightReciever.openMouth = sprite;
                break;
            default:
                break;
        }
    }

    void UpdateTargetTextureIndex() {
        if(cyclicTargetTextureIndex == 0) {
            cyclicTargetTextureIndex = 1;
        } else {
            cyclicTargetTextureIndex = 0;
        }
    }
}
