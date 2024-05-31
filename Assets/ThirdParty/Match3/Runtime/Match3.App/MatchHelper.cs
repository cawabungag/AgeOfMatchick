using System;
using System.Text;
using Match3.App.Interfaces;
using Match3.Core.Interfaces;
using UnityEngine;

namespace Match3.App
{
	public static class MatchHelper<TGridSlot> where TGridSlot : IGridSlot
	{
		public static (bool, Vector2Int[]) IsPotentialMatch(IGameBoard<TGridSlot> gameBoard)
		{
			int rowCount = gameBoard.RowCount;
			int colCount = gameBoard.ColumnCount;

			for (int row = 0; row < rowCount; row++)
			{
				for (int col = 0; col < colCount - 2; col++)
				{
					int current = gameBoard[row, col].ItemId;
					var itemId = gameBoard[row, col + 1].ItemId;
					var id = gameBoard[row, col + 2].ItemId;
					if (current == itemId && current == id)
					{
						return (true,
							new[]
							{
								new Vector2Int(row, col), new Vector2Int(row, col + 1),
								new Vector2Int(row, col + 2)
							});
					}
				}
			}

			for (int col = 0; col < colCount; col++)
			{
				for (int row = 0; row < rowCount - 2; row++)
				{
					int current = gameBoard[row, col].ItemId;
					if (current == gameBoard[row + 1, col].ItemId
						&& current == gameBoard[row + 2, col].ItemId)
					{
						return (true,
							new[]
							{
								new Vector2Int(row, col), new Vector2Int(row + 1, col),
								new Vector2Int(row + 2, col)
							});
					}
				}
			}

			return (false, new Vector2Int[] { });
		}

		public static string ConvertArrayToString(string[,] array)
		{
			StringBuilder sb = new StringBuilder();
			int rowCount = array.GetLength(0);
			int colCount = array.GetLength(1);

			sb.Append("{ ");
			for (int row = 0; row < rowCount; row++)
			{
				sb.Append("{ ");
				for (int col = 0; col < colCount; col++)
				{
					sb.Append(array[row, col]);

					if (col < colCount - 1)
					{
						sb.Append(", ");
					}
				}

				sb.Append(" }");

				if (row < rowCount - 1)
				{
					sb.AppendLine(",");
				}
			}

			sb.Append(" }");

			return sb.ToString();
		}

		public static (bool, Vector2Int[] vector2Ints) HasPotentialMatch(
			IGameBoard<TGridSlot> gameBoardOrig)
		{
			var gameBoard = gameBoardOrig.Items;
			int rowCount = gameBoardOrig.RowCount;
			int colCount = gameBoardOrig.ColumnCount;

			// Check for horizontal swaps
			for (int row = 0; row < rowCount; row++)
			{
				for (int col = 0; col < colCount - 1; col++)
				{
					Swap(gameBoard, row, col, row, col + 1);
					var (b, vector2Ints) = HasMatch(gameBoard);
					if (b)
					{
						foreach (var vector2 in vector2Ints)
						{
							Debug.LogError($"{ConvertArrayToString(gameBoardOrig.Items)}");
							gameBoardOrig[vector2.y, vector2.x].Item.DebugColor();
						}

						return (true, vector2Ints);
					}

					Swap(gameBoard, row, col, row, col + 1); // Swap back
				}
			}

			// Check for vertical swaps
			for (int row = 0; row < rowCount - 1; row++)
			{
				for (int col = 0; col < colCount; col++)
				{
					Swap(gameBoard, row, col, row + 1, col);
					var (b, vector2Ints) = HasMatch(gameBoard);
					if (b)
					{
						foreach (var vector2 in vector2Ints)
						{
							Debug.LogError($"{ConvertArrayToString(gameBoardOrig.Items)}");
							gameBoardOrig[vector2.y, vector2.x].Item.DebugColor();
						}

						return (true, vector2Ints);
					}

					Swap(gameBoard, row, col, row + 1, col); // Swap back
				}
			}

			return (false, new Vector2Int[] { });
		}

		private static void Swap(string[,] board, int row1, int col1, int row2, int col2)
		{
			(board[row1, col1], board[row2, col2]) = (board[row2, col2], board[row1, col1]);
		}

