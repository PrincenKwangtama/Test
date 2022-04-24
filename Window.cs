
using System;
using System.Collections.Generic;
using System.IO;
using LearnOpenTK.Common;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Tes
{
    internal class Window : GameWindow
    {
        float[] _vertices =
            {
            1f,  1f,  1f, 
            2f, 3f, 1f, 
            3f, 1f, 1f,
        };
        
        /*
        float[] _vertices =
            {
            0.5f,  0.00f,  0.00f, 1.0f,0.0f,0.0f,
            0.0f, 0.50f, 0.0f,    0.0f,1.0f,0.0f,
            -0.5f, 0.0f, 0.0f,    0.0f,0.0f,1.0f
        };
        */

        /*
        float[] _vertices =
        {
            0.5f,0.5f,0.0f, //atas kanan
            0.5f,-0.5f,0.0f, //bawah kanan
            -0.5f,-0.5f,0.0f,//bawah kiri
            -0.5f,0.5f,0.0f,//bawah kiri
        };
        */

        int _vertexBufferObject;
        //int _elementBufferObject;
        int _vertexArrayObject;

        uint[] _indices =
        {
             //0,1,3, //segitiga peratama (atas)
             //1,2,3 //segitigaa kedua (bawah)
        };

        //Asset2D[] _object2d = new Asset2D[10];
        //Asset3D[] _object3d = new Asset3D[20];
        List<Asset3D> _object3d = new List<Asset3D>();
        List<Matrix4>  models = new List<Matrix4>();
        List<Asset3D> child = new List<Asset3D>();
        Camera _camera;

        double _time;
        float degr = 0;
        Shader _shader;
        int texture;
        bool firstMove = true;
        Vector2 lastPos;
        Vector3 _objectPos = new Vector3(0,0,0);
        float _rotationSpeed = 0.1f;
        
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            CenterWindow();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.75f, 0.77f, 0.55f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
                        
            _camera = new Camera(new Vector3(-0.5f, -1f, 1f), Size.X /(float) Size.Y);

            //_object3d.Add(new Asset3D(0, 0, 1f, 1f));
            //_object3d[0].createBoxVertices(0, 0, 0, 1f, 1f, 1f);
            
            //atap
            _object3d.Add(new Asset3D(0.55f,0.55f,0.55f,1f));
            _object3d[0].createTrapezoid(0, 0, 0,1.8f,0.8f,0.4f,0.2f);
            
            //tiang tegak
            _object3d.Add(new Asset3D(1f,1f,1f,1f));
            _object3d[1].createBoxVertices(-0.72f, -0.5f, -0.33f, 0.02f, 0.7f, 0.02f);
            _object3d[1].createBoxChild(-0.4f, -0.3f, -0.9f, 0.02f, 0.4f, 0.02f);
            _object3d[1].createBoxChild(0.2f, -0.3f, -0.9f, 0.02f, 0.4f, 0.02f);
            _object3d[1].createBoxChild(0.8f, -0.3f, -0.9f, 0.02f, 0.4f, 0.02f);
            _object3d[1].createBoxChild(1.4f, -0.3f, -0.9f, 0.02f, 0.4f, 0.02f);
            _object3d[1].createBoxChild(2f, -0.3f, -0.9f, 0.02f, 0.4f, 0.02f);
            _object3d[1].createBoxChild(-0.35f, -0.4f, -0.33f, 0.02f, 0.5f, 0.02f);
            _object3d[1].createBoxChild(-0.35f, -0.4f, -0.33f, 0.02f, 0.5f, 0.02f);
            _object3d[1].createBoxChild(0.3f, -0.3f, -0.36f, 0.14f, 0.4f, 0.05f, 1f, 1f, 0, 1f);
            _object3d[1].createBoxChild(1f, -0.3f, -0.36f, 0.14f, 0.4f, 0.05f, 1f, 1f, 0, 1f);
            _object3d[1].createBoxChild(-0.35f, -0.4f, 0.30f, 0.14f, 0.5f, 0.05f, 1f, 1f, 0, 1f);
            _object3d[1].createBoxChild(-0.72f, -0.5f, 0.33f, 0.02f, 0.7f, 0.02f);
            _object3d[1].createBoxChild(0.1f,-0.3f, 0.33f, 0.02f, 0.3f, 0.02f);
            _object3d[1].createBoxChild(0.9f, -0.3f, 0.33f, 0.02f, 0.3f, 0.02f);
            _object3d[1].createBoxChild(0.5f, -0.3f, 0.33f, 0.02f, 0.3f, 0.02f);                        

            //gedung miring kuning biru
            _object3d.Add(new Asset3D(0.8f,0.8f,0.8f,1f));
            _object3d[2].createPararelogram(-0.65f, 0.35f, -0.8f, 0.6f, 2.5f, 0.9f, 0.7f);

            //tangga depan 1
            _object3d.Add(new Asset3D(0.8f,0.8f,0.8f,1f));
            _object3d[3].createStaircase(-0.72f, -0.82f, 0, 0.68f, 8);

            //lantai 
            _object3d.Add(new Asset3D(0.7f,0.7f,0.7f,1f));
            _object3d[4].createBoxVertices(0.34f, -0.74f, 0, 1.54f, 0.2f, 0.68f);
            _object3d[4].createBoxChild(0.6f, -0.56f, 0, 1f, 0.2f, 0.68f);
            _object3d[4].createBoxChild(0.8f, -0.66f, -0.65f, 2.5f, 0.4f, 0.6f);

            //tangga depan 2
            _object3d.Add(new Asset3D(0.8f,0.8f,0.8f,1f));
            _object3d[5].createStaircase(-0.25f, -0.64f, 0, 0.68f, 8);
            
            //teras biru kuning
            _object3d.Add(new Asset3D(0,0,1f,10f));
            _object3d[6].createBoxVertices(-0.48f, 0.75f, -0.9f, 0.05f, 0.05f, 0.55f);

            //tembok teras putih
            _object3d.Add(new Asset3D(1f, 1f, 1f, 1f));
            _object3d[7].createPararelogram(-0.38f, 0.35f, 0.46f, 0.05f, 0.05f, 0.8f, 0.6f);

            //tangga gedung
            _object3d.Add(new Asset3D(0.9f, 0.9f, 0.9f, 1f));
            _object3d[8].createBoxVertices(-0.48f, 0.7f, -1.2f, 0.07f, 0.05f, 0.18f);

            for (int local = 0; local < 1; local++)
            {
                float j = -0.95f;
                float l = -0.65f;
                int k = 0;                
                _object3d[6].createBoxChild(1.32f, 0.75f, l, 1.48f, 0.05f, 0.05f, 1f, 1f, 0, 1f);
                _object3d[6].createBoxChild(0.05f, 0.75f, l, 1.05f, 0.05f, 0.05f);

                for (float i = 0.65f; i >= -0.15f; i -= 0.1f)
                {
                    k = k + 1;
                    j = j + 0.04f;
                    l = l + 0.04f;

                    if (k == 1 || k == 7 || k == 6 || k == 5)
                    {
                        _object3d[6].createBoxChild(-0.48f, i, (j + 0.05f), 0.05f, 0.05f, 0.55f, 1f, 1f, 0f, 1f);
                        _object3d[6].createBoxChild(1.32f, i, l, 1.48f, 0.05f, 0.05f);
                        _object3d[6].createBoxChild(0.05f, i, l, 1.05f, 0.05f, 0.05f, 1f, 1f, 0, 1f);
                    }
                    else
                    {
                        _object3d[6].createBoxChild(-0.48f, i, (j + 0.05f), 0.05f, 0.05f, 0.55f);
                        _object3d[6].createBoxChild(1.32f, i, l, 1.48f, 0.05f, 0.05f, 1f, 1f, 0, 1f);
                        _object3d[6].createBoxChild(0.05f, i, l, 1.05f, 0.05f, 0.05f);
                    }
                }

                for(float i = -0.065f; i >= (0.065f - 0.525f * 4); i-=0.525f)
                {
                    _object3d[7].createPararelogramChild(-0.38f, 0.35f, i, 0.08f, 0.025f, 0.8f, 0.6f);
                }

                _object3d[7].createPararelogramChild(-0.98f, 0.35f, 0.46f, 0.05f, 0.05f, 0.8f, 0.6f);
                _object3d[7].createPararelogramChild(-0.38f, 0.35f, -2.06f, 0.05f, 0.05f, 0.78f, 0.6f);

                _object3d[6].createBoxChild(-0.48f, 0.75f, -1.24f, 0.05f, 0.05f, 0.13f,0.9f,0.9f,0.9f,1f);

                
                float n = -1.2f;
                for(float i = 0.6f; i > 0; i -= 0.1f)
                {
                    n = n + 0.04f;
                    _object3d[8].createBoxChild(-0.48f, i, n, 0.07f, 0.05f, 0.18f);
                }
                

                
            }
            
            //gedung miring abu2
            _object3d.Add(new Asset3D(0.5f, 0.5f, 0.5f, 1f));
            _object3d[9].createPararelogram(0,-0.15f,2.38f,0.68f,2.5f,1.4f,0.3f);

            //logo ukp 
            _object3d.Add(new Asset3D(1f, 1f, 1f, 1f));
            _object3d[10].createTube(-0.8f, 0.08f, 0.4f, 0.09f, 0.05f,1,true);
            _object3d[10].createTubeChild(-0.8f, 0.08f, 0.46f, 0.07f, 0.01f, 0.4, true, 0.8f, 0.8f, 0.8f, 1f);
            _object3d[10].createTubeChild(-0.8f, 0.08f, 0.45f, 0.07f, 0.01f, 1, false, 0.2f, 0.2f, 0.8f, 1f);
            _object3d[10].createBoxChild(-0.78f, 0.037f, 0.465f, 0.02f, 0.045f, 0.01f,0,0,0,1f);
            _object3d[10].createBoxChild(-0.82f, 0.037f, 0.465f, 0.02f, 0.045f, 0.01f, 0, 0, 0, 1f);
            _object3d[10].createBoxChild(-0.78f, 0.037f, 0.472f, 0.001f, 0.045f, 0.01f);
            _object3d[10].createBoxChild(-0.82f, 0.037f, 0.472f, 0.001f, 0.045f, 0.01f);
            _object3d[10].createBoxChild(-0.8f, 0.11f, 0.46f, 0.07f, 0.01f, 0.01f);
            _object3d[10].createBoxChild(-0.8f, 0.095f, 0.46f, 0.01f, 0.09f, 0.01f);
            _object3d[10].createCrescentChild(-0.842f,0.094f,0.46f, 0.038f,0.025f, 0.0075f, true,1f,1f,0,1f);
            _object3d[10].createCrescentChild(-0.758f, 0.094f, 0.46f, 0.038f, 0.025f, 0.0075f, true, 1f, 1f, 0, 1f);

            //tulisan ukp
            _object3d.Add(new Asset3D(1f, 1f, 1f, 1f));
            //u
            _object3d[11].createBoxVertices(-0.6f,0.08f,0.4f,0.05f,0.1f,0.05f);
            _object3d[11].createRingChild(-0.55f,0.05f,0.4f,0.075f,0.025f,0.05f,-180,0);
            _object3d[11].createBoxChild(-0.5f,0.08f,0.4f, 0.05f, 0.1f, 0.05f);

            //k
            _object3d[11].createBoxChild(-0.4f, 0.05f, 0.4f, 0.05f, 0.16f, 0.05f);
            _object3d[11].createPararelogramChild(-0.3f, 0.01f, 0.4f, 0.06f, 0.05f, 0.08f, 0.15f);
            _object3d[11].createPararelogramChild(-0.3f, 0.09f, 0.4f, 0.06f, 0.05f, 0.08f, 0.15f);

            //p
            _object3d[11].createBoxChild(-0.2f, 0.05f, 0.4f, 0.05f, 0.16f, 0.05f);
            _object3d[11].createBoxChild(-0.155f, 0.11f, 0.4f, 0.05f, 0.04f, 0.05f);
            _object3d[11].createBoxChild(-0.155f, 0.05f, 0.4f, 0.05f, 0.04f, 0.05f);
            _object3d[11].createRingChild(-0.135f, 0.081f, 0.4f, 0.0475f, 0.01f, 0.025f, -90, 90);

            _object3d.Add(new Asset3D(0.87f, 0.68f, 0.45f, 1f));

            //_object3d[0].createBoxVertices(-1.6f, -0.10f, 0, 0.15f);
            _object3d[12].createBoxVertices(-1.6f, -0.17f, 0, 0.15f);
            _object3d[12].addChild(-1.53f, -0.15f, 0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[12].addChild(-1.53f, -0.15f, 0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[12].addChild(-1.53f, -0.15f, -0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[12].addChild(-1.53f, -0.15f, -0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[12].addChild(-1.53f, -0.20f, 0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[12].addChild(-1.53f, -0.20f, -0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);

            //rambut
            _object3d[12].addChildBalok(-1.6f, -0.085f, 0.00f, 0.15f, 0.02f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut belakang
            _object3d[12].addChildBalok(-1.68f, -0.145f, -0f, 0.02f, 0.14f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            //rambut samping
            _object3d[12].addChildBalok(-1.65f, -0.145f, -0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[12].addChildBalok(-1.6f, -0.125f, -0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[12].addChildBalok(-1.545f, -0.110f, -0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[12].addChildBalok(-1.65f, -0.145f, 0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[12].addChildBalok(-1.6f, -0.125f, 0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[12].addChildBalok(-1.545f, -0.110f, 0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut depan
            _object3d[12].addChildBalok(-1.53f, -0.095f, 0f, 0.02f, 0.04f, 0.19f, 1, 0.13f, 0.13f, 0.13f, 1f);


            //badan
            _object3d[12].addChild(-1.6f, -0.26f, 0, 0.07f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[12].addChildBalok(-1.6f, -0.44f, 0f, 0.12f, 0.33f, 0.12f, 1, 0.05f, 0.31f, 0.55f, 1f);

            //Toga
            _object3d[12].addChildElipsoids(0.03f, 0.03f, 0.03f, -1.6f, -0.07f, 0f, 0.01f, 0.25f, 0.53f, 1f);
            _object3d[12].addChildBalok(-1.59f, -0.04f, -0.005f, 0.12f, 0.01f, 0.12f, 1, 0.01f, 0.25f, 0.53f, 1f);

            //tangan
            _object3d[12].addChild(-1.605f, -0.315f, 0.10f, 0.08f, 0, 0.05f, 0.31f, 0.55f, 1f);
            _object3d[12].addChild(-1.605f, -0.315f, 0.12f, 0.08f, 0, 0.05f, 0.31f, 0.55f, 1f);
            _object3d[12].addChild(-1.605f, -0.395f, 0.14f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[12].addChild(-1.605f, -0.465f, 0.12f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[12].addChild(-1.605f, -0.525f, 0.11f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            _object3d[12].addChild(-1.605f, -0.315f, -0.10f, 0.08f, 0, 0.05f, 0.31f, 0.55f, 1f);
            _object3d[12].addChild(-1.605f, -0.315f, -0.12f, 0.08f, 0, 0.05f, 0.31f, 0.55f, 1f);
            _object3d[12].addChild(-1.605f, -0.395f, -0.14f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[12].addChild(-1.605f, -0.465f, -0.12f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[12].addChild(-1.605f, -0.525f, -0.11f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            //obj 1
            _object3d.Add(new Asset3D(0.87f, 0.68f, 0.45f, 1f));
            //_object3d[1].createNewEllipsoid(0.2f, 0.5f, 0.2f, 0.0f, 0.0f, 0.0f, 72, 24);
            _object3d[13].createBoxVertices(-2.04f, -0.17f, 0, 0.15f);
            _object3d[13].addChild(-1.97f, -0.15f, 0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[13].addChild(-1.97f, -0.15f, 0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[13].addChild(-1.97f, -0.15f, -0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[13].addChild(-1.97f, -0.15f, -0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[13].addChild(-1.97f, -0.20f, 0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[13].addChild(-1.97f, -0.20f, -0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);

            //rambut
            _object3d[13].addChildBalok(-2.04f, -0.085f, 0.00f, 0.15f, 0.02f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut belakang
            _object3d[13].addChildBalok(-2.12f, -0.145f, -0f, 0.02f, 0.14f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            //rambut samping
            _object3d[13].addChildBalok(-2.09f, -0.145f, -0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[13].addChildBalok(-2.04f, -0.125f, -0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[13].addChildBalok(-1.985f, -0.110f, -0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[13].addChildBalok(-2.09f, -0.145f, 0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[13].addChildBalok(-2.04f, -0.125f, 0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[13].addChildBalok(-1.985f, -0.110f, 0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut depan
            _object3d[13].addChildBalok(-1.97f, -0.095f, 0f, 0.02f, 0.04f, 0.19f, 1, 0.13f, 0.13f, 0.13f, 1f);

            //badan
            _object3d[13].addChild(-2.04f, -0.26f, 0, 0.07f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[13].addChildBalok(-2.04f, -0.44f, 0f, 0.12f, 0.33f, 0.12f, 1, 0.54f, 0.64f, 0.48f, 1f);

            //Toga
            _object3d[13].addChildElipsoids(0.03f, 0.03f, 0.03f, -2.04f, -0.07f, 0f, 0.01f, 0.25f, 0.53f, 1f);
            _object3d[13].addChildBalok(-2.03f, -0.04f, -0.005f, 0.12f, 0.01f, 0.12f, 1, 0.01f, 0.25f, 0.53f, 1f);

            //obj 2
            _object3d.Add(new Asset3D(0.87f, 0.68f, 0.45f, 1f));
            _object3d[14].createBoxVertices(-2.50f, -0.17f, 0, 0.15f);
            _object3d[14].addChild(-2.43f, -0.15f, 0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[14].addChild(-2.43f, -0.15f, 0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[14].addChild(-2.43f, -0.15f, -0.05f, 0.02f, 0, 0.90f, 0.90f, 0.90f, 1f);
            _object3d[14].addChild(-2.43f, -0.15f, -0.03f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[14].addChild(-2.43f, -0.20f, 0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[14].addChild(-2.43f, -0.20f, -0.01f, 0.02f, 0, 0.13f, 0.13f, 0.13f, 1f);

            //rambut
            _object3d[14].addChildBalok(-2.50f, -0.085f, 0.00f, 0.15f, 0.02f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut belakang
            _object3d[14].addChildBalok(-2.58f, -0.145f, -0f, 0.02f, 0.14f, 0.15f, 1, 0.13f, 0.13f, 0.13f, 1f);

            //rambut samping
            _object3d[14].addChildBalok(-2.55f, -0.145f, -0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[14].addChildBalok(-2.50f, -0.125f, -0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[14].addChildBalok(-2.445f, -0.110f, -0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[14].addChildBalok(-2.55f, -0.145f, 0.085f, 0.08f, 0.14f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[14].addChildBalok(-2.50f, -0.125f, 0.085f, 0.08f, 0.10f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[14].addChildBalok(-2.445f, -0.110f, 0.085f, 0.04f, 0.07f, 0.02f, 1, 0.13f, 0.13f, 0.13f, 1f);

            ////rambut depan
            _object3d[14].addChildBalok(-2.43f, -0.095f, 0f, 0.02f, 0.04f, 0.19f, 1, 0.13f, 0.13f, 0.13f, 1f);


            //badan
            _object3d[14].addChild(-2.50f, -0.26f, 0, 0.07f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[14].addChildBalok(-2.50f, -0.44f, 0f, 0.12f, 0.33f, 0.12f, 1, 0.26f, 0.17f, 0.18f, 1f);

            //Toga
            _object3d[14].addChildElipsoids(0.03f, 0.03f, 0.03f, -2.50f, -0.07f, 0f, 0.01f, 0.25f, 0.53f, 1f);
            _object3d[14].addChildBalok(-2.49f, -0.04f, -0.005f, 0.12f, 0.01f, 0.12f, 1, 0.01f, 0.25f, 0.53f, 1f);

            // jam tangan
            _object3d.Add(new Asset3D(0.8f, 0.8f, 0.8f, 1f));
            _object3d[15].createTube(-2.51f, -0.23f, 0.26f, 0.015f, 0.015f, 1, true);

            _object3d[15].addChildBalok(-2.535f, -0.23f, 0.265f, 0.02f, 0.01f, 0.011f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[15].addChildBalok(-2.48f, -0.23f, 0.265f, 0.03f, 0.01f, 0.011f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[15].addChildBalok(-2.55f, -0.23f, 0.22f, 0.01f, 0.011f, 0.10f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[15].addChildBalok(-2.46f, -0.23f, 0.22f, 0.01f, 0.011f, 0.10f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[15].addChildBalok(-2.505f, -0.23f, 0.17f, 0.101f, 0.01f, 0.011f, 1, 0.13f, 0.13f, 0.13f, 1f);

            _object3d[15].addChildBalok(-2.51f, -0.225f, 0.28f, 0.005f, 0.015f, 0.005f, 1, 0.13f, 0.13f, 0.13f, 1f);
            _object3d[15].addChildBalok(-2.513f, -0.23f, 0.28f, 0.010f, 0.005f, 0.005f, 1, 0.13f, 0.13f, 0.13f, 1f);

            // kaki orang 1
            _object3d.Add(new Asset3D(0.13f, 0.13f, 0.13f, 1f));
            _object3d[16].createBoxVertices(-1.60f, -0.75f, -0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[16].addChildBalok(-1.60f, -0.915f, -0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            _object3d.Add(new Asset3D(0.13f, 0.13f, 0.13f, 1f));
            _object3d[17].createBoxVertices(-1.60f, -0.75f, 0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[17].addChildBalok(-1.60f, -0.915f, 0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            //kaki orang 2

            _object3d.Add(new Asset3D(0.14f, 0.38f, 0.35f, 1f));
            _object3d[18].createBoxVertices(-2.04f, -0.75f, -0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[18].addChildBalok(-2.04f, -0.915f, -0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            _object3d.Add(new Asset3D(0.14f, 0.38f, 0.35f, 1f));
            _object3d[19].createBoxVertices(-2.04f, -0.75f, 0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[19].addChildBalok(-2.04f, -0.915f, 0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            //tangan orang 2

            _object3d.Add(new Asset3D(0.54f, 0.64f, 0.48f, 1f));
            _object3d[20].createBoxVertices(-2.045f, -0.315f, 0.10f, 0.08f);
            _object3d[20].addChild(-2.045f, -0.395f, 0.11f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[20].addChild(-2.045f, -0.465f, 0.12f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[20].addChild(-2.045f, -0.525f, 0.13f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            _object3d.Add(new Asset3D(0.54f, 0.64f, 0.48f, 1f));
            _object3d[21].createBoxVertices(-2.045f, -0.315f, -0.10f, 0.08f);
            _object3d[21].addChild(-2.045f, -0.395f, -0.11f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[21].addChild(-2.045f, -0.465f, -0.12f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[21].addChild(-2.045f, -0.525f, -0.13f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            // kaki orang 3

            _object3d.Add(new Asset3D(0.14f, 0.38f, 0.35f, 1f));
            _object3d[22].createBoxVertices(-2.50f, -0.75f, -0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[22].addChildBalok(-2.50f, -0.915f, -0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            _object3d.Add(new Asset3D(0.14f, 0.38f, 0.35f, 1f));
            _object3d[23].createBoxVertices(-2.50f, -0.75f, 0.05f, 0.08f, 0.29f, 0.08f);
            _object3d[23].addChildBalok(-2.50f, -0.915f, 0.05f, 0.08f, 0.04f, 0.08f, 1, 0.87f, 0.68f, 0.45f, 1f);

            // tangan orang 3

            _object3d.Add(new Asset3D(0.26f, 0.17f, 0.18f, 1f));
            _object3d[24].createBoxVertices(-2.505f, -0.315f, 0.10f, 0.08f);
            _object3d[24].addChild(-2.505f, -0.315f, 0.18f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[24].addChild(-2.505f, -0.315f, 0.22f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[24].addChild(-2.505f, -0.275f, 0.22f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[24].addChild(-2.505f, -0.195f, 0.22f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[24].addChild(-2.505f, -0.195f, 0.22f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[24].addChild(-2.505f, -0.195f, 0.22f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            _object3d.Add(new Asset3D(0.26f, 0.17f, 0.18f, 1f));
            _object3d[25].createBoxVertices(-2.505f, -0.315f, -0.10f, 0.08f);
            _object3d[25].addChild(-2.505f, -0.395f, -0.11f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[25].addChild(-2.505f, -0.465f, -0.12f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);
            _object3d[25].addChild(-2.505f, -0.525f, -0.13f, 0.08f, 0, 0.87f, 0.68f, 0.45f, 1f);

            for (int i = 0; i < _object3d.Count(); i++)
            {
                models.Add(Matrix4.Identity);
                
                if (i == 2 || i == 7)
                {
                    _object3d[i].RotateOnZero(1, 315);
                    _object3d[i].RotateOnZero(0, 30);

                    child = _object3d[i].GetChild();

                    foreach (Asset3D asset3d in child)
                    {
                        asset3d.RotateOnZero(1, 315);
                        asset3d.RotateOnZero(0, 30);
                        asset3d.load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
                    }
                }
                else if (i == 9)
                {
                    _object3d[i].RotateOnZero(1, 135);
                    _object3d[i].RotateOnZero(0, 30);

                    child = _object3d[i].GetChild();

                    foreach (Asset3D asset3d in child)
                    {
                        asset3d.RotateOnZero(1, 135);
                        asset3d.RotateOnZero(0, 30);
                        asset3d.load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
                    }
                }
                else
                {
                    _object3d[i].RotateOnZero(1, 45);
                    _object3d[i].RotateOnZero(0, 30);

                    child = _object3d[i].GetChild();

                    foreach (Asset3D asset3d in child)
                    {
                        asset3d.RotateOnZero(1, 45);
                        asset3d.RotateOnZero(0, 30);
                        asset3d.load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
                    }
                }
                                               
                _object3d[i].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);               
            }

            

            //transformasi  mulai disini
            _object3d[8].RotateOnCore(0,30);
            _object3d[10].RotateOnCore(2,18,0);
            _object3d[10].RotateOnCore(2, 25, 2);
            _object3d[10].RotateOnCore(2, 25, 4);
            _object3d[10].RotateOnCore(2,-25,3);
            _object3d[10].RotateOnCore(2, -25, 5);
            _object3d[10].RotateOnCore(2,30 , 8);
            _object3d[10].RotateOnCore(2, -30, 9);
            _object3d[11].RotateOnCore(0, 180, 4);                                                   
        }
               
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //animasi mulai disini
            //models[10] = models[10] * Matrix4.CreateTranslation(0.0f, 0.0f, 1.0f);

            for (int i = 0; i < _object3d.Count(); i++)
            {                
                _object3d[i].render(_time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }
           
            SwapBuffers();
        }
        
        protected override void OnUnload()
        {
            base.OnUnload();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            var key = KeyboardState;
            var mouse = MouseState;
            var sensitifity = 0.2f;

            if (key.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            float cameraSpeed = 0.5f;

            if (key.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
            }

            if (key.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
            }

            if (key.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
            }

            if (key.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
            }

            if (key.IsKeyDown(Keys.Q))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;
            }

            if (key.IsKeyDown(Keys.E))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;
            }

            /*
            if (firstMove)
            {
                lastPos = new Vector2(mouse.X, mouse.Y);
                firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - lastPos.X;
                var deltaY = mouse.Y - lastPos.Y;
                lastPos = new Vector2(mouse.X, mouse.Y);
                               
                _camera.Yaw += deltaX * sensitifity;
                _camera.Pitch -= deltaY * sensitifity;
            }

            if (KeyboardState.IsKeyDown(Keys.N))
            {
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objectPos;
                _camera.Yaw += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
            }

            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objectPos;
                _camera.Yaw -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed)
                    .ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objectPos;
                _camera.Pitch -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objectPos;
                _camera.Pitch += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;
            }
            */
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _camera.Fov = _camera.Fov - e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float) Size.Y;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            /*
            base.OnMouseDown(e);

            if (e.Button == MouseButton.Left)
            {
                float _x = (MousePosition.X - Size.X / 2) / (Size.X / 2);
                float _y = -(MousePosition.Y - Size.X / 2) / (Size.X / 2);

                Console.WriteLine("x = " + _x + "y= " + _y);
                _object2d[4].UpdateMousePosition(_x, _y);
            }
            */
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
