# Online Alışveriş Platformu Prototipi
## Projenin Amacı
Bu proje, temel olarak ürün arama/ekleme, kategori filtreleme, sipariş yönetimi ve kullanıcı geçmişi gösterimi gibi işlevlere sahip bir e-ticaret platformu prototipi oluşturmaktadır. Kullanıcılar, sisteme ürün ekleyebilir, ürünleri ID veya anahtar kelime ile hızlıca arayabilir; farklı kategoriler altında ürünleri filtreleyebilir; siparişlerini sıraya ekleyip (FIFO) işleyebilir; ayrıca kullanıcıların işlem geçmişini görüntüleyebilir. Projenin amacı, gerçek bir online alışveriş platformunda yaygın olarak karşılaşılan bu işlemleri, uygun veri yapıları kullanarak verimli bir şekilde modellemektir. Bu sayede ürün kataloguna hızlı erişim, dinamik kategori yönetimi, sıralı sipariş işleme ve kullanıcı geçmişi takibi mümkün hale getirilmiştir.
## Grup Üyeleri
032390003  Yekta Ağra  
032390004  Emirhan Berat Beyoğlu  
032390005  Hazar Demir  
032390006  Furkan Akan  
032390009  Mustafa Pektaş  
## Kullanılan Veri Yapıları
### ***Hash Tablosu (Ürün Kataloğu)***  
  
Hash tablosu, anahtar-değer çiftlerini hızlıca saklamak ve aramak için ideal bir yapıdır. Ürün kataloğunda her bir ürünün benzersiz bir ID’si (anahtar) vardır. Bizim uygulamamızda `CustomHashTable<int, Product>` kullanılarak ürünler ID’ye göre bir sabit boyutlu diziye yerleştirilmiş ve aynı kovadaki çakışmalar bağlı liste ile zincirleme çözülmüştür. Bu yöntem, büyük ürün sayısında bile ortalama düzeyde sabit zamanlı arama/ekleme sağlar. Hash tablosu, diziye göre dinamik boyut ekleme imkânı ve ID’ye doğrudan erişim avantajı sunduğundan tercih edilmiştir.  
  
### ***Ağaç Yapısı (Kategori Filtreleme)***  

Kategori filtreleme için dengeli bir ikili arama ağacı (AVL ağacı) kullanılmıştır. AVL ağacı, sol ve sağ alt ağaçların yükseklik farkını en fazla 1 ile sınırlandırarak otomatik dengeleme yapar. Bizim uygulamamızda `CustomAvlTree<Category>` yapısı, kategorileri isimlerine göre sıralı biçimde tutar. Yeni kategori eklendiğinde veya bir ürün var olan bir kategoriye atandığında, ağaca uygun pozisyona ekleme yapılır ve gerekirse dengeleme işlemi yapılır. Bu yapı, kullanıcıların kategori adına göre ürün listesine hızlı erişim ve kategorilerde gezinti imkânı sunar.  

### ***Kuyruk (Sipariş Yönetimi)***  
  
Sipariş yönetimi için kuyruk (queue) yapısı kullanılmıştır. Bir kuyruk, en önce eklenen (first-in) öğenin önce işleneceği (first-out) bir FIFO veri yapısıdır. Yeni bir sipariş geldiğinde `Enqueue` işlemi ile kuyruğun sonuna eklenir; işleme alınan siparişler ise `Dequeue` ile kuyruğun başından alınır. `CustomQueue<Order>` sınıfımız, `LinkedList<Order>` ile kuyruk implementasyonu yapar. Kuyruk yapısı, siparişleri sıralı işlemek (örneğin sıraya alınan siparişleri işleme sırası ile ele almak) için idealdir.  

### ***Bağlı Liste (Kullanıcı Geçmişi)***  

Kullanıcıların işlem/gezinme geçmişi için bağlı liste (linked list) kullanılmıştır. Bağlı listede her bir işlem bir düğüm olarak tutulur ve düğümler, sırayla birbirine bağlanır. `CustomLinkedList<UserHistory>` sınıfı, .NET’in `LinkedList<T>` yapısını thread-safe şekilde sarmalar. Yeni bir kullanıcı geçmişi girdisi eklenirken `AddLast` ile listenin sonuna ekleme yapılır. Kullanıcı geçmişi listesi taranırken belirli kullanıcıya ait işlemler tek tek kontrol edilir. Bağlı liste, dinamik olarak artan geçmiş verisini yönetmek ve sırayla ekleme-silme yapmak için uygun bir seçimdir.

