using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Collections;

namespace GetOut.Models
{
    public class Enemy : Entity
    {
        private readonly Point[] vectors = new Point[] {
            new Point(10, 0), new Point(0, 10),
            new Point(-10, 0), new Point(0, -10) };

        private int baseDir = 5;
        private int pause = 10;
        private int lenghtWay;

        public Enemy(int posX, int posY, int width, int height, string name)
            : base(posX, posY, new Size(width, height), name)
        {
            CurrentLimit = 7;
        }

        public int CurrentAnimation { get; private set; }
        public int CurrentLimit { get; private set; }
        public int CurrentFrame { get; set; }
        private bool flip;

        public void MoveTo(Point target, GameMap map)
        {
            if (target == new Point(PosX, PosY))
                map.Lose = true;
            var pathToTarget = FindPaths(new Point(PosX, PosY), target, map)?.Reverse().ToList();
            if (pathToTarget != null)
            {
                var pathInDir = ParseToDirection(pathToTarget).FirstOrDefault();
                if (pathInDir.X < 0)
                    flip = true;
                else if (pathInDir.X > 0)
                    flip = false;
                if (pathInDir.X != 0 || pathInDir.Y != 0)
                    SetAnimationConfiguration("Run");
                else
                    SetAnimationConfiguration("State");
                PosX += pathInDir.X;
                PosY += pathInDir.Y;
            }
            else
            {
                if (pause != 0)
                {
                    SetAnimationConfiguration("State");
                    pause--;
                }
                else
                {
                    PosX += baseDir;
                    lenghtWay += baseDir;
                    flip = baseDir < 0;
                    SetAnimationConfiguration("Run");
                    if (map.IsCollide(this, new Point(PosX + baseDir, PosY)) || Math.Abs(lenghtWay) >= 100)
                    {
                        pause = 10;
                        baseDir *= -1;
                    }
                }
            }
        }

        public void SetAnimationConfiguration(string animation)
        {
            if (animation == "State" && !flip)
                CurrentAnimation = 0;
            else if (animation == "State" && flip)
                CurrentAnimation = 1;
            else if (animation == "Run" && !flip)
                CurrentAnimation = 2;
            else if (animation == "Run" && flip)
                CurrentAnimation = 3;

            switch (animation)
            {
                case "State":
                    CurrentLimit = 7;
                    break;
                case "Run":
                    CurrentLimit = 7;
                    break;
                case "Deth":
                    CurrentLimit = 1;
                    break;
            }
        }

        private SinglyLinkedList<Point> FindPaths(Point start, Point target, GameMap map)
        {
            var queue = new Queue<Point>();
            var visited = new HashSet<Point>();
            var ways = new Dictionary<Point, SinglyLinkedList<Point>>();

            queue.Enqueue(start);
            visited.Add(start);
            ways[start] = new SinglyLinkedList<Point>(start);
            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                if (map.IsCollide(this, point) || ways[point].Length > 20)
                    continue;

                foreach (var vector in vectors)
                {
                    var nextPoint = new Point(point.X + vector.X, point.Y + vector.Y);
                    if (visited.Contains(nextPoint)) continue;
                    queue.Enqueue(nextPoint);
                    visited.Add(nextPoint);
                    ways[nextPoint] = new SinglyLinkedList<Point>(nextPoint, ways[point]);
                }

            }
            if (ways.ContainsKey(target))
            {
                return ways[target];
            }
            return null;
        }

        private List<Point> ParseToDirection(List<Point> points)
        {
            return points
                .Zip(points.Skip(1), (first, second) => new Point(second.X - first.X, second.Y - first.Y))
                .ToList();
        }

        internal class SinglyLinkedList<T> : IEnumerable<T>
        {
            public readonly T Value;
            public readonly SinglyLinkedList<T> Previous;
            public readonly int Length;

            public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
            {
                Value = value;
                Previous = previous;
                Length = previous?.Length + 1 ?? 1;
            }

            public IEnumerator<T> GetEnumerator()
            {
                yield return Value;
                var pathItem = Previous;
                while (pathItem != null)
                {
                    yield return pathItem.Value;
                    pathItem = pathItem.Previous;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
