using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Logic.LevelGeneration
{

    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int minBlocksToTurn = 10;
        [SerializeField] private int maxBlocksToTurn = 15;
        [Space(10)]
        [SerializeField] private BlockPool pool;
        [SerializeField] private int levelLenght;
        
        private Direction _forward;
        private Direction _left;
        private Direction _right;
        private Direction _firstLeft;
        private Direction _firstRight;
        
        private void Awake()
        {
            pool.Initialize();
            InitializeDirections();
            GenerateNewLevel();
        }

        private void GenerateNewLevel()
        {
            pool.Reset();
            GenerateLevel(levelLenght);
        }

        private void InitializeDirections()
        {
            _forward = new Direction(new Vector3(0, 0, 20), new Vector3(0, 0, 0));
            _left = new Direction(new Vector3(-20, 0, 0), new Vector3(0, 0, 0));
            _right = new Direction(new Vector3(20, 0, 0), new Vector3(0, 0, 0));
            _firstLeft = new Direction(new Vector3(-10, 0, 10), new Vector3(0, -90, 0));
            _firstRight = new Direction(new Vector3(10, 0, 10), new Vector3(0, 90, 0));
        }

        private void GenerateLevel(int lenght)
        {
            Direction currentDirection = _forward;
            
            Vector3 currentPosition = Vector3.zero;
            Vector3 currentRotation = Vector3.zero;
            
            Turn currentTurn = Turn.None;
            int blockToTurn = Random.Range(minBlocksToTurn, maxBlocksToTurn);
            
            for (int i = 0; i < lenght; i++)
            {
                if (blockToTurn <= 0)
                {
                    if (currentTurn == Turn.None)
                    {
                        bool isRightTurn = Random.Range(0, 1f) >= 0.5f;
                        currentTurn = isRightTurn ? Turn.Left : Turn.Right;
                        if (currentTurn == Turn.Left)
                        {
                            currentTurn = TurnRight(_firstRight, ref currentPosition, ref currentRotation);
                            currentDirection = _right;
                        }
                        else
                        {
                            currentTurn = TurnLeft(_firstLeft, ref currentPosition, ref currentRotation);
                            currentDirection = _left;
                        }
                    }
                    else
                    {
                        if (currentTurn == Turn.Left)
                        {
                            TurnRight(_firstRight, ref currentPosition, ref currentRotation);
                            currentPosition += currentDirection.Vector;
                        }
                        else if (currentTurn == Turn.Right)
                        {
                            TurnLeft(_firstLeft, ref currentPosition, ref currentRotation);
                            currentPosition += currentDirection.Vector;
                        }

                        currentTurn = Turn.None;
                        currentDirection = _forward;
                    }
                    blockToTurn = Random.Range(minBlocksToTurn, maxBlocksToTurn);
                }
                else
                {
                    pool.TryGetDefault(out GameObject turnBlock);
                    PlaceBlock(turnBlock, currentDirection, ref currentPosition, ref currentRotation);
                }
                blockToTurn--;
            }
        }

        private Turn TurnRight(Direction direction, ref Vector3 currentPosition, ref Vector3 currentRotation)
        {
            pool.TryGetRightTurn(out GameObject turnBlock);
            PlaceBlock(turnBlock, direction, ref currentPosition, ref currentRotation);
            return Turn.Right;
        }

        private Turn TurnLeft(Direction direction, ref Vector3 currentPosition, ref Vector3 currentRotation)
        {
            pool.TryGetLeftTurn(out GameObject turnBlock);
            PlaceBlock(turnBlock, direction, ref currentPosition, ref currentRotation);
            return Turn.Left;
        }

        private void PlaceBlock(GameObject block, Direction direction, ref Vector3 currentPosition, ref Vector3 currentRotation)
        {
            block.transform.position = currentPosition;
            block.transform.eulerAngles = currentRotation;
            block.gameObject.SetActive(true);
            currentPosition += direction.Vector;
            currentRotation += direction.Rotation;
        }
    }

}