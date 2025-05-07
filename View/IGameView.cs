using System.Windows.Forms;

namespace Final_Project.View
{
    public interface IGameView
    {
        // UI Control Updates
        void SetButtonEnabled(Button btn, bool enabled);
        void SetButtonBackColor(Button btn, System.Drawing.Color color);
        void SetButtonBackground(Button btn, System.Drawing.Image image);

        // Score and Round Updates
        void UpdatePlayerScore(int score);
        void UpdateEnemyScore(int score);
        void UpdateRound(int round);

        // Game Feedback
        void ShowMessage(string message, string title);
        void SetEnemyMoveText(string text);

        // Resetting UI on Game Restart
        void RestartGameUI();

        // Mystery Box: Reveal hidden enemy tile
        void RevealEnemyTile(Button btn);
    }
}
