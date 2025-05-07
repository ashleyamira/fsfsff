using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Final_Project.View;

namespace Final_Project
{
    public partial class Form1 : Form, IGameView
    {
        private GamePresenter presenter;
        private List<Button> playerButtons;
        private List<Button> enemyButtons;

        public Form1()
        {
            InitializeComponent();

            playerButtons = new List<Button> { w1, w2, w3, w4, x1, x2, x3, x4, y1, y2, y3, y4, z1, z2, z3, z4 };
            enemyButtons = new List<Button> { a1, a2, a3, a4, b1, b2, b3, b4, c1, c2, c3, c4, d1, d2, d3, d4 };

            presenter = new GamePresenter(this, playerButtons, enemyButtons);

            foreach (var btn in playerButtons)
                btn.Click += PlayerPositionButton_Click;

            foreach (var btn in enemyButtons)
                btn.Click += EnemyTile_Click;

            btnMysteryBox.Click += BtnMysteryBox_Click;
            btnBomb.Click += BtnBomb_Click;
        }

        private void PlayerPositionButton_Click(object sender, EventArgs e)
        {
            presenter.OnPlayerPositionButtonClick(sender as Button);
        }

        private void EnemyTile_Click(object sender, EventArgs e)
        {
            presenter.OnEnemyTileClick(sender as Button);
        }

        private void BtnMysteryBox_Click(object sender, EventArgs e)
        {
            presenter.OnMysteryBoxClick();
            btnMysteryBox.Enabled = false;
        }

        private void BtnBomb_Click(object sender, EventArgs e)
        {
            presenter.OnBombButtonClick();
        }

        #region IGameView Implementation

        public void UpdatePlayerScore(int score)
        {
            txtPlayer.Text = score.ToString();
        }

        public void UpdateEnemyScore(int score)
        {
            txtEnemy.Text = score.ToString();
        }

        public void UpdateRound(int round)
        {
            txtRounds.Text = "Round: " + round;
        }

        public void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title);
        }

        public void SetButtonEnabled(Button btn, bool enabled)
        {
            btn.Enabled = enabled;
        }

        public void SetButtonBackground(Button btn, Image image)
        {
            btn.BackgroundImage = image;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void SetButtonBackColor(Button btn, Color color)
        {
            btn.BackColor = color;
        }

        public void RestartGameUI()
        {
            foreach (var btn in playerButtons)
            {
                btn.Enabled = true;
                btn.Tag = null;
                btn.BackColor = Color.White;
                btn.BackgroundImage = null;
            }

            foreach (var btn in enemyButtons)
            {
                btn.Enabled = true;
                btn.Tag = null;
                btn.BackColor = Color.White;
                btn.BackgroundImage = null;
            }

            btnMysteryBox.Enabled = true;
            enemyMove.Text = "AI";
        }

        public void SetEnemyMoveText(string text)
        {
            enemyMove.Text = text;
        }

        public void RevealEnemyTile(Button btn)
        {
            btn.BackColor = Color.Gray;
            btn.Enabled = false;
        }

        #endregion
    }
}
