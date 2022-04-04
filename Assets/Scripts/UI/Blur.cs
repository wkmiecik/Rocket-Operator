using UnityEngine;
using DG.Tweening;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Blur/Blur")]
public class Blur : MonoBehaviour
{
    public bool blurEnabled = false;

    [Range(0, 10)]
    public int iterations = 3;

    [Range(0.0f, 1.0f)]
    public float blurSpread = 0.6f;

    public Shader blurShader = null;

    static Material m_Material = null;

    Sequence blurInSequence;
    Sequence blurOutSequence;

    protected Material material
    {
        get
        {
            if (m_Material == null)
            {
                m_Material = new Material(blurShader);
                m_Material.hideFlags = HideFlags.DontSave;
            }
            return m_Material;
        }
    }

    protected void OnDisable()
    {
        if (m_Material)
        {
            DestroyImmediate(m_Material);
        }
    }


    protected void Start()
    {
        // Disable if the shader can't run on the users graphics card
        if (!blurShader || !material.shader.isSupported)
        {
            enabled = false;
            return;
        }

        // Setup dotween
        DOTween.Init();
    }

    public void BlurIn()
    {
        DOTween.CompleteAll(true);
        blurEnabled = true;
        blurInSequence = DOTween.Sequence();
        blurInSequence.SetEase(Ease.Linear);
        blurInSequence.Append(DOTween.To(() => iterations, x => iterations = x, 3, .1f));
        blurInSequence.Append(DOTween.To(() => blurSpread, x => blurSpread = x, .5f, .1f));
    }
    public void BlurOut()
    {
        DOTween.CompleteAll(true);
        blurOutSequence = DOTween.Sequence();
        blurOutSequence.SetEase(Ease.Linear);
        blurOutSequence.Append(DOTween.To(() => iterations, x => iterations = x, 0, .2f));
        blurOutSequence.Insert(0, DOTween.To(() => blurSpread, x => blurSpread = x, 0f, .1f));
        blurOutSequence.OnComplete(() => enabled = false);
    }

    // Performs one blur iteration.
    public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
    {
        float off = 0.5f + iteration * blurSpread;
        Graphics.BlitMultiTap(source, dest, material,
                                new Vector2(-off, -off),
                                new Vector2(-off, off),
                                new Vector2(off, off),
                                new Vector2(off, -off)
        );
    }

    // Downsamples the texture to a quarter resolution.
    private void DownSample4x(RenderTexture source, RenderTexture dest)
    {
        float off = 1.0f;
        Graphics.BlitMultiTap(source, dest, material,
                                new Vector2(-off, -off),
                                new Vector2(-off, off),
                                new Vector2(off, off),
                                new Vector2(off, -off)
        );
    }

    // Called by the camera to apply the image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (blurEnabled)
        {
            int rtW = source.width / 4;
            int rtH = source.height / 4;
            RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);

            // Copy source to the 4x4 smaller texture.
            DownSample4x(source, buffer);

            // Blur the small texture
            for (int i = 0; i < iterations; i++)
            {
                RenderTexture buffer2 = RenderTexture.GetTemporary(rtW, rtH, 0);
                FourTapCone(buffer, buffer2, i);
                RenderTexture.ReleaseTemporary(buffer);
                buffer = buffer2;
            }
            Graphics.Blit(buffer, destination);

            RenderTexture.ReleaseTemporary(buffer);
        } 
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}