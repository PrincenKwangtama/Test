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

        Assets_3D[] _object3d = new Assets_3D[10];
        Camera _camera;

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

            _object3d[0] = new Assets_3D();
            //dinidng bawah kiri
            _object3d[0].createBoxVertices(-0.14f, -0.0f, 0, 0.08f);
            _object3d[0].addChild(-0.14f, -0.0f, 0.08f, 0.08f,0);
            _object3d[0].addChild(-0.14f, -0.0f, 0.16f, 0.08f,0);
            _object3d[0].addChild(-0.14f, -0.0f, 0.24f, 0.08f,0);
            _object3d[0].addChild(-0.14f, -0.0f, 0.32f, 0.08f,0);
            _object3d[0].addChild(-0.14f, -0.0f, 0.40f, 0.08f,0);
            //dinding bawah belakang
            _object3d[0].addChild(-0.06f, -0.0f, 0.40f, 0.08f, 0);
            _object3d[0].addChild(0.02f, -0.0f, 0.40f, 0.08f, 0);
            _object3d[0].addChild(0.10f, -0.0f, 0.40f, 0.08f, 0);
            _object3d[0].addChild(0.18f, -0.0f, 0.40f, 0.08f, 0);
            _object3d[0].addChild(0.26f, -0.0f, 0.40f, 0.08f, 0);
            _object3d[0].addChild(0.34f, -0.0f, 0.40f, 0.08f, 0);
            //dinding bawah kanan
            _object3d[0].addChild(0.40f, -0.0f, 0, 0.08f,0);
            _object3d[0].addChild(0.40f, -0.0f, 0.08f, 0.08f, 0);
            _object3d[0].addChild(0.40f, -0.0f, 0.16f, 0.08f, 0);
            _object3d[0].addChild(0.40f, -0.0f, 0.24f, 0.08f, 0);
            _object3d[0].addChild(0.40f, -0.0f, 0.32f, 0.08f, 0);
            _object3d[0].addChild(0.40f, -0.0f, 0.40f, 0.08f, 0);
            //dinding bawah depan
            _object3d[0].addChild(-0.06f, -0.0f, -0.0f, 0.08f, 0);
            _object3d[0].addChild(0.02f, -0.0f, -0.0f, 0.08f, 0);
            _object3d[0].addChild(0.10f, -0.0f, -0.0f, 0.08f, 0);
            _object3d[0].addChild(0.18f, -0.0f, -0.0f, 0.08f, 0);
            _object3d[0].addChild(0.26f, -0.0f, -0.0f, 0.08f, 0);
            _object3d[0].addChild(0.34f, -0.0f, -0.0f, 0.08f, 0);

            //_object3d[0].createEllipsoid2(0.2f, 0.2f, 0.2f, 0.0f, 0.0f, 0.0f, 72, 24);
            //_object3d[0].createEllipsoid(0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 0.0f);
            //_object3d[0].addChild(-0.54f, -0.68f, 0.0f, 0.25f,0);
            //_object3d[0].addChild(-0.54f, -0.82f, 0.0f, 0.25f,0);
            //_object3d[0].addChild(-0.54f, -0.96f, 0.0f, 0.25f,0);

            _object3d[1] = new Assets_3D();
            //_object3d[1].createNewEllipsoid(0.2f, 0.5f, 0.2f, 0.0f, 0.0f, 0.0f, 72, 24);
            _object3d[1].createBoxVertices(-0.54f, -0.0f, 0, 0.28f);
            _object3d[1].addChild(-0.54f, -0.09f, 0, 0.20f,0);
            _object3d[1].addChild(-0.54f, -0.30f, 0, 0.25f,0);
            _object3d[1].addChild(-0.54f, -0.44f, 0, 0.25f,0);
            _object3d[1].addChild(-0.54f, -0.58f, 0, 0.25f,0);
            _object3d[1].addChild(-0.54f, -0.63f, 0, 0.25f,0);

            _object3d[1].addChild(-0.34f, -0.34f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.33f, -0.44f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.32f, -0.54f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.31f, -0.58f, 0, 0.15f, 0);

            _object3d[1].addChild(-0.74f, -0.34f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.75f, -0.44f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.76f, -0.54f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.77f, -0.58f, 0, 0.15f, 0);

            //kaki

            _object3d[1].addChild(-0.64f, -0.78f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.64f, -0.87f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.64f, -0.98f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.64f, -1.04f, 0, 0.15f, 0);

            _object3d[1].addChild(-0.44f, -0.78f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.44f, -0.87f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.44f, -0.98f, 0, 0.15f, 0);
            _object3d[1].addChild(-0.44f, -1.04f, 0, 0.15f, 0);

            _object3d[2] = new Assets_3D();
            _object3d[2].createNewEllipsoid(0.2f, 0.5f, 0.2f, 0.0f, 0.0f, 0.0f, 72, 24);



            _object3d[0].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            _object3d[1].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            _object3d[2].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);

            _camera = new Camera(new Vector3(0, 0, 1), Size.X / (float)Size.Y);
            CursorGrabbed = true;
            
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
            temp = temp * Matrix4.CreateRotationY(degr);
            //_object3d[0].rotate(_object3d[0]._centerPosition, _object3d[0]._euler[1], 0.32f);
            _object3d[0].render(0.43f, 0.54f, 0.63f, 3, _time, temp,_camera.GetViewMatrix(),_camera.GetProjectionMatrix());
            _object3d[1].render(0.43f, 0.54f, 0.63f, 3, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[2].render(0.43f, 0.54f, 0.63f, 3, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());

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

        protected override void OnResize(ResizeEventArgs e)// akan jalan tiap kali ada perubahan
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}
