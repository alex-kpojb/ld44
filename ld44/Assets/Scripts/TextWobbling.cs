using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWobbling : MonoBehaviour
{
    public float AngleMultiplier = 1.0f;
    public float SpeedMultiplier = 0.2f;
    public float CurveScale = 1.0f;

    TMP_Text textComponent;
    bool hasTextChanged;

    struct VertexAnim
    {
        public float angleRange;
        public float angle;
        public float speed;
    }

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
    }

    private void OnEnable() {
        StartCoroutine(AnimateVertex());
    }

    private void OnDestroy() {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }

    void ON_TEXT_CHANGED(Object obj) {
        if (obj == textComponent) 
            hasTextChanged = true;
    }

    void Start()
    {
        
    }

    IEnumerator AnimateVertex() {
        textComponent.ForceMeshUpdate();

        var textInfo = textComponent.textInfo;

        Matrix4x4 matrix;

        int loopCount = 0;
        hasTextChanged = true;

        VertexAnim[] vertexAnim = new VertexAnim[1024];
        for (int i =0; i<1024; i++) {
            vertexAnim[i].angleRange = Random.Range(10f, 25f);
            vertexAnim[i].speed = Random.Range(1f, 3f);
        }

        TMP_MeshInfo[] cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

        while (true) {
            if (hasTextChanged) {
                cachedMeshInfo = textInfo.CopyMeshInfoVertexData();
                hasTextChanged = false;
            }

            int characterCount = textInfo.characterCount;

            for (int i = 0; i < characterCount; i++) {
                var charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible)
                    continue;

                VertexAnim vertAnim = vertexAnim[i];
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                Vector3[] sourceVertices = cachedMeshInfo[materialIndex].vertices;

                Vector2 charMidBaseLine = (sourceVertices[vertexIndex + 0] + sourceVertices[vertexIndex + 2]) / 2;

                Vector3 offset = charMidBaseLine;

                Vector3[] destinationVertices = textInfo.meshInfo[materialIndex].vertices;

                destinationVertices[vertexIndex + 0] = sourceVertices[vertexIndex + 0] - offset;
                destinationVertices[vertexIndex + 1] = sourceVertices[vertexIndex + 1] - offset;
                destinationVertices[vertexIndex + 2] = sourceVertices[vertexIndex + 2] - offset;
                destinationVertices[vertexIndex + 3] = sourceVertices[vertexIndex + 3] - offset;

                vertAnim.angle = Mathf.SmoothStep(
                    -vertAnim.angleRange,
                    vertAnim.angleRange,
                    Mathf.PingPong(loopCount / 25f * vertAnim.speed, 1f));

                Vector3 wobblingOffset = new Vector3(
                    Random.Range(-0.25f, 0.25f),
                    Random.Range(-0.25f, 0.25f), 0);

                matrix = Matrix4x4.TRS(
                    wobblingOffset * CurveScale,
                    Quaternion.Euler(0, 0, Random.Range(-5f, 5f) * AngleMultiplier),
                    Vector3.one);

                destinationVertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 0]);
                destinationVertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 1]);
                destinationVertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 2]);
                destinationVertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(destinationVertices[vertexIndex + 3]);

                destinationVertices[vertexIndex + 0] += offset;
                destinationVertices[vertexIndex + 1] += offset;
                destinationVertices[vertexIndex + 2] += offset;
                destinationVertices[vertexIndex + 3] += offset;

                vertexAnim[i] = vertAnim;
            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++) {
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                textComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            loopCount += 1;

            yield return new WaitForSeconds(1f * SpeedMultiplier);
        }
    }
}
