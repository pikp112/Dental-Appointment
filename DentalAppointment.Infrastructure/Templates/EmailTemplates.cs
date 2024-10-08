﻿namespace DentalAppointment.Infrastructure.Templates
{
    public static class EmailTemplates
    {
        public static string GetAppointmentCreationTemplate()
        {
            return @"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>New Appointment Created</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }

                    .container {
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #ffffff;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        border-radius: 8px;
                    }

                    .header {
                        background-color: #4CAF50;
                        padding: 10px 20px;
                        border-top-left-radius: 8px;
                        border-top-right-radius: 8px;
                        color: white;
                        text-align: center;
                    }

                    .content {
                        padding: 20px;
                    }

                        .content p {
                            font-size: 16px;
                            color: #333;
                        }

                    .btn {
                        display: inline-block;
                        padding: 10px 20px;
                        margin-top: 20px;
                        text-decoration: none;
                        font-weight: bold;
                        border-radius: 4px;
                        color: white;
                    }

                    .btn-confirm {
                        background-color: #4CAF50;
                    }

                    .btn-reject {
                        background-color: #f44336;
                    }

                    .footer {
                        text-align: center;
                        margin-top: 20px;
                        font-size: 14px;
                        color: #888;
                    }
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>New Appointment Created</h1>
                    </div>
                    <div class=""content"">
                        <p>A new appointment has been created with the following details:</p>
                        <p><strong>Appointment Date:</strong> {{appointmentDateTime}}</p>
                        <p><strong>Patient Name:</strong> {{patientName}}</p>
                        <p><strong>Patient Phone Number:</strong> {{patientPhoneNumber}}</p>
                        <p>Please confirm or reject the appointment:</p>
                        <a href=""{{confirmationLink}}"" class=""btn btn-confirm"">Confirm Appointment</a>
                        <a href=""{{rejectionLink}}"" class=""btn btn-reject"">Reject Appointment</a>
                    </div>
                    <div class=""footer"">
                        <p>This is an automated email, please do not reply.</p>
                    </div>
                </div>
            </body>
            </html>";
        }

        public static string GetAppointmentUpdateTemplate()
        {
            return @"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Appointment Updated</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }

                    .container {
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #ffffff;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        border-radius: 8px;
                    }

                    .header {
                        background-color: #2196F3;
                        padding: 10px 20px;
                        border-top-left-radius: 8px;
                        border-top-right-radius: 8px;
                        color: white;
                        text-align: center;
                    }

                    .content {
                        padding: 20px;
                    }

                        .content p {
                            font-size: 16px;
                            color: #333;
                        }

                    .btn {
                        display: inline-block;
                        padding: 10px 20px;
                        margin-top: 20px;
                        text-decoration: none;
                        font-weight: bold;
                        border-radius: 4px;
                        color: white;
                    }

                    .btn-confirm {
                        background-color: #4CAF50;
                    }

                    .btn-reject {
                        background-color: #f44336;
                    }

                    .footer {
                        text-align: center;
                        margin-top: 20px;
                        font-size: 14px;
                        color: #888;
                    }
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Appointment Updated</h1>
                    </div>
                    <div class=""content"">
                        <p>The following appointment has been updated:</p>
                        <p><strong>Old Appointment Date:</strong> {{oldAppointmentDateTime}}</p>
                        <p><strong>New Appointment Date:</strong> {{appointmentDateTime}}</p>
                        <p><strong>Patient Name:</strong> {{patientName}}</p>
                        <p><strong>Patient Phone Number:</strong> {{patientPhoneNumber}}</p>
                        <p>Please confirm or reject the updated appointment:</p>
                        <a href=""{{confirmationLink}}"" class=""btn btn-confirm"">Confirm Appointment</a>
                        <a href=""{{rejectionLink}}"" class=""btn btn-reject"">Reject Appointment</a>
                    </div>
                    <div class=""footer"">
                        <p>This is an automated email, please do not reply.</p>
                    </div>
                </div>
            </body>
            </html>";
        }

        public static string GetAppointmentConfirmationTemplate(bool isConfirmed, DateTime appointmentDateTime)
        {
            return $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Appointment {(isConfirmed ? "Confirmed" : "Rejected")}</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #ffffff;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        border-radius: 8px;
                    }}
                    .header {{
                        background-color: {(isConfirmed ? "#4CAF50" : "#f44336")};
                        padding: 10px 20px;
                        border-top-left-radius: 8px;
                        border-top-right-radius: 8px;
                        color: white;
                        text-align: center;
                    }}
                    .content {{
                        padding: 20px;
                    }}
                    .content p {{
                        font-size: 16px;
                        color: #333;
                    }}
                    .footer {{
                        text-align: center;
                        margin-top: 20px;
                        font-size: 14px;
                        color: #888;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Appointment {(isConfirmed ? "Confirmed" : "Rejected")}</h1>
                    </div>
                    <div class=""content"">
                        <p>The appointment scheduled for <strong>{appointmentDateTime:dd/MM/yyyy HH:mm}</strong> has been {(isConfirmed ? "confirmed" : "rejected")}.</p>
                    </div>
                    <div class=""footer"">
                        <p>This is an automated message, please do not reply.</p>
                    </div>
                </div>
            </body>
            </html>";
        }
    }
}