using System;
using System.Collections.Generic;
using System.Text;

namespace BankCore.Domain.Constants;

public static class ValidationMessages
{
    public const string FullNameRequired = "Ad soyad boş bırakılamaz.";
    public const string FullNameTooLong = "Ad soyad 100 karakterden uzun olamaz.";
    public const string NationalIdRequired = "TC Kimlik No boş bırakılamaz.";
    public const string NationalIdLength = "TC Kimlik No 11 haneli olmalıdır.";
    public const string NationalIdDigitsOnly = "TC Kimlik No yalnızca rakamlardan oluşmalıdır.";
    public const string EmailRequired = "E-posta boş bırakılamaz.";
    public const string EmailInvalid = "Geçerli bir e-posta adresi giriniz.";
    public const string PasswordRequired = "Şifre boş bırakılamaz.";
    public const string PasswordTooShort = "Şifre en az 6 karakter olmalıdır.";
    public const string AccountIdRequired = "Hesap seçilmelidir.";
    public const string AccountTypeInvalid = "Geçersiz hesap türü.";
    public const string AmountMustBePositive = "Tutar sıfırdan büyük olmalıdır.";
    public const string CurrencyRequired = "Para birimi belirtilmelidir.";
    public const string CurrencyLength = "Para birimi 3 karakter olmalıdır (örn. TRY).";
    public const string SourceAccountRequired = "Kaynak hesap seçilmelidir.";
    public const string TargetAccountRequired = "Hedef hesap seçilmelidir.";
    public const string SourceTargetSameAccount = "Kaynak ve hedef hesap aynı olamaz.";
    public const string DontUseDifferentCurrency = "Farklı para birimleri işleme alınamaz";
    public const string SubtractionOperationCannotBeNegative = "Çıkarma işlemi sonucu negatif olamaz.";
    public const string InvalidTransactionAmount = "Geçersiz İşlem Tutarı";
}
