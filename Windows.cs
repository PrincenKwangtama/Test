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

        bool expired;
        float time_passed;
        int animation_stage = 0;

        double _time;
        float degr = 0;
        int count = 0;


        public window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            CenterWindow();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.75f, 0.77f, 0.55f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            _camera = new Camera(new Vector3(-0.5f, -1f, 1f), Size.X / (float)Size.Y);
            //muka
            _object3d.Add(new Assets_3D(0.87f, 0.68f, 0.45f, 1f));

            //_object3d[0].createBoxVertices(-1.6f, -0.10f, 0, 0.15f);
            _object3d[0].createBoxVertices(-1.6f, -0.17f, 0, 0.15f);
            _object3d[0].addChild(-1.53f, -0.15f, 0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[0].addChild(-1.53f, -0.15f, 0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[0].addChild(-1.53f, -0.15f, -0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[0].addChild(-1.53f, -0.15f, -0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[0].addChild(-1.53f, -0.20f, 0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[0].addChild(-1.53f, -0.20f, -0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);

            //rambut
            _object3d[0].addChildBalok(-1.6f, -0.085f, 0.00f, 0.15f, 0.02f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut belakang
            _object3d[0].addChildBalok(-1.68f, -0.145f, -0f, 0.02f, 0.14f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            //rambut samping
            _object3d[0].addChildBalok(-1.65f, -0.145f, -0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[0].addChildBalok(-1.6f, -0.125f, -0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[0].addChildBalok(-1.545f, -0.110f, -0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[0].addChildBalok(-1.65f, -0.145f, 0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[0].addChildBalok(-1.6f, -0.125f, 0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[0].addChildBalok(-1.545f, -0.110f, 0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut depan
            _object3d[0].addChildBalok(-1.53f, -0.095f, 0f, 0.02f, 0.04f, 0.19f, 1, 0.13f, 0.13f, 0.13f, 1f);


            //badan
            _object3d[0].addChild(-1.6f, -0.26f, 0, 0.07f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[0].addChildBalok(-1.6f, -0.44f, 0f, 0.12f, 0.33f, 0.12f, 1, 0.05f, 0.31f, 0.55f, 1f);

            //Toga
            _object3d[0].addChildElipsoids(0.03f, 0.03f, 0.03f, -1.6f, -0.07f, 0f, 0.01f, 0.25f, 0.53f, 1f);
            _object3d[0].addChildBalok(-1.59f, -0.04f, -0.005f, 0.12f, 0.01f, 0.12f, 1, 0.01f, 0.25f, 0.53f, 1f);

            //tangan
            _object3d[0].addChild(-1.605f, -0.315f, 0.10f, 0.08f, 0, 0.05f, 0.31f, 0.55f, 1f);
            _object3d[0].addChild(-1.605f, -0.315f, 0.12f, 0.08f, 0, 0.05f, 0.31f, 0.55f, 1f);
            _object3d[0].addChild(-1.605f, -0.395f, 0.14f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[0].addChild(-1.605f, -0.465f, 0.12f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[0].addChild(-1.605f, -0.525f, 0.11f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            _object3d[0].addChild(-1.605f, -0.315f, -0.10f, 0.08f, 0, 0.05f, 0.31f, 0.55f, 1f);
            _object3d[0].addChild(-1.605f, -0.315f, -0.12f, 0.08f, 0, 0.05f, 0.31f, 0.55f, 1f);
            _object3d[0].addChild(-1.605f, -0.395f, -0.14f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[0].addChild(-1.605f, -0.465f, -0.12f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[0].addChild(-1.605f, -0.525f, -0.11f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            //obj 1
            _object3d.Add(new Assets_3D(0.87f, 0.68f, 0.45f,1f));
            //_object3d[1].createNewEllipsoid(0.2f, 0.5f, 0.2f, 0.0f, 0.0f, 0.0f, 72, 24);
            _object3d[1].createBoxVertices(-2.04f, -0.17f, 0, 0.15f);
            _object3d[1].addChild(-1.97f, -0.15f, 0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[1].addChild(-1.97f, -0.15f, 0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[1].addChild(-1.97f, -0.15f, -0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[1].addChild(-1.97f, -0.15f, -0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[1].addChild(-1.97f, -0.20f, 0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[1].addChild(-1.97f, -0.20f, -0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);

            //rambut
            _object3d[1].addChildBalok(-2.04f, -0.085f, 0.00f, 0.15f, 0.02f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut belakang
            _object3d[1].addChildBalok(-2.12f, -0.145f, -0f, 0.02f, 0.14f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            //rambut samping
            _object3d[1].addChildBalok(-2.09f, -0.145f, -0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[1].addChildBalok(-2.04f, -0.125f, -0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[1].addChildBalok(-1.985f, -0.110f, -0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[1].addChildBalok(-2.09f, -0.145f, 0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[1].addChildBalok(-2.04f, -0.125f, 0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[1].addChildBalok(-1.985f, -0.110f, 0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut depan
            _object3d[1].addChildBalok(-1.97f, -0.095f, 0f, 0.02f, 0.04f, 0.19f, 1, 0.13f, 0.13f, 0.13f, 1f);

            //badan
            _object3d[1].addChild(-2.04f, -0.26f, 0, 0.07f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[1].addChildBalok(-2.04f, -0.44f, 0f, 0.12f, 0.33f, 0.12f, 1, 0.54f, 0.64f, 0.48f, 1f);

            //Toga
            _object3d[1].addChildElipsoids(0.03f, 0.03f, 0.03f, -2.04f, -0.07f, 0f, 0.01f, 0.25f, 0.53f, 1f);
            _object3d[1].addChildBalok(-2.03f, -0.04f, -0.005f, 0.12f, 0.01f, 0.12f, 1, 0.01f, 0.25f, 0.53f, 1f);

            //obj 2
            _object3d.Add(new Assets_3D(0.87f, 0.68f, 0.45f, 1f));
            _object3d[2].createBoxVertices(-2.50f, -0.17f, 0, 0.15f);
            _object3d[2].addChild(-2.43f, -0.15f, 0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[2].addChild(-2.43f, -0.15f, 0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[2].addChild(-2.43f, -0.15f, -0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[2].addChild(-2.43f, -0.15f, -0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[2].addChild(-2.43f, -0.20f, 0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[2].addChild(-2.43f, -0.20f, -0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);

            //rambut
            _object3d[2].addChildBalok(-2.50f, -0.085f, 0.00f, 0.15f, 0.02f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut belakang
            _object3d[2].addChildBalok(-2.58f, -0.145f, -0f, 0.02f, 0.14f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            //rambut samping
            _object3d[2].addChildBalok(-2.55f, -0.145f, -0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[2].addChildBalok(-2.50f, -0.125f, -0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[2].addChildBalok(-2.445f, -0.110f, -0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[2].addChildBalok(-2.55f, -0.145f, 0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[2].addChildBalok(-2.50f, -0.125f, 0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[2].addChildBalok(-2.445f, -0.110f, 0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut depan
            _object3d[2].addChildBalok(-2.43f, -0.095f, 0f, 0.02f, 0.04f, 0.19f, 1, 0.13f, 0.13f, 0.13f, 1f);


            //badan
            _object3d[2].addChild(-2.50f, -0.26f, 0, 0.07f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[2].addChildBalok(-2.50f, -0.44f, 0f, 0.12f, 0.33f,0.12f,1, 0.26f, 0.17f, 0.18f, 1f);

            //Toga
            _object3d[2].addChildElipsoids(0.03f,0.03f,0.03f,-2.50f,-0.07f,0f, 0.01f, 0.25f, 0.53f, 1f);
            _object3d[2].addChildBalok(-2.49f, -0.04f, -0.005f, 0.12f, 0.01f, 0.12f, 1, 0.01f, 0.25f, 0.53f, 1f);

            // jam tangan
            _object3d.Add(new Assets_3D(0.8f, 0.8f, 0.8f, 1f));
            _object3d[3].createTube(-2.51f, -0.23f, 0.26f, 0.015f, 0.015f, 1, true);

            _object3d[3].addChildBalok(-2.535f, -0.23f, 0.265f, 0.02f, 0.01f, 0.011f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[3].addChildBalok(-2.48f, -0.23f, 0.265f, 0.03f, 0.01f, 0.011f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[3].addChildBalok(-2.55f, -0.23f, 0.22f, 0.01f, 0.011f, 0.10f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[3].addChildBalok(-2.46f, -0.23f, 0.22f, 0.01f, 0.011f, 0.10f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[3].addChildBalok(-2.505f, -0.23f, 0.17f, 0.101f, 0.01f, 0.011f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[3].addChildBalok(-2.51f, -0.225f, 0.28f, 0.005f, 0.015f, 0.005f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[3].addChildBalok(-2.513f, -0.23f, 0.28f, 0.010f, 0.005f, 0.005f, 1, 0.13f, 0.13f, 0.13f, 1f);

            // kaki orang 1
            _object3d.Add(new Assets_3D(0.13f, 0.13f, 0.13f, 1f));
            _object3d[4].createBoxVertices(-1.60f, -0.75f, -0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[4].addChildBalok(-1.60f, -0.915f, -0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            _object3d.Add(new Assets_3D(0.13f, 0.13f, 0.13f, 1f));
            _object3d[5].createBoxVertices(-1.60f, -0.75f, 0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[5].addChildBalok(-1.60f, -0.915f, 0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            //kaki orang 2

            _object3d.Add(new Assets_3D(0.14f, 0.38f, 0.35f, 1f));
            _object3d[6].createBoxVertices(-2.04f, -0.75f, -0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[6].addChildBalok(-2.04f, -0.915f, -0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            _object3d.Add(new Assets_3D(0.14f, 0.38f, 0.35f, 1f));
            _object3d[7].createBoxVertices(-2.04f, -0.75f, 0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[7].addChildBalok(-2.04f, -0.915f, 0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            //tangan orang 2

            _object3d.Add(new Assets_3D(0.54f, 0.64f, 0.48f, 1f));
            _object3d[8].createBoxVertices(-2.045f, -0.315f, 0.10f, 0.08f);
            _object3d[8].addChild(-2.045f, -0.395f, 0.11f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[8].addChild(-2.045f, -0.465f, 0.12f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[8].addChild(-2.045f, -0.525f, 0.13f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            _object3d.Add(new Assets_3D(0.54f, 0.64f, 0.48f, 1f));
            _object3d[9].createBoxVertices(-2.045f, -0.315f, -0.10f, 0.08f);
            _object3d[9].addChild(-2.045f, -0.395f, -0.11f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[9].addChild(-2.045f, -0.465f, -0.12f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[9].addChild(-2.045f, -0.525f, -0.13f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            // kaki orang 3

            _object3d.Add(new Assets_3D(0.14f, 0.38f, 0.35f, 1f));
            _object3d[10].createBoxVertices(-2.50f, -0.75f, -0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[10].addChildBalok(-2.50f, -0.915f, -0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            _object3d.Add(new Assets_3D(0.14f, 0.38f, 0.35f, 1f));
            _object3d[11].createBoxVertices(-2.50f, -0.75f, 0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[11].addChildBalok(-2.50f, -0.915f, 0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            // tangan orang 3

            _object3d.Add(new Assets_3D(0.26f, 0.17f, 0.18f, 1f));
            _object3d[12].createBoxVertices(-2.505f, -0.315f, 0.10f, 0.08f);
            _object3d[12].addChild(-2.505f, -0.315f, 0.18f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[12].addChild(-2.505f, -0.315f, 0.22f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[12].addChild(-2.505f, -0.275f, 0.22f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[12].addChild(-2.505f, -0.195f, 0.22f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[12].addChild(-2.505f, -0.195f, 0.22f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[12].addChild(-2.505f, -0.195f, 0.22f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            _object3d.Add(new Assets_3D(0.26f, 0.17f, 0.18f, 1f));
            _object3d[13].createBoxVertices(-2.505f, -0.315f, -0.10f, 0.08f);
            _object3d[13].addChild(-2.505f, -0.395f, -0.11f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[13].addChild(-2.505f, -0.465f, -0.12f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[13].addChild(-2.505f, -0.525f, -0.13f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);





            _object3d[4].translate(0.3f, 0.18f, 0.0f);
            _object3d[5].translate(0.3f, 0.18f, 0f);



            for (int i = 0; i < _object3d.Count(); i++)
            {
                _object3d[i].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            }



            _camera = new Camera(new Vector3(0, 0, 1), Size.X / (float)Size.Y);
            CursorGrabbed = true;
            
        }

        protected override void OnRenderFrame(FrameEventArgs args) // ini update tiap frame
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Matrix4 temp = Matrix4.Identity;
            //temp += temp * Matrix4.CreateTranslation(0.0f, 0.0f, 1.0f);
            //deg += MathHelper.DegreesToRadians(0.01f);
            //temp = temp * Matrix4.CreateRotationX(deg);
            //_object3d[0].render(0, temp);

            //_time += args.Time;
            Matrix4 temp = Matrix4.Identity;
            //temp = temp * Matrix4.CreateTranslation(0.5f, 0.5f, 0.0f);
            //degr += MathHelper.DegreesToRadians(0.05f);
            //temp = temp * Matrix4.CreateRotationY(degr);

            //test_animation(0.02f, 1f, 1);

            _object3d[4].rotate(_object3d[0]._centerPosition, _object3d[0]._euler[2], 0.01f);
            _object3d[5].rotate(_object3d[0]._centerPosition, _object3d[1]._euler[2], 0.01f);

            for (int i = 0; i < _object3d.Count(); i++)
            {
                if(i != 3)
                {
                    _object3d[i].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                }
                
            }

            _object3d[3].render(1, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());


            SwapBuffers();

        }

        public void test_animation(float delta, float duration, int mode)
        {
            int count = 0;
            if(duration - time_passed < delta)
            {
                delta = duration - time_passed;
                time_passed = 0;
                expired = true;
                animation_stage++;
            }
            foreach(Assets_3D i in _object3d)
            {
                switch (0)
                {
                    case 0:
                        i.rotate(_object3d[0]._centerPosition, _object3d[0]._euler[1],-45 * delta / 4.0f);
                        break;
                    case 1:
                        i.rotate(_object3d[0]._centerPosition, _object3d[0]._euler[1], -45 * delta / 2.0f);
                        break;
                }
                count += 1;
            }

            if (!expired)
            {
                time_passed += delta;
            }
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
