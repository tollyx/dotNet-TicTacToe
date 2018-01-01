using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace TicTacToe {
  public partial class Game : Form {
    Board board;
    uint turncount = 0;
    Image cross;
    Image circle;
    bool gameover = false;

    public Game() {
      InitializeComponent();

      foreach (string name in Assembly.GetExecutingAssembly().GetManifestResourceNames()) {
        if (name.EndsWith("circle.png", StringComparison.InvariantCultureIgnoreCase)) {
          using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name)) {
            circle = Image.FromStream(stream);
          }
        }
        if (name.EndsWith("cross.png", StringComparison.InvariantCultureIgnoreCase)) {
          using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name)) {
            cross = Image.FromStream(stream);
          }
        }
      }

      newGame();
    }

    private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

    }

    private void pictureBox00_Click(object sender, EventArgs e) {
      playerTurn(0, 0);
      drawBoard();
    }

    private void pictureBox10_Click(object sender, EventArgs e) {
      playerTurn(1, 0);
      drawBoard();
    }

    private void pictureBox20_Click(object sender, EventArgs e) {
      playerTurn(2, 0);
      drawBoard();
    }

    private void pictureBox01_Click(object sender, EventArgs e) {
      playerTurn(0, 1);
      drawBoard();
    }

    private void pictureBox11_Click(object sender, EventArgs e) {
      playerTurn(1, 1);
      drawBoard();
    }

    private void pictureBox21_Click(object sender, EventArgs e) {
      playerTurn(2, 1);
      drawBoard();
    }

    private void pictureBox02_Click(object sender, EventArgs e) {
      playerTurn(0, 2);
      drawBoard();
    }

    private void pictureBox12_Click(object sender, EventArgs e) {
      playerTurn(1, 2);
      drawBoard();
    }

    private void pictureBox22_Click(object sender, EventArgs e) {
      playerTurn(2, 2);
      drawBoard();
    }

    public void newGame() {
      board = new Board();
      drawBoard();
      label1.Text = "Current turn: " + BoardState.Cross.ToString();
      gameover = false;
      turncount = 0;
    }

    public Image getImage(int x, int y) {
      switch (board.getTile(x,y)) {
        case BoardState.Cross:
          return cross;
        case BoardState.Circle:
          return circle;
        default:
          return null;
      }
    }

    public BoardState currentTurn() {
      switch (turncount % 2) {
        case 0:
          return BoardState.Cross;
        case 1:
          return BoardState.Circle;
        default:
          return BoardState.None;
      }
    }

    public void drawBoard() {
      pictureBox00.Image = getImage(0, 0);
      pictureBox01.Image = getImage(0, 1);
      pictureBox02.Image = getImage(0, 2);
      pictureBox10.Image = getImage(1, 0);
      pictureBox11.Image = getImage(1, 1);
      pictureBox12.Image = getImage(1, 2);
      pictureBox20.Image = getImage(2, 0);
      pictureBox21.Image = getImage(2, 1);
      pictureBox22.Image = getImage(2, 2);
    }

    public bool hasWon(BoardState player) {
      for (int ix = 0; ix < 3; ix++) {
        for (int iy = 0; iy < 3; iy++) {
          if (
            // Horizontal
            (board.getTile(ix, iy) == player &&         
            board.getTile(ix + 1, iy) == player &&
            board.getTile(ix + 2, iy) == player) ||

            // Vertical
            (board.getTile(ix, iy) == player &&         
            board.getTile(ix, iy + 1) == player &&
            board.getTile(ix, iy + 2) == player) ||

            // Top-left to bottom-right diagonal
            (board.getTile(ix, iy) == player &&         
            board.getTile(ix + 1, iy + 1) == player &&
            board.getTile(ix + 2, iy + 2) == player) ||

            // Bottom-left to top-right diagonal
            (board.getTile(ix, iy) == player &&         
            board.getTile(ix + 1, iy - 1) == player &&
            board.getTile(ix + 2, iy - 2) == player)
            ) {
            return true;
          }
        }
      }
      return false;
    }

    public void playerTurn(int x, int y) {
      if (gameover) {
        newGame();
        return;
      }
      if (board.getTile(x,y) != BoardState.None) {
        return;
      }
      
      BoardState player = currentTurn();
      board.setTile(x, y, player);

      turncount++;
      if (hasWon(player)) {
        label1.Text = player.ToString() + " won!";
        gameover = true;
      }
      else if (turncount >= 9) {
        label1.Text = "It's a tie!";
        gameover = true;
      }
      else {
        label1.Text = "Current turn: " + currentTurn().ToString();
      }
    }
  }
}
