using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRowManager : MonoBehaviour
{
    public Transform rowContainer;
    public List<Transform> correctOrder = new List<Transform>();

    public List<Transform> slots = new List<Transform>();
    public List<DraggableItem> items = new List<DraggableItem>();

    public float animationDuration = 0.3f;

    private void Start()
    {
        slots.Clear();
        foreach (Transform slot in rowContainer)
        {
            slots.Add(slot);
        }

        items = new List<DraggableItem>(rowContainer.GetComponentsInChildren<DraggableItem>());

        int count = Mathf.Min(items.Count, slots.Count);
        for (int i = 0; i < count; i++)
        {
            items[i].transform.SetParent(slots[i], worldPositionStays: false);
            items[i].transform.localPosition = Vector3.zero;
            items[i].originalParent = slots[i];
        }
    }

    public bool IsCorrectlyOrdered()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var item = slots[i].GetComponentInChildren<DraggableItem>();
            if (item == null || item.itemID != correctOrder[i].GetComponent<DraggableItem>().itemID)
                return false;
        }
        return true;
    }

    public void InsertItemAtPosition(DraggableItem draggedItem)
    {
        int oldIndex = items.IndexOf(draggedItem);
        if (oldIndex == -1) return;

        int targetIndex = FindClosestSlotIndex(draggedItem.transform.position);

        if (targetIndex == -1 || targetIndex == oldIndex)
        {
            StartCoroutine(AnimateItemToSlot(draggedItem, slots[oldIndex]));
            return;
        }

        items.RemoveAt(oldIndex);
        items.Insert(targetIndex, draggedItem);

        for (int i = 0; i < items.Count; i++)
        {
            items[i].originalParent = slots[i];
            StartCoroutine(AnimateItemToSlot(items[i], slots[i]));
        }
        Debug.Log(IsCorrectlyOrdered());
    }

    private int FindClosestSlotIndex(Vector3 position)
    {
        int closestIndex = -1;
        float minDist = float.MaxValue;
        for (int i = 0; i < slots.Count; i++)
        {
            float dist = Mathf.Abs(position.x - slots[i].position.x);
            if (dist < minDist)
            {
                minDist = dist;
                closestIndex = i;
            }
        }
        return closestIndex;
    }

    private IEnumerator AnimateItemToSlot(DraggableItem item, Transform slot)
    {
        RectTransform itemRect = item.GetComponent<RectTransform>();
        RectTransform slotRect = slot.GetComponent<RectTransform>();

        Vector3 startPos = itemRect.position;

        // Меняем родителя без сохранения позиции
        itemRect.SetParent(slotRect, worldPositionStays: false);

        Vector3 endPos = slotRect.position;

        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            itemRect.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        itemRect.position = endPos;
        itemRect.localPosition = Vector3.zero; // гарантируем локальное выравнивание
    }
}