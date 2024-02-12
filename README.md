# Aqva Test Case Readme

Projede Python ve C# dillerini kullandim. Python ile Sozcu websitesinden verileri cekip ElasticSearch’e gonderiyorum. Net Core ile verileri ElasticSearch’ten cekip Razor Page kullanarak  listeliyorum. 

## Nasıl Çalışır?

### Python

python klasörüne gidip

```jsx
python3 aqva.py
```

kodunu yazmanız yeterlidir. Veriler EleasticSearch’e yuklenmiş olacaktır.

**Config.ini içerisinde ES Cloud Endpointi ve Api Keyi güncellemeniz gerekiyor.**

### Net Core

Net Core klasöründe AqvaCode projesini açmanız gerekiyor. 

**Çalıştırmadan önce appsettings.json içerisinde ES Cloud ID ve Api Keyi güncellemeniz gerekiyor.**

```jsx
dotnet run
```

komutu ile projeyi direkt çalıştırabilirsiniz.

Anasayfada tüm veriler listelenecektir. Arama inputu ile url veya title içerisinde arama yapabilirsiniz.

## Ekran Görüntüleri

### Anasayfa

![Screenshot 2024-02-12 at 15.44.24.png](https://i.imgur.com/bAr2pdj.png)

### Arama Sonucu

![Screenshot 2024-02-12 at 15.45.53.png](https://i.imgur.com/6JzZfk5.png)
