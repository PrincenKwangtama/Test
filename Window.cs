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
    static class Constant
    {
        public const string path = "../../../Shader/";

    }
    public class Window : GameWindow
    {
        private readonly string path = "D:/Coding Area/Semester 4/GrafKom/Test/Shader/";


        //float[] data = {
        //    //x     //y    //y   //r    //g   //b
        //    -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, // vertex 1 merah
        //    0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, // vertex 2 hijau
        //    0.5f, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f, // vertex 3 biru
        //};

        //float[] data = { // ini untuk persergi
        //    0.5f, 0.5f, 0.0f,// kanan atas
        //    0.5f, -0.5f, 0.0f,// kanan bawah
        //    -0.5f, -0.5f, 0.0f,// kiri atas
        //    -0.5f, 0.5f, 0.0f,// kiri bawah
        //};

        //uint[] data_element =
        //{
        //    0,1,3, // segetiga pertama 
        //    1,2,3, // segitiga kedua

        //};


        List<Assets_3D> objectlist = new List<Assets_3D> ();
        double _time;

        public Window (GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            CenterWindow();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.75f, 0.77f, 0.55f, 1.0f);

            var ellipsoid1 = new Assets_3D(new Vector3(0, 0.5f, 1));
            //radius nya (parameter ke 4 - 6: di bawah 0.5f
            ellipsoid1.createEllipsoid(0, 0, 0, 0.4f, 0.4f, 0.4f);
            objectlist.Add(ellipsoid1);

            foreach (Assets_3D i in objectlist)
            {
                i.Onload();
            }


        }

        protected override void OnRenderFrame(FrameEventArgs args) // ini update tiap frame
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _time += 7.0 * args.Time;
            Matrix4 temp = Matrix4.Identity;

            //int vertexColorLocation = GL.GetUniformLocation(_shader.Handle,"unicolor");
            //GL.Uniform4(vertexColorLocation, 0.78f, 0.44f, 0.52f, 1.0f);

            foreach (Assets_3D i in objectlist)
            {
                i.Onrender(1,_time,temp);
            }

            SwapBuffers();


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

        protected override void OnResize(ResizeEventArgs e)// akan jalan tiap kali ada perubahan
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}