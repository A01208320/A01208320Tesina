using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableTransform {
    public List<Transform> Transforms;
}
[System.Serializable]
public class SerializableVector3 {
    public List<Vector3> Vector3s;
}
[System.Serializable]
public class SerializableRenderer {
    public List<Renderer> Renderers;
}
[System.Serializable]
public class SerializableColor {
    public List<Color> Colors;
}

[System.Serializable]
public class TransformList {
    public List<SerializableTransform> TransformLists;
}
[System.Serializable]
public class Vector3List {
    public List<SerializableVector3> Vector3Lists;
}
[System.Serializable]
public class RendererList {
    public List<SerializableRenderer> RendererLists;
}
[System.Serializable]
public class ColorList {
    public List<SerializableColor> ColorLists;
}
