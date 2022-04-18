
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
            //texture = TextureLoader.LoadTexture("C:/Users/Ryan Inka Chandra/OneDrive/Pictures/Photo Studio/Foto Bareng.jpg");

            _camera = new Camera(new Vector3(-0.5f, -1f, 1f), Size.X /(float) Size.Y);

            //_object3d.Add(new Asset3D(0, 0, 1f, 1f));
            //_object3d[0].createBoxVertices(0, 0, 0, 1f, 1f, 1f);
            
            //atap
            _object3d.Add(new Asset3D(0.55f,0.55f,0.55f,1f));
            _object3d[0].createReversePyramidBox(true,0, 0, 0,1.8f,0.8f,0.4f,0.2f);
            
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
            _object3d[6].createBoxVertices(-0.38f, 0.75f, -1.0f, 0.05f, 0.05f, 0.55f);

            //gedung miring abu2
            _object3d.Add(new Asset3D(0.5f, 0.5f, 0.5f, 1f));
            _object3d[7].createPararelogram(0,-0.15f,2.38f,0.68f,2.5f,1.4f,0.3f);

            //tes
            _object3d.Add(new Asset3D(0, 0, 1f, 0.1f));
            _object3d[8].createEllipsoid(0.1f, 0.1f, 0.1f, 1f, 1f, 1f);

            for (int local = 0; local < 1; local++)
            {
                float j = -1.05f;
                float l = -0.7f;
                int k = 0;
                _object3d[6].createBoxChild(1.35f, 0.75f, l, 1.5f, 0.05f, 0.05f, 1f, 1f, 0, 1f);
                _object3d[6].createBoxChild(0.1f, 0.75f, l, 1f, 0.05f, 0.05f);
                
                for (float i = 0.65f; i >= -0.15f; i -= 0.1f)
                {                   
                    k = k + 1;
                    j = j + 0.04f;
                    l = l + 0.04f;
                    
                    if (k == 1 || k == 7 || k == 6 || k == 5)
                    {
                        _object3d[6].createBoxChild(-0.38f, i, (j + 0.05f), 0.05f, 0.05f, 0.55f, 1f, 1f, 0f, 1f);
                        _object3d[6].createBoxChild(1.35f, i, l, 1.5f, 0.05f, 0.05f);
                        _object3d[6].createBoxChild(0.1f, i, l, 1f, 0.05f, 0.05f,1f,1f,0,1f);
                    }
                    else
                    {
                        _object3d[6].createBoxChild(-0.38f, i, (j + 0.05f), 0.05f, 0.05f, 0.55f);
                        _object3d[6].createBoxChild(1.35f, i, l, 1.5f, 0.05f, 0.05f, 1f, 1f, 0, 1f);
                        _object3d[6].createBoxChild(0.1f, i, l, 1f, 0.05f, 0.05f);                        
                    }
                }
            }
            
            for (int i = 0; i < _object3d.Count(); i++)
            {
                child = _object3d[i].GetChild();

                _object3d[i].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);

                foreach (Asset3D asset3d in child)
                {
                    asset3d.load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
                }
            }

            /*
            foreach(Asset3D asset3d in child)
            {
                asset3d.load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            }
            */
        }       

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //GL.BindTexture(TextureTarget.Texture2D, texture);
            
            //Matrix4 temp = Matrix4.Identity;
            //temp += temp * Matrix4.CreateTranslation(0.0f, 0.0f, 1.0f);
            //deg += MathHelper.DegreesToRadians(0.01f);
            //temp = temp * Matrix4.CreateRotationX(deg);
            //_object3d[0].render(0, temp);

            _time += args.Time;

            Matrix4 temp = Matrix4.Identity;
            temp = temp * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(45));
            temp = temp * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(30));

            Matrix4 temp2 = Matrix4.Identity;
            temp2 = temp2 * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(315));
            temp2 = temp2 * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(30));

            Matrix4 temp3 = Matrix4.Identity;
            temp3 = temp3 * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(135));
            temp3 = temp3 * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(30));

            //_object3d[0].rotate(_object3d[0]._centerPosition, _object3d[0]._euler[1], 0.05f);
            //_object3d[1].rotate(_object3d[1]._centerPosition, _object3d[1]._euler[1], 0.05f);
            
            
            _object3d[2].render(3, temp2, _time,_camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[6].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[7].render(3, temp3, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[4].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[3].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[5].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[1].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[0].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[8].render(3, temp, _time, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            
            //_object3d[0].render(3,temp,_time,_camera.GetViewMatrix(),_camera.GetProjectionMatrix());
            
            //_object3d[0].render(5, temp, _time, 0, 0, 0, 1.0f, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            //CursorGrabbed = true;

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
            var sensitifity = 0.5f;

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