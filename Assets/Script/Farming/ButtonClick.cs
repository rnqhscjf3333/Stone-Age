using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Button noButton;
    public Button yesButton;
    public ItemData item;

    void Start()
    {
        // noButton�� onClick �̺�Ʈ�� �߰�
        noButton.onClick.AddListener(DisableCanvas);
    }

    // ĵ������ ��Ȱ��ȭ�ϴ� �Լ�
    public void DisableCanvas()
    {
        Debug.Log("No ��ư�� ���Ƚ��ϴ�!");
        // ���� ��ũ��Ʈ�� ����� ĵ������ ��Ȱ��ȭ
        Destroy(gameObject);
    }

    public void callYesButton()
    {
        bool isPanelActive = UIManager.Instance.IsInventoryPanelActive();
        if (!isPanelActive)
        {
            ItemSlotData[] items = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
            bool addedToExistingStack = false;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Stackable(item))
                {
                    // ���� �������� �̹� ������ ������ �߰�
                    items[i].AddQuantity();
                    addedToExistingStack = true;
                    break;
                }
            }

            // ���� �������� ��� �� ���Կ� �߰��ϴ� ���
            if (!addedToExistingStack)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].itemData == null)
                    {
                        items[i].itemData = item;
                        item = null;
                        break;
                    }
                }
            }

            // �κ��丮�� �ٽ� �������ϰ� ���� ���� ������Ʈ�� �����մϴ�.
            UIManager.Instance.RenderInventory();
            //Destroy(gameObject);
        }
    }
}
