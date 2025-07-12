using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTextEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text textMesh;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.cyan;

    void Start()
    {
        if (textMesh == null)
            textMesh = GetComponent<TMP_Text>();

        textMesh.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMesh.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.color = normalColor;
    }
}
