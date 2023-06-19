using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace lab2
{
    
    public partial class Form2 : Form// Наследование класса об абстрактного класса Form;
    {
        internal List<figure> list = new List<figure>();// Объявление структуры динамического массива List;
        internal List<figure> choosed = new List<figure>();// Объявление структуры динамического массива List;
        figure figure;
        private bool mb = false;// Объявление переменной-датчика нажатия кнопки мыши;
        private bool ch = false;// Объявление переменной-датчика изменения в рисунке;
        public Color f = Color.White, lc = Color.Black;// Объекты класса Color, хранящие цвет фона и линии;
        public int size = 1;// Переменная хранящая размер линии;
        internal Size s;
        private int choosen = 1;// Хранит номер выбранной фигуры;
        private bool br = true;// Хранит состояние кнопки "Заливка";
        private BufferedGraphics bf;
        internal Font font = new Font("Times New Roman", 12);
        int shag = 10;
        bool setka = false;
        public bool withsetka = false;
        public string col;
        public Form2(Size s)//Конструктор класса;
        
        {
            this.s = s;// Получение размера рисунка;
            InitializeComponent();
            AutoScrollMinSize = s;// Задание минимального размера;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            bf = new BufferedGraphicsContext().Allocate(this.CreateGraphics(), new Rectangle(new Point(0, 0), s));// Инициализация объекта хранящего буфер;
            
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)// Метод-обработчик события нажатия на кнопку мыши;
        {
            Form1 form = (Form1)this.ParentForm;// Объявление объекта класса Form1 и присвоение ему ссылки на родительскую форму;
            f = form.f;//Получение значения из родительской формы;
            lc = form.lc;//Получение значения из родительской формы;
            size = form.size;//Получение значения из родительской формы;
            if (choosen != 7)
            choosen = form.choosen;//Получение значения из родительской формы;
            br = form.br;//Получение значения из родительской формы;
            font = form.font;
            switch (choosen)//Выбор фигуры;
            {
                case 1:
                    figure = new rect(e.X, e.Y, f, lc, size, s, br, font);// Инициализация объекта класса rect;
                    break;
                case 2:
                    figure = new line(e.X, e.Y, f, lc, size, s, br, font);// Инициализация объекта класса line;
                    break;
                case 3:
                    figure = new curve(e.X, e.Y, f, lc, size, s, br, font);// Инициализация объекта класса curve;
                    break;
                case 4:
                    figure = new ellipse(e.X, e.Y, f, lc, size, s, br, font);// Инициализация объекта класса ellipse;
                    break;
            }

            mb = true;// Изменение состояния переменной-датчика на true(нажата);
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)// Метод-обработчик события движения курсора;
        {
            if (mb == true)// Проверка условия нажатия кнопки мыши;
            {   

                 Graphics g = CreateGraphics();// Создание объекта класса Graphics для рисования 2D объектов;
                 My_Paint();
                 figure.MouseMove(e.X, e.Y, g);// Вызов метода класса rect, реализующий изменение координат по которым рисуется прямоугольник;
                 Form1 f = (Form1)ParentForm;
            }

            
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)// Метод-обработчик события отпускания кнопки мыши;
        {
            if ((e.X > s.Width) || (e.Y > s.Height))// Проверка условия не выхода за границы рисования;
            {

            }
           else
            {
 
                    Graphics g = CreateGraphics();// Создание объекта класса Graphics для рисования 2D объектов;
                    figure.scroll(AutoScrollPosition);// Вызов метода класса rect, реализующий пересчет координат в зависимости от скроллинга;
                    figure.Draw(bf.Graphics, AutoScrollPosition.X, AutoScrollPosition.Y);// Вызов метода класса rect, реализующий отображение прямоугольника на экране;

                    ch = true;// Изменение состояния переменной датчика на true;
                    list.Add(figure);// Добавление нового элемента в динамический массив;


            }
            mb = false;// Изменение состояния переменной-датчика на false(не нажата);
            Invalidate();// Перерисовка;
        }


        private void My_Paint()// Метод-обработчик события перерисовки;
        {
            Graphics g = CreateGraphics();// Создание объекта класса Graphics для рисования 2D объектов;
            bf.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(new Point(0, 0), s));// Заливка допустимой для рисования области;
            foreach (figure f in list)// Цикл перебирающий все элементы массива;
            {
                f.Draw(bf.Graphics, AutoScrollPosition.X, AutoScrollPosition.Y);//Вызов метода рисования фигуры;
                if (f.red)
                {
                    figure.DrawDash(bf.Graphics, true);
                }
            }
            choosed.Clear();
            foreach (figure f in choosed)
            {
                f.DrawDashRect(bf.Graphics);
                f.DrawDash(bf.Graphics, true);
            }
            if (setka)
            {
                for (int i = shag; i < s.Width; i += shag)
                {
                    Pen pen = new Pen(Color.Black, size);// Инициализация объекта класса Pen;
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;// Присвоение полю "DashStyle" значения, меняющего тип линии на пунктир;
                    bf.Graphics.DrawLine(pen, i, 0, i, s.Height);
                }
                for (int i = shag; i < s.Height; i += shag)
                {
                    Pen pen = new Pen(Color.Black, size);// Инициализация объекта класса Pen;
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;// Присвоение полю "DashStyle" значения, меняющего тип линии на пунктир;
                    bf.Graphics.DrawLine(pen, 0, i, s.Width, i);
                }
            }
            bf.Render(g);//Передача объектов из буфера в Graphics;

        }

        private void Form2_Paint(object sender, PaintEventArgs e)// Метод-обработчик события перерисовки;
        {
            Graphics g = CreateGraphics();// Создание объекта класса Graphics для рисования 2D объектов;
            bf.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(new Point(0, 0), s));// Заливка допустимой для рисования области;
            foreach (figure f in list)// Цикл перебирающий все элементы массива;
            {
                f.Draw(bf.Graphics, AutoScrollPosition.X, AutoScrollPosition.Y);//Вызов метода рисования фигуры;
                if (f.red)
                {
                    figure.DrawDash(bf.Graphics, true);
                }    
            }
            foreach (figure f in choosed)
            {
                //f.DrawDashRect(bf.Graphics);
                f.DrawDash(bf.Graphics, true);
            }
            if (setka)
            {
                for (int i = shag; i < s.Width; i += shag)
                {
                    Pen pen = new Pen(Color.Black, size);// Инициализация объекта класса Pen;
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;// Присвоение полю "DashStyle" значения, меняющего тип линии на пунктир;
                    bf.Graphics.DrawLine(pen, i, 0, i, s.Height);
                }
                for (int i = shag; i < s.Height; i += shag)
                {
                    Pen pen = new Pen(Color.Black, size);// Инициализация объекта класса Pen;
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;// Присвоение полю "DashStyle" значения, меняющего тип линии на пунктир;
                    bf.Graphics.DrawLine(pen, 0, i, s.Width, i);
                }
            }
            bf.Render(g);//Передача объектов из буфера в Graphics;
           
        }

        public void openfile()// Метод, реализующий открытие рисунка из файла;
        {
            OpenFileDialog open = new OpenFileDialog();// Создание объекта класса OpenFileDialog;
            open.InitialDirectory = Environment.CurrentDirectory;// Задание стартовой папки для диалога;
            open.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)// Отображение диалогового окна;
            {
                BinaryFormatter formatter = new BinaryFormatter();// Создание объекта класса BinaryFormatter; 
                Stream stream = new FileStream(open.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);// Создание объекта класса Stream и настройка потока на открытие файла;              
                list = (List<figure>)formatter.Deserialize(stream);// Получение динамического массива из файла;
                this.Text = Path.GetFileName(open.FileName);// Присвоение окну имени файла;
                s = list.First().s;// Получение размера из файла;
                stream.Close();// Закрытие потока; 
            }
            else 
            { 
            }

        }
        public void savefile()// Метод, реализующий сохранение рисунка в файла;
        {
            SaveFileDialog save = new SaveFileDialog();// Создание объекта класса SaveFileDialog;
            save.InitialDirectory = Environment.CurrentDirectory;// Задание стартовой папки для диалога;
            save.FileName = this.Text;// Присвоение полю имя файла имени текущей формы;
            save.ShowDialog();// Отображение диалогового окна;
            BinaryFormatter formatter = new BinaryFormatter();// Создание объекта класса BinaryFormatter; 
            Stream stream = new FileStream(save.FileName, FileMode.Create, FileAccess.Write, FileShare.None);// Создание объекта класса Stream и настройка потока на запись в файл;
            formatter.Serialize(stream, list);// Запись динамического массива в файл;
            this.Text = Path.GetFileName(save.FileName);// Присвоение окну имени файла;
            stream.Close();// Закрытие потока;
            ch = false;// Установка датчика изменений в false;
        }

        public void save()// Метод, реализующий сохранение рисунка в файла;
        {
            BinaryFormatter formatter = new BinaryFormatter();// Создание объекта класса BinaryFormatter; 
            Stream stream = new FileStream(this.Text, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, list);// Создание объекта класса Stream и настройка потока на запись в файл;
            stream.Close();// Закрытие потока;
            ch = false;// Установка датчика изменений в false;
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            Form1 f = (Form1)ParentForm;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)// Метод-обработчик события закрытия формы;
        {
            if (ch == true)// Проверка были ли изменения в рисунке;
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;// Параметр MessageBox;
                DialogResult result;//Объект класса Dialog result;
                result = MessageBox.Show("Сохранить изменения?", "Закрыть", buttons);// Присвоение результата работы диалога объекту созданному ранее;
                if (result == DialogResult.Yes)// Проверка нажатия на кнопку "Да";
                {
                    save();// Вызов метода, отвечающего за сохранение;
                    Form1 f = (Form1)this.ParentForm;// Создание объекта класса Form1 и присвоение ему ссылки на родительскую форму;
                    f.saveactive();// Вызов метода saveactive класса Form1;
                }
                else if (result == DialogResult.No)// Проверка нажатия на кнопку "Нет";
                {
                    Form1 f = (Form1)this.ParentForm;// Создание объекта класса Form1 и присвоение ему ссылки на родительскую форму;
                    f.saveactive();// Вызов метода saveactive класса Form1;
                }
                else// Проверка нажатия на кнопку "Отмена";
                {
                    e.Cancel = true;//Отмена закрытия окна с рисунком;
                }
            }
        }

        public void changeParametr()
        {
            Form1 form = (Form1)this.ParentForm;// Объявление объекта класса Form1 и присвоение ему ссылки на родительскую форму;
            figure.f = form.f;//Получение значения из родительской формы;
            figure.lc = form.lc;//Получение значения из родительской формы;
            figure.br = form.br;//Получение значения из родительской формы;
            figure.size = form.size;
            figure.font = form.font;
            
        }
        
    }
}
