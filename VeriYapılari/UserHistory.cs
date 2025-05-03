using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriYapılari
{
    internal class UserHistory
    {
        // Listenin ilk elemanı (head)
        private HistoryNode head;

        // Tarihçeye yeni kayıt ekleme
        public void AddAction(string action)
        {
            // Yeni düğüm oluştur
            HistoryNode newNode = new HistoryNode(action);

            // Eğer liste boşsa, yeni düğümü head yap
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                // Listenin sonuna git
                HistoryNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                // Yeni düğümü sona ekle
                current.Next = newNode;
            }
        }
        public void PrintLast5Actions()
        {
            HistoryNode current = head;
            List<string> lastActions = new List<string>();

            // Tüm kayıtları listeye aktar
            while (current != null)
            {
                lastActions.Add(current.Action);
                current = current.Next;
            }

            // Son 5 kaydı al (veya daha az)
            int startIndex = Math.Max(0, lastActions.Count - 5);
            int count = Math.Min(5, lastActions.Count);

            Console.WriteLine("=== Son 5 İşlem ===");
            for (int i = startIndex; i < startIndex + count; i++)
            {
                Console.WriteLine($"{i + 1}. {lastActions[i]}");
            }
        }

        // Tüm tarihçeyi ekrana yazdır
        public void PrintHistory()
        {
            HistoryNode current = head;
            int counter = 1;

            Console.WriteLine("=== Kullanıcı Geçmişi ===");
            if (current == null)
            {
                Console.WriteLine("Gösterilecek kayıt yok");
            }
            else
            {
                while (current != null)
                {
                    Console.WriteLine($"{counter}. {current.Action}");
                    current = current.Next;
                    counter++;
                }
            }
        }
    }
}