## Teknik İşleyiş  
* **Ürün Ekleme/Arama:** Yeni bir ürün eklendiğinde `ProductService.AddProduct` metodu çağrılır. Ürün önce hash tablosuna ID anahtarıyla eklenir (`_products.Add(product.ProductId, product)`). Ardından ürünün kategorisi AVL ağacında aranır. Eğer kategori zaten ağaca kayıtlıysa, o kategori nesnesinin ürün listesine eklenir; yoksa önce kategori AVL ağacına `Insert` ile eklenir, sonra ürün listeye konur. Ürün sorgulamada `GetProduct(id)` hash tablosundan ID ile O(1) zamanında alınır. Anahtar kelimeye göre aramada ise tüm ürünler (`_products.GetAll()`) filtrelenir; bu durumda her ürün tek tek incelenir (O(n) liste taraması). Bu iş akışında, hash tablosu ürünlere hızlı ID erişimi sağlarken, AVL ağacı kategori bazlı düzenli gruplamayı mümkün kılar.
* **Kategori Oluşturma:** Yeni kategori ekleme işlemi, `CategoryController` veya `ProductService` aracılığıyla AVL ağacına yapılır. Örneğin bir ürün farklı bir kategoriye kaydedilecekse, kategori ismiyle yeni bir `Category` nesnesi yaratılarak ağaca `Insert` edilir. AVL ağacı, dalarını dengede tutarak kategori ekleme işleminin O(log n) sürmesini garanti eder. Bu sayede çok sayıda kategori olsa bile, kategori ekleme ve arama işlemleri hızlı gerçekleşir.
* **Sipariş İşleme:** Kullanıcı bir sipariş verdiğinde `ProductService.ProcessOrder(order)` ile `Enqueue(order)` çağrısı yapılır. Siparişler `CustomQueue<Order>` nesnesine eklenir. Daha sonra sistem sıradaki siparişi işlerken `Dequeue()` ile kuyruğun başından sipariş alınır ve işleme alınır. Ekleme ve çıkarma işlemleri O(1) olarak yürütülür. Bu tasarım, siparişlerin alınış sırasına göre sırayla işlenmesini sağlar. Ayrıca kuyruk arayüzü işlenmeyi bekleyen siparişleri listelemek için de kullanılabilir.
* **Geçmiş Gösterimi:** Bir kullanıcı sisteme giriş yaptıkça veya alışveriş yaparken her işlem bir `UserHistory` nesnesine kaydedilir. `UserHistoryService.AddHistory` metodu ile bu nesne bağlı listenin sonuna eklenir (`_userHistories.AddLast(history)`). Kullanıcının geçmişi görüntülenmek istendiğinde `GetUserHistory(userId)` ile tüm liste taranarak ilgili `UserHistory` kayıtları toplanır (her eleman kontrol edilir, O(n) tarama). Bağlı liste burada kronolojik sıralı kayıt tutmayı ve hızlı eklemeyi sağlar. Kullanıcının geçmiş verilerine kolayca bakılması, alışveriş deneyimini zenginleştirir.
## Algoritma Analizi ve Performans Değerlendirmesi  
* **Hash Tablosu (ürün kataloğu):** Ekleme, arama, silme işlemleri ortalama O(1) zaman alır. Kötü durumda (tüm anahtarlar aynı kovada toplandığında) işlem süreleri O(n)’e yükselebilir. Hash fonksiyonu iyi seçilirse çakışmalar azaltılır ve pratikte sabit zamanlı performans elde edilir.
* **AVL Ağaç (kategori):** Dengeli ikili arama ağacı olduğu için hem arama hem ekleme hem de silme işlemleri O(log n) sürede gerçekleşir. Ortalama ve en kötü durum analizleri aynıdır (O(log n)) çünkü AVL ağacı yüksekliği her zaman logaritmik kalır.
* **Kuyruk (sipariş):** Kuyrukta ekleme (`enqueue`) ve çıkarma (`dequeue`) işlemleri O(1) zaman alır. (Arka uca ekleme ve önden çıkarma, kilit altında LinkedList ile yapıldığından sabit zamanlıdır.) Kuyrukta arama işlemi nadiren kullanılır; teorik olarak yapılırsa O(n) olur (tüm eleman taranır).
* **Bağlı Liste (kullanıcı geçmişi):** Listeye sonuna eleman ekleme O(1), başlangıç veya sonu biliniyorsa ekleme/silme O(1)’dir. Ancak rastgele bir öğe arama veya tüm listeyi tarama işlemi O(n)’dir (çünkü dizin yoktur). Kullanıcı geçmişi sorgulamada, ilgili kullanıcı kimliği listede tek tek kontrol edilir, bu yüzden O(n) zaman gerekir.

