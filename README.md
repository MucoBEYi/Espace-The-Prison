DÜZENLENME AŞAMASINDA:
ParticleSystem kodları yapıldı fakat inspectörden görsel ayarlamaları iyileştirilmesi gerekiyor(Level Dizayncıyı kim belirlediyseniz o başlasın şimdiden)
yapman gereken tek şey: prefabs klasöründeki blue ve red particle objelerinin görünümünü daha güzel hale getirmek(yardım için devoloperleri etiketleyebilirsin)

UI düzenleniyor(A.Ş ilgileniyor).

başlat butonuna basmak yerine durdur butonuna basınca çıkan seçenek ile oyunu başlatabiliyor(A.Ş ilgileniyor)

hareket komutu tekrar yazılması gerekiyor(ray sistemine olan bağlılığı kaldırılmalı) (enes halledecek).

karakter çoğaltma ve silme sistemi pool sistemine geçirilmesi(MÜCAHİT DENİYOR).



BİLİNEN SORUNLAR(aklınıza gelenleri buraya yazınız):

oyunu başlatma, finish, kazanma, kaybetme, obsctacle(engel) bulunmamakta. (GÜNCELLEME: SCRİPTLERİ NEREDEYSE BİTTİ UI KALDI VE OBSTACLE MODELLERİ EKSİK)

düşmanlar koşma animasyonu çalışana kadar ölüyor(hareket komutu güncellendikten sonra düzeltilecek).

Ses eklenecek(Onur Çiçek bu görev senindir)

FormatStickMan metodu çok fazla güç harcıyor.

karakter ve düşmanın üstündeki textler iç içe giriyor(önerisi olan?)

çıkarma işlemi gerçekleştiren gates(geçitler) çalışmıyor(pool sistemine geçiş yapılınca düzeltilmesi bekleniyor).



DÜZELDİ(UMARIM):

karakterin çoğalma hızı yavaş, bunu hızlandırmak gerekiyor. (MÜCAHİT DÜZELTTİ)

karakterin rotasyonu savaş bittikten sonra sıfırlanmıyor, bunu düzeltilmesi gerek.    (MÜCAHİT DÜZELTTİ, KOD SATIRI PLAYERMANAGER SCRİPTİNE TAŞINDI)

karakter/düşman karakter öldüğünde üstündeki text güncellenmiyor. (MÜCAHİT DÜZELTTİ)

gates(geçitler)den 2 veya daha fazla geçince olması gerekenden daha fazla karakter oluşturuyor. (MÜCAHİT DÜZELTTİ)

oyunu düşmanlar kazandığında:
düşmanların animasyonu idle animasyonuna dönsün(bunun metodu enemyManager scriptinde yapılmalı). (MÜCAHİT DÜZELTTİ)

oyuncu karakterin ve düşman üstündeki text, savaş sonrası hatalı konumlanma (MÜCAHİT DÜZELTTİ)

obstacleye çarpınca particleEfect çalışacak(MÜCAHİT EKLEDİ)