		private static (bool, Vector2Int[]) HasMatch(string[,] board)
		{
			int rowCount = board.GetLength(0);
			int colCount = board.GetLength(1);

			// Check for horizontal matches
			for (int row = 0; row < rowCount; row++)
			{
				for (int col = 0; col < colCount - 2; col++)
				{
					var current = board[row, col];
					if (current == board[row, col + 1] && string.Equals(current,
							board[row, col + 2], StringComparison.Ordinal))
					{
						return (true,
							new[]
							{
								new Vector2Int(col, row), new Vector2Int(col + 1, row),
								new Vector2Int(col + 2, row)
							});
					}
				}
			}

			// Check for vertical matches
			for (int col = 0; col < colCount; col++)
			{
				for (int row = 0; row < rowCount - 2; row++)
				{
					var current = board[row, col];
					if (current == board[row + 1, col] && string.Equals(current,
							board[row + 2, col], StringComparison.Ordinal))
					{
						return (true,
							new[]
							{
								new Vector2Int(col, row), new Vector2Int(col, row + 1),
								new Vector2Int(col, row + 2)
							});
					}
				}
			}

			return (false, new Vector2Int[] { });
		}

		public static bool HasPotentialMatchSrt(IGameBoard<TGridSlot> gameBoardOrig)
		{
			var board = gameBoardOrig.Items;
			
			int rowCount = board.GetLength(0);
			int colCount = board.GetLength(1);

			// Check for horizontal swaps
			for (int row = 0; row < rowCount; row++)
			{
				for (int col = 0; col < colCount - 1; col++)
				{
					SwapStr(board, row, col, row, col + 1);
					var hasMatchStr = HasMatchStr(board);
					if (hasMatchStr)
					{
						gameBoardOrig[row, col].Item.DebugColor();
						gameBoardOrig[row, col + 1].Item.DebugColor();
						gameBoardOrig[row, col + 2].Item.DebugColor();
						return true;
					}

					SwapStr(board, row, col, row, col + 1); // Swap back
				}
			}

			// Check for vertical swaps
			for (int row = 0; row < rowCount - 1; row++)
			{
				for (int col = 0; col < colCount; col++)
				{
					SwapStr(board, row, col, row + 1, col);
					var hasMatchStr = HasMatchStr(board);
					if (hasMatchStr)
					{
						gameBoardOrig[row, col].Item.DebugColor();
						gameBoardOrig[row + 1, col].Item.DebugColor();
						gameBoardOrig[row + 2, col].Item.DebugColor();
						return true;
					}

					SwapStr(board, row, col, row + 1, col); // Swap back
				}
			}

			return false;
		}

		static void SwapStr(string[,] board, int row1, int col1, int row2, int col2)
		{
			(board[row1, col1], board[row2, col2]) = (board[row2, col2], board[row1, col1]);
		}

		static bool HasMatchStr(string[,] board)
		{
			var rowCount = board.GetLength(0);
			var colCount = board.GetLength(1);

			for (int row = 0; row < rowCount; row++)
			{
				for (int col = 0; col < colCount - 2; col++)
				{
					var current = board[row, col];
					var itemId = board[row, col + 1];
					var id = board[row, col + 2];
					if (string.Equals(current, itemId, StringComparison.Ordinal) && string.Equals(
							current, id, StringComparison.Ordinal))
					{
						Debug.LogError($"HasMatchStr VERTICAL {row}:{col}|{row}:{col + 1}|{row}:{col + 2}");
						return true;
					}
				}
			}

			for (int col = 0; col < colCount; col++)
			{
				for (int row = 0; row < rowCount - 2; row++)
				{
					var current = board[row, col];
					if (string.Equals(current, board[row + 1, col], StringComparison.Ordinal)
						&& string.Equals(current, board[row + 2, col], StringComparison.Ordinal))
					{
						Debug.LogError($"HasMatchStr HORIZONTAL {row}:{col}|{row + 1}:{col}|{row + 2}:{col}");
						return true;
					}
				}
			}

			return false;
		}
	}
}