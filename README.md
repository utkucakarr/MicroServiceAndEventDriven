# Microservices & Event-Driven Architecture with .NET 8
Bu depo, .NET 8 üzerinde Event-Driven Architecture yaklaşımıyla geliştirdiğim bir mikroservis uygulamasıdır. Temel amacım, Clean Architecture prensiplerini uygulayarak servisler arası asenkron iletişimi (RabbitMQ & MassTransit) pratik bir senaryoda göstermekti.

Projeyi lokalde uğraşmadan hızlıca ayağa kaldırabilmek için tüm altyapıyı Docker Compose ile konteynerize ettim.

## Kullanılan Teknolojiler
Platform: .NET 8, ASP.NET Core Web API
Mimari: Microservices, Event-Driven Architecture, Clean Architecture
Message Broker: RabbitMQ (MassTransit üzerinden)
Database: PostgreSQL, EF Core (Code-First)
DevOps / Ortam: Docker, Docker Compose
API Dokümantasyonu: Swagger

## Servislerin Yapısı
Sistem şu an için birbiriyle asenkron haberleşen iki temel servisten oluşuyor:
Order Service: Gelen sipariş isteklerini alır ve PostgreSQL'e kaydeder. İşlem başarılı olduğunda RabbitMQ ortamına bir OrderCreatedEvent mesajı bırakır (Publisher).
Inventory Service: Arka planda RabbitMQ'yu dinler. İlgili event kuyruğa düştüğü an bunu yakalar (Consumer) ve satılan ürünlerin stok bilgisini kendi veritabanında günceller.

## Kurulum ve Çalıştırma
Projeyi bilgisayarınızda çalıştırmak için Docker'ın kurulu olması yeterlidir.
Repoyu klonlayın ve dizine gidin:

## Bash
git clone https://github.com/kullanici-adiniz/MicroServiceAndEventDriven.git
cd MicroServiceAndEventDriven
Docker Compose ile tüm servisleri ve altyapıyı (PostgreSQL, RabbitMQ) ayağa kaldırın:

## Bash
docker-compose up -d --build
(Not: İlk kurulumda imajların indirilmesi ve build süreci internet hızınıza bağlı olarak birkaç dakika sürebilir.)

## Hızlı Test Adımları
Konteynerler çalışmaya başladıktan sonra akışı şu şekilde test edebilirsiniz:

http://localhost:5001/swagger adresine gidin ve Order API üzerinden yeni bir sipariş (POST) oluşturun.

İşlemden hemen sonra http://localhost:5002/swagger üzerinden Inventory API'yi (veya DB'yi) kontrol ederek, sipariş edilen ürünlerin stoklarının arka planda otomatik olarak düşüldüğünü görebilirsiniz.

RabbitMQ Arayüzü (Management):
Kuyruktaki mesajları arayüzden incelemek isterseniz http://localhost:15672 adresini kullanabilirsiniz.
(Giriş bilgileri yerel Docker ortamı için varsayılan değerlerdir: Kullanıcı: guest | Şifre: guest. Canlı ortamda bu bilgiler environment variables ile yönetilir.)
