using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FightClubDemo
{
    public partial class FightClubForm : Form
    {
        private bool isPlaying = false;
        private PictureBox playButton;
        private Label greeting;
        private String username = null;
        private TextBox nameBox;
        private PictureBox head, body, legs;
        private PictureBox userHeart, enemyHeart;
        private Label userHealth, enemyHealth;
        private Label nameOfUser, enemyName;
        private TextBox eventRecords;
        private UserFighter user, enemy;
        private int round;
        private Random aiSelect = new Random();
        private PictureBox command;

        public FightClubForm()
        {
            InitializeComponent();

            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Aqua;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.SizeChanged += FightClubForm_SizeChanged;


            greeting = new Label();
            greeting.Text = "Welcome to FightClub!";
            greeting.TextAlign = ContentAlignment.BottomCenter;
            greeting.ForeColor = Color.Tomato;
            greeting.Font = new Font("Times New Roman", 20, FontStyle.Italic);
            greeting.AutoSize = true;
            greeting.Location = new Point(this.Width / 5 + 10, this.Height / 4);
            this.Controls.Add(greeting);


            playButton = new PictureBox();
            playButton.Image = Image.FromFile("play1.png");
            playButton.Size = new Size(100, 100);
            playButton.Location = new Point(this.Width / 2 - 40, this.Height / 2 - 40);
            playButton.Click += PlayButton_Click;
            playButton.MouseEnter += PlayButton_MouseEnter;
            playButton.MouseLeave += PlayButton_MouseLeave;
            this.Controls.Add(playButton);

            nameBox = new TextBox();
            nameBox.Text = "Please, Enter your name ";
            nameBox.Size = new Size(150, 100);
            nameBox.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            nameBox.BackColor = Color.Aqua;
            nameBox.Location = new Point(this.Width / 2 - nameBox.Size.Width / 2, this.Height / 3);
            nameBox.TextChanged += NameBox_TextChanged;
            this.Controls.Add(nameBox);
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            username = nameBox.Text;
        }

        private void FightClubForm_SizeChanged(object sender, EventArgs e)
        {
            if(!isPlaying)
            {
                playButton.Location = new Point(this.Width / 2 - 40, this.Height / 2 - 40);
                greeting.Location = new Point(this.Width / 2 - greeting.Width/2, this.Height / 4);
                nameBox.Location = new Point(this.Width / 2 - nameBox.Size.Width / 2, this.Height / 3);
            }
            else
            {
                //TODO
                head.Location = new Point(this.Width / 2 - head.Size.Width / 2, this.Height / 6 + 30);
                body.Location = new Point(head.Location.X, head.Location.Y + 105);
                legs.Location = new Point(head.Location.X, body.Location.Y + 105);
                userHeart.Location = new Point((head.Location.X - 50) / 2, 30);
                enemyHeart.Location = new Point(head.Location.X + (head.Location.X - userHeart.Location.X) + 55, 30);
                enemyHealth.Location = new Point(enemyHeart.Location.X - 55, 60);
                userHealth.Location = new Point((head.Location.X - 50) / 2 - 55, 60);
                nameOfUser.Location = new Point(userHeart.Location.X - nameOfUser.Text.Length / 2, 0);
                enemyName.Location = new Point(enemyHeart.Location.X - enemyName.Text.Length / 2, 0);
                eventRecords.Location = new Point((head.Location.X + 50) - 150, this.Height - 150);
                command.Location = new Point(head.Location.X, 0);

            }
        }

        private void PlayButton_MouseLeave(object sender, EventArgs e)
        {
            playButton.Image = Image.FromFile("play1.png");
        }

        private void PlayButton_MouseEnter(object sender, EventArgs e)
        {
            playButton.Image = Image.FromFile("play.png");
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if(username != null)
            {
                isPlaying = true;
                StartGame();           
            }
            else
            {
                MessageBox.Show("Error! Enter your name!");
            }
        }

        private void StartGame()
        {
            this.Controls.Clear();
            user = new UserFighter(username);
            enemy = new UserFighter("Enemy");
            user.Block += Blocked;
            user.Death += Dead;
            user.Wound += Wouned;
            enemy.Block += Blocked;
            enemy.Death += Dead;
            enemy.Wound += Wouned;
            round = 1;

            head = new PictureBox();
            head.Image = Image.FromFile("head1.png");
            head.Size = new Size(100, 100);
            head.Location = new Point(this.Width / 2 - head.Size.Width / 2, this.Height / 6 + 30);
            head.MouseEnter += Head_MouseEnter;
            head.MouseLeave += Head_MouseLeave;
            head.Click += Head_Click;
            this.Controls.Add(head);

            body = new PictureBox();
            body.Image = Image.FromFile("body1.png");
            body.Size = new Size(100, 100);
            body.Location = new Point(this.Width / 2 - head.Size.Width / 2, this.Height / 3);
            body.MouseEnter += Body_MouseEnter;
            body.MouseLeave += Body_MouseLeave;
            body.Click += Body_Click;
            this.Controls.Add(body);

            legs = new PictureBox();
            legs.Image = Image.FromFile("legs1.png");
            legs.Size = new Size(100, 100);
            legs.Location = new Point(this.Width / 2 - head.Size.Width / 2, this.Height / 2 + 5);
            legs.MouseEnter += Legs_MouseEnter;
            legs.MouseLeave += Legs_MouseLeave;
            legs.Click += Legs_Click;
            this.Controls.Add(legs);

            userHeart = new PictureBox();
            userHeart.Image = Image.FromFile("heart.png");
            userHeart.Size = new Size(100, 100);
            userHeart.Location = new Point((head.Location.X-50)/2, 30);
            this.Controls.Add(userHeart);

            enemyHeart = new PictureBox();
            enemyHeart.Image = Image.FromFile("heart.png");
            enemyHeart.Size = new Size(100, 100);
            enemyHeart.Location = new Point(head.Location.X + (head.Location.X - userHeart.Location.X) + 55, 30);
            this.Controls.Add(enemyHeart);

            userHealth = new Label();
            userHealth.Text = "100%";
            userHealth.ForeColor = Color.Red;
            userHealth.AutoSize = true;
            userHealth.Font = new Font("Times New Roman", 15, FontStyle.Bold);
            userHealth.Location = new Point((head.Location.X - 50) /2 - 55, 60);
            this.Controls.Add(userHealth);

            enemyHealth = new Label();
            enemyHealth.Text = "100%";
            enemyHealth.ForeColor = Color.Red;
            enemyHealth.AutoSize = true;
            enemyHealth.Font = new Font("Times New Roman", 15, FontStyle.Bold);
            enemyHealth.Location = new Point(enemyHeart.Location.X - 55, 60);
            this.Controls.Add(enemyHealth);

            nameOfUser = new Label();
            nameOfUser.Text = username;
            nameOfUser.AutoSize = true;
            nameOfUser.Font = new Font("Times New Roman", 15, FontStyle.Bold);
            nameOfUser.ForeColor = Color.DarkBlue;
            nameOfUser.Location = new Point(userHeart.Location.X - nameOfUser.Text.Length / 2, 0);
            this.Controls.Add(nameOfUser);

            enemyName = new Label();
            enemyName.Text = "Enemy";
            enemyName.AutoSize = true;
            enemyName.Font = new Font("Times New Roman", 15, FontStyle.Bold);
            enemyName.ForeColor = Color.DarkBlue;
            enemyName.Location = new Point(enemyHeart.Location.X - enemyName.Text.Length / 2, 0);
            this.Controls.Add(enemyName);

            eventRecords = new TextBox();
            eventRecords.Size = new Size(300, 200);
            eventRecords.BackColor = Color.Aqua;
            eventRecords.ReadOnly = true;
            eventRecords.ScrollBars = ScrollBars.Vertical;
            eventRecords.MaximumSize = new Size(300, 20);
            eventRecords.Location = new Point((head.Location.X + 50) - 150, this.Height - 150);
            eventRecords.Text = "";
            eventRecords.Multiline = true;
            eventRecords.MinimumSize = new Size(300, 100);

            this.Controls.Add(eventRecords);

            command = new PictureBox();
            command.Image = Image.FromFile("attack.png");
            command.Size = new Size(100, 100);
            command.Location = new Point(head.Location.X, 0);
            this.Controls.Add(command);
        }

        private void Legs_Click(object sender, EventArgs e)
        {
            //AI attacks
            if(round%2 == 0)
            {
                attack(enemy, (BodyPart)aiSelect.Next(1, 3), user, BodyPart.Legs);
            }
            else
            {
                attack(user, BodyPart.Legs, enemy, (BodyPart)aiSelect.Next(1, 3));
            }
        }

        private void Body_Click(object sender, EventArgs e)
        {
            //AI attacks
            if (round % 2 == 0)
            {
                attack(enemy, (BodyPart)aiSelect.Next(1, 3), user, BodyPart.Body);
            }
            else
            {
                attack(user, BodyPart.Body, enemy, (BodyPart)aiSelect.Next(1, 3));
            }
        }

        private void Head_Click(object sender, EventArgs e)
        {
            //AI attacks
            if (round % 2 == 0)
            {
                attack(enemy, (BodyPart)aiSelect.Next(1, 3), user, BodyPart.Head);
            }
            else
            {
                attack(user, BodyPart.Head, enemy, (BodyPart)aiSelect.Next(1, 3));

            }
        }

        private void Legs_MouseLeave(object sender, EventArgs e)
        {
            legs.Image = Image.FromFile("legs1.png");
        }

        private void Legs_MouseEnter(object sender, EventArgs e)
        {
            legs.Image = Image.FromFile("legs2.png");
        }

        private void Body_MouseLeave(object sender, EventArgs e)
        {
            body.Image = Image.FromFile("body1.png");
        }


        private void Body_MouseEnter(object sender, EventArgs e)
        {
            body.Image = Image.FromFile("body2.png");
        }

        private void Head_MouseLeave(object sender, EventArgs e)
        {
            head.Image = Image.FromFile("head1.png");
        }

        private void Head_MouseEnter(object sender, EventArgs e)
        {
            head.Image = Image.FromFile("head2.png");
        }

        private void FightClubForm_Load(object sender, EventArgs e)
        {

        }
        
        private void Blocked(UserInfo u)
        {
            logEvent($"User {u.Name} has blocked the hit \n");
        }
        private void Wouned(UserInfo u)
        {
            logEvent($"User {u.Name} has not blocked the hit \n" +
                $"HP: {u.HP}");
            if(u.Name == "Enemy")
            {
                enemyHealth.Text = u.HP.ToString() + "%";
            }
            else
            {
                userHealth.Text = u.HP.ToString() + "%";
            }
        }

        private void Dead(UserInfo u)
        {
            logEvent( $"User {u.Name} is killed");
            if(u.Name == "Enemy")
            {
                MessageBox.Show("You Won!");
            }
            else
            {
                MessageBox.Show("You lose!");
            }
            StartGame();
        }

        private void attack(UserFighter attacking, BodyPart attackPart, UserFighter deffender, BodyPart defPart)
        {
            
            logEvent($"Round {round}: ");
            logEvent($"User {attacking.Name} attacks");
            logEvent($"User {deffender.Name} deffends");
            deffender.SetBlock(defPart);
            deffender.GetHit(attackPart);
            
            round++;
            if (round % 2 == 0)
            {
                command.Image = Image.FromFile("defence.png");
            }
            else
            {
                command.Image = Image.FromFile("attack.png");
            }
        }

        private void logEvent(string log)
        {
            eventRecords.Text += log + Environment.NewLine;
        }
    }
}
