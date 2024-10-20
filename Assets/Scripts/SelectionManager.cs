using UnityEngine;

public static class SelectionManager
{
    public static T GetObjectAtCursor<T>()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        foreach (var hit in hits)
        {
            T obj = hit.collider.GetComponent<T>();
            if (obj != null) return obj;
            obj = hit.collider.GetComponentInParent<T>();
            if (obj != null) return obj;
        }
        return default;
    }
}
