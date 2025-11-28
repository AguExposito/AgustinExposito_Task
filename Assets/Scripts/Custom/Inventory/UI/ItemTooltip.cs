using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Code.Inventory.UI
{
    public class ItemTooltip : MonoBehaviour
    {
        [Tooltip("Offset applied to tooltip position (screen space)")]
        public Vector2 Offset = new Vector2(0, -50f);
        public TextMeshProUGUI NameText;
        public TextMeshProUGUI DescriptionText;
        public RectTransform Background;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Show(ItemDefinition item, Vector2 screenPos)
        {
            NameText.text = item.name;
            DescriptionText.text = item.Description;
            transform.position = screenPos + Offset;
            LayoutRebuilder.ForceRebuildLayoutImmediate(Background);
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
            // Disable raycast targets so pointer events pass through and avoid flicker
            foreach (var graphic in GetComponentsInChildren<UnityEngine.UI.Graphic>())
            {
                graphic.raycastTarget = false;
            }
        }

        public void Hide()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}
