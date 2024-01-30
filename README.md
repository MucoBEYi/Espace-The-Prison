DÜZENLENME AŞAMASINDA:

ParticleSystem kodları yapıldı fakat inspectörden görsel ayarlamaları iyileştirilmesi gerekiyor(Level Dizayncıyı kim belirlediyseniz o başlasın şimdiden)
yapman gereken tek şey: prefabs klasöründeki blue ve red particle objelerinin görünümünü daha güzel hale getirmek(yardım için devoloperleri etiketleyebilirsin)

UI düzenleniyor(DÜZENLENİYOR, E.G).

Ses eklenecek(DÜZENLENİYOR, A.Ş)

oyunu başlatma, finish, kazanma, kaybetme, obsctacle (DÜZENLENİYOR, E.G, A.Ş)

çıkarma işlemi gerçekleştiren gates(geçitler) çalışmıyor(pool sistemine geçiş yapılınca düzeltilmesi bekleniyor). (DÜZENLENİYOR, MÜCAHİT)

ParticleSystem pooling sistemine geçirilmeli(DÜZENLENİYOR, MÜCAHİT)

**********************************

BİLİNEN SORUNLAR(aklınıza gelenleri buraya yazınız):

geçitlerden geçince ortadan karakter spawnlanıyor. 

karakterleri ienumerator ile yapmaya çalışınca oyun çok kasıyor(voide çevrildi, çözüm bulunursa uygulanacak)

karakter ve düşmanın üstündeki textler iç içe giriyor(önerisi olan?)

BÜYÜK PROBLEMLİ OLAN SORUNLAR:

oyun genel olarak kasıyor, optimizasyon yapılmalı(ENES beline kuvvet :D)

karakterlerimizin sayısını arttırmak için kullandığımız liste çok nadir de olsa 1 tanesini eklemiyor(bazen de 1 tane fazla bırakıyor).

FormatStickMan metodu çok fazla güç harcıyor.

**********************************

DÜZELDİ(UMARIM):

Savaşlarda ve obstacle ile etkileşime girildiğinde consolede bazen hatalar çıkıyor. (MÜCAHİT DÜZELTTİ, SEBEBİ "PlayerManager" SCRİPTİNDEKİ GAMEOBJE LİSTESİNDEYDİ).

karakter çoğaltma ve silme sistemi pool sistemine geçirilmesi(MÜCAHİT EKLEDİ).

karakterin çoğalma hızı yavaş, bunu hızlandırmak gerekiyor. (MÜCAHİT DÜZELTTİ)

karakterin rotasyonu savaş bittikten sonra sıfırlanmıyor, bunu düzeltilmesi gerek.    (MÜCAHİT DÜZELTTİ, KOD SATIRI PLAYERMANAGER SCRİPTİNE TAŞINDI)

karakter/düşman karakter öldüğünde üstündeki text güncellenmiyor. (MÜCAHİT DÜZELTTİ)

gates(geçitler)den 2 veya daha fazla geçince olması gerekenden daha fazla karakter oluşturuyor. (MÜCAHİT DÜZELTTİ)

oyunu düşmanlar kazandığında:
düşmanların animasyonu idle animasyonuna dönsün(bunun metodu enemyManager scriptinde yapılmalı). (MÜCAHİT DÜZELTTİ)

oyuncu karakterin ve düşman üstündeki text, savaş sonrası hatalı konumlanma (MÜCAHİT DÜZELTTİ)

obstacleye çarpınca particleEfect çalışacak(MÜCAHİT EKLEDİ)

düşmanlar koşma animasyonu çalışana kadar ölüyor (DÜZELDİ).

hareket komutu tekrar yazılması gerekiyor (ENES DÜZELTTİ).

başlat butonuna basmak yerine durdur butonuna basınca çıkan seçenek ile oyunu başlatabiliyor(ENES DÜZELTTİ)
