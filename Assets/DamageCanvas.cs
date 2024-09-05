using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour
{
    public Image backgroundImage;
    public TMP_Text damageText;
    public Ease ease = Ease.InBounce;

    private CanvasGroup _canvasGroup;
    private Transform _parent;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void LateUpdate()
    {
        transform.position = _parent.transform.position;
        transform.position += new Vector3(0f, 0.1f, 0f);
    }

    private IEnumerator AnimateCoroutine(int amount)
    {
        damageText.text = $"-{amount}";

        transform.localScale = Vector3.zero;
        transform.DOScale(1f,0.4f).SetEase(ease);

        yield return new WaitForSeconds(1);

        var fadeDuration = 0.4f;
        _canvasGroup.DOFade(0, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        Destroy(gameObject);
    }

    public void StartAnimateCoroutine(int damageAmount)
    {
        StartCoroutine(AnimateCoroutine(damageAmount));
    }

    public void SetFakeParent(Transform transform)
    {
        _parent = transform;
    }
}
