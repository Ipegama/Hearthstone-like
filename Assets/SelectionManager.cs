using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager 
{
    private static Camera _camera;
    public static Camera Camera 
    {
        get
        {
            if(_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    public static T GetObjectAtCursor<T>() where T : Component
    {
        var ray = Camera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        foreach(var hit in hits)
        {
            T obj = hit.collider.GetComponent<T>();
            if(obj != null) return obj;
        }
        return null;
    }
}
