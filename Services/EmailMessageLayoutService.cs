using AquaServiceSPA.Models;
using System;

namespace AquaServiceSPA.Services
{
    public class EmailMessageLayoutService : IEmailMessageLayoutService
    {
        public string ContactModMessage(Email email)
            => $"<div><p><strong>date: </strong>{DateTime.Now}</p><p><strong>email address: </strong>{email.EmailAddress}</p><p><strong>user name: </strong>{email.Username}</p><p><strong>Subject: </strong>{email.Subject}</p><p><strong>Description: </strong>{email.Description}</p><p><strong>Message: </strong></p><div>{email.Message}</div></div>";

        public string ContactUserFeedback(string userName, string serviceName)
            => $"<div><p> Witamy {userName},</p><p>Twoja wiadomość została zarejestrowana w systemie. Niebawem udzielimy odpowiedzi. Dziękujemy za kontakt.</p><p>Pozdrawiamy serdecznie, załoga portalu {serviceName}.</p><p><small>To jest wiadomość automatyczna, prosimy na nią nie odpisywać.</small></p></div>";

        public string ConfirmEmailMessage(string serviceName, string confirmLink)
            => $"<div><p>Wiadomość z serwisu {serviceName}</p><a href=\"{confirmLink}\">Kliknij link aby dokończyć porces rejestracji</a><p>Jeżeli nie brałeś udziału w rejestacji zignoruj tą wiadomość</p><p><small>To jest wiadomość automatyczna, prosimy na nią nie odpisywać.</small></p></div>";

        public string ResetPasswordMessageOne(string serviceName, string confirmLink)
            => $"<div>Wiadomość z serwisu {serviceName}</div><div>Właśnie została uruchomiona procedura resetowania hasła</div><div>Jeżeli nie jesteś użytkownikiem serwisu {serviceName} zignoruj tą wiadomość</div><a href=\"{confirmLink}\">Kliknij ten link aby kontynuować proces resetowania hasła</a><p>Pozdrawiamy serdecznie, załoga portalu {serviceName}</p><div><small>To jest wiadomość automatyczna prosimy na nią nie odpisywać.</small></div>";

        public string ResetPasswordMessageTwo(string serviceName, string userName, string password, string callbackUrl)
            => $"<div><p>Wiadomość z portalu {serviceName}</p><p>Procedura resetowania hasła zakończona powodzeniem.</p><p>Poniżej podane są nowe dane logowania:</p><p><strong>Nazwa użytkownika: </strong>{userName}</p><p><strong>Hasło: </strong>{password}</p><p>Hasło możesz zmienić w sekcji: Panel użytkownika/Zmiana hasła.</p><a href=\"{callbackUrl}\">Kliknij aby się zalogować</a><p>Pozdrawiamy załoga portalu {serviceName}</p><p><small>To jest wiadommość automatyczna prosimy na nią nie odpisywać</small></p></div>";
    }
}
