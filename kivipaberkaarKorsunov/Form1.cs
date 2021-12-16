using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kivipaberkaarKorsunov
{
    public partial class Form1 : Form
    {
        Button btn1, btn2, btn3, btn_ka, btn_p, btn_ki, btn_ka2, btn_p2, btn_ki2, btn_ka3, btn_p3, btn_ki3;
        PictureBox pb_bot, pb_user, pb_user2, pb_user3;
        Label lbl_bot, lbl_user, lbl_user2, lbl_user3, lbl_lb;
        string[] AIchoice = { "kivi", "paber", "käärid" };
        public int randomNumber;
        string command;
        Random rnd = new Random();
        string playerChoice;
        string playerChoice2;
        string playerChoice3;
        public Form1()
        {
            game_menu();
        }
        public void game_menu()
        {
            this.Height = 550;
            this.Width = 700;
            this.Text = "Kivi Paber Käärid";

            btn1 = new Button();
            btn1.Text = "Arvutiga";
            btn1.Location = new Point(275, 10);
            btn1.Height = 50;
            btn1.Width = 150;
            btn1.Click += Btn_Click_bot;
            this.Controls.Add(btn1);

            btn2 = new Button();
            btn2.Text = "1 versus 1(pole veel lõppenud)";
            btn2.Location = new Point(275, 75);
            btn2.Height = 50;
            btn2.Width = 150;
            btn2.Click += Btn_Click_1v1;
            this.Controls.Add(btn2);

            btn3 = new Button();
            btn3.Text = "Parimad mängijad(pole tehtud)";
            btn3.Location = new Point(275, 140);
            btn3.Height = 50;
            btn3.Width = 150;
            btn3.Click += Btn_Click_leaderboard;
            this.Controls.Add(btn3);

            MainMenu menu = new MainMenu();
            MenuItem menuFile = new MenuItem("Fail");
            menuFile.MenuItems.Add("Tagasi menüüsse", new EventHandler(menuFile_Tagasi_Select));
            menuFile.MenuItems.Add("Reeglid", new EventHandler(menuFile_Rules_Select));
            menuFile.MenuItems.Add("Välja", new EventHandler(menuFile_Exit_Select));
            menu.MenuItems.Add(menuFile);
            this.Menu = menu;
        }
        private void Btn_Click_leaderboard(object sender, EventArgs e)
        {
            btn1.Dispose();
            btn2.Dispose();
            btn3.Dispose();

            lbl_lb = new Label();
            lbl_lb.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            lbl_lb.Text = "TOP 10";
            lbl_lb.Location = new Point(280, 10);
            this.Controls.Add(lbl_lb);


        }
        private void menuFile_Tagasi_Select(object sender, EventArgs e)
        {
            Controls.Clear();
            game_menu();
        }
        public void game_vs_bot_start()
        {
            string text = Interaction.InputBox("Sisesta siia oma nimi", "InputBox");

            btn1.Dispose();
            btn2.Dispose();
            btn3.Dispose();

            pb_bot = new PictureBox();
            pb_bot.Location = new Point(430, 100);
            pb_bot.Height = 300;
            pb_bot.Width = 235;
            pb_bot.Image = Properties.Resources.vopros;
            pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;

            pb_user = new PictureBox();
            pb_user.Location = new Point(20, 100);
            pb_user.Height = 300;
            pb_user.Width = 235;
            pb_user.Image = Properties.Resources.vopros;
            pb_user.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Add(pb_bot);
            this.Controls.Add(pb_user);

            btn_ka = new Button();
            btn_ka.Text = "Käärid";
            btn_ka.Location = new Point(20, 60);
            btn_ka.Height = 25;
            btn_ka.Width = 75;
            btn_ka.Click += kaarid_Click;
            this.Controls.Add(btn_ka);

            btn_ki = new Button();
            btn_ki.Text = "Kivi";
            btn_ki.Location = new Point(100, 60);
            btn_ki.Height = 25;
            btn_ki.Width = 75;
            btn_ki.Click += kivi_Click;
            this.Controls.Add(btn_ki);

            btn_p = new Button();
            btn_p.Text = "Paber";
            btn_p.Location = new Point(180, 60);
            btn_p.Height = 25;
            btn_p.Width = 75;
            btn_p.Click += paber_Click;
            this.Controls.Add(btn_p);

            lbl_bot = new Label();
            lbl_bot.Text = "Arvuti";
            lbl_bot.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            lbl_bot.Location = new Point(522, 420);
            this.Controls.Add(lbl_bot);

            lbl_user = new Label();
            lbl_user.Text = text;
            lbl_user.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            lbl_user.Location = new Point(110, 420);
            this.Controls.Add(lbl_user);
        }
        private void menuFile_Exit_Select(object sender, EventArgs e)
        {
            this.Close();
        }
        private void menuFile_Rules_Select(object sender, EventArgs e)
        {
            MessageBox.Show("Võitja selgitatakse välja järgmiste reeglite alusel:" +
                "\n- kivi lööb käärid (kivi nüristab käärid)" +
                "\n- käärid peksid paberit (käärid lõikasid paberit)" +
                "\n- paber lööb kivi (paber mähib kivi)" +
                "\n- viik, kui kõigil mängijatel on korraga sama märk.\n" +
                "\nKui vajutate nupule (Arvutiga), siis mängite arvutiga." +
                "\nKui vajutate nupule (1 versus 1), saate mängida teiste inimestega." +
                "\nKui vajutate nupule (Parimad mängijad), näete kõigi aegade võitude arvestuses 10 parimat mängijat.", "Reeglid");
        }
        private void nextRound()
        {
            playerChoice = "mitte";
            command = "mitte";
            pb_bot.Image = Properties.Resources.vopros;
            pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_user.Image = Properties.Resources.vopros;
            pb_user.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void Btn_Click_bot(object sender, EventArgs e)
        {
            game_vs_bot_start();
        }
        private void Btn_Click_1v1(object sender, EventArgs e)
        {
            game_vs_inimene_start();
        }
        private void kivi_Click(object sender, EventArgs e)
        {
            playerChoice = "kivi";
            pb_user.Image = Properties.Resources.Kivi;
            pb_user.SizeMode = PictureBoxSizeMode.StretchImage;
            if (playerChoice == "kivi" || playerChoice == "paber" || playerChoice == "käärid")
            {
                randomNumber = rnd.Next(0, 3);
                command = AIchoice[randomNumber];
                switch (command)
                {
                    case "kivi":
                        pb_bot.Image = Properties.Resources.Kivi;
                        pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    case "paber":
                        pb_bot.Image = Properties.Resources.Paber;
                        pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    case "käärid":
                        pb_bot.Image = Properties.Resources.Käärid;
                        pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;

                    default:
                        break;
                }
            }
            checkGame();
        }
        private void paber_Click(object sender, EventArgs e)
        {
            playerChoice = "paber";
            pb_user.Image = Properties.Resources.Paber;
            pb_user.SizeMode = PictureBoxSizeMode.StretchImage;
            if (playerChoice == "kivi" || playerChoice == "paber" || playerChoice == "käärid")
            {
                randomNumber = rnd.Next(0, 3);
                command = AIchoice[randomNumber];
                switch (command)
                {
                    case "kivi":
                        pb_bot.Image = Properties.Resources.Kivi;
                        pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    case "paber":
                        pb_bot.Image = Properties.Resources.Paber;
                        pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    case "käärid":
                        pb_bot.Image = Properties.Resources.Käärid;
                        pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    default:
                        break;
                }
            }
            checkGame();
        }
        private void kaarid_Click(object sender, EventArgs e)
        {
            playerChoice = "käärid";
            pb_user.Image = Properties.Resources.Käärid;
            pb_user.SizeMode = PictureBoxSizeMode.StretchImage;
            if (playerChoice == "kivi" || playerChoice == "paber" || playerChoice == "käärid")
            {
                randomNumber = rnd.Next(0, 3);
                command = AIchoice[randomNumber];
                switch (command)
                {
                    case "kivi":
                        pb_bot.Image = Properties.Resources.Kivi;
                        pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    case "paber":
                        pb_bot.Image = Properties.Resources.Paber;
                        pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    case "käärid":
                        pb_bot.Image = Properties.Resources.Käärid;
                        pb_bot.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;

                    default:
                        break;
                }
            }
            checkGame();
        }
        private void checkGame()
        {
            if (playerChoice == "kivi" && command == "paber")
            {
                MessageBox.Show("Arvuti võidab", "Tulemus");
                nextRound();
            }
            else if (playerChoice == "paber" && command == "kivi")
            {
                MessageBox.Show(lbl_user.Text + " võidab", "Tulemus");
                nextRound();
            }
            else if (playerChoice == "paber" && command == "käärid")
            {
                MessageBox.Show("Arvuti võidab", "Tulemus");
                nextRound();
            }
            else if (playerChoice == "käärid" && command == "paber")
            {
                MessageBox.Show(lbl_user.Text + " võidab", "Tulemus");
                nextRound();
            }
            else if (playerChoice == "käärid" && command == "kivi")
            {
                MessageBox.Show("Arvuti võidab", "Tulemus");
                nextRound();
            }
            else if (playerChoice == "kivi" && command == "käärid")
            {
                MessageBox.Show(lbl_user.Text + " võidab", "Tulemus");
                nextRound();
            }
            else
            {
                MessageBox.Show("Viik", "Tulemus");
                nextRound();
            }
        }
        public void game_vs_inimene_start()
        {
            string text2 = Interaction.InputBox("Sisesta siia oma nimi", "Esimene mängija");
            string text3 = Interaction.InputBox("Sisesta siia oma nimi", "Teine mängija");

            btn1.Dispose();
            btn2.Dispose();
            btn3.Dispose();

            pb_user2 = new PictureBox();
            pb_user2.Location = new Point(20, 100);
            pb_user2.Height = 300;
            pb_user2.Width = 235;
            pb_user2.Image = Properties.Resources.vopros;
            pb_user2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pb_user2);

            pb_user3 = new PictureBox();
            pb_user3.Location = new Point(430, 100);
            pb_user3.Height = 300;
            pb_user3.Width = 235;
            pb_user3.Image = Properties.Resources.vopros;
            pb_user3.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pb_user3);

            btn_ka2 = new Button();
            btn_ka2.Text = "Käärid";
            btn_ka2.Location = new Point(20, 60);
            btn_ka2.Height = 25;
            btn_ka2.Width = 75;
            btn_ka2.Click += kaarid_Click2;
            this.Controls.Add(btn_ka2);

            btn_ka3 = new Button();
            btn_ka3.Text = "Käärid";
            btn_ka3.Location = new Point(420, 60);
            btn_ka3.Height = 25;
            btn_ka3.Width = 75;
            btn_ka3.Click += kaarid_Click3;
            this.Controls.Add(btn_ka3);

            btn_ki2 = new Button();
            btn_ki2.Text = "Kivi";
            btn_ki2.Location = new Point(100, 60);
            btn_ki2.Height = 25;
            btn_ki2.Width = 75;
            btn_ki2.Click += kivi_Click2;
            this.Controls.Add(btn_ki2);

            btn_ki3 = new Button();
            btn_ki3.Text = "Kivi";
            btn_ki3.Location = new Point(500, 60);
            btn_ki3.Height = 25;
            btn_ki3.Width = 75;
            btn_ki3.Click += kivi_Click3;
            this.Controls.Add(btn_ki3);

            btn_p2 = new Button();
            btn_p2.Text = "Paber";
            btn_p2.Location = new Point(180, 60);
            btn_p2.Height = 25;
            btn_p2.Width = 75;
            btn_p2.Click += paber_Click2;
            this.Controls.Add(btn_p2);

            btn_p3 = new Button();
            btn_p3.Text = "Paber";
            btn_p3.Location = new Point(580, 60);
            btn_p3.Height = 25;
            btn_p3.Width = 75;
            btn_p3.Click += paber_Click3;
            this.Controls.Add(btn_p3);

            lbl_user2 = new Label();
            lbl_user2.Text = text2;
            lbl_user2.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            lbl_user2.Location = new Point(110, 420);
            this.Controls.Add(lbl_user2);

            lbl_user3 = new Label();
            lbl_user3.Text = text3;
            lbl_user3.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            lbl_user3.Location = new Point(522, 420);
            this.Controls.Add(lbl_user3);
        }
        private void kaarid_Click2(object sender, EventArgs e)
        {
            playerChoice2 = "käärid";
            if (playerChoice3 == "kivi" || playerChoice3 == "paber" || playerChoice3 == "käärid")
            {
                pb_user2.Image = Properties.Resources.Käärid;
                pb_user2.SizeMode = PictureBoxSizeMode.StretchImage;
                checkGame2();
            }
            else
            {
                pb_user2.Image = Properties.Resources.vopros;
                pb_user2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void kaarid_Click3(object sender, EventArgs e)
        {
            playerChoice3 = "käärid";
            if (playerChoice2 == "kivi" || playerChoice2 == "paber" || playerChoice2 == "käärid")
            {
                pb_user3.Image = Properties.Resources.Käärid;
                pb_user3.SizeMode = PictureBoxSizeMode.StretchImage;
                checkGame2();
            }
            else
            {
                pb_user3.Image = Properties.Resources.vopros;
                pb_user3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void paber_Click2(object sender, EventArgs e)
        {
            playerChoice2 = "paber";
            if (playerChoice3 == "kivi" || playerChoice3 == "paber" || playerChoice3 == "käärid")
            {
                pb_user2.Image = Properties.Resources.Käärid;
                pb_user2.SizeMode = PictureBoxSizeMode.StretchImage;
                checkGame2();
            }
            else
            {
                pb_user2.Image = Properties.Resources.vopros;
                pb_user2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void paber_Click3(object sender, EventArgs e)
        {
            playerChoice3 = "paber";
            if (playerChoice2 == "kivi" || playerChoice2 == "paber" || playerChoice2 == "käärid")
            {
                pb_user3.Image = Properties.Resources.Käärid;
                pb_user3.SizeMode = PictureBoxSizeMode.StretchImage;
                checkGame2();
            }
            else
            {
                pb_user3.Image = Properties.Resources.vopros;
                pb_user3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void kivi_Click2(object sender, EventArgs e)
        {
            playerChoice2 = "paber";
            if (playerChoice3 == "kivi" || playerChoice3 == "paber" || playerChoice3 == "käärid")
            {
                pb_user2.Image = Properties.Resources.Käärid;
                pb_user2.SizeMode = PictureBoxSizeMode.StretchImage;
                checkGame2();
            }
            else
            {
                pb_user2.Image = Properties.Resources.vopros;
                pb_user2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void kivi_Click3(object sender, EventArgs e)
        {
            playerChoice3 = "paber";
            if (playerChoice2 == "kivi" || playerChoice2 == "paber" || playerChoice2 == "käärid")
            {
                pb_user3.Image = Properties.Resources.Käärid;
                pb_user3.SizeMode = PictureBoxSizeMode.StretchImage;
                checkGame2();
            }
            else
            {
                pb_user3.Image = Properties.Resources.vopros;
                pb_user3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void nextRound2()
        {
            playerChoice2 = "mitte";
            playerChoice3 = "mitte";
            pb_user2.Image = Properties.Resources.vopros;
            pb_user2.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_user3.Image = Properties.Resources.vopros;
            pb_user3.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void checkGame2()
        {
            if (playerChoice2 == "kivi" && playerChoice3 == "paber")
            {
                MessageBox.Show(lbl_user3.Text + " võidab", "Tulemus");
                nextRound2();
            }
            else if (playerChoice2 == "paber" && playerChoice3 == "kivi")
            {
                MessageBox.Show(lbl_user2.Text + " võidab", "Tulemus");
                nextRound2();
            }
            else if (playerChoice2 == "paber" && playerChoice3 == "käärid")
            {
                MessageBox.Show(lbl_user3.Text + " võidab", "Tulemus");
                nextRound2();
            }
            else if (playerChoice2 == "käärid" && playerChoice3 == "paber")
            {
                MessageBox.Show(lbl_user2.Text + " võidab", "Tulemus");
                nextRound2();
            }
            else if (playerChoice2 == "käärid" && playerChoice3 == "kivi")
            {
                MessageBox.Show(lbl_user3.Text + " võidab", "Tulemus");
                nextRound2();
            }
            else if (playerChoice2 == "kivi" && playerChoice3 == "käärid")
            {
                MessageBox.Show(lbl_user2.Text + " võidab", "Tulemus");
                nextRound2();
            }
            else
            {
                MessageBox.Show("Viik", "Tulemus");
                nextRound2();
            }
        }
    }
}