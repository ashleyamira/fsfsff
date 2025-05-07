using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace Final_Project
{
    public class GameModel
    {
        public List<Button> PlayerPositionButtons { get; private set; }
        public List<Button> EnemyPositionButtons { get; private set; }

        public int TotalShips { get; private set; }
        public int Round { get; private set; }
        public int PlayerScore { get; private set; }
        public int EnemyScore { get; private set; }
        public bool MysteryBoxUsed { get; private set; }
        public Button PlayerBombTile { get; private set; }
        public Button EnemyBombTile { get; private set; }

        private Random rand;

        public GameModel(List<Button> playerButtons, List<Button> enemyButtons)
        {
            PlayerPositionButtons = playerButtons;
            EnemyPositionButtons = enemyButtons;
            rand = new Random();
            RestartGame();
        }

        public void RestartGame()
        {
            TotalShips = 3;
            Round = 15;
            PlayerScore = 0;
            EnemyScore = 0;
            MysteryBoxUsed = false;

            foreach (var btn in PlayerPositionButtons)
            {
                btn.Enabled = true;
                btn.Tag = null;
                btn.BackColor = Color.White;
                btn.BackgroundImage = null;
            }

            foreach (var btn in EnemyPositionButtons)
            {
                btn.Enabled = true;
                btn.Tag = null;
                btn.BackColor = Color.White;
                btn.BackgroundImage = null;
            }

            enemyLocationPicker();
            playerLocationPicker();
        }

        private void enemyLocationPicker()
        {
            for (int i = 0; i < 3; i++)
            {
                int index = rand.Next(EnemyPositionButtons.Count);
                if (EnemyPositionButtons[index].Enabled && EnemyPositionButtons[index].Tag == null)
                {
                    EnemyPositionButtons[index].Tag = "enemyShip";
                }
                else
                {
                    i--;
                }
            }

            while (true)
            {
                int bombIndex = rand.Next(EnemyPositionButtons.Count);
                if (EnemyPositionButtons[bombIndex].Tag == null)
                {
                    EnemyBombTile = EnemyPositionButtons[bombIndex];
                    EnemyBombTile.Tag = "enemyBomb";
                    break;
                }
            }
        }

        private void playerLocationPicker()
        {
            while (true)
            {
                int bombIndex = rand.Next(PlayerPositionButtons.Count);
                if (PlayerPositionButtons[bombIndex].Tag == null)
                {
                    PlayerBombTile = PlayerPositionButtons[bombIndex];
                    PlayerBombTile.BackgroundImage = Properties.Resources.bombIcon;
                    break;
                }
            }
        }

        public bool CanPlaceShip() => TotalShips > 0;

        public void PlacePlayerShip(Button button)
        {
            if (TotalShips <= 0)
                return;

            button.Tag = "playerShip";
            button.BackColor = Color.Orange;
            button.BackgroundImage = Properties.Resources.boatImage;
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.Enabled = false;
            TotalShips--;
        }

        public void PlayerAttack(Button btn)
        {
            Round--;
            if (btn == PlayerBombTile)
                throw new Exception("Game Over: You clicked your own bomb.");

            if (btn.Tag != null && btn.Tag.ToString() == "enemyBomb")
                throw new Exception("Game Over: You hit the enemy's bomb!");

            if (btn.Tag != null && btn.Tag.ToString() == "enemyShip")
                PlayerScore++;
        }

        public void EnemyAttack(Button btn)
        {
            Round--;
            if (btn.Tag != null && btn.Tag.ToString() == "playerShip")
                EnemyScore++;
        }

        public bool IsGameOver()
        {
            return Round < 1 || EnemyScore > 2 || PlayerScore > 2;
        }

        public int GetRound() => Round;
        public int GetPlayerScore() => PlayerScore;
        public int GetEnemyScore() => EnemyScore;
        public bool IsMysteryBoxUsed() => MysteryBoxUsed;

        public void UseMysteryBox(out int reward)
        {
            MysteryBoxUsed = true;
            reward = rand.Next(3);

            switch (reward)
            {
                case 0:
                    PlayerScore++;
                    break;
                case 1:
                    // Reveal enemy tile handled in presenter
                    break;
                case 2:
                    Round = Math.Max(Round - 1, 0);
                    break;
            }
        }
    }
}
