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

            // (x,y,z,t) xyz buat warnanya sedangkana t buat tranparancy                      
            //untuk warna caranya float point / 255 inisialisasi

            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            //GL.EnableVertexAttribArray(0);

            //GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            //GL.EnableVertexAttribArray(1);

            //GL.GetInteger(GetPName.MaxVertexAttribs, out int maxAttributeCount);
            //Console.WriteLine("Max number " + maxAttributeCount);

            //_object[0] = new Assets1(
            //    new float[] {
            //        -0.75f, 0.0f, 0.0f,// kiri
            //        -0.5f, 0.5f, 0.0f,// tinggi
            //        -0.25f, 0.0f, 0.0f// kanan
            //    },
            //    new uint[]
            //    {

            //    }
            //    );
            //_object[1] = new Assets1(
            //    new float[] {
            //        -0.75f, -0.4f, 0.0f,
            //        -0.5f, 0.2f, 0.0f,
            //        -0.25f, -0.4f, 0.0f
            //    },
            //    new uint[]
            //    {

            //    }
            //    );
            //_object[2] = new Assets1
            //    (
            //    new float[]
            //    {
            //        -0.75f, -0.7f, 0.0f,
            //        -0.5f, -0.2f, 0.0f,
            //        -0.25f, -0.7f, 0.0f

            //    },
            //    new uint[]
            //    {

            //    }
            //    );
            //_object[3] = new Assets1
            //    (
            //    new float[]
            //    {
            //        -0.55f,-0.5f,0.0f, //atas kanan
            //        -0.55f,-1.0f,0.0f, //bawah kanan
            //        -0.45f,-1.0f,0.0f,//bawah kiri
            //         -0.45f,-0.5f,0.0f,//bawah kiri
            //    },
            //    new uint[]
            //    {
            //        0,1,3, //segitiga peratama (atas)
            //        1,2,3 //segitigaa kedua (bawah)
            //    }
            //    );
            //_object[4] = new Assets1
            //    (
            //    new float[] { },
            //    new uint[] { }
            //    );

            //_object[5] = new Assets1
            //    (
            //    new float[1080],
            //    new uint[] { }
            //    );

            //_object[6] = new Assets1
            //    (
            //    new float[] { },
            //    new uint[] { }
            //    );

            //_object[0].load(Constants.path + "shader.vert", Constants.path + "shader.frag");
            //_object[1].load(Constants.path + "shader.vert", Constants.path + "shader.frag");
            //_object[2].load(Constants.path + "shader.vert", Constants.path + "shader.frag");
            //_object[3].load(Constants.path + "shader.vert", Constants.path + "shader.frag");

            //_object[4].createEllips(0.0f, -0.5f, 0.25f, 0.5f);// inisialisasi karena pakenya beda dari segitiga
            //_object[4].load(Constants.path + "shader.vert", Constants.path + "shader.frag");

            //_object[5].load(Constants.path + "shader.vert", Constants.path + "shader.frag");
            //_object[6].load(Constants.path + "shader.vert", Constants.path + "shader.frag");

            _object3d[0] = new Assets_3D();
            _object3d[0].createBoxVertices(0.0f, 0.0f, 0, 0.5f);
            //_object3d[0].createEllipsoid2(0.2f, 0.2f, 0.2f, 0.0f, 0.0f, 0.0f, 72, 24);
            //_object3d[0].createEllipsoid(0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 0.0f);
            _object3d[0].addChild(0.5f, 0.5f, 0.0f, 0.25f);
            _object3d[0].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);

            //_obj3D[1].createEllipsoid();
            //_obj3D[2].createHyperboloid();

            //_obj3D[1].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            //_obj3D[2].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);



        }

        protected override void OnRenderFrame(FrameEventArgs args) // ini update tiap frame
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //_object[3].render(0.20f, 0.77f, 0.65f,0); // batang
            //_object[2].render(0.10f, 0.57f, 0.45f, 0);
            //_object[1].render(0.50f, 0.87f, 0.50f, 0);
            //_object[0].render(0.30f, 0.67f, 0.25f, 0);
            //_object[4].render(0.20f, 0.77f, 0.65f,1); // lingkaran
            //if (_object[5].getVerticesLength())
            //{
            //    List<float> _verticestemp = _object[5].createCurveBezier();
            //    _object[6].setVertices(_verticestemp.ToArray());
            //    _object[6].load(Constants.path + "shader.vert", Constants.path + "shader.frag");
            //    _object[6].render(0.30f, 0.47f, 0.53f, 3);
            //}
            //_object[5].render(0.30f, 0.47f, 0.53f,2);


            //int vertexColorLocation = GL.GetUniformLocation(_shader.Handle,"unicolor");
            //GL.Uniform4(vertexColorLocation, 0.78f, 0.44f, 0.52f, 1.0f);
             
            //Matrix4 temp = Matrix4.Identity;
            //temp += temp * Matrix4.CreateTranslation(0.0f, 0.0f, 1.0f);
            //deg += MathHelper.DegreesToRadians(0.01f);
            //temp = temp * Matrix4.CreateRotationX(deg);
            //_object3d[0].render(0, temp);

            //_time += 9.0 * args.Time;
            Matrix4 temp = Matrix4.Identity;
            //temp = temp * Matrix4.CreateTranslation(0.5f, 0.5f, 0.0f);
            //degr += MathHelper.DegreesToRadians(0.23f);
            //temp = temp * Matrix4.CreateRotationY(degr);
            _object3d[0].rotate(_object3d[0]._centerPosition, _object3d[0]._euler[1], 1);
            _object3d[0].Child[0].rotate(_object3d[0].Child[0]._centerPosition, _object3d[0].Child[0]._euler[0], 1);
            _object3d[0].render(0.43f,0.54f,0.63f,3, _time, temp);

            //int vertexColorLocation = GL.GetUniformLocation(_shader.Handle,"unicolor");
            //GL.Uniform4(vertexColorLocation, 0.78f, 0.44f, 0.52f, 1.0f);


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
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButton.Left)
            {
                float _x = (MousePosition.X - Size.X/2)/ (Size.X/2);// ini buat normalisasiin
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