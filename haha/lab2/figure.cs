using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    [Serializable] abstract class figure// abstract означает, что нельзя инициализировать объект этого класса;
    {
        public Point p1, p2, pabs, p11, p21;//Объявление вспомонательных переменных для рисования;
        public Color f = Color.White, lc = Color.Black;// Объекты класса Color, хранящие цвет фона и линии;
        public int size = 1;// Переменная хранящая размер линии;
        public Size s;
        public bool br;
        public Font font;
        public Rectangle rectangle;
        public Point p3 = new Point(0, 0);
        public bool red = false;
        public bool tc = false;
        Rectangle[] rectangles = new Rectangle[8];

        
        public figure(int x, int y, Color f, Color lc, int size, Size s, bool br, Font font)// Конструктор класса;
        {   
            p1 = new Point(x, y);//Инициализация переменной;
            p11 = new Point(x, y);//Инициализация переменной;
            p21 = new Point(x, y);//Инициализация переменной;
            p2 = new Point(x, y);//Инициализация переменной;
            pabs = new Point(x, y);//Инициализация переменной;
            this.f = f;// Инициализация пременной;
            this.lc = lc;// Инициализация пременной;
            this.size = size;// Инициализация пременной;
            this.s = s;// Инициализация переменной;
            this.br = br; // Инициализация переменной;
            this.font = font;
            rectangle = new Rectangle(p1, new Size(0, 0));
        }

        public abstract void Draw(Graphics g, int x, int y);//Объявление абстрактного метода, который будет реализован в наследующих классах;
        public abstract void DrawDash(Graphics g, bool k);//Объявление абстрактного метода, который будет реализован в наследующих классах;
        public abstract void Clear(Graphics g);//Объявление абстрактного метода, который будет реализован в наследующих классах;
        public virtual void MouseMove(int x, int y, Graphics g)// Метод, реализующий изменение координат по которым рисуется фигура;
        {
            Clear(g);//Вызов метода очистки предыдущего изображения;
            if (x > pabs.X)
            {
                p2.X = x;
                p21.X = x;
            }
            else
            {
                p1.X = x;
                p11.X = x;
            }
            if (y > pabs.Y)
            {
                p2.Y = y;
                p21.Y = y;
            }
            else
            {
                p1.Y = y;
                p11.Y = y;
            }// Нормализация координат;
            DrawDash(g, false);//Вызов метода рисования пунктиром;
        }
        public virtual void scroll(Point pos)
        {
            p1.X -= pos.X;// Отображение относительно смещения экрана при скроллинге;
            p1.Y -= pos.Y;// Отображение относительно смещения экрана при скроллинге;
            p2.X -= pos.X;// Отображение относительно смещения экрана при скроллинге;
            p2.Y -= pos.Y;// Отображение относительно смещения экрана при скроллинге;
            p11.X -= pos.X;// Отображение относительно смещения экрана при скроллинге;
            p11.Y -= pos.Y;// Отображение относительно смещения экрана при скроллинге;
            p21.X -= pos.X;// Отображение относительно смещения экрана при скроллинге;
            p21.Y -= pos.Y;// Отображение относительно смещения экрана при скроллинге;
        }
        public void DrawDashRect(Graphics g)
        {
           
            Pen pen = new Pen(Color.Black, 1);// Инициализация объекта класса Pen;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;// Присвоение полю "DashStyle" значения, меняющего тип линии на пунктир;
            g.DrawRectangle(pen, rectangle);// Вызов метода класса Graphics, отоброжающего прямоугольник на экране;
        }

    }
}
