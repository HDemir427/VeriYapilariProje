using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriYapılari
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Kullanıcı tarihçesi oluştur
            UserHistory userHistory = new UserHistory();

            // Örnek veriler ekle
            userHistory.AddAction("Ürün görüntülendi: Laptop");
            userHistory.AddAction("Arama yapıldı: Telefon");
            userHistory.AddAction("Sipariş verildi: Kablosuz Kulaklık");

            // Tarihçeyi göster
            userHistory.PrintHistory();

            Console.ReadLine(); // Konsolun kapanmaması için
        }
    }
}
