using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form // Наследование класса об абстрактного класса Form;
    {
        public Color f = Color.White, lc = Color.Black;// Объекты класса Color, хранящие цвет фона и линии;
        public int size = 1;// Переменная хранящая размер линии;
        public Size s = new Size(800, 600);// Хранит размер рисунка;
        public int choosen = 1;// Хранит номер выбранной фигуры;
        public bool br = true;// Хранит состояние кнопки "Заливка";
        public Font font = new Font("Times New Roman", 12);// Хранит тип шрифта;
        public int ssetka = 10;
        public bool actsetka = false;
        public Size h = new Size(800, 600);
        public Form1()// Конструктор класса;
        {
            InitializeComponent();
        }

        private void новоеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (s == h)
            {
                Form f = new Form2(s);// Создание объекта класса Form2;

                f.MdiParent = this;// Присвоение полю "Родительская форма" созданного объекта ссылки на эту форму;

                f.Text = "Рисунок " + this.MdiChildren.Length.ToString();// Присвоения названия окну ;

                f.Show();// Отоброжение окна на экране;
                this.сохранитьToolStripMenuItem.Enabled = true;// Установка кнопки меню "Сохранить" в активное состояние;
                this.сохранитьКакToolStripMenuItem.Enabled = true;// Установка кнопки меню "Сохранить как" в активное состояние;
            }
            else
            {
                Form f = new Form2(h);// Создание объекта класса Form2;

                f.MdiParent = this;// Присвоение полю "Родительская форма" созданного объекта ссылки на эту форму;

                f.Text = "Рисунок " + this.MdiChildren.Length.ToString();// Присвоения названия окну ;

                f.Show();// Отоброжение окна на экране;
                this.сохранитьToolStripMenuItem.Enabled = true;// Установка кнопки меню "Сохранить" в активное состояние;
                this.сохранитьКакToolStripMenuItem.Enabled = true;// Установка кнопки меню "Сохранить как" в активное состояние;
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(s);//Создание объекта класса Form2;
            f.MdiParent = this;// Присвоение полю "Родительская форма" созданного объекта ссылки на эту форму;
            f.openfile();//Вызов метода openfile класса Form2;
            f.Show();//Оборажение окна на экране;
            this.сохранитьToolStripMenuItem.Enabled = true;// Установка кнопки меню "Сохранить" в активное состояние;
            this.сохранитьКакToolStripMenuItem.Enabled = true;// Установка кнопки меню "Сохранить как" в активное состояние;

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = (Form2)this.ActiveMdiChild;//Объявление объекта класса Form2 и присвоение ему ссылки на активное дочернее окно;
            f.save();//Вызов метода save класса Form2;
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = (Form2)this.ActiveMdiChild;//Объявление объекта класса Form2 и присвоение ему ссылки на активное дочернее окно;
            f.savefile();//Вызов метода savefile класса Form2;
        }

        public void saveactive()
        {
            if (this.MdiChildren.Length == 1)//Проверка кол-ва открытых дочерних окон;
            {
                this.сохранитьToolStripMenuItem.Enabled = false;// Установка кнопки меню "Сохранить" в неактивное состояние;
                this.сохранитьКакToolStripMenuItem.Enabled = false;// Установка кнопки меню "Сохранить как" в неактивное состояние;
            }
        }

        private void цветЛинииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();//Создание объекта класса ColorDialog;
            color.ShowDialog();//Отображение диалогового окна;
            lc = color.Color;//Присвоение переменной выбранного цвета;
            if (MdiChildren.Length > 0)
            {
                Form2 f = (Form2)ActiveMdiChild;
                f.changeParametr();
            }
        }

        private void толщинаЛинииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();// Создание объекта класса Form3;
            f.size = size;// Присвоение полю size класса Form3 текущего значения толщины линии;

            f.ShowDialog();// Отображение диалогового окна;
            size = f.size;// Получение значения из диалога;
            if (MdiChildren.Length > 0)
            {
                Form2 form2 = (Form2)ActiveMdiChild;
                form2.changeParametr();
            }
        }

        private void размерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();// Создание объекта класса Form3;
            f.ShowDialog();// Отображение диалогового окна;
            h = f.s;// Получение значения из диалога;
        }

        private void прямоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            choosen = 1;// Установка номера выбранной фигуры;
            заливкаToolStripMenuItem.Enabled = true;// Установка кнопки заливка в активное состояние;
            прямоугольникToolStripMenuItem.Checked = true;// Установка датчика выбранной фигуры в активное состояние;
            прямаяЛинияToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
            криваяЛинияToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
            эллипсToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
    
        }

        private void прямаяЛинияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            choosen = 2;// Установка номера выбранной фигуры;
            заливкаToolStripMenuItem.Enabled = false;// Установка кнопки заливка в неактивное состояние;
            прямоугольникToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
            прямаяЛинияToolStripMenuItem.Checked = true;// Установка датчика выбранной фигуры в активное состояние;
            криваяЛинияToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
            эллипсToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
     
        }

        private void криваяЛинияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            choosen = 3;// Установка номера выбранной фигуры;
            заливкаToolStripMenuItem.Enabled = false;// Установка кнопки заливка в неактивное состояние;
            прямоугольникToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
            прямаяЛинияToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
            криваяЛинияToolStripMenuItem.Checked = true;// Установка датчика выбранной фигуры в активное состояние;
            эллипсToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
 
        }

        private void эллипсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            choosen = 4;// Установка номера выбранной фигуры;
            заливкаToolStripMenuItem.Enabled = true;// Установка кнопки заливка в активное состояние;
            прямоугольникToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
            прямаяЛинияToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
            криваяЛинияToolStripMenuItem.Checked = false;// Установка датчика не выбранной фигуры в неактивное состояние;
            эллипсToolStripMenuItem.Checked = true;// Установка датчика выбранной фигуры в активное состояние;
     
        }

        private void заливкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (br)
            {
                br = false;//Смена состояния;

            }
            else
            {
                br = true;//Смена состояния;


            }
            if (MdiChildren.Length > 0)
            {
                Form2 f = (Form2)ActiveMdiChild;
                f.changeParametr();
            }
        }


        private void цветФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();//Создание объекта класса ColorDialog;
            color.ShowDialog();//Отображение диалогового окна;
            f = color.Color;//Присвоение переменной выбранного цвета;
            if (MdiChildren.Length > 0)
            {
                Form2 f = (Form2)ActiveMdiChild;
                f.changeParametr();
            }
        }

        

    }
}
