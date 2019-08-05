using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Tools_for_World_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string PlayersDataFolderPath;
        string PlayersDataPath;
        List<FileStream> datas;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PKCountry A = new PKCountry();
                A.Skill = Convert.ToDouble(skill1.Text);
                A.People = Convert.ToDouble(people1.Text);
                A.Science = Convert.ToDouble(science1.Text);
                A.Money = Convert.ToDouble(money1.Text);
                PKCountry B = new PKCountry();
                B.Skill = Convert.ToDouble(skill2.Text);
                B.People = Convert.ToDouble(people2.Text);
                B.Science = Convert.ToDouble(science2.Text);
                B.Money = Convert.ToDouble(money2.Text);
                PKCountry W = new PKCountry();
                W.Skill = Convert.ToDouble(skillNUD.Value);
                W.People = Convert.ToDouble(peopleNUD.Value);
                W.Science = Convert.ToDouble(scienceNUD.Value);
                W.Money = Convert.ToDouble(moneyNUD.Value);
                MessageBox.Show(PK(A, B, W) ? "甲胜利" : "乙胜利");
            }
            catch
            {
                MessageBox.Show("UNKNOWN ERROR");
            }
        }

        public bool PK(PKCountry A, PKCountry B, PKCountry W)
        {
            double Aall = GetSum(A, W);
            double Ball = GetSum(B, W);
            return Aall > Ball;
        }

        public double GetSum(PKCountry i, PKCountry W)
        {
            return i.Skill * W.Skill + i.People * W.People + i.Science * W.Science + i.Money * W.Science;
        }

        public Country GetCountry()
        {
            Country c = new Country();
            c.Name = CName.Text;
            c.Population = Convert.ToDouble(Population.Text);
            c.Money = Convert.ToDouble(Money.Text);
            c.Place = Convert.ToDouble(Place.Text);
            c.Technology = Convert.ToDouble(Technology.Text);
            c.Army = Convert.ToDouble(Army.Text);
            c.Industral = Convert.ToDouble(Industrial.Text);
            return c;
        }

        public void SetCountry(Country c)
        {
            CName.Text = c.Name.ToString();
            Population.Text = c.Population.ToString();
            Money.Text = c.Money.ToString();
            Place.Text = c.Place.ToString();
            Technology.Text = c.Technology.ToString();
            Army.Text = c.Army.ToString();
            Industrial.Text = c.Industral.ToString();
        }

        public void SavePlayerDatas(Country c)
        {
            try
            {
                using (FileStream fs = File.Create(PlayersDataPath + @"\" + c.Name + @".dat"))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(Population.Text);
                        sw.WriteLine(Place.Text);
                        sw.WriteLine(Money.Text);
                        sw.WriteLine(Army.Text);
                        sw.WriteLine(Industrial.Text);
                        sw.WriteLine(Technology.Text);
                    }
                }
            }
            catch
            {
                MessageBox.Show("UNKNOWN ERROR");
            }
        }

        public Country ReadPlayerDatas(string filename)
        {
            Country c = new Country();
            try
            {
                using (FileStream fs = File.Open(PlayersDataPath + @"\" + filename, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        c.Name = filename.Replace(".dat", "");
                        c.Population = Convert.ToDouble(sr.ReadLine());
                        c.Place = Convert.ToDouble(sr.ReadLine());
                        c.Money = Convert.ToDouble(sr.ReadLine());
                        c.Army = Convert.ToDouble(sr.ReadLine());
                        c.Industral = Convert.ToDouble(sr.ReadLine());
                        c.Technology = Convert.ToDouble(sr.ReadLine());
                    }
                }
            }
            catch
            {
                MessageBox.Show("UNKNOWN ERROR");
            }
            return c;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                string MyDoc = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                PlayersDataFolderPath = MyDoc + @"\Tools for World Game";
                PlayersDataPath = PlayersDataFolderPath + @"\PlayersData";
                if (!Directory.Exists(PlayersDataPath)) Directory.CreateDirectory(PlayersDataPath);
                DirectoryInfo di = new DirectoryInfo(PlayersDataPath);
                foreach (FileInfo fi in di.GetFiles())
                {
                    listBox1.Items.Add(fi.Name);
                }
            }
            catch
            {
                MessageBox.Show("UNKNOWN ERROR");
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            try
            {
                Country c = GetCountry();
                foreach (Object s in listBox1.Items)
                {
                    if (s.ToString() == c.Name + ".dat")
                    {
                        MessageBox.Show("已有该国家");
                        return;
                    }
                }
                listBox1.Items.Add(c.Name);
                SavePlayerDatas(c);
            }
            catch
            {
                MessageBox.Show("UNKNOWN ERROR");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Country c = ReadPlayerDatas(listBox1.SelectedItem.ToString());
            SetCountry(c);
        }

        private void SaveChange_Click(object sender, EventArgs e)
        {
            Country c = GetCountry();
            c.Name = listBox1.SelectedItem.ToString().Replace(".dat", "");
            SavePlayerDatas(c);
        }

        private void Spend_Click(object sender, EventArgs e)
        {
            Country input = GetCountry();
            Country output = input;
            output.Population = input.Population * (1 + Convert.ToDouble(PG.Text));
            SetCountry(output);
        }
    }

    public struct PKCountry
    {
        double skill;

        public double Skill
        {
            get { return skill; }
            set { skill = value; }
        }

        double people;

        public double People
        {
            get { return people; }
            set { people = value; }
        }

        double science;

        public double Science
        {
            get { return science; }
            set { science = value; }
        }

        double money;

        public double Money
        {
            get { return money; }
            set { money = value; }
        }

    }

    public struct Country
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        double population;

        public double Population
        {
            get { return population; }
            set { population = value; }
        }

        double place;

        public double Place
        {
            get { return place; }
            set { place = value; }
        }

        double money;

        public double Money
        {
            get { return money; }
            set { money = value; }
        }

        double army;

        public double Army
        {
            get { return army; }
            set { army = value; }
        }

        double industral;

        public double Industral
        {
            get { return industral; }
            set { industral = value; }
        }

        double technology;

        public double Technology
        {
            get { return technology; }
            set { technology = value; }
        }
    }
}
