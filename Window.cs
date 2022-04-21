using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace Test
{
    static class Constants
    {
        public const string path = "../../../Shader/";

    }
    public class window : GameWindow
    {
        private readonly string path = "D:/Coding Area/Semester 4/GrafKom/Test/Shader/";

        //Assets1[] _object = new Assets1[7];

        //Assets_3D[] _object3d = new Assets_3D[13];
        List<Assets_3D> _object3d = new List<Assets_3D>();
        List<Assets_3D> child = new List<Assets_3D>();
        Camera _camera;
        bool _firstmove = true;
        Vector2 _lastPos;
        Vector3 _objecPost = new Vector3(0,0,0);
        float _rotationSpeed = 0.25f;

        double _time;
        float degr = 0;


        public window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            CenterWindow();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.75f, 0.77f, 0.55f, 1.0f);

            _object3d.Add(new Assets_3D(new Vector3(0.13f, 0.13f, 0.13f)));

            //muka
            _object3d[0].createBoxVertices(0.0f, -0.10f, 0, 0.18f);
            _object3d[0].addChild(0.00f, -0.17f, 0, 0.18f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[0].addChild(-0.05f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.90f, 0.90f, 0.90f));
            _object3d[0].addChild(-0.03f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(0.05f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.99f, 0.99f, 0.99f));
            _object3d[0].addChild(0.03f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(-0.01f, -0.21f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(0.01f, -0.21f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));

            //topi
            _object3d[0].addChild(-0.07f, -0.08f, 0.05f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(-0.07f, -0.08f, 0.08f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(-0.07f, -0.08f, 0.13f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));

            _object3d[0].addChild(-0.02f, -0.08f, 0.05f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(-0.02f, -0.08f, 0.08f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(-0.02f, -0.08f, 0.13f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));

            _object3d[0].addChild(0.03f, -0.08f, 0.05f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(0.03f, -0.08f, 0.08f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(0.03f, -0.08f, 0.13f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));

            _object3d[0].addChild(0.07f, -0.08f, 0.05f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(0.07f, -0.08f, 0.08f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(0.07f, -0.08f, 0.13f, 0.05f, 0, new Vector3(0.13f, 0.13f, 0.13f));



            //badan
            _object3d[0].addChild(0.0f, -0.29f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[0].addChild(0.0f, -0.365f, 0, 0.15f, 0, new Vector3(0.05f, 0.31f, 0.55f));
            _object3d[0].addChild(0.0f, -0.44f, 0, 0.15f, 0, new Vector3(0.05f, 0.31f, 0.55f));
            _object3d[0].addChild(0.0f, -0.58f, 0, 0.15f, 0, new Vector3(0.05f, 0.31f, 0.55f));
            _object3d[0].addChild(0.0f, -0.61f, 0, 0.15f, 0, new Vector3(0.05f, 0.31f, 0.55f));

            //tangan
            _object3d[0].addChild(-0.12f, -0.34f, 0, 0.10f, 0, new Vector3(0.05f, 0.31f, 0.55f));
            _object3d[0].addChild(-0.15f, -0.36f, 0, 0.10f, 0, new Vector3(0.05f, 0.31f, 0.55f));
            _object3d[0].addChild(-0.17f, -0.43f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[0].addChild(-0.15f, -0.52f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[0].addChild(-0.13f, -0.58f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));

            _object3d[0].addChild(0.12f, -0.34f, 0, 0.10f, 0, new Vector3(0.05f, 0.31f, 0.55f));
            _object3d[0].addChild(0.15f, -0.36f, 0, 0.10f, 0, new Vector3(0.05f, 0.31f, 0.55f));
            _object3d[0].addChild(0.17f, -0.43f, 0f, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[0].addChild(0.15f, -0.52f, 0f, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[0].addChild(0.13f, -0.58f, 0f, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));


            //kaki
            _object3d[0].addChild(0.06f, -0.73f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(0.06f, -0.81f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(0.06f, -0.89f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(0.06f, -0.97f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[0].addChild(0.06f, -0.92f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));

            _object3d[0].addChild(-0.06f, -0.73f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(-0.06f, -0.81f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(-0.06f, -0.89f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[0].addChild(-0.06f, -0.97f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[0].addChild(-0.06f, -0.92f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));


            //_object3d[0].createEllipsoid2(0.2f, 0.2f, 0.2f, 0.0f, 0.0f, 0.0f, 72, 24);
            //_object3d[0].createEllipsoid(0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 0.0f);
            //_object3d[0].addChild(-0.54f, -0.68f, 0.0f, 0.25f,0);
            //_object3d[0].addChild(-0.54f, -0.82f, 0.0f, 0.25f,0);
            //_object3d[0].addChild(-0.54f, -0.96f, 0.0f, 0.25f,0);

            _object3d.Add(new Assets_3D(new Vector3(0.87f, 0.68f, 0.45f)));
            //_object3d[1].createNewEllipsoid(0.2f, 0.5f, 0.2f, 0.0f, 0.0f, 0.0f, 72, 24);
            _object3d[1].createBoxVertices(-0.54f, -0.17f, 0, 0.18f);
            _object3d[1].addChild(-0.49f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.90f, 0.90f, 0.90f));
            _object3d[1].addChild(-0.51f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[1].addChild(-0.59f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.90f, 0.90f, 0.90f));
            _object3d[1].addChild(-0.57f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[1].addChild(-0.53f, -0.21f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[1].addChild(-0.55f, -0.21f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));

            //badan
            _object3d[1].addChild(-0.54f, -0.29f, 0, 0.10f,0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[1].addChild(-0.54f, -0.365f, 0, 0.15f,0, new Vector3(0.54f, 0.64f, 0.48f));
            _object3d[1].addChild(-0.54f, -0.44f, 0, 0.15f, 0, new Vector3(0.54f, 0.64f, 0.48f));
            _object3d[1].addChild(-0.54f, -0.58f, 0, 0.15f, 0, new Vector3(0.54f, 0.64f, 0.48f));
            _object3d[1].addChild(-0.54f, -0.61f, 0, 0.15f, 0, new Vector3(0.54f, 0.64f, 0.48f));

            //tangan
            _object3d[1].addChild(-0.42f, -0.34f, 0, 0.10f, 0, new Vector3(0.54f, 0.64f, 0.48f));
            _object3d[1].addChild(-0.41f, -0.44f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[1].addChild(-0.40f, -0.54f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[1].addChild(-0.39f, -0.58f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));

            _object3d[1].addChild(-0.66f, -0.34f, 0, 0.10f, 0, new Vector3(0.54f, 0.64f, 0.48f));
            _object3d[1].addChild(-0.67f, -0.44f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[1].addChild(-0.68f, -0.54f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[1].addChild(-0.69f, -0.58f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));

            //kaki
            _object3d[1].addChild(-0.60f, -0.73f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[1].addChild(-0.60f, -0.81f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[1].addChild(-0.60f, -0.89f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[1].addChild(-0.60f, -0.97f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[1].addChild(-0.60f, -0.92f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));

            _object3d[1].addChild(-0.48f, -0.73f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[1].addChild(-0.48f, -0.81f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[1].addChild(-0.48f, -0.89f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[1].addChild(-0.48f, -0.97f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[1].addChild(-0.48f, -0.92f, 0, 0.10f, 0, new Vector3(0.13f, 0.13f, 0.13f));

            //obj 2
            _object3d.Add(new Assets_3D(new Vector3(0.87f, 0.68f, 0.45f)));
            _object3d[2].createBoxVertices(-1.20f, -0.17f, 0, 0.18f);
            _object3d[2].addChild(-1.15f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.90f, 0.90f, 0.90f));
            _object3d[2].addChild(-1.17f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[2].addChild(-1.25f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.90f, 0.90f, 0.90f));
            _object3d[2].addChild(-1.23f, -0.15f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[2].addChild(-1.19f, -0.21f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));
            _object3d[2].addChild(-1.21f, -0.21f, 0.03f, 0.03f, 0, new Vector3(0.13f, 0.13f, 0.13f));

            //badan
            _object3d[2].addChild(-1.20f, -0.29f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[2].addChild(-1.20f, -0.366f, 0, 0.15f, 0, new Vector3(0.26f, 0.17f, 0.18f));
            _object3d[2].addChild(-1.20f, -0.44f, 0, 0.15f, 0, new Vector3(0.26f, 0.17f, 0.18f));
            _object3d[2].addChild(-1.20f, -0.58f, 0, 0.15f, 0, new Vector3(0.26f, 0.17f, 0.18f));
            _object3d[2].addChild(-1.20f, -0.61f, 0, 0.15f, 0, new Vector3(0.26f, 0.17f, 0.18f));

            //tangan
            _object3d[2].addChild(-1.32f, -0.34f, 0, 0.10f, 0, new Vector3(0.26f, 0.17f, 0.18f));
            _object3d[2].addChild(-1.36f, -0.34f, 0, 0.10f, 0, new Vector3(0.26f, 0.17f, 0.18f));
            _object3d[2].addChild(-1.40f, -0.34f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[2].addChild(-1.44f, -0.34f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[2].addChild(-1.44f, -0.30f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[2].addChild(-1.44f, -0.22f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[2].addChild(-1.44f, -0.22f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[2].addChild(-1.44f, -0.22f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));

            //bola
            _object3d[2].addChildElipsoid(-1.44f, -0.08f, 0, 0.10f, 0.10f, 0.10f, 72, 24, new Vector3(0.74f, 0.46f, 0.26f));


            //C
            _object3d[2].addChild(-1.48f, -0.05f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.47f, -0.05f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.46f, -0.05f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));

            _object3d[2].addChild(-1.48f, -0.06f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.48f, -0.07f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.48f, -0.08f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.48f, -0.09f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));

            _object3d[2].addChild(-1.48f, -0.09f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.47f, -0.09f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.46f, -0.09f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));

            //K
            _object3d[2].addChild(-1.43f, -0.05f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.43f, -0.06f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.43f, -0.07f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));

            _object3d[2].addChild(-1.42f, -0.07f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.41f, -0.06f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.40f, -0.05f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.41f, -0.08f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.40f, -0.09f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));


            _object3d[2].addChild(-1.43f, -0.08f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.43f, -0.09f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));
            _object3d[2].addChild(-1.43f, -0.09f, 0.03f, 0.01f, 0, new Vector3(0.15f, 0.15f, 0.15f));



            _object3d[2].addChild(-1.08f, -0.34f, 0, 0.10f, 0, new Vector3(0.26f, 0.17f, 0.18f));
            _object3d[2].addChild(-1.07f, -0.44f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[2].addChild(-1.06f, -0.54f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[2].addChild(-1.05f, -0.58f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));

            //kaki
            _object3d[2].addChild(-1.26f, -0.73f, 0, 0.10f, 0, new Vector3(0.14f, 0.38f, 0.35f));
            _object3d[2].addChild(-1.26f, -0.81f, 0, 0.10f, 0, new Vector3(0.14f, 0.38f, 0.35f));
            _object3d[2].addChild(-1.26f, -0.89f, 0, 0.10f, 0, new Vector3(0.14f, 0.38f, 0.35f));
            _object3d[2].addChild(-1.26f, -0.97f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[2].addChild(-1.26f, -0.92f, 0, 0.10f, 0, new Vector3(0.14f, 0.38f, 0.35f));

            _object3d[2].addChild(-1.14f, -0.73f, 0, 0.10f, 0, new Vector3(0.14f, 0.38f, 0.35f));
            _object3d[2].addChild(-1.14f, -0.81f, 0, 0.10f, 0, new Vector3(0.14f, 0.38f, 0.35f));
            _object3d[2].addChild(-1.14f, -0.89f, 0, 0.10f, 0, new Vector3(0.14f, 0.38f, 0.35f));
            _object3d[2].addChild(-1.14f, -0.97f, 0, 0.10f, 0, new Vector3(0.87f, 0.68f, 0.45f));
            _object3d[2].addChild(-1.14f, -0.92f, 0, 0.10f, 0, new Vector3(0.14f, 0.38f, 0.35f));

            //ryan
            _object3d.Add(new Assets_3D(0.55f, 0.55f, 0.55f, 1f));
            _object3d[3].createReversePyramidBox(true, 0, 0, 0, 1.8f, 0.8f, 0.4f, 0.2f);

            //tiang tegak
            _object3d.Add(new Assets_3D(1f, 1f, 1f, 1f));
            _object3d[4].createBoxVertices(-0.72f, -0.5f, -0.33f, 0.02f, 0.7f, 0.02f);
            _object3d[4].createBoxChild(-0.4f, -0.3f, -0.9f, 0.02f, 0.4f, 0.02f);
            _object3d[4].createBoxChild(0.2f, -0.3f, -0.9f, 0.02f, 0.4f, 0.02f);
            _object3d[4].createBoxChild(0.8f, -0.3f, -0.9f, 0.02f, 0.4f, 0.02f);
            _object3d[4].createBoxChild(1.4f, -0.3f, -0.9f, 0.02f, 0.4f, 0.02f);
            _object3d[4].createBoxChild(2f, -0.3f, -0.9f, 0.02f, 0.4f, 0.02f);
            _object3d[4].createBoxChild(-0.35f, -0.4f, -0.33f, 0.02f, 0.5f, 0.02f);
            _object3d[4].createBoxChild(-0.35f, -0.4f, -0.33f, 0.02f, 0.5f, 0.02f);
            _object3d[4].createBoxChild(0.3f, -0.3f, -0.36f, 0.14f, 0.4f, 0.05f, 1f, 1f, 0, 1f);
            _object3d[4].createBoxChild(1f, -0.3f, -0.36f, 0.14f, 0.4f, 0.05f, 1f, 1f, 0, 1f);
            _object3d[4].createBoxChild(-0.35f, -0.4f, 0.30f, 0.14f, 0.5f, 0.05f, 1f, 1f, 0, 1f);
            _object3d[4].createBoxChild(-0.72f, -0.5f, 0.33f, 0.02f, 0.7f, 0.02f);
            _object3d[4].createBoxChild(0.1f, -0.3f, 0.33f, 0.02f, 0.3f, 0.02f);
            _object3d[4].createBoxChild(0.9f, -0.3f, 0.33f, 0.02f, 0.3f, 0.02f);
            _object3d[4].createBoxChild(0.5f, -0.3f, 0.33f, 0.02f, 0.3f, 0.02f);

            //gedung miring kuning biru
            _object3d.Add(new Assets_3D(0.8f, 0.8f, 0.8f, 1f));
            _object3d[5].createPararelogram(-0.65f, 0.35f, -0.8f, 0.6f, 2.5f, 0.9f, 0.7f);

            //tangga depan 1
            _object3d.Add(new Assets_3D(0.8f, 0.8f, 0.8f, 1f));
            _object3d[6].createStaircase(-0.72f, -0.82f, 0, 0.68f, 8);

            //lantai 
            _object3d.Add(new Assets_3D(0.7f, 0.7f, 0.7f, 1f));
            _object3d[7].createBoxVertices(0.34f, -0.74f, 0, 1.54f, 0.2f, 0.68f);
            _object3d[7].createBoxChild(0.6f, -0.56f, 0, 1f, 0.2f, 0.68f);
            _object3d[7].createBoxChild(0.8f, -0.66f, -0.65f, 2.5f, 0.4f, 0.6f);

            //tangga depan 2
            _object3d.Add(new Assets_3D(0.8f, 0.8f, 0.8f, 1f));
            _object3d[8].createStaircase(-0.25f, -0.64f, 0, 0.68f, 8);

            //teras biru kuning
            _object3d.Add(new Assets_3D(0, 0, 1f, 10f));
            _object3d[9].createBoxVertices(-0.38f, 0.75f, -1.0f, 0.05f, 0.05f, 0.55f);

            //gedung miring abu2
            _object3d.Add(new Assets_3D(0.5f, 0.5f, 0.5f, 1f));
            _object3d[10].createPararelogram(0, -0.15f, 2.38f, 0.68f, 2.5f, 1.4f, 0.3f);


            for (int local = 0; local < 1; local++)
            {
                float j = -1.05f;
                float l = -0.7f;
                int k = 0;
                _object3d[9].createBoxChild(1.35f, 0.75f, l, 1.5f, 0.05f, 0.05f, 1f, 1f, 0, 1f);
                _object3d[9].createBoxChild(0.1f, 0.75f, l, 1f, 0.05f, 0.05f);

                for (float i = 0.65f; i >= -0.15f; i -= 0.1f)
                {
                    k = k + 1;
                    j = j + 0.04f;
                    l = l + 0.04f;

                    if (k == 1 || k == 7 || k == 6 || k == 5)
                    {
                        _object3d[9].createBoxChild(-0.38f, i, (j + 0.05f), 0.05f, 0.05f, 0.55f, 1f, 1f, 0f, 1f);
                        _object3d[9].createBoxChild(1.35f, i, l, 1.5f, 0.05f, 0.05f);
                        _object3d[9].createBoxChild(0.1f, i, l, 1f, 0.05f, 0.05f, 1f, 1f, 0, 1f);
                    }
                    else
                    {
                        _object3d[9].createBoxChild(-0.38f, i, (j + 0.05f), 0.05f, 0.05f, 0.55f);
                        _object3d[9].createBoxChild(1.35f, i, l, 1.5f, 0.05f, 0.05f, 1f, 1f, 0, 1f);
                        _object3d[9].createBoxChild(0.1f, i, l, 1f, 0.05f, 0.05f);
                    }
                }
            }

            for (int i = 0; i < _object3d.Count(); i++)
            {
                _object3d[i].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            }



            _camera = new Camera(new Vector3(0, 0, 1), Size.X / (float)Size.Y);
            CursorGrabbed = false;
            
        }

        protected override void OnRenderFrame(FrameEventArgs args) // ini update tiap frame
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Matrix4 temp = Matrix4.Identity;
            //temp += temp * Matrix4.CreateTranslation(0.0f, 0.0f, 1.0f);
            //deg += MathHelper.DegreesToRadians(0.01f);
            //temp = temp * Matrix4.CreateRotationX(deg);
            //_object3d[0].render(0, temp);

            _time += args.Time;
            Matrix4 temp = Matrix4.Identity;
            temp = temp * Matrix4.CreateTranslation(0.5f, 0.5f, 0.0f);
            degr += MathHelper.DegreesToRadians(0.05f);
            //temp = temp * Matrix4.CreateRotationY(degr);

            //_object3d[0].rotate(_object3d[0]._centerPosition, _object3d[0]._euler[1], 0.32f);\

            for (int i = 0; i < 3; i++)
            {
                _object3d[i].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }

            _object3d[3].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[7].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[8].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[5].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[4].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[6].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[2].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[1].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[9].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());

            SwapBuffers();

        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)// ini jalan berdasarkan fps kita, kalau yang onrenderframe sudah 60 frame maka fungsi ini akan jalan
        {
            base.OnUpdateFrame(args);

            var key_input = KeyboardState;
            var mouse_input = MouseState;

            if (key_input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (key_input.IsKeyPressed(Keys.A))
            {
                Console.WriteLine("A Pressed");
            }
            if (key_input.IsKeyReleased(Keys.A))
            {
                Console.WriteLine("Key A Sudah ditekan");
            }

            float cameraspeed = 0.5f;
            if (key_input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraspeed * (float)args.Time;
            }
            if (key_input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraspeed * (float) args.Time;    
            }
            if (key_input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraspeed * (float)args.Time;
            }
            if (key_input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraspeed * (float)args.Time;
            }
            if (key_input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraspeed * (float)args.Time;
            }
            if (key_input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraspeed * (float)args.Time;
            }

            var mouse = MouseState;
            var sensitivity = 0.2f;

            if (_firstmove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstmove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var delatY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= delatY * sensitivity;
            }

            if (KeyboardState.IsKeyDown(Keys.N))
            {
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objecPost;
                _camera.Yaw += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, _rotationSpeed).ExtractRotation());
                _camera.Position += _objecPost;

                _camera._front = -Vector3.Normalize(_camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objecPost;
                _camera.Yaw -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objecPost;

                _camera._front = -Vector3.Normalize(_camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objecPost;
                _camera.Pitch -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, _rotationSpeed).ExtractRotation());
                _camera.Position += _objecPost;
                _camera._front = -Vector3.Normalize(_camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objecPost;
                _camera.Pitch += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objecPost;
                _camera._front = -Vector3.Normalize(_camera.Position - _objecPost);
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButton.Left)
            {
                float _x = (MousePosition.X - Size.X / 2) / (Size.X / 2);// ini buat normalisasiin
                float _y = -(MousePosition.Y - Size.Y / 2) / (Size.Y / 2);// dikasi minus biar ndk kebalek

                Console.WriteLine("x = " + _x + " y = " + _y);
                //_object[5].updateMousePosition(_x, _y);

            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _camera.Fov = _camera.Fov - e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)// akan jalan tiap kali ada perubahan
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float) Size.Y;
        }

        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatix;
        }
    }
}
