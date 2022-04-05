using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using LearnOpenTK.Common;
using OpenTK.Mathematics;

namespace Test
{
    internal class Assets1
    {
        float[] data = {

        };

        uint[] data_element =
        {

        };

        int _vertexBufferObject;
        int _elementBufferObject; 
        int _vertexArrayObject;
        int index;
        int[] _pascal;
        Shader _shader;

        public Assets1(float[] vertices, uint[] indices)
        {
            data = vertices;
            data_element = indices;
            index = 0;
        }

        public void load(string shadervert, string shaderfrag)
        {
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            //parameter 1 --> variable _vertices nya itu disimpan di shader index
            //keberapa?
            //parameter 2 --> didalam variable _vertices, ada berapa vertex?
            //paramter 3  --> jenis vertex yang dikirim typenya apa?
            //parameter 4 --> datanya perlu dinormalisasi ndak?
            //parameter 5 --> dalam 1 vertex/1 baris itu mengandung berapa banyak
            //titik?
            //parameter 6 --> data yang mau diolah mulai da

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            // 0 ini start dari mana 
            GL.EnableVertexAttribArray(0);

            if(data_element.Length != 0)
            {
                _elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, data_element.Length * sizeof(uint), data_element, BufferUsageHint.StaticDraw);
            }

            _shader = new Shader(shadervert,shaderfrag);
            _shader.Use();
        }

        public void render(float r, float g, float b,int pilihan)
        {
            _shader.Use();

            int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "unicolor");
            GL.Uniform4(vertexColorLocation, r, g, b, 1.0f);

            GL.BindVertexArray(_vertexArrayObject);


            if(data_element.Length != 0)
            {
                GL.DrawElements(PrimitiveType.Triangles, data_element.Length, DrawElementsType.UnsignedInt, 0);
            }
            else
            {
                if(pilihan == 0)
                {
                    //parameter 2 mulai dari titik berapa
                    //parameter 3 jumalah yang mau digambar
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                }
                else if(pilihan == 1)
                {
                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, (data.Length + 1) / 3);
                }
                else if (pilihan == 2)
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, index);
                }
                else if (pilihan == 3)
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, (data.Length + 1) / 3);
                }

            }
            
        }

        public void createCircle(float center_x, float center_y, float radius)
        {
            data = new float[1080];

            for(int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180; // ubah dari derajat ke radiant
                //x 
                data[i * 3] = radius * (float)Math.Cos(degInRad) + center_x;
                //y
                data[i*3 + 1] = radius * (float)Math.Sin(degInRad) + center_y;
                //y
                data[i * 3 + 2] = 0;
            }


        }
        public void createEllips(float center_x, float center_y, float radius_x, float radius_y)
        {
            data = new float[1080];

            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180; // ubah dari derajat ke radiant
                //x 
                data[i * 3] = radius_x * (float)Math.Cos(degInRad) + center_x;
                //y
                data[i * 3 + 1] = radius_y * (float)Math.Sin(degInRad) + center_y;
                //y
                data[i * 3 + 2] = 0;
            }


        }

        public void updateMousePosition(float _x, float _y)
        {
            data[index * 3] = _x;
            data[index * 3 + 1] = _y;
            data[index * 3 + 2] = 0;
            index++;

            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            
        }

        public List<int> getRow(int rowIndex)
        {
            List<int> currow = new List<int>();
            //------
            currow.Add(1);
            if (rowIndex == 0)
            {
                return currow;// return kalau segitiga pascal paling atas
            }
            //-----
            List<int> prev = getRow(rowIndex - 1);//rekursi
            for (int i = 1; i < prev.Count; i++)
            {
                int curr = prev[i - 1] + prev[i];
                currow.Add(curr);
            }
            currow.Add(1);
            return currow;
        }

        public List<float> createCurveBezier()
        {
            List<float> _vertices_bezier = new List<float>();
            List<int> pascal = getRow(index - 1);
            _pascal = pascal.ToArray();

            for (float t = 0; t <= 1.0f; t += 0.01f)
            {
                Vector2 p = getP(index,t);
                _vertices_bezier.Add(p.X);
                _vertices_bezier.Add(p.Y);
                _vertices_bezier.Add(0);
            }
            return _vertices_bezier;
        }

        public Vector2 getP(int n, float t)
        {
            Vector2 p = new Vector2(0, 0);    
            float k;
            for (int i = 0; i < n; i++)
            {
                k = (float) Math.Pow((1 - t),n - 1 -i) * (float) Math.Pow(t,i) * _pascal[i];
                p.X += k * data[i * 3];
                p.Y += k * data[i * 3 + 1];
            }
            return p;
        }

        public bool getVerticesLength()
        {
            if(data[0] == 0)
            {
                return false;
            }

            if((data.Length + 1)/3 > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setVertices(float[] _temp)
        {
            data = _temp;
        }


    }
}
