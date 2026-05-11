using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

namespace Solution
{
    public class ZombieParade : OOPEnemy
    {
        // �� LinkedList 㹡�èѴ�����ǹ�ͧ�����ͻ���Է���Ҿ㹡������/ź
        // �� LinkedList 㹡�èѴ�����ǹ�ͧ�����ͻ���Է���Ҿ㹡������/ź
        private LinkedList<GameObject> Parade = new LinkedList<GameObject>();
        public int SizeParade = 3;
        int timer = 0;
        public GameObject[] bodyPrefab; // Prefab �ͧ��ǹ�ӵ�ǧ�
        public float moveInterval = 0.5f; // ��ǧ����㹡������͹��� (0.5 �Թҷ�)

        private Vector3 moveDirection;

        public void Start()
        {
            moveDirection = Vector3.up;
            // ����� Coroutine ����Ѻ�������͹���
            positionX = (int)transform.position.x;
            positionY = (int)transform.position.y;
            StartCoroutine(MoveParade());
        }
        private Vector3 RandomizeDirection()
        {
            List<Vector3> possibleDirections = new List<Vector3>
            {
                Vector3.up,
                Vector3.down,
                Vector3.left,
                Vector3.right
            };

            return possibleDirections[Random.Range(0, possibleDirections.Count)];
        }
        // Coroutine ����Ѻ�������͹�����Ъ�ͧ
        IEnumerator MoveParade()
        {
            Parade.AddFirst(this.gameObject);
            //0. ���ҧ��ǧ�
            while (isAlive)
            {
                LinkedListNode<GameObject> firstNode = Parade.First;
                GameObject firstPart = firstNode.Value;

                LinkedListNode<GameObject> lastNode = Parade.Last;
                GameObject lastPart = lastNode.Value;

                Parade.RemoveLast();

                int toX = 0;
                int toY = 0;

                bool isCollide = true;
                int countTryFind = 0;

                while (isCollide == true || countTryFind > 10)
                {
                    moveDirection = RandomizeDirection();
                    toX = (int)(firstPart.transform.position.x + moveDirection.x);
                    toY = (int)(firstPart.transform.position.y + moveDirection.y);
                    countTryFind++;
                    if (countTryFind > 10)
                    {
                        toX = positionX;
                        toY = positionY;
                    }
                    isCollide = IsCollision(toX, toY);
                }

                positionX = toX;
                positionY = toY;

                lastPart.transform.position = new Vector3(positionX, positionY, 0);

                Parade.AddFirst(lastNode);

                if (Parade.Count < SizeParade) ;
                {
                    timer++;
                    if (timer > 3)
                    {
                        Grow();
                        timer = 0;
                    }
                }

                yield return new WaitForSeconds(moveInterval);
            }
        }
        private bool IsCollision(int x, int y)
        {
            // 4. ��Ǩ�ͺ��觡մ��ҧ
            if (HasPlacement(x, y))
            {
                return true;
            }
            return false;
        }

        // �ѧ��ѹ����Ѻ������ǹ�ͧ�� (Grow)
        private void Grow()
        {
            GameObject newPart = Instantiate(bodyPrefab[0]);

            GameObject lastPart = Parade.Last.Value;

            newPart.transform.position = lastPart.transform.position;

            Parade.AddLast(newPart);

        }

    }
}
