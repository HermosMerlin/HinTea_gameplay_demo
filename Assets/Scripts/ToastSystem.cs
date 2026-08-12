using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToastSystem : MonoBehaviour
{
    public static ToastSystem Instance { get; private set; }

    [Header("Layout")]
    [SerializeField] private float toastWidth = 400f;
    [SerializeField] private float toastHeight = 64f;
    [SerializeField] private float stackSpacing = 10f;
    [SerializeField] private float topMargin = 20f;
    [SerializeField] private float rightMargin = 20f;

    [Header("Timing")]
    [SerializeField] private float slideInDuration = 0.25f;
    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    [SerializeField] private float forceFadeDuration = 0.3f;
    [SerializeField] private float fadeRiseDistance = 20f;

    [Header("Limit")]
    [SerializeField] private int maxToasts = 5;

    [Header("Appearance")]
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private Color backgroundColor = new Color(0.09f, 0.12f, 0.18f, 0.95f);
    [SerializeField] private Color textColor = new Color(0.96f, 0.88f, 0.70f);
    [SerializeField] private float fontSize = 24f;

    [SerializeField] private float moveLerpSpeed = 10f;

    private readonly List<ToastItem> activeToasts = new List<ToastItem>();

    private class ToastItem
    {
        public GameObject gameObject;
        public RectTransform rectTransform;
        public CanvasGroup canvasGroup;
        public Coroutine lifeRoutine;
        public float targetY;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple ToastSystem instances found, destroying duplicate");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public static void Show(string message)
    {
        if (Instance == null)
        {
            Debug.LogWarning("ToastSystem is not in the scene, message printed to console only");
            Debug.Log(message);
            return;
        }
        Instance.ShowInternal(message);
    }

    private void ShowInternal(string message)
    {
        Debug.Log(message);

        if (activeToasts.Count >= maxToasts)
        {
            ForceEvictOldest();
        }

        ToastItem item = CreateToastItem(message);
        activeToasts.Insert(0, item);
        RecomputeTargetY();
        item.lifeRoutine = StartCoroutine(LifecycleRoutine(item));
    }

    private void ForceEvictOldest()
    {
        ToastItem oldest = activeToasts[activeToasts.Count - 1];
        StopCoroutine(oldest.lifeRoutine);
        StartFadeOut(oldest, forceFadeDuration);
    }

    private void RecomputeTargetY()
    {
        for (int i = 0; i < activeToasts.Count; i++)
        {
            activeToasts[i].targetY = -topMargin - i * (toastHeight + stackSpacing);
        }
    }

    private ToastItem CreateToastItem(string message)
    {
        GameObject go = new GameObject("Toast", typeof(RectTransform), typeof(CanvasGroup), typeof(Image));
        go.transform.SetParent(transform, false);

        RectTransform rect = go.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(1f, 1f);
        rect.anchorMax = new Vector2(1f, 1f);
        rect.pivot = new Vector2(1f, 0.5f);
        rect.sizeDelta = new Vector2(toastWidth, toastHeight);
        rect.anchoredPosition = new Vector2(rightMargin + toastWidth, -topMargin);

        Image background = go.GetComponent<Image>();
        background.color = backgroundColor;
        if (backgroundSprite != null)
        {
            background.sprite = backgroundSprite;
            background.type = Image.Type.Sliced;
        }

        CanvasGroup canvasGroup = go.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        GameObject textGo = new GameObject("Text", typeof(RectTransform));
        textGo.transform.SetParent(go.transform, false);
        TextMeshProUGUI text = textGo.AddComponent<TextMeshProUGUI>();
        text.text = message;
        text.color = textColor;
        text.fontSize = fontSize;
        text.alignment = TextAlignmentOptions.MidlineLeft;
        text.raycastTarget = false;

        RectTransform textRect = textGo.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = new Vector2(20f, 0f);
        textRect.offsetMax = new Vector2(-20f, 0f);

        return new ToastItem
        {
            gameObject = go,
            rectTransform = rect,
            canvasGroup = canvasGroup
        };
    }

    private IEnumerator LifecycleRoutine(ToastItem item)
    {
        float elapsed = 0f;
        while (elapsed < slideInDuration)
        {
            elapsed += Time.deltaTime;
            item.canvasGroup.alpha = Mathf.Clamp01(elapsed / slideInDuration);
            yield return null;
        }
        item.canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(displayDuration);

        StartFadeOut(item, fadeOutDuration);
    }

    private void StartFadeOut(ToastItem item, float duration)
    {
        activeToasts.Remove(item);
        RecomputeTargetY();
        StartCoroutine(FadeOutRoutine(item, duration));
    }

    private IEnumerator FadeOutRoutine(ToastItem item, float duration)
    {
        Vector2 startPosition = item.rectTransform.anchoredPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            item.canvasGroup.alpha = 1f - progress;
            item.rectTransform.anchoredPosition = startPosition + new Vector2(0f, fadeRiseDistance * progress);
            yield return null;
        }
        Destroy(item.gameObject);
    }

    private void Update()
    {
        for (int i = 0; i < activeToasts.Count; i++)
        {
            ToastItem item = activeToasts[i];
            Vector2 target = new Vector2(-rightMargin, item.targetY);
            item.rectTransform.anchoredPosition = Vector2.Lerp(item.rectTransform.anchoredPosition, target, Time.deltaTime * moveLerpSpeed);
        }
    }
}
