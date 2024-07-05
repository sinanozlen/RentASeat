using DtoLayer.ReservationDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MailKit.Security;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Reservation")]
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ReservationController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Reservations/GetReservationWithLocationAndCar");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultReservationDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        [Route("ConfirmReservation/{id}")]
        public async Task<IActionResult> ConfirmReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Reservations/ChangeReservationStatusToConfirm?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                await SendReservationStatusEmail(id, "onaylandı");
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("RejectReservation/{id}")]
        public async Task<IActionResult> RejectReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Reservations/ChangeReservationStatusToDecline?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                await SendReservationStatusEmail(id, "reddedildi");
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        private async Task SendReservationStatusEmail(int reservationID, string status)
        {
            var reservation = await GetReservationDetails(reservationID);
            if (reservation == null) return;

            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var server = smtpSettings["Server"];
            var port = int.Parse(smtpSettings["Port"]);
            var username = smtpSettings["Username"];
            var password = smtpSettings["Password"];

            string statusMessage;

            if (status == "onaylandı")
            {
                statusMessage = "onaylandı";
            }
            else
            {
                statusMessage = "üzülerek belirtmek isteriz ki rezervasyonunuz onaylanmadı. Detaylı bilgi için lütfen bizimle iletişime geçin: destek@rentaseat.com";
            }


            var body = $@"
<html>
<body>
<p>Merhaba {reservation.Name},</p>
<p>Rezervasyon durumunuz güncellendi.</p>
<p>Rezervasyonunuz {statusMessage}.</p>
";

            
            if (status == "onaylandı")
            {
                body += $@"
    <hr />
    <h3>Rezervasyon Detayları:</h3>
    <ul>
        <li><strong>Ad:</strong> {reservation.Name}</li>
        <li><strong>Soyad:</strong> {reservation.Surname}</li>
        <li><strong>E-posta:</strong> {reservation.Email}</li>
        <li><strong>Telefon:</strong> {reservation.Phone}</li>
        <li><strong>Alış Yeri:</strong> {reservation.PickUpLocationName}</li>
        <li><strong>Bırakma Yeri:</strong> {reservation.DropOffLocationName}</li>
        <li><strong>Araç:</strong> {reservation.CarName}</li>
        <li><strong>Yaş:</strong> {reservation.Age}</li>
        <li><strong>Sürücü Ehliyet Yılı:</strong> {reservation.DriverLicenseYear}</li>
        <li><strong>Açıklama:</strong> {reservation.Description}</li>
        <li><strong>Durum:</strong> {reservation.Status}</li>
    </ul>
    ";
            }

            body += $@"
<p>Bizi Tercih Ettiğiniz için Teşekkürler,<br/>
RentASeat</p>
</body>
</html>";

            using (var message = new MimeMessage())
            {
                message.From.Add(new MailboxAddress("RentASeat", username));
                message.To.Add(new MailboxAddress(reservation.Name, reservation.Email));
                message.Subject = "Rezervasyon Durumu";

                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(server, port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(username, password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
        }

        private async Task<ResultReservationDto> GetReservationDetails(int reservationID)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Reservations/GetReservationById?id={reservationID}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values= JsonConvert.DeserializeObject<ResultReservationDto>(jsonData);
                return values;
            }
            return null;
        }
    }
}
