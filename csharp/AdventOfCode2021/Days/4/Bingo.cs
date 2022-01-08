using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
   internal class Bingo
   {
      private readonly Queue<ushort> _numbersToDraw;
      private readonly List<Board> _boards;

      public Bingo(ushort[] numbersToDraw, List<Board> boards)
      {
         _numbersToDraw = new Queue<ushort>(numbersToDraw);
         _boards = boards;
      }

      public int GetWinningScore()
      {
         while (_numbersToDraw.Count > 0)
         {
            var numberDrawn = _numbersToDraw.Dequeue();
            foreach(var board in _boards)
            {
               if (board.NumberDrawn(numberDrawn))
               {
                  return board.SumUnmatchedValues() * numberDrawn;
               }
            }
         }
         return 0;
      }

      public int GetLastWinningScore()
      {
         var lastWinningScore = 0;
         while (_numbersToDraw.Count > 0)
         {
            var numberDrawn = _numbersToDraw.Dequeue();
            
            foreach (var board in _boards)
            {
               if (!board.HasWon && board.NumberDrawn(numberDrawn))
               {
                  lastWinningScore = board.SumUnmatchedValues() * numberDrawn;
               }
            }
         }
         return lastWinningScore;
      }
   }

   internal struct BoardNumber
   {
      public ushort Value { get; set; }
      public bool IsMatched { get; set; }
   }

   internal class Board
   {
      internal static readonly int BOARD_SIZE = 5;
      private readonly BoardNumber[,] _board = new BoardNumber[BOARD_SIZE, BOARD_SIZE];

      internal bool HasWon { get; private set; }
      internal Board(string[] boardValues)
      {
         BuildBoard(boardValues);
      }

      internal bool NumberDrawn(ushort number)
      {
         if (HasWon)
         {
            // already won
            return false;
         }

         for (ushort rowIdx = 0; rowIdx < BOARD_SIZE; rowIdx++)
         {
            for (ushort colIdx = 0; colIdx < BOARD_SIZE; colIdx++)
            {
               if(number == _board[rowIdx, colIdx].Value)
               {
                  _board[rowIdx, colIdx].IsMatched = true;
                  return HasWon = HasWonAfterUpdate(rowIdx, colIdx);
                  
               }
            }
         }
         return false;
      }

      internal int SumUnmatchedValues()
      {
         var unmatchedSum = 0;
         foreach (var val in _board)
         {
            if (!val.IsMatched)
            {
               unmatchedSum += val.Value;
            }
         }
         return unmatchedSum;
      }

      private bool HasWonAfterUpdate(int rowIndex, int colIndex) 
         => HasCompleteRow(rowIndex) || HasCompleteColumn(colIndex);

      private bool HasCompleteRow(int rowIndex)
      {
         var hasCompleteRow = true;
         for (ushort colIdx = 0; colIdx < BOARD_SIZE; colIdx++)
         {
            hasCompleteRow &= _board[rowIndex, colIdx].IsMatched;
         }
         return hasCompleteRow;
      }

      private bool HasCompleteColumn(int colIndex)
      {
         var hasCompleteCol = true;
         for (ushort rowIdx = 0; rowIdx < BOARD_SIZE; rowIdx++)
         {
            hasCompleteCol &= _board[rowIdx, colIndex].IsMatched;
         }
         return hasCompleteCol;
      }


      private void BuildBoard(string[] boardRows)
      {
         if(boardRows?.Length != BOARD_SIZE)
         {
            throw new ArgumentException($"Board with not enough rows (got {boardRows?.Length}, expected {BOARD_SIZE})");
         }

         for (ushort rowIdx = 0; rowIdx < BOARD_SIZE; rowIdx++)
         {
            var values = boardRows[rowIdx]
               .Split(' ')
               .Where(s => !string.IsNullOrWhiteSpace(s))
               .Select(ushort.Parse)
               .ToArray();

            if (values.Length != BOARD_SIZE) 
            {
               throw new ArgumentException($"row {rowIdx} of Board doesn't have enough values (got {values?.Length}, expected {BOARD_SIZE})");
            }

            SetRowValues(rowIdx, values);
         }
      }

      private void SetRowValues(ushort rowIdx, ushort[] values)
      {
         for (ushort colIdx = 0; colIdx < BOARD_SIZE; colIdx++)
         {
            _board[rowIdx, colIdx] = new BoardNumber { Value = values[colIdx] };
         }
      }
   }
}
