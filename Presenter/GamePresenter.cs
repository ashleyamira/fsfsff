using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Final_Project.View;

namespace Final_Project
{
    public class GamePresenter
    {
        private readonly IGameView view;
        private readonly GameModel model;
        private readonly Timer enemyPlayTimer;

        public GamePresenter(IGameView view, List<Button> playerButtons, List<Button> enemyButtons)
        {
            this.view = view;
            this.model = new GameModel(playerButtons, enemyButtons);

            enemyPlayTimer = new Timer { Interval = 1000 };
            enemyPlayTimer.Tick += EnemyPlayTimer_Tick;

            RestartGame();
        }

        public void RestartGame()
        {
            enemyPlayTimer.Stop();
            model.RestartGame();
            view.RestartGameUI();
            UpdateScoresAndRounds();

            view.ShowMessage("Place your 3 ships by clicking on your tiles.", "Game Start");
        }

        private void UpdateScoresAndRounds()
        {
            view.UpdatePlayerScore(model.GetPlayerScore());
            view.UpdateEnemyScore(model.GetEnemyScore());
            view.UpdateRound(model.GetRound());
        }

        public void OnPlayerPositionButtonClick(Button btn)
        {
            if (!model.CanPlaceShip())
                return;

            model.PlacePlayerShip(btn);
            view.SetButtonEnabled(btn, false);
            view.SetButtonBackground(btn, Properties.Resources.boatImage);
            view.SetButtonBackColor(btn, Color.Orange);

            if (!model.CanPlaceShip())
                view.ShowMessage("You can now start attacking!", "Ready to Attack!");
        }

        public void OnEnemyTileClick(Button btn)
        {
            if (model.GetRound() <= 0 || model.CanPlaceShip() || !btn.Enabled)
                return;

            try
            {
                model.PlayerAttack(btn);
            }
            catch (Exception ex)
            {
                view.ShowMessage(ex.Message, "Bomb Activated");
                EndGame(false);
                return;
            }

            view.SetButtonEnabled(btn, false);

            if (btn.Tag != null && btn.Tag.ToString() == "enemyShip")
            {
                view.SetButtonBackground(btn, Properties.Resources.fireIcon);
                view.SetButtonBackColor(btn, Color.DarkBlue);
            }
            else
            {
                view.SetButtonBackground(btn, Properties.Resources.missIcon);
                view.SetButtonBackColor(btn, Color.DarkBlue);
            }

            UpdateScoresAndRounds();

            if (model.IsGameOver())
            {
                EndGame(model.GetPlayerScore() > model.GetEnemyScore());
                return;
            }

            enemyPlayTimer.Start();
        }

        private void EnemyPlayTimer_Tick(object sender, EventArgs e)
        {
            enemyPlayTimer.Stop();

            var targets = model.PlayerPositionButtons.Where(b => b.Enabled).ToList();
            if (!targets.Any() || model.GetRound() <= 0)
            {
                EndGame(model.GetPlayerScore() > model.GetEnemyScore());
                return;
            }

            var rand = new Random();
            var target = targets[rand.Next(targets.Count)];

            model.EnemyAttack(target);

            if (target.Tag != null && target.Tag.ToString() == "playerShip")
            {
                view.SetButtonBackground(target, Properties.Resources.fireIcon);
                view.SetButtonBackColor(target, Color.DarkBlue);
            }
            else
            {
                view.SetButtonBackground(target, Properties.Resources.missIcon);
                view.SetButtonBackColor(target, Color.DarkBlue);
            }

            view.SetButtonEnabled(target, false);
            view.SetEnemyMoveText(target.Text);

            UpdateScoresAndRounds();

            if (model.IsGameOver())
            {
                EndGame(model.GetPlayerScore() > model.GetEnemyScore());
            }
        }

        private void EndGame(bool playerWon)
        {
            if (playerWon)
                view.ShowMessage("You Win!!", "Victory");
            else
                view.ShowMessage("I sunk your battleship!", "Defeat");

            RestartGame();
        }

        public void OnMysteryBoxClick()
        {
            if (model.IsMysteryBoxUsed())
            {
                view.ShowMessage("Mystery Box already used!", "Warning");
                return;
            }

            model.UseMysteryBox(out int reward);

            switch (reward)
            {
                case 0:
                    view.ShowMessage("Bonus: +1 Point!", "Mystery Box");
                    break;
                case 1:
                    view.ShowMessage("Enemy tile revealed!", "Mystery Box");
                    RevealRandomEnemyTile();
                    break;
                case 2:
                    view.ShowMessage("Bad luck! You lost a round.", "Mystery Box");
                    break;
            }

            UpdateScoresAndRounds();
        }

        private void RevealRandomEnemyTile()
        {
            var hiddenTiles = model.EnemyPositionButtons.Where(b => b.Enabled).ToList();
            if (!hiddenTiles.Any()) return;

            var rand = new Random();
            var btn = hiddenTiles[rand.Next(hiddenTiles.Count)];

            view.RevealEnemyTile(btn);
        }

        public void OnBombButtonClick()
        {
            if (model.GetRound() <= 0 || model.CanPlaceShip())
                return;

            bool playerWon = model.GetEnemyScore() == 0;
            if (playerWon)
                view.ShowMessage("Enemy hit their own bomb. You win!", "Game Over");
            else
                view.ShowMessage("You hit your own bomb. You lose!", "Game Over");

            RestartGame();
        }
    }
}
