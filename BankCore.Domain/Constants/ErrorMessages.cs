using System;
using System.Collections.Generic;
using System.Text;

namespace BankCore.Domain.Constants;

public static class ErrorMessages
{
     //müşteri için
    public const string CustomerNotFound = "Belirtilen müşteri bulunamadı.";
    public const string EmailAlreadyInUse = "Bu e-posta adresi zaten kullanılıyor.";

    //hesap için
    public const string AccountNotFound = "Hesap bulunamadı.";
    public const string SourceAccountNotFound = "Kaynak hesap bulunamadı.";
    public const string TargetAccountNotFound = "Hedef hesap bulunamadı.";
    public const string InactiveAccountOperation = "Pasif hesaba işlem yapılamaz.";
    public const string InsufficientBalance = "Bakiye yetersizdir.";
    public const string InactiveAccount= "Hesap aktif değildir.";
    public const string AccountNumberCannotBeEmpty = "Hesap numarası boş olamaz.";

    //kimlik doğrulama
    public const string InvalidLoginCredentials = "E-posta veya şifre hatalı.";
    public const string UserCreationFailed = "Kullanıcı oluşturulamadı: {0}";
    public const string AccessDenied = "Bu işleme erişim yetkiniz bulunmamaktadır.";
}
