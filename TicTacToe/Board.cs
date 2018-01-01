using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe {
  public enum BoardState {
    Cross,
    Circle,
    None,
  }

  class Board {
    private BoardState[] grid;

    public Board() {
      grid = Enumerable.Repeat(BoardState.None, 9).ToArray();
    }

    public void setTile(int x, int y, BoardState state) {
      if (x >= 0 && x < 3 && y >= 0 && y < 3) {
        grid[x + y * 3] = state;
      }
    }

    public BoardState getTile(int x, int y) {
      if (x >= 0 && x < 3 && y >= 0 && y < 3) {
        return grid[x + y * 3];
      }
      return BoardState.None;
    }
  }
}
