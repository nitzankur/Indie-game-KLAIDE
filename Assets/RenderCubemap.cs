using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RenderCubemap : MonoBehaviour {
    public RenderTexture cubemap;
    public int resolution = 256;
    [SerializeField] private Camera cam;

    void Initialize() {
      if(cubemap == null) {
        cubemap = new RenderTexture(resolution, resolution, 24);
        cubemap.dimension = TextureDimension.Cube;
      }
    }
    
    void Awake () {
      Initialize();
    }

    void LateUpdate () {
      cam.RenderToCubemap(cubemap, 63, Camera.MonoOrStereoscopicEye.Mono);
    }
    
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
       Graphics.Blit(source, destination);
    }

}
