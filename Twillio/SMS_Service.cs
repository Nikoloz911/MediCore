using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Twilio;
using MediCore.DTOs.AppointmentsDTOs;
using MediCore.Data;

namespace MediCore.Twillio
{
    public static class SMS_Service
    {
        private static readonly string _accountSid = "AC6050d2b967b8562ed0db05e3c377433f";
        private static readonly string _authToken = "defcdd55f679f44e56ec86f8b898a034"; // TWILIO_AUTH_TOKEN
        private static readonly string _fromPhoneNumber = "+19783076324";

        public static string SendAppointmentDetails(
            AddAppointmentResponseDTO appointmentDto,
            string patientPhoneNumber,
            DataContext context)
        {
            try
            {
                // Get doctor's user details
                var doctorUser = context.Users
                    .FirstOrDefault(u => u.Id == context.Doctors
                        .Where(d => d.Id == appointmentDto.DoctorId)
                        .Select(d => d.UserId)
                        .FirstOrDefault());

                string doctorName = doctorUser?.FirstName ?? "Unknown";
                // MESSAGE BODY
                string messageBody = $"Appointment: Date: {appointmentDto.Date}, Time: {appointmentDto.Time}, Doctor: {doctorName}";

                TwilioClient.Init(_accountSid, _authToken);
                var message = MessageResource.Create(
                    to: new PhoneNumber(patientPhoneNumber),
                    from: new PhoneNumber(_fromPhoneNumber),
                    body: messageBody
                );
                return $"SMS sent: {message.Sid}";
            }
            catch (Exception ex)
            {
                return $"Error sending SMS: {ex.Message}";
            }
        }
    }
}