Özetle, hash tablosu ve AVL ağacı temel işlemlerde hızlı (ortalama O(1) / O(log n)) performans sağlarken; bağlı listeye özgü tarama işlemi veya hash çakışmaları gibi senaryolarda karmaşıklık O(n) olabilmektedir.  
## Alternatif Veri Yapıları ile Karşılaştırma  
* **Hash Tablo vs. Sabit Dizi veya Liste:** Sabit boyutlu bir dizi veya sıradan bir liste (array veya List<T>) kullanılsaydı, belli bir ID’li ürüne erişmek için lineer arama gerekir ve bu O(n) zaman alırdı. Oysa hash tablosunda anahtar ile doğrudan indeks hesaplanıp O(1) ortalama erişim sağlanır.
* **AVL Ağaç vs. Basit BST veya Dizi:** Kategoriler için basit bir sıralı liste kullanılsaydı, kategori ekleme O(n) sürebilir; ayrıca hiyerarşik arama zor olurdu. Dengeli olmayan bir BST kullanılsaydı, en kötü durumda (az dengeli eklemelerle) O(n) arama olurdu.
* **Kuyruk vs. Liste veya Yığın:** Siparişleri saklamak için dinamik bir liste (`List<Order>`) kullanılabilirdi; fakat List’te baştan silme (remove) O(n) zaman alır. Bağlı liste ile yapılmış bir kuyruk, dequeue işlemini O(1) tutar. Yığından farklı olarak kuyruk sıralama (FIFO) sağladığı için siparişlerde doğru işleme sırası garanti edilir.
* **Bağlı Liste vs. Dinamik Dizi veya Diğer Yapılar:** Kullanıcı geçmişi için bir dizi ya da `List<UserHistory>` kullanılsaydı, sonuna ekleme amortize O(1) yapılabilirdi ve index erişim O(1) olurdu. Ancak kullanıcı bazlı aramada her seferinde tüm liste taranacağı için yine O(n) olurdu.

## Projenin Kurulumu ve Çalıştırılması  
Proje C# diliyle geliştirilmiş ve .NET 8.0 çerçevesi (ASP.NET Core Web API) üzerine inşa edilmiştir. Çalıştırmak için şu adımlar izlenmelidir:  
1. Öncelikle bilgisayarda .NET 8.0 SDK ve tercihen bir IDE (Visual Studio 2022/2023 veya Visual Studio Code) yüklü olmalıdır. Ayrıca SQL Server veritabanı sunucusu veya LocalDB kullanılabilir.
2. `VeriYapilariProje.zip` dosyasını açıp proje dosyalarını bir dizine çıkarın. İçerisindeki `OrderManagementSystem.sln` çözüm dosyasını IDE ile açın.
3. `appsettings.json` dosyasında `ConnectionStrings:DefaultConnection` altında SQL Server bağlantı dizesi yer almaktadır.
4. Son olarak IDE’den uygulamayı çalıştırın veya komut satırında `dotnet run` ile uygulamayı başlatın. Uygulama bir Web API olarak ayağa kalkacak ve belirtilen port üzerinden dinlemeye başlayacaktır.

## Sonuç  
Bu projede geliştirilen modüller (ürün yönetimi, kategori yönetimi, sipariş yönetimi, kullanıcı geçmişi) özel veri yapıları kullanılarak etkili bir şekilde bir araya getirilmiştir. Her bir veri yapısı, ilgili işlevde performans avantajı sağlamıştır: Hash tablosu ürünlere hızlı ID erişimi; AVL ağacı kategorilerde hızlı hiyerarşik arama; kuyruk siparişlerin sıralı işlenmesi; bağlı liste ise dinamik kullanıcı geçmişi takibi sunmuştur. Performans analizlerinde görüldüğü üzere, bu yapılar sayesinde temel işlemler genellikle sabit veya logaritmik zaman karmaşıklığına sahip olmuştur.  
