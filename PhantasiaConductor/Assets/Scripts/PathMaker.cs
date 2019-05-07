using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

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

        public Camera playerCamera;


        private bool tracking = false;

        private float acc;

        private SteamVR_Action_Boolean gripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
        private SteamVR_Action_Boolean pinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

        private SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

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

            
            if (gripAction.GetStateUp(hand.handType))
            {
                tracking = !tracking;
            }

            // make vertex with mouse
            if (Input.GetMouseButtonDown(0))
            {
                AddVertexFromMouse();
            }

            // save
            if (Input.GetKeyUp(KeyCode.X))
            {
                SavePath();
            }

            if (teleportAction.GetStateUp(hand.handType))
            {
                SavePath();
            }
        }

        void AddVertexFromHand()
        {
            Vector3 dir = (hand.transform.rotation * transform.forward);
            AddVertexFromDir(dir);
        }

        void AddVertexFromMouse()
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            var dir = ray.direction;
            AddVertexFromDir(dir);
        }

        void AddVertexFromDir(Vector3 dir)
        {
            vertexCount = vertexCount + 1;
            lineRenderer.SetPosition(vertexCount - 1, dir * radiusFromCenter + centerPoint.transform.position);

            if (vertexCount > 1)
            {
                var startPos = lineRenderer.GetPosition(vertexCount - 2);
                var endPos = lineRenderer.GetPosition(vertexCount - 1);
                var dif = endPos - startPos;

                float length = Vector3.Distance(startPos, endPos);
                var offset = dif.normalized * length / 2;

                // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                // BoxCollider collider = cube.AddComponent<BoxCollider>();
                // cube.transform.position = startPos + offset;
                // cube.transform.parent = transform;

                // collider.size = new Vector3(pathWidth, pathWidth, length);
                // cube.transform.localScale = collider.size;

                // Quaternion rot = Quaternion.FromToRotation(cube.transform.forward, dif);
                // cube.transform.rotation = rot;
            }
        }

        void SavePath()
        {
            string path = "Assets/Paths/path.txt";

            StreamWriter writer = new StreamWriter(path, false);
            Vector3[] vertices = positions;

            foreach (var vertex in vertices) {
                writer.WriteLine(vertex.x + "," + vertex.y + "," + vertex.z);
            }

            writer.Close();
        }

        public Vector3[] positions
        {
            get
            {
                int count = lineRenderer.positionCount;
                Vector3[] p = new Vector3[count];

                lineRenderer.GetPositions(p);
                return p;
            }
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