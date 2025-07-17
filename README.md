# WebAPI Code Review Test

Bu proje basit bir ASP.NET Core WebAPI uygulamasıdır. Kod incelemesi ve test amaçları için oluşturulmuştur.

## Özellikler

Proje aşağıdaki 3 endpoint'i içerir:
- `GET /products` - Örnek ürün listesi döndürür
- `GET /users` - Örnek kullanıcı listesi döndürür  
- `GET /status` - Servis durumu bilgisi döndürür

Tüm endpoint'ler sabit/fake veriler döndürür.

## Gereksinimler

- .NET 8.0 SDK

## Nasıl Çalıştırılır

1. Projeyi klonlayın veya indirin
2. Proje klasörüne gidin
3. Uygulamayı başlatın:
   ```bash
   dotnet run
   ```
4. Uygulama http://localhost:5096 adresinde çalışacaktır

## API Endpoint'lerini Test Etme

### Manuel Test
```bash
# Ürünleri listele
curl http://localhost:5096/products

# Kullanıcıları listele  
curl http://localhost:5096/users

# Servis durumunu kontrol et
curl http://localhost:5096/status
```

### Swagger UI
Uygulama çalışırken Swagger UI'a aşağıdaki adresten erişebilirsiniz:
http://localhost:5096/swagger

### HTTP Dosyası
Proje kökündeki `WebApiCodeReviewTest.http` dosyasını kullanarak Visual Studio Code veya JetBrains IDE'lerinde endpoint'leri test edebilirsiniz.

## Proje Yapısı

- `Program.cs` - Ana uygulama dosyası, tüm endpoint'ler ve veri modelleri bu dosyada tanımlı
- `WebApiCodeReviewTest.csproj` - Proje dosyası
- `WebApiCodeReviewTest.http` - HTTP test dosyası
- `appsettings.json` - Uygulama ayarları