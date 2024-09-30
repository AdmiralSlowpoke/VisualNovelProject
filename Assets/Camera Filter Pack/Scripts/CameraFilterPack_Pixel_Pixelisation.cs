using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixel/Pixelisation")]
public class CameraFilterPack_Pixel_Pixelisation : MonoBehaviour
{
    #region Variables
    public Shader SCShader;
    [Range(0.6f, 120)]
    public float _Pixelisation = 8f;
    [Range(0.6f, 120)]
    public float _SizeX = 1f;
    [Range(0.6f, 120)]
    public float _SizeY = 1f;
    private Material SCMaterial;

    public LayerMask excludeLayer; // Слой для объектов, которые не подвергаются пикселизации
    private Camera mainCamera;
    private RenderTexture nonPixelatedTexture;
    #endregion

    #region Properties
    Material material
    {
        get
        {
            if (SCMaterial == null)
            {
                SCMaterial = new Material(SCShader);
                SCMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return SCMaterial;
        }
    }
    #endregion

    void Start()
    {
        SCShader = Shader.Find("CameraFilterPack/Pixel_Pixelisation");
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        mainCamera = GetComponent<Camera>();

        // Создаём RenderTexture для объектов без фильтра
        nonPixelatedTexture = new RenderTexture(Screen.width, Screen.height, 16);
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (SCShader != null)
        {
            // Сохраняем текущую маску слоёв камеры
            int originalCullingMask = mainCamera.cullingMask;

            // 1. Рендерим объекты из исключённого слоя в отдельную текстуру без применения фильтра
            mainCamera.cullingMask = excludeLayer; // Рендерим только объекты из исключённого слоя
            Graphics.Blit(sourceTexture, nonPixelatedTexture);

            // 2. Применяем пикселизацию ко всему остальному
            RenderTexture tempTexture = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height);
            mainCamera.cullingMask = originalCullingMask & ~excludeLayer; // Теперь рендерим всё, кроме исключённых объектов
            material.SetFloat("_Val", _Pixelisation);
            material.SetFloat("_Val2", _SizeX);
            material.SetFloat("_Val3", _SizeY);
            Graphics.Blit(sourceTexture, tempTexture, material);

            // 3. Накладываем исключённые объекты на пикселизированную сцену
            Graphics.Blit(nonPixelatedTexture, tempTexture);

            // 4. Выводим результат
            Graphics.Blit(tempTexture, destTexture);

            // Освобождаем временные ресурсы
            RenderTexture.ReleaseTemporary(tempTexture);

            // Восстанавливаем оригинальную маску слоёв камеры
            mainCamera.cullingMask = originalCullingMask;
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying != true)
        {
            SCShader = Shader.Find("CameraFilterPack/Pixel_Pixelisation");
        }
#endif
    }

    void OnDisable()
    {
        if (SCMaterial)
        {
            DestroyImmediate(SCMaterial);
        }

        if (nonPixelatedTexture)
        {
            nonPixelatedTexture.Release();
            DestroyImmediate(nonPixelatedTexture);
        }
    }
}