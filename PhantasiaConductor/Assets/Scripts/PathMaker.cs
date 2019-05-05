using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class PathMaker : MonoBehaviour
    {
        // List<LineRenderer> lineRenderers = new List<LineRenderer>();
        // Start is called before the first frame update

        public Hand hand;

        public LineRenderer lineRenderer;

        public GameObject centerPoint;

        public float radiusFromCenter = 25;


        public float pathWidth = 1;


        // refresh rate in s
        public float refreshRate = 0.1f;


        private bool tracking = false;

        private float acc;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.T))
            {
                AddVertexFromHand();
            }

            if (tracking)
            {
                if (acc > refreshRate)
                {
                    AddVertexFromHand();
                    acc -= refreshRate;
                }
                acc += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                tracking = !tracking;
            }
        }

        void AddVertexFromHand()
        {
            vertexCount = vertexCount + 1;
            Vector3 dir = (hand.transform.rotation * transform.forward);
            lineRenderer.SetPosition(vertexCount - 1, dir * radiusFromCenter + centerPoint.transform.position);

            if (vertexCount > 1) {
                var startPos = lineRenderer.GetPosition(vertexCount - 2);
                var endPos = lineRenderer.GetPosition(vertexCount - 1);
                
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                BoxCollider collider = cube.AddComponent<BoxCollider>();
                cube.transform.position = startPos;
                cube.transform.parent = transform;
                float length = Vector3.Distance(startPos, endPos);
                collider.size = new Vector3(1, 1, length);

            }
        }

        LineRenderer CreateNewPath()
        {
            // Instantiate(LineRenderer, transform.position, transform.rotation);
            return new LineRenderer();
        }

        private int vertexCount
        {
            get
            {
                return lineRenderer.positionCount;
            }

            set
            {
                lineRenderer.positionCount = value;
            }
        }
    }
}