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
            new Point(5, 0), new Point(0, 5),
            new Point(-5, 0), new Point(0, -5) };

        public Enemy(int posX, int posY, int width, int height, string name)
            : base(posX, posY, new Size(width, height), name)
        {
        }

        public void MoveTo(Point target, Map map)
        {
            if (target == new Point(PosX, PosY))
                map.Lose = true;
            var pathToTarget = FindPaths(new Point(PosX, PosY), target, map)?.Reverse().ToList();
            if (pathToTarget != null)
            {
                var pathInDir = ParseToDirection(pathToTarget).FirstOrDefault();
                PosX += pathInDir.X;
                PosY += pathInDir.Y;
            }
        }

        private SinglyLinkedList<Point> FindPaths(Point start, Point target, Map map)
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
                if (Map.IsCollide(this, point))
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
            public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
            {
                Value = value;
                Previous = previous;
            }

            public T Value { get; }
            public SinglyLinkedList<T> Previous { get; }

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
